using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using CMS2.Common.Enums;

namespace CMS2.Entities
{
    public abstract class BaseEntity
    {
        public Guid CreatedBy { get; set; }
        

        public DateTime CreatedDate { get; set; }

        public Guid ModifiedBy { get; set; }

        public DateTime ModifiedDate { get; set; }

        // RecordStatus: 1-Active(default), 2-InActive/Unused, 3-Deleted
        public int RecordStatus { get; set; }
        [NotMapped]
        public RecordStatus Record_Status { get; set; }
        [NotMapped]
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
