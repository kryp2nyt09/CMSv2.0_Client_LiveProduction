using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CMS2.Entities
{
    public class Role:BaseEntity
    {
        #region Fields
        private ICollection<User> _users;
        #endregion

        #region Scalar Properties
        [Key]
        public Guid RoleId { get; set; }
        [Required]
        [MaxLength(20)]
        [DisplayName("Role")]
        public string RoleName { get; set; }
        [DisplayName("Description")]
        public string Description { get; set; }

        #endregion

        #region Navigation Properties
        public ICollection<User> Users
        {
            get { return _users ?? (_users = new List<User>()); }
            set { _users = value; }
        }
        #endregion
    }
}
