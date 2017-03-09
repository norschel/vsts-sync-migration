using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace VstsSyncMigrator.Core2.Tests
{
    [TestClass]
    public class TeamMigratorTests  
    {
        [TestMethod]
        public void SaveSourceToStore()
        {
            TeamMigrator teamMigrator = new TeamMigrator();
            CollectionContext source = new CollectionContext();
            teamMigrator.SourcetoStore()
        }
    }
}
