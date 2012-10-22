using System.Linq;
using OravaciData.Model;
using Raven.Abstractions.Indexing;
using Raven.Client.Indexes;

namespace OravaciData.Indexes
{
    public class PersonIndexResult
    {
        public string Id { get; set; }

        public string FullName { get; set; }

        public object[] Query { get; set; }
    }

    public class PersonIndex : AbstractIndexCreationTask<Person, PersonIndexResult>
    {
        public PersonIndex()
        {
            Map = persons => from item in persons
                             select new PersonIndexResult()
                                 {
                                     Id = item.Id, 
                                     FullName = item.FullName,
                                     Query = new []
                                         {
                                             item.FullName, 
                                             item.Occupation
                                         }
                                 };
            Index(x => x.Query, FieldIndexing.Analyzed);
        }
    }
}
