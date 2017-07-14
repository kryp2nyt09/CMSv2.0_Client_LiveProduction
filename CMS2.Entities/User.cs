using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMS2.Entities
{
    public class User : BaseEntity
    {
        #region Fields
        private ICollection<Claim> _claims;
        //private ICollection<ExternalLogin> _externalLogins;
        private ICollection<Role> _roles;
        #endregion

        #region Scalar Properties
        [Key]
        public Guid UserId { get; set; }
        [Required]
        [MaxLength(15)]
        public string UserName { get; set; }
        public virtual byte[] PasswordHash { get; set; }
        public virtual byte[] SaltHash { get; set; }
        public Guid EmployeeId { get; set; }
        public virtual Employee Employee { get; set; }
        public DateTime? LastLogInDate { get; set; }
        public byte[] OldPassword { get; set; }
        public DateTime LastPasswordChange { get; set; }
        
        #endregion

        #region Navigation Properties
        public virtual ICollection<Claim> Claims
        {
            get { return _claims ?? (_claims = new List<Claim>()); }
            set { _claims = value; }
        }

        //public virtual ICollection<ExternalLogin> Logins
        //{
        //    get
        //    {
        //        return _externalLogins ??
        //            (_externalLogins = new List<ExternalLogin>());
        //    }
        //    set { _externalLogins = value; }
        //}

        public virtual ICollection<Role> Roles
        {
            get { return _roles ?? (_roles = new List<Role>()); }
            set { _roles = value; }
        }
        #endregion

        [NotMapped]
        public string LastLogInDateString { get { return LastLogInDate.ToString(); } }
        [NotMapped]
        public string LastPasswordChangeString { get { return LastPasswordChange.ToString("MMM dd, yyyy HH:mm"); } }
        
    }
}
