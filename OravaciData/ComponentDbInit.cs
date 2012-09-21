﻿using System;
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
                           //typeof(Device),
                           //typeof(Celebrity)
                       };
        }

        /// <summary>
        /// Registers the indexes.
        /// </summary>
        /// <param name="documentStore">The document store.</param>
        public void RegisterIndexes(IDocumentStore documentStore)
        {
            //new CelebritySearchIndex().Execute(documentStore);
            //new CelebrityViewIndex().Execute(documentStore);
            //new CelebritySortIndex().Execute(documentStore);
            //new DeviceSortIndex().Execute(documentStore);
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
