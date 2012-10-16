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
        /// <returns>All persons.</returns>
        Person[] GetAll();

        /// <summary>
        /// Searches the specified search query.
        /// </summary>
        /// <param name="searchQuery">The search query.</param>
        /// <returns>Search results.</returns>
        Person[] Search(string searchQuery);
    }
}
