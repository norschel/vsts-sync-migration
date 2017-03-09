using Microsoft.TeamFoundation.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using Microsoft.TeamFoundation.WorkItemTracking.Client;
using System.Collections.ObjectModel;
using VstsSyncMigrator.DataContracts;
using VstsSyncMigrator.Vsts.Contexts;

namespace VstsSyncMigrator.Vsts
{
    public class TeamFoundationTeamRepository : IRepository<Team>
    {
        TeamProject teamProject;
        CollectionContext sourceCollection;
        TfsTeamService teamService;

        public TeamFoundationTeamRepository(TeamProject teamProject)
        {
            this.teamProject = teamProject;
            sourceCollection = new CollectionContext(teamProject);
            teamService = sourceCollection.Collection.GetService<TfsTeamService>();
        }

        public Team Add(Team entity)
        {
            TeamFoundationTeam newTeam = teamService.CreateTeam(sourceCollection.GetProjectId(), entity.Name, entity.Description, null);
            return ConvertTeamFoundationTeamToTeam(newTeam);
        }

        public bool Delete(Team entity)
        {
            
            return false;
        }

        public Team Get(Expression<Func<Team, bool>> filter)
        {
            return GetAll().AsQueryable().Where(filter).SingleOrDefault();
        }

        public ICollection<Team> GetAll()
        {
            ICollection<Team> localTeams = new Collection<Team>();
            List<TeamFoundationTeam> remoteTeams = teamService.QueryTeams(teamProject.Name).ToList();
            foreach (TeamFoundationTeam team in remoteTeams)
            {
                localTeams.Add(ConvertTeamFoundationTeamToTeam(team));
                //var sourceTSCS = me.Source.Collection.GetService<TeamSettingsConfigurationService>();
            }
            return localTeams;
        }

        public bool Update(Team entity)
        {
            return false;
        }

        private Team ConvertTeamFoundationTeamToTeam(TeamFoundationTeam TfsTeam)
        {
            return new Team { Name = TfsTeam.Name, Project = TfsTeam.Project, Description = TfsTeam.Description };
        }
    }
}



// Set Team Settings
//foreach (TeamFoundationTeam sourceTeam in sourceTL)
//{
//    Stopwatch witstopwatch = new Stopwatch();
//    witstopwatch.Start();
//    var foundTargetTeam = (from x in targetTL where x.Name == sourceTeam.Name select x).SingleOrDefault();
//    if (foundTargetTeam == null)
//    {
//        Trace.WriteLine(string.Format("Processing team {0}", sourceTeam.Name));
//        var sourceTCfU = sourceTSCS.GetTeamConfigurations((new[] { sourceTeam.Identity.TeamFoundationId })).SingleOrDefault();
//        TeamSettings newTeamSettings = CreateTargetTeamSettings(sourceTCfU);
//        TeamFoundationTeam newTeam = targetTS.CreateTeam(targetProject.Uri.ToString(), sourceTeam.Name, sourceTeam.Description, null);
//        targetTSCS.SetTeamSettings(newTeam.Identity.TeamFoundationId, newTeamSettings);
//    }
//    else
//    {
//        Trace.WriteLine(string.Format("Team found.. skipping"));
//    }

//    witstopwatch.Stop();
//    elapsedms = elapsedms + witstopwatch.ElapsedMilliseconds;
//    current--;
//    count++;
//    TimeSpan average = new TimeSpan(0, 0, 0, 0, (int)(elapsedms / count));
//    TimeSpan remaining = new TimeSpan(0, 0, 0, 0, (int)(average.TotalMilliseconds * current));
//    Trace.WriteLine("");
//    //Trace.WriteLine(string.Format("Average time of {0} per work item and {1} estimated to completion", string.Format(@"{0:s\:fff} seconds", average), string.Format(@"{0:%h} hours {0:%m} minutes {0:s\:fff} seconds", remaining)));

//}
//////////////////////////////////////////////////