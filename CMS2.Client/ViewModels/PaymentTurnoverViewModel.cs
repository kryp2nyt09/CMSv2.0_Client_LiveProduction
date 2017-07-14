using System;
using CMS2.Entities;

namespace CMS2.Client.ViewModels
{
    public class PaymentTurnoverViewModel
    {
        public Guid PaymentTurnOverId { get; set; }
        public DateTime CollectionDate { get; set; }
        public Guid CollectedById { get; set; }
        public Employee CollectedBy { get; set; }
        public decimal ReceivedCashAmount { get; set; }
        public decimal ReceivedCheckAmount { get; set; }
        public string Remarks { get; set; }



        public string CollectionDateString { get { return CollectionDate.ToString("MMM dd, yyyy"); } }
        public string ReceivedCashAmountString { get { return ReceivedCashAmount.ToString("N"); } }
        public string ReceivedCheckAmountString { get { return ReceivedCheckAmount.ToString("N"); } }


        public decimal TotalCashCollection { get; set; }
        public decimal TotalCheckCollection { get; set; }
        public decimal TotalCollection { get { return TotalCheckCollection + TotalCashCollection; } }
        public decimal TotalTaxWithheld { get; set; }
        public decimal TotalPending { get; set; }
        public decimal TotalPdcAmount { get; set; }
        public decimal TotalReceivedAmount { get { return ReceivedCashAmount + ReceivedCheckAmount; } }
        public decimal AmountDifference {
            get { return TotalCollection - TotalReceivedAmount; }
        }

        public string TotalCashCollectionString { get { return TotalCashCollection.ToString("N"); } }
        public string TotalCheckCollectionString { get { return TotalCheckCollection.ToString("N"); } }
        public string TotalCollectionString { get { return TotalCollection.ToString("N"); } }
        public string TotalTaxWithheldString { get { return TotalTaxWithheld.ToString("N"); } }
        public string TotalPendingString { get { return TotalPending.ToString("N"); } }
        public string TotalPdcAmountString { get { return TotalPdcAmount.ToString("N"); } }
        public string TotalReceivedAmountString { get { return TotalReceivedAmount.ToString("N"); } }
        public string AmountDifferenceString { get { return AmountDifference.ToString("N"); } }
    }
}
