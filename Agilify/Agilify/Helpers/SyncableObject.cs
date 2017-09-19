using System;
using Agilify.Helpers;

namespace Agilify.Helpers
{
    public class SyncableObject : ObservableObject
    {
        public SyncableObject()
        {
            Id = Guid.NewGuid().ToString();
        }

        public string Id { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset UpdatedAt { get; set; }
        public string Version { get; set; }
    }
}
