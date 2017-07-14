using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS2.Entities.Models
{
    public class PaymentSummaryModel
    {
        [Key]
        public Guid PaymentSummaryId { get; set; }
        public Guid ClientId { get; set; }
        public Guid PaymentId { get; set; }
        public Guid CheckedBy { get; set; }
        public Guid ValidatedBy { get; set; }
        public Guid RemittedBy { get; set; }
        public Guid PaymentSummaryStatusId { get; set; }
        public DateTime DateAccepted { get; set; }
        public string Remarks { get; set; }
        public byte[] Signature { get; set; }
        public Guid CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public Guid ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
        [DisplayName("Record Status")]
        public int RecordStatus { get; set; }
        public string PaymentModeCode  { get; set; }
    }
}
