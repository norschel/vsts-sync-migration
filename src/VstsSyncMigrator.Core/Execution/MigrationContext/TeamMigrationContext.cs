using Microsoft.TeamFoundation.Client;
using Microsoft.TeamFoundation.ProcessConfiguration.Client;
using Microsoft.TeamFoundation.WorkItemTracking.Client;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using VstsSyncMigrator.DataContracts;
using VstsSyncMigrator.Engine.Configuration.Processing;
using VstsSyncMigrator.Vsts;
using VstsSyncMigrator.Vsts.Contexts;

namespace VstsSyncMigrator.Engine
{
    public class TeamMigrationContext : MigrationContextBase
    {

        TeamMigrationConfig _config;
        MigrationEngine _me;

        public override string Name
        {
            get
            {
                return "TeamMigrationContext";
            }
        }

        public TeamMigrationContext(MigrationEngine me, TeamMigrationConfig config) : base(me, config)
        {
            _me = me;
            _config = config;
        }

        internal override void InternalExecute()
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            //////////////////////////////////////////////////
            //WorkItemStoreContext sourceStore = new WorkItemStoreContext(me.Source, WorkItemStoreFlags.BypassRules);
            TeamProject source = new TeamProject(me.Source.CollectionURL, me.Source.Name);
            IRepository<Team> SourceTeamRepo = new TeamFoundationTeamRepository(source);
            List<Team> sourceTeams = SourceTeamRepo.GetAll().ToList();
            Trace.WriteLine(string.Format("Found {0} teams in Source?", sourceTeams.Count));
            //////////////////////////////////////////////////
            TeamProject target = new TeamProject(me.Target.CollectionURL, me.Target.Name);
            IRepository<Team> targetTeamRepo = new TeamFoundationTeamRepository(target);
            List<Team> targetTeams = targetTeamRepo.GetAll().ToList();
            Trace.WriteLine(string.Format("Found {0} teams in Target?", targetTeams.Count));
            //////////////////////////////////////////////////
            int current = sourceTeams.Count;
            int count = 0;
            long elapsedms = 0;

            /// Create teams
            /// 
            foreach (Team sourceTeam in sourceTeams)
            {
                Stopwatch witstopwatch = new Stopwatch();
                witstopwatch.Start();
                var foundTargetTeam = (from x in targetTeams where x.Name == sourceTeam.Name select x).SingleOrDefault();
                if (foundTargetTeam == null)
                {
                    Trace.WriteLine(string.Format("Processing team {0}", sourceTeam.Name));
                    targetTeamRepo.Add(sourceTeam);
                }
                else
                {
                    Trace.WriteLine(string.Format("Team found.. skipping"));
                }

                witstopwatch.Stop();
                elapsedms = elapsedms + witstopwatch.ElapsedMilliseconds;
                current--;
                count++;
                TimeSpan average = new TimeSpan(0, 0, 0, 0, (int)(elapsedms / count));
                TimeSpan remaining = new TimeSpan(0, 0, 0, 0, (int)(average.TotalMilliseconds * current));
                Trace.WriteLine("");
                //Trace.WriteLine(string.Format("Average time of {0} per work item and {1} estimated to completion", string.Format(@"{0:s\:fff} seconds", average), string.Format(@"{0:%h} hours {0:%m} minutes {0:s\:fff} seconds", remaining)));
            }
            stopwatch.Stop();
            Console.WriteLine(@"DONE in {0:%h} hours {0:%m} minutes {0:s\:fff} seconds", stopwatch.Elapsed);
        }


        private TeamSettings CreateTargetTeamSettings(TeamConfiguration sourceTCfU)
        {
            ///////////////////////////////////////////////////
            TeamSettings newTeamSettings = sourceTCfU.TeamSettings;
            newTeamSettings.BacklogIterationPath = newTeamSettings.BacklogIterationPath.Replace(me.Source.Name, me.Target.Name);
            List<string> newIterationPaths = new List<string>();
            foreach (var ip in newTeamSettings.IterationPaths)
            {
                newIterationPaths.Add(ip.Replace(me.Source.Name, me.Target.Name));
            }
            newTeamSettings.IterationPaths = newIterationPaths.ToArray();

            ///////////////////////////////////////////////////
            return newTeamSettings;
        }
    }
}