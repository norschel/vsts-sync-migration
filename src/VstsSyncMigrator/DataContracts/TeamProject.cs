using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace VstsSyncMigrator.DataContracts
{
    public class TeamProject
    {
        private Uri _CollectionUrl;
        private string _TeamProjectName;

        public string Name
        {
            get
            {
                return _TeamProjectName;
            }
        }

        public Uri CollectionUrl
        {
            get
            {
                return _CollectionUrl;
            }
        }

        public TeamProject(Uri collectionUrl, string teamProjectName)
        {

            this._CollectionUrl = collectionUrl;
            this._TeamProjectName = teamProjectName;
        }

      
    }
}