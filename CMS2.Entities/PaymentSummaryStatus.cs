using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS2.Entities
{
   public class PaymentSummaryStatus : BaseEntity
    {
        [Key]
        public Guid PaymentSummaryStatusId { get; set; }

        public string PaymentSummaryStatusName { get; set; }
    }
}
