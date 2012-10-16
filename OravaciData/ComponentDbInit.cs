using System;
using OravaciData.Indexes;
using OravaciData.Model;
using Raven.Client;
using vlko.core.RavenDB;

namespace OravaciData
{
    public class ComponentDbInit : IComponentDbInit
    {

        /// <summary>
        /// Lists the of model types.
        /// </summary>
        /// <returns>List of model types.</returns>
        public Type[] ListOfModelTypes()
        {
            return new Type[]
                       {
                           typeof(Person)
                       };
        }

        /// <summary>
        /// Registers the indexes.
        /// </summary>
        /// <param name="documentStore">The document store.</param>
        public void RegisterIndexes(IDocumentStore documentStore)
        {
            new PersonIndex().Execute(documentStore);
        }

        /// <summary>
        /// Customizes the document store.
        /// </summary>
        /// <param name="documentStore">The document store.</param>
        public void CustomizeDocumentStore(IDocumentStore documentStore)
        {

        }
    }
}
