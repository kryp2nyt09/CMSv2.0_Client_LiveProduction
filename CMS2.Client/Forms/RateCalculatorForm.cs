using CMS2.BusinessLogic;
using CMS2.Common.Enums;
using CMS2.Entities;
using CMS2.Entities.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls;
using System.Linq;
using Telerik.WinControls.UI;
using CMS2.Common;

namespace CMS2.Client.Forms
{
    public partial class RateCalculatorForm : Telerik.WinControls.UI.RadForm
    {
        private ShipmentModel shipment;
        private ShipmentBL shipmentService;
        private PaymentMode _paymentMode;
        private PackageDimensionModel packageDimensionModel;

        private List<City> _cities;
        private List<CommodityType> _commodityType;
        private List<ServiceType> _serviceType;
        private List<ServiceMode> _serviceMode;
        private List<ShipMode> _shipMode;
        private List<Crating> _crating;
        private List<TransShipmentLeg> _hub;

        public RateCalculatorForm(List<City> _cities, List<CommodityType> _commodityType, List<ServiceType> _serviceType, List<ServiceMode> _serviceMode, List<ShipMode> _shipMode, List<Crating> _crating, List<TransShipmentLeg> _hub, PaymentMode _paymode)
        {
            InitializeComponent();


            shipment = new ShipmentModel();
            shipment.PackageDimensions = new List<PackageDimensionModel>();
            shipmentService = new ShipmentBL();
            _paymentMode = _paymode;

            this._cities = _cities;
            this._commodityType = _commodityType;
            this._serviceType = _serviceType;
            this._serviceMode = _serviceMode;
            this._shipMode = _shipMode;
            this._crating = _crating;
            this._hub = _hub;


            BindingSource origin = new BindingSource();
            origin.DataSource = _cities;
            BindingSource destination = new BindingSource();
            destination.DataSource = _cities;

            //ORIGIN CITY
            cmbOriginCity.DataSource = origin;
            cmbOriginCity.DisplayMember = "CityName";
            cmbOriginCity.ValueMember = "CityId";

            //DESTINATION CITY
            cmbDestinationCity.DataSource = destination;
            cmbDestinationCity.DisplayMember = "CityName";
            cmbDestinationCity.ValueMember = "CityId";

            //COMMODITY TYPE
            cmdCommodityType.DataSource = _commodityType;
            cmdCommodityType.DisplayMember = "CommodityTypeName";
            cmdCommodityType.ValueMember = "CommodityTypeId";

            //SERVICE TYPE
            cmbServiceType.DataSource = _serviceType;
            cmbServiceType.DisplayMember = "ServiceTypeName";
            cmbServiceType.ValueMember = "ServiceTypeId";

            //SERVICE MODE 
            cmbServiceMode.DataSource = _serviceMode;
            cmbServiceMode.DisplayMember = "ServiceModeName";
            cmbServiceMode.ValueMember = "ServiceModeId";

            //SHIP MODE
            cmbShipMode.DataSource = _shipMode;
            cmbShipMode.DisplayMember = "ShipModeName";
            cmbShipMode.ValueMember = "ShipModeId";

            //CRATING
            lstCrating.DataSource = _crating;
            lstCrating.DisplayMember = "CratingName";
            lstCrating.ValueMember = "CratingId";
            lstCrating.SelectedIndex = -1;

            //HUB
            cmdHub.DataSource = _hub;
            cmdHub.DisplayMember = "LegName";
            cmdHub.ValueMember = "TransShipmentLegId";

        }



