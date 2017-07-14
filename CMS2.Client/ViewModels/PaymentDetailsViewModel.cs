using System;

namespace CMS2.Client.ViewModels
{
    public class PaymentDetailsViewModel
    {
        
        public Guid AwbSoaId { get; set; } // PaymentId or SoaPaymentId
        public string AwbSoa { get; set; }
        public string PayorName { get; set; }
        public string PaymentMode { get; set; }
        public string PaymentType { get; set; } // cash/check
        public decimal AmountPaid { get; set; }
        public string AmountPaidString { get { return AmountPaid.ToString("N"); } }
        public decimal TaxWithheld { get; set; }
        public string TaxWithheldString { get { return TaxWithheld.ToString("N"); } }
        public decimal NetAmount { get; set; }
        public string NetAmountString { get { return NetAmount.ToString("N"); } }
        public string OrNo { get; set; }
        public string PrNo { get; set; }
        public Guid CollectedById { get; set; }
        public string CollectedBy { get;set; }
        public string Remarks { get; set; }
        public bool ReceivedStatus { get; set; }
        public bool IsVerified { get; set; }
    }
}
