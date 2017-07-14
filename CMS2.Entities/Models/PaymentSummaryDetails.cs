using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS2.Entities.Models
{
    public class PaymentSummaryDetails
    {
        public Guid PaymentId { get; set; }
        public Guid RemittedById { get; set; }
        public Employee RemittedBy { get; set; }
        public PaymentSummaryStatus PaymentSummaryStatus { get; set; }
        public string AirwayBillNo { get; set; }
        public Client Client { get; set; }
        public string PaymentTypeName { get; set; }
        public decimal AmountDue { get; set; }
        public decimal AmountPaid { get; set; }
        public decimal TaxWithheld { get; set; }
        public string OrNo { get; set; }
        public string PrNo { get; set; }
        public Employee CollectedBy { get; set; }
        public Guid ValidatedById { get; set; }
        public string ValidatedBy { get; set; }
        public string PaymentModeCode { get; set; }
        public RevenueUnit AcceptedArea { get; set; }
        public bool Status { get; set; }
        public bool IsSaved { get; set; }

    }
}
