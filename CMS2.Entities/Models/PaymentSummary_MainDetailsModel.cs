using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS2.Entities.Models
{
    public class PaymentSummary_MainDetailsModel
    {
        public string CollectionDate { get; set; }
        public string CollectedBy { get; set; }

        public string Area { get; set; }

        public decimal TotalCash { get; set; }
        public decimal TotalCheck { get; set; }
        public decimal TotalCollection { get; set; }
        public decimal TotalTaxWithheld { get; set; }
        public decimal TotalPDC { get; set; }

        public decimal TotalCashReceived { get; set; }
        public decimal TotalCheckReceived { get; set; }
        public decimal TotalAmountReceived { get; set; }
        public decimal Difference { get; set; }

        public string RemittedBy { get; set; }

        public byte[] Signature { get; set; }
        public string ValidatedBy { get; set; }
    }
}
