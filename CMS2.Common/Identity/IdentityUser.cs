using System;
using System.ComponentModel;
using CMS2.Common.Enums;
using Microsoft.AspNet.Identity;

namespace CMS2.Common.Identity
{
    public class IdentityUser : IUser<Guid>
    {
        public IdentityUser()
        {
            this.Id = Guid.NewGuid();
        }

        public IdentityUser(string userName)
            : this()
        {
            this.UserName = userName;
        }

        public Guid Id { get; set; }
        public string UserName { get; set; }
        public virtual byte[] PasswordHash { get; set; }
        public virtual byte[] SaltHash { get; set; }
        public Guid EmployeeId { get; set; }

        public Guid AssignedToAreaId { get; set; }

        public DateTime? LastLogInDate { get; set; }
        public byte[] OldPassword { get; set; }
        public DateTime LastPasswordChange { get; set; }

        public Guid CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        public Guid ModifiedBy { get; set; }

        public DateTime ModifiedDate { get; set; }

        // RecordStatus: 1-Active(default), 2-InActive/Unused, 3-Deleted
        public int RecordStatus { get; set; }
        public RecordStatus Record_Status { get; set; }
        [DisplayName("Record Status")]
        public string RecordStatusString
        {
            get
            {
                RecordStatus recordStatus = (RecordStatus)this.RecordStatus;
                return recordStatus.ToString();
            }
        }
    }
}