        /// <summary>
        /// ADD PACKAGE GRID
        /// </summary>
        public void AddPackage()
        {

            if (shipment.PackageDimensions == null)
                shipment.PackageDimensions = new List<PackageDimensionModel>();

            if (shipment.CommodityTypeId == null || shipment.CommodityType == null)
            {
                if (cmdCommodityType.SelectedValue == null)
                {
                    cmdCommodityType.SelectedIndex = 0;
                }
                shipment.CommodityTypeId = Guid.Parse(cmdCommodityType.SelectedValue.ToString());
                shipment.CommodityType = this._commodityType.Find(x => x.CommodityTypeId == shipment.CommodityTypeId);
            }

            if (shipment.ServiceTypeId == null || shipment.ServiceType == null)
            {

                if (cmbServiceType.SelectedValue == null)
                {
                    cmbServiceType.SelectedIndex = 0;
                }
                shipment.ServiceTypeId = Guid.Parse(cmbServiceType.SelectedValue.ToString());
                shipment.ServiceType = this._serviceType.Find(x => x.ServiceTypeId == shipment.ServiceTypeId);
            }

            if (shipment.ServiceModeId == null || shipment.ServiceMode == null)
            {

                if (cmbShipMode.SelectedValue == null)
                {
                    cmbShipMode.SelectedIndex = 0;
                }
                shipment.ServiceModeId = Guid.Parse(cmbShipMode.SelectedValue.ToString());
                shipment.ServiceMode = this._serviceMode.Find(x => x.ServiceModeId == shipment.ServiceModeId);
            }

            if (shipment.ShipModeId == null || shipment.ShipMode == null)
            {
                if (cmbShipMode.SelectedValue == null)
                {
                    cmbShipMode.SelectedIndex = 0;
                }
                shipment.ShipModeId = Guid.Parse(cmbShipMode.SelectedValue.ToString());
                shipment.ShipMode = this._shipMode.Find(x => x.ShipModeId == shipment.ShipModeId);
            }

            if (shipment.ShipMode.ShipModeName == "Transhipment")
            {
                shipment.TransShipmentLegId = Guid.Parse(cmdHub.SelectedValue.ToString());
                shipment.TransShipmentLeg = this._hub.Find(x => x.TransShipmentLegId == shipment.TransShipmentLegId);
            }
            
            try
            {
                shipment.Quantity = Int32.Parse(txtQuantity.Text);
            }
            catch (Exception ex)
            {
                Logs.ErrorLogs("AddPackage", ex);
                MessageBox.Show("Invalid Quantity.", "Data Error", MessageBoxButtons.OK);
                txtQuantity.Text = "1";
                txtQuantity.Focus();
                return;
            }
            if (shipment.Quantity <= 0)
            {
                MessageBox.Show("Invalid Quantity.", "Data Error", MessageBoxButtons.OK);
                txtQuantity.Text = "1";
                txtQuantity.Focus();
                return;
            }
            try
            {
                shipment.Weight = Decimal.Parse(txtWeight.Text);
            }
            catch (Exception ex)
            {
                Logs.ErrorLogs("AddPackage", ex);
                MessageBox.Show("Invalid Weight.", "Data Error", MessageBoxButtons.OK);
                txtWeight.Text = "1";
                txtWeight.Focus();
                return;
            }
            if (shipment.Weight <= 0)
            {
                MessageBox.Show("Invalid Weight.", "Data Error", MessageBoxButtons.OK);
                txtWeight.Text = "1";
                txtWeight.Focus();
                return;
            }
            decimal length = 0;
            decimal width = 0;
            decimal height = 0;
            try
            {
                length = Decimal.Parse(txtLength.Text);
                width = Decimal.Parse(txtWidth.Text);
                height = Decimal.Parse(txtHeight.Text);
                if (!(length > 0 && width > 0 && height > 0))
                {
                    txtLength.Focus();
                    return;
                }
            }
            catch (Exception ex)
            {
                Logs.ErrorLogs("AddPackage", ex);
                MessageBox.Show("Invalid Dimension.", "Data Error", MessageBoxButtons.OK);
                return;
            }

            int index = 0;
            for (index = 0; index < shipment.PackageDimensions.Count; index++)
            {
                var temp = shipment.PackageDimensions.Find(x => x.No == index);
                if (temp == null)
                {
                    break;
                }
            }

            packageDimensionModel = new PackageDimensionModel();
            packageDimensionModel.Length = Int32.Parse(txtLength.Text); ;
            packageDimensionModel.Width = Int32.Parse(txtWidth.Text);
            packageDimensionModel.Height = Int32.Parse(txtHeight.Text);
            packageDimensionModel.RecordStatus = (int)RecordStatus.Active;
            packageDimensionModel.CommodityTypeId = shipment.CommodityTypeId;
            packageDimensionModel.CratingId = null;

            if (lstCrating.SelectedValue != null || lstCrating.SelectedIndex != -1)
            {
                packageDimensionModel.CratingId = Guid.Parse(lstCrating.SelectedValue.ToString());
                packageDimensionModel.CratingName = lstCrating.SelectedItem.ToString();
            }
            packageDimensionModel.ForPackaging = chkPackaging.Checked;
            packageDimensionModel.ForDraining = chkDraining.Checked;
            shipment.PackageDimensions.Add(packageDimensionModel);
                        
            shipment = shipmentService.ComputePackageEvmCrating(shipment);
            gridPackage.DataSource = null;
            gridPackage.DataSource = shipment.PackageDimensions;
            gridPackage.Refresh();

            ComputeCharges();
        }

