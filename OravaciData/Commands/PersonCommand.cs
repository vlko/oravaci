using System;
using System.Linq;
using OravaciData.Indexes;
using OravaciData.Model;
using Raven.Client.Linq;
using vlko.core.RavenDB.Repository;
using vlko.core.RavenDB.Repository.RepositoryAction;
using vlko.core.Repository;

namespace OravaciData.Commands
{
    public class PersonCommand : CrudCommands<Person>, IPersonCommand
    {
        /// <summary>
        /// Gets all.
        /// </summary>
        /// <returns>All persons.</returns>
        public IQueryResult<Person> GetAll()
        {
            return new QueryLinqResult<Person>(SessionFactory<Person>.IndexQuery<PersonIndex>());
        }

        /// <summary>
        /// Gets all.
        /// </summary>
        /// <returns>All persons.</returns>
        public Person[] Top50()
        {
            return SessionFactory<Person>.IndexQuery<PersonIndex>().Take(50).ToArray();
        }

        /// <summary>
        /// Searches the specified search query.
        /// </summary>
        /// <param name="searchQuery">The search query.</param>
        /// <returns>Search results.</returns>
        public Person[] Search(string searchQuery)
        {
            if (!searchQuery.EndsWith("*"))
            {
                searchQuery += "*";
            }
            return PerformQuery(searchQuery);
        }

        private static Person[] PerformQuery(string search, bool guessIfNoResultsFound = true)
        {
            var persons = SessionFactory<Person>.IndexQuery<PersonIndex, PersonIndexResult>()
                .Search<PersonIndexResult>(x => x.Query, search, escapeQueryOptions: EscapeQueryOptions.AllowPostfixWildcard)
                .As<Person>()
                .ToArray();

            if (persons.Length > 0)
            {
                return persons;
            }
            if (guessIfNoResultsFound)
            {
                return DidYouMean(search);
            }
            return new Person[] { };
        }

        private static Person[] DidYouMean(string search)
        {
            var suggestionQueryResult = SessionFactory<Person>.IndexQuery<PersonIndex, PersonIndexResult>()
                .Search(x => x.Query, search, escapeQueryOptions: EscapeQueryOptions.AllowPostfixWildcard)
                .Suggest();
            switch (suggestionQueryResult.Suggestions.Length)
            {
                case 0:
                    return new Person[] { };
                default:
                    return PerformQuery(suggestionQueryResult.Suggestions[0], guessIfNoResultsFound: false);
            }
        }
    }
}