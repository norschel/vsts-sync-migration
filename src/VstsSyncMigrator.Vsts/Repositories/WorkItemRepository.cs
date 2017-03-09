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
using System.Diagnostics;

namespace VstsSyncMigrator.Vsts
{
    public class WorkItemRepository : IRepository<WorkItemData>
    {
        TeamProject teamProject;
        CollectionContext sourceCollection;
        WorkItemStoreContext workItemStoreContext;

        public string Query { get; set; }

        public WorkItemRepository(TeamProject teamProject)
        {
            this.teamProject = teamProject;
            sourceCollection = new CollectionContext(teamProject);
            workItemStoreContext = new WorkItemStoreContext(sourceCollection, WorkItemStoreFlags.BypassRules);
        }

        public WorkItemData Add(WorkItemData entity)
        {
            throw new NotImplementedException();
        }

        public bool Delete(WorkItemData entity)
        {
            throw new NotImplementedException();
        }

        public WorkItemData Get(Expression<Func<WorkItemData, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public ICollection<WorkItemData> GetAll()
        {
            List<WorkItemData> wids = new List<WorkItemData>();
            TfsQueryContext tfsqc = new TfsQueryContext(workItemStoreContext);
            tfsqc.AddParameter("TeamProject", teamProject.Name);
            tfsqc.Query = string.Format(@"SELECT [System.Id], [System.Tags] FROM WorkItems WHERE [System.TeamProject] = @TeamProject {0} ORDER BY [System.ChangedDate] desc", _config.QueryBit);
            WorkItemCollection workitems = tfsqc.Execute();
            foreach (WorkItem wi in workitems)
            {
                wids.Add(ConvertTo(wi));
            }
            return wids;
        }

        public bool Update(WorkItemData entity)
        {
            throw new NotImplementedException();
        }

        private WorkItemData ConvertTo(WorkItem item)
        {
            return new WorkItemData { ID = item.Id};
        }

    }
}