        private void ComputeCharges()
        {

            shipment.CommodityTypeId = Guid.Parse(cmdCommodityType.SelectedValue.ToString());
            shipment.ServiceTypeId = Guid.Parse(cmbServiceType.SelectedValue.ToString());
            shipment.ServiceModeId = Guid.Parse(cmbServiceMode.SelectedValue.ToString());
            shipment.ShipModeId = Guid.Parse(cmbShipMode.SelectedValue.ToString());
            shipment.Weight = Convert.ToDecimal(txtWeight.Value);
            if (cmbShipMode.SelectedText == "Transhipment")
            {
                if (cmdHub.SelectedValue != null)
                {
                    shipment.TransShipmentLegId = Guid.Parse(cmdHub.SelectedValue.ToString());
                }
                else
                {
                    shipment.TransShipmentLegId = null;
                }
            }

            if (txtDeclaredValue.Value.ToString().Contains("₱"))
            {
                shipment.DeclaredValue = Decimal.Parse(txtDeclaredValue.Value.ToString().Replace("₱", ""));
                shipment.HandlingFee = Decimal.Parse(txtHandlineFee.Value.ToString().Replace("₱", ""));
                shipment.QuarantineFee = Decimal.Parse(txtQuarantineFee.Value.ToString().Replace("₱", ""));
                shipment.Discount = Decimal.Parse(txtRfa.Value.ToString()) * 100;
            }
            else
            {
                shipment.DeclaredValue = Decimal.Parse(txtDeclaredValue.Value.ToString().Replace("Php", ""));
                shipment.HandlingFee = Decimal.Parse(txtHandlineFee.Value.ToString().Replace("Php", ""));
                shipment.QuarantineFee = Decimal.Parse(txtQuarantineFee.Value.ToString().Replace("Php", ""));
                shipment.Discount = Decimal.Parse(txtRfa.Value.ToString()) * 100;
            }


            if (shipment.Shipper != null)
            {
                if (shipment.Shipper.Company != null)
                {
                    shipment.Discount = shipment.Shipper.Company.Discount;
                }
            }

            if (chkNonVatable.Checked)
            {
                shipment.IsVatable = false;
            }
            else
            {
                shipment.IsVatable = true;
            }

            shipment.PaymentMode = _paymentMode;
            shipment.OriginCityId = Guid.Parse(cmbOriginCity.SelectedValue.ToString());
            shipment.DestinationCityId = Guid.Parse(cmbDestinationCity.SelectedValue.ToString());

            shipment = shipmentService.ComputePackageEvmCrating(shipment);

            shipment = shipmentService.ComputeCharges(shipment);
            PopulateSummary();
        }

        private void RateCalculatorForm_Load(object sender, EventArgs e)
        {
            // InitializeDataSource();
        }

