using System.Collections.Generic;
using Microsoft.WindowsAzure.MobileServices.SQLiteStore;

using Agilify.Helpers;
using Agilify.Models;

namespace Agilify.Services
{
    public class SyncManager
    {
        public SyncStore<Member> MembersStore { get; set; } = new SyncStore<Member>();
        public SyncStore<Team> TeamsStore { get; set; } = new SyncStore<Team>();
        public SyncStore<Project> ProjectsStore { get; set; } = new SyncStore<Project>();
        public SyncStore<Sprint> SprintsStore { get; set; } = new SyncStore<Sprint>();

        public SyncManager()
        {
            App.LocalDB.DefineTable<Member>();
            App.LocalDB.DefineTable<Team>();
            App.LocalDB.DefineTable<Project>();
            App.LocalDB.DefineTable<Epic>();
            App.LocalDB.DefineTable<Feature>();
            App.LocalDB.DefineTable<Sprint>();
            App.LocalDB.DefineTable<WorkItem>();
            App.LocalDB.DefineTable<AgileTask>();
            App.LocalDB.DefineTable<AgileBug>();
            App.LocalDB.DefineTable<ChatThread>();
            App.LocalDB.DefineTable<ChatMessage>();
        }

    }
}
