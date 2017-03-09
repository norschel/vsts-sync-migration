using Microsoft.TeamFoundation.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using VstsSyncMigrator.Engine;
using Microsoft.TeamFoundation.WorkItemTracking.Client;
using System.Collections.ObjectModel;
using VstsSyncMigrator.DataContracts;

namespace VstsSyncMigrator.Vsts.Teams
{
    public class TeamFoundationTeamRepository : IRepository<Team>
    {
        ITeamProjectContext teamProjectContext;

        public TeamFoundationTeamRepository(ITeamProjectContext teamProjectContext)
        {
            this.teamProjectContext = teamProjectContext;
        }

        public Team Add(Team entity)
        {
            throw new NotImplementedException();
        }

        public bool Delete(Team entity)
        {
            throw new NotImplementedException();
        }

        public Team Get(Expression<Func<Team, bool>> filter)
        {
            return GetAll().AsQueryable().Where(filter).SingleOrDefault();
        }

        public ICollection<Team> GetAll()
        {
            ICollection<Team> localTeams = new Collection<Team>();
            WorkItemStoreContext sourceStore = new WorkItemStoreContext(teamProjectContext, WorkItemStoreFlags.BypassRules);
            TfsTeamService sourceTS = teamProjectContext.Collection.GetService<TfsTeamService>();
            List<TeamFoundationTeam> remoteTeams = sourceTS.QueryTeams(teamProjectContext.Name).ToList();
            foreach (TeamFoundationTeam team in remoteTeams)
            {
                localTeams.Add(new Team {Name = team.Name, Project = team.Project, Description = team.Description });
            }

            return localTeams;
        }

        public bool Update(Team entity)
        {
            throw new NotImplementedException();
        }
    }
}
