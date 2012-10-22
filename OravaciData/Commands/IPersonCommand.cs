using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using OravaciData.Model;
using vlko.core.Repository;
using vlko.core.Repository.RepositoryAction;

namespace OravaciData.Commands
{
    [InheritedExport]
    public interface IPersonCommand : ICommandGroup<Person>, ICrudCommands<Person>
    {
        /// <summary>
        /// Gets all.
        /// </summary>
        /// <returns>all persons.</returns>
        IQueryResult<Person> GetAll();

        /// <summary>
        /// Gets top 50.
        /// </summary>
        /// <returns>Top 50 persons.</returns>
        Person[] Top50();

        /// <summary>
        /// Searches the specified search query.
        /// </summary>
        /// <param name="searchQuery">The search query.</param>
        /// <returns>Search results.</returns>
        Person[] Search(string searchQuery);


    }
}
