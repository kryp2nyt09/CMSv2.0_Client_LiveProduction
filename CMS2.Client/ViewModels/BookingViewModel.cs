using CMS2.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS2.Client.ViewModels
{
    public class BookingViewModel
    {
        [DisplayName("Booking ID")]
        public Guid BookingId { get; set; }
        [DisplayName("Booking Number")]
        public string BookingNo { get; set; }
        [DisplayName("Booking Date")]
        public string DateBooked { get; set; }
        [DisplayName("Booking Time")]
        public string TimeBooked { get; set; }
        public virtual CMS2.Entities.Client Consignee { get; set; }
        public virtual CMS2.Entities.Client Shipper { get; set; }
        public Guid ConsigneeId { get; set; }
        [DisplayName("Consignee Name")]
        public string ConsigneeFullname { get; set; }
        public Guid ShipperId { get; set; }
        [DisplayName("Shipper Name")]
        public string ShipperFullname { get; set; }
        public string OriginAddress1 { get; set; }
        public string OriginAddress2 { get; set; }
        public string OriginStreet { get; set; }
        public string OriginBarangay { get; set; }
        public string DestinationAddress1 { get; set; }
        public string DestinationAddress2 { get; set; }
        public string DestinationStreet { get; set; }
        public string DestinationBarangay { get; set; }
        public string Remarks { get; set; }
        public Guid? BookingRemarkId { get; set; }
        public virtual BookingRemark BookingRemark { get; set; }
        public Guid BookingStatusId { get; set; }
        public virtual BookingStatus BookingStatus { get; set; }
        public virtual Employee BookedBy { get; set; }
        public bool HasDailyBooking { get; set; }
        public Guid? PreviousBookingId { get; set; }
        public virtual Booking PreviousBooking { get; set; }
        public Guid OriginCityId { get; set; }
        public virtual City OriginCity { get; set; }
        public string OriginCityName { get; set; }
        public Guid DestinationCityId { get; set; }
        public virtual City DestinationCity { get; set; }
        public string DestinationCityName { get; set; }
        public Guid? AssignedToAreaId { get; set; }
        public virtual RevenueUnit AssignedToArea { get; set; }


        public List<BookingViewModel> EntitiesToModel(List<Booking> bookings)
        {
            List<BookingViewModel> list = new List<BookingViewModel>();
            foreach (Booking booking in bookings)
            {
                BookingViewModel model = new BookingViewModel();
                model.BookingId = booking.BookingId;
                model.BookingNo = booking.BookingNo;
                model.DateBooked = booking.DateBooked.ToShortDateString();
                model.TimeBooked = booking.DateBooked.ToShortTimeString();
                model.ShipperFullname = booking.Shipper.LastName + booking.Shipper.FirstName;
                model.ConsigneeFullname = booking.Consignee.LastName + booking.Consignee.FirstName;
                model.OriginCityName = booking.OriginCity.CityName;
                model.DestinationCityName = booking.DestinationCity.CityName;
            }

            return list;
        }
    }
}
