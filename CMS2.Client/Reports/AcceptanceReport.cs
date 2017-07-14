namespace CMS2.Client.Reports
{
    using System;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Windows.Forms;
    using Telerik.Reporting;
    using Telerik.Reporting.Drawing;

    /// <summary>
    /// Summary description for AcceptanceReport.
    /// </summary>
    public partial class AcceptanceReport : Telerik.Reporting.Report
    {
        public AcceptanceReport()
        {
            //
            // Required for telerik Reporting designer support
            //
            InitializeComponent();

            var objectDataSource = new Telerik.Reporting.ObjectDataSource();
            DataTable dataTable = AcceptanceModelReport.table;
            objectDataSource.DataSource = dataTable;
            table1.DataSource = objectDataSource;

            txtServiceMode.Value = AcceptanceModelReport.ServiceMode;
            txtPaymentMode.Value = AcceptanceModelReport.PaymentMode;

            txtOrigin.Value =       AcceptanceModelReport.Origin;
            txtDestination.Value =  AcceptanceModelReport.Destination;
            txtDateandPlace.Value = AcceptanceModelReport.DateandPlace;

            txtAWB1.Value = AcceptanceModelReport.AWB1;
            txtAWB2.Value = AcceptanceModelReport.AWB2;

            txtShipperName.Value =    AcceptanceModelReport.ShipperName;
            txtShipperAddress.Value = AcceptanceModelReport.ShipperAddress;

            txtConsigneeName.Value =    AcceptanceModelReport.ConsigneeName;
            txtConsigneeAddress.Value = AcceptanceModelReport.ConsigneeAddress;

            txt1.Value =  AcceptanceModelReport.txt1;
            txt2.Value =  AcceptanceModelReport.txt2;
            txt3.Value =  AcceptanceModelReport.txt3;
            txt4.Value =  AcceptanceModelReport.txt4;
            txt5.Value =  AcceptanceModelReport.txt5;
            txt6.Value =  AcceptanceModelReport.txt6;
            txt7.Value =  AcceptanceModelReport.txt7;
            txt8.Value =  AcceptanceModelReport.txt8;
            txt9.Value =  AcceptanceModelReport.txt9;
            txt10.Value = AcceptanceModelReport.txt10;
            txt11.Value = AcceptanceModelReport.txt11;
            txt12.Value = AcceptanceModelReport.txt12;
            txt13.Value = AcceptanceModelReport.txt13;
            txt14.Value = AcceptanceModelReport.txt14;
            txt15.Value = AcceptanceModelReport.txt15;
            txt16.Value = AcceptanceModelReport.txt16;
            txt17.Value = AcceptanceModelReport.txt17;
            txt18.Value = AcceptanceModelReport.txt18;

            txtGrandTotal.Value = AcceptanceModelReport.GrandTotal;
        }
    }
}