        private void btnAddPackage_Click(object sender, EventArgs e)
        {
            try
            {
                AddPackage();
                txtLength.Text = "0";
                txtWidth.Text = "0";
                txtHeight.Text = "0";
                lstCrating.SelectedIndex = -1;
                chkPackaging.Checked = false;
                chkDraining.Checked = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private bool ValidateData()
        {

            if (cmdCommodityType.SelectedIndex <= -1 || cmbServiceType.SelectedIndex <= -1 || cmbServiceMode.SelectedIndex <= -1
                || cmbShipMode.SelectedIndex <= -1 || cmbOriginCity.SelectedIndex <= -1 || cmbDestinationCity.SelectedIndex <= -1)
            {
                return false;
            }

            if (cmbShipMode.SelectedText == "Transhipment")
            {
                if (cmdHub.SelectedIndex <= -1)
                {
                    return false;
                }

            }
            if (shipment.PackageDimensions == null)
            {
                return false;
            }
            else
            {
                if (shipment.PackageDimensions.Count <= 0)
                {
                    return false;
                }
            }

            return true;

        }

        private void PopulateSummary()
        {
            txtSumChargeableWeight.Text = shipment.ChargeableWeightString;
            txtSumWeightCharge.Text = shipment.WeightChargeString;
            if (shipment.AwbFee != null)
            {
                txtSumAwbFee.Text = shipment.AwbFee.Amount.ToString("N");
            }
            else
            {
                txtSumAwbFee.Text = "0.00";
            }
            txtSumValuation.Text = "0.00";
            txtSumValuation.Text = shipment.ValuationAmountString;
            if (shipment.DeliveryFee != null)
                txtSumDeliveryFee.Text = shipment.DeliveryFee.AmountString;
            else
            {
                txtSumDeliveryFee.Text = "0.00";
            }
            if (shipment.FreightCollectCharge != null)
            {
                txtSumFreightCollect.Text = shipment.FreightCollectCharge.Amount.ToString("N");
            }
            else
            {
                txtSumFreightCollect.Text = "0.00";
            }
            if (shipment.PeracFee != null)
            {
                txtSumPeracFee.Text = shipment.PeracFee.Amount.ToString("N");
            }
            else
            {
                txtSumPeracFee.Text = "0.00";
            }
            if (shipment.DangerousFee != null)
            {
                txtSumDangerousFee.Text = shipment.DangerousFee.AmountString;
            }
            else
            {
                txtSumDangerousFee.Text = "0.00";
            }
            txtSumFuelSurcharge.Text = shipment.FuelSurchargeAmountstring;

            txtSumCratingFee.Text = shipment.CratingFeeString;
            txtSumDrainingFee.Text = shipment.DrainingFeeString;
            txtSumPackagingFee.Text = shipment.PackagingFeeString;

            txtSumHandlingFee.Text = shipment.HandlingFeeString;
            txtSumQuarantineFee.Text = shipment.QuanrantineFeeString;

            txtSumDiscount.Text = shipment.DiscountAmountString;

            if (shipment.Insurance != null)
            {
                txtSumInsurance.Text = shipment.InsuranceAmountString;
            }
            else
            {
                txtSumInsurance.Text = "0.00";
            }
            if (chkNonVatable.Checked)
            {
                txtSumVatAmount.Text = "0.00";
            }
            else
            {
                txtSumVatAmount.Text = shipment.ShipmentVatAmountString;
            }
            txtSumSubTotal.Text = shipment.ShipmentSubTotalString;
            txtSumTotal.Text = "₱ " + shipment.ShipmentTotalString;
        }

        private void btnCompute_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidateData())
                {
                    ComputeCharges();
                }
                else
                {
                    MessageBox.Show("Unable to compute.", "Data Validation");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void cmbShipMode_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        {
            if (cmbShipMode.SelectedIndex > -1)
            {
                if (cmbShipMode.SelectedItem.Text == "Transhipment")
                {
                    lblHub.Visible = true;
                    cmdHub.Visible = true;
                }
                else
                {
                    lblHub.Visible = false;
                    cmdHub.Visible = false;
                }
            }
        }

        private void radGroupBox1_Click(object sender, EventArgs e)
        {

        }

        private void gridPackage_CellFormatting(object sender, Telerik.WinControls.UI.CellFormattingEventArgs e)
        {
            if (e.CellElement.ColumnInfo.Name == "No" && string.IsNullOrEmpty(e.CellElement.Text))
            {
                e.CellElement.Text = (e.CellElement.RowIndex + 1).ToString();
            }
        }

        private void txtDeclaredValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                if (ValidateData())
                {
                    ComputeCharges();
                }
            }
        }

        private void txtQuarantineFee_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                if (ValidateData())
                {
                    ComputeCharges();
                }
            }
        }

        private void txtHandlineFee_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                if (ValidateData())
                {
                    ComputeCharges();
                }
            }
        }

        private void txtRfa_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                if (ValidateData())
                {
                    ComputeCharges();
                }
            }
        }

        private void gridPackage_UserDeletingRow(object sender, Telerik.WinControls.UI.GridViewRowCancelEventArgs e)
        {
            try
            {
                int index = Convert.ToInt32(e.Rows[0].Cells["No"].Value) - 1; ;
                packageDimensionModel = shipment.PackageDimensions.FirstOrDefault(x => x.No == index);
                packageDimensionModel.RecordStatus = (int)RecordStatus.Deleted;

                gridPackage.DataSource = null;
                gridPackage.DataSource = shipment.PackageDimensions;
            }
            catch (Exception ex)
            {
                Logs.ErrorLogs("GridPackage_DeletingRow", ex);
                MessageBox.Show("Unable to delete.");
            }

        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    foreach (RadControl control in this.Controls)
            //    {
            //        if (control is RadMaskedEditBox)
            //        {
            //            RadMaskedEditBox radMasked = (RadMaskedEditBox)control;
            //            radMasked.ResetText();
            //        }
            //    }
            //}catch(Exception ex) {

            //}
        }
    }



}
