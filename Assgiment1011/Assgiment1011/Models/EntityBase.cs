using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.Contracts;

namespace Assgiment1011.Models
{
    public enum EntityState : int
    {
        Active = 0,
        Inactive = 1,
        Deleted = 2,
    }

    public enum SetSyncOption
    {
        Keep = 0,
        SetSync =1,
        SetNotSync =2
    }

    public enum SyncStatus
    {
        NotSync = 0,
        Synchroized = 1,
    }
    public class EntityBase
    {
        [NotMapped()]
        public SetSyncOption IgnoreIsSync { get; set; }

        [Key()]
        public Guid Id { get; set; }

        
        public EntityState State { get; set; }

        public string Name { get; set; }


        [ForeignKey(nameof(Author))]
        public int AuthorId { get; set; }

        public Author Author { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime UpdatedDate { get; set; }

        public List<string>? ModifiedParams { get; set; }

        public Guid ModifiedBy { get; set; }

        private bool IsSyncJobInternal { get; set; }

        public bool IsSyncJob()
        {
            return IsSyncJobInternal;
        }

        private bool IsDuplicated { get; set; }
        public void SetIsDuplicated()
        {
            IsDuplicated = true;
        }

        public bool GetIsDuplicated()
        {
            return IsDuplicated;
        }

        public SyncStatus SyncStatus { get; set; }
    }
}
