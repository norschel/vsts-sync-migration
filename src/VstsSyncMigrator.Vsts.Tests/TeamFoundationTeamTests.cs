using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VstsSyncMigrator;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using VstsSyncMigrator.Engine;
using VstsSyncMigrator.Vsts.Contexts;
using VstsSyncMigrator.DataContracts;

namespace VstsSyncMigrator.Vsts.Tests
{
    [TestClass]
    public class TeamFoundationTeamTests
    {
        [TestMethod]
        public void GetAllTeams()
        {
            TeamProject target = new TeamProject(new Uri("https://nkdtestdata.visualstudio.com/"), "TestMigrationSource");
            IRepository<Team> teamRepo = new TeamFoundationTeamRepository(target);
            ICollection<Team> teams = teamRepo.GetAll();
            Assert.AreEqual(5, teams.Count);
        }

        [TestMethod]
        public void GetOneTeam()
        {
            TeamProject target = new TeamProject(new Uri("https://nkdtestdata.visualstudio.com/"), "TestMigrationSource");
            IRepository<Team> teamRepo = new TeamFoundationTeamRepository(target);
            Team team = teamRepo.Get(t => t.Name == "Team A");
            Assert.IsNotNull(team);
        }

        [TestMethod]
        public void AddOneTeam()
        {
             TeamProject target = new TeamProject(new Uri("https://nkdtestdata.visualstudio.com/"), "TestMigrationSource");
            IRepository<Team> teamRepo = new TeamFoundationTeamRepository(target);
            string NewTeamName = "Test-AddOneTeam";
            teamRepo.Add(new Team { Name = NewTeamName, Description = "Bla Bla", Project = target.Name });
            Team team = teamRepo.Get(t => t.Name == NewTeamName);
            Assert.IsNotNull(team);
            teamRepo.Delete(team);
        }
    }
}
