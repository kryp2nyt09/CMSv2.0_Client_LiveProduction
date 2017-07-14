using CMS2.Client.Reports;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls;

namespace CMS2.Client.Forms.TrackingReportsView
{
    public partial class ReportViewer : Telerik.WinControls.UI.RadForm
    {
        public ReportViewer()
        {
            InitializeComponent();
        }
        
        private void ReportViewer_Load(object sender, EventArgs e)
        {
            var typeReportSource = new Telerik.Reporting.TypeReportSource();
            switch (ReportGlobalModel.Report)
            {
                case "PickUpCargo":
                    typeReportSource.TypeName = typeof(PickupCargoManifestReportView).AssemblyQualifiedName;
                    break;
                case "BranchAcceptance":
                    typeReportSource.TypeName = typeof(BranchAcceptanceReportView).AssemblyQualifiedName;
                    break;
                case "Bundle":
                    typeReportSource.TypeName = typeof(BundleReportView).AssemblyQualifiedName;
                    break;
                case "Unbundle":
                    typeReportSource.TypeName = typeof(UnbundleReportView).AssemblyQualifiedName;
                    break;
                case "GatewayTransmital":
                    typeReportSource.TypeName = typeof(GatewayTransmitalReportView).AssemblyQualifiedName;
                    break;
                case "GatewayOutbound":
                    typeReportSource.TypeName = typeof(GatewayOutboundReportView).AssemblyQualifiedName;
                    break;
                case "GatewayInbound":
                    typeReportSource.TypeName = typeof(GatewayInboundReportView).AssemblyQualifiedName;
                    break;
                case "CargoTransfer":
                    typeReportSource.TypeName = typeof(CargoTransferReportView).AssemblyQualifiedName;
                    break;
                case "Segregation":
                    typeReportSource.TypeName = typeof(SegregationReportView).AssemblyQualifiedName;
                    break;
                case "DailyTrip":
                    typeReportSource.TypeName = typeof(DailyTripReportView).AssemblyQualifiedName;
                    break;
                case "DeliveryStatus":
                    typeReportSource.TypeName = typeof(DeliveryStatusReportView).AssemblyQualifiedName;
                    break;
                case "AcceptanceReport":
                    typeReportSource.TypeName = typeof(AcceptanceReport).AssemblyQualifiedName;
                    break;
                case "Manifest":
                    typeReportSource.TypeName = typeof(ManifestReport).AssemblyQualifiedName;
                    break;
                case "PaymentSummaryReport":
                    typeReportSource.TypeName = typeof(PaymentSummaryReportView).AssemblyQualifiedName;
                    break;
                default:
                    break;
            }
            this.reportViewer1.ReportSource = typeReportSource;            
            reportViewer1.RefreshReport();
        }
    }
}
