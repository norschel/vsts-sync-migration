using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VstsSyncMigrator;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using VstsSyncMigrator.Engine;
using VstsSyncMigrator.Vsts;
using VstsSyncMigrator.DataContracts;

namespace VstsSyncMigrator.Vsts.Tests
{
    [TestClass]
    public class TeamFoundationTeamTests
    {
        [TestMethod]
        public void GetAllTeams()
        {
            ITeamProjectContext target = new CollectionContext(new Uri("https://nkdagility.visualstudio.com/"), "nkdProducts");
            IRepository<Team> teamRepo = new TeamFoundationTeamRepository(target);
            ICollection<Team> teams = teamRepo.GetAll();
            Assert.AreEqual(10, teams.Count);
        }

        [TestMethod]
        public void GetOneTeam()
        {
            ITeamProjectContext target = new CollectionContext(new Uri("https://nkdagility.visualstudio.com/"), "nkdProducts");
            IRepository<Team> teamRepo = new TeamFoundationTeamRepository(target);
            Team team = teamRepo.Get(t => t.Name == "vsts-sync-migration");
            Assert.IsNotNull(team);
        }
    }
}
