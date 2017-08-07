using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using CMS2.Common;
using CMS2.Common.Enums;
using CMS2.DataAccess.Interfaces;
using CMS2.Entities.Models;
//using CrystalDecisions.CrystalReports.Engine;
//using CrystalDecisions.Shared;
using CMS2.Entities;

namespace CMS2.BusinessLogic
{
    public class StatementOfAccountBL : BaseAPCargoBL<StatementOfAccount>
    {
        private ICmsUoW _unitOfWork;
        private CompanyBL companyService;
        private ShipmentBL shipmentService;
        private PaymentTypeBL paymentTypeService;
        private ShipmentAdjustmentBL shipmentAdjustmentService;
        private StatementOfAccountPaymentBL soaPaymentService;
        private PaymentBL paymentService;

        public StatementOfAccountBL()
        {
            _unitOfWork = GetUnitOfWork();
            companyService = new CompanyBL(_unitOfWork);
            shipmentService = new ShipmentBL(_unitOfWork);
            paymentTypeService = new PaymentTypeBL(_unitOfWork);
            shipmentAdjustmentService = new ShipmentAdjustmentBL(_unitOfWork);
            soaPaymentService = new StatementOfAccountPaymentBL(_unitOfWork);
            paymentService = new PaymentBL(_unitOfWork);
        }

        public StatementOfAccountBL(ICmsUoW unitOfWork)
            : base(unitOfWork)
        {
            _unitOfWork = unitOfWork;
            soaPaymentService = new StatementOfAccountPaymentBL(unitOfWork);
            paymentService = new PaymentBL(unitOfWork);
        }

        public StatementOfAccountModel EntityToModel(StatementOfAccount entity)
        {
            StatementOfAccountModel model = new StatementOfAccountModel();
            if (entity != null)
            {
                model.StatementOfAccountId = entity.StatementOfAccountId;
                model.StatementOfAccountNo = entity.StatementOfAccountNo;
                model.StatementOfAccountDate = entity.StatementOfAccountDate;
                model.StatementOfAccountPeriodFrom = entity.StatementOfAccountPeriodFrom;
                model.StatementOfAccountPeriodUntil = entity.StatementOfAccountPeriodUntil;
                model.CompanyId = entity.CompanyId;
                model.Company = entity.Company;
                model.SoaDueDate = entity.SoaDueDate;
                model.CiNo = entity.CiNo;
                model.Remarks = entity.Remarks;
                //StatementOfAccountPrints = entity.StatementOfAccountPrints,
                model.CreatedBy = entity.CreatedBy;
                model.CreatedDate = entity.CreatedDate;
                model.ModifiedBy = entity.ModifiedBy;
                model.ModifiedDate = entity.ModifiedDate;
                model.RecordStatus = entity.RecordStatus;
                //if (model.CompanyId != null || model.CompanyId == Guid.Empty)
                //{ model.CompanyAccountNoBarcode = BarcodeGenerator.GetBarcode(model.Company.AccountNo); }

                //model.StatementOfAccountNoBarcode = BarcodeGenerator.GetBarcode(model.StatementOfAccountNo);
            }

            return model;
        }

        public List<StatementOfAccountModel> EntitiesToModels(List<StatementOfAccount> entities)
        {
            List<StatementOfAccountModel> models = new List<StatementOfAccountModel>();
            foreach (var item in entities)
            {
                StatementOfAccountModel model = EntityToModel(item);
                //model = ComputeSoa(model);
                models.Add(model);
            }
            return models;
        }

        public StatementOfAccount ModelToEntity(StatementOfAccountModel model)
        {
            StatementOfAccount entity = new StatementOfAccount()
            {
                StatementOfAccountId = model.StatementOfAccountId,
                StatementOfAccountNo = model.StatementOfAccountNo,
                StatementOfAccountDate = model.StatementOfAccountDate,
                StatementOfAccountPeriodFrom = model.StatementOfAccountPeriodFrom,
                StatementOfAccountPeriodUntil = model.StatementOfAccountPeriodUntil,
                CompanyId = model.CompanyId,
                SoaDueDate = model.SoaDueDate,
                CiNo = model.CiNo,
                Remarks = model.Remarks,
                CreatedBy = model.CreatedBy,
                CreatedDate = model.CreatedDate,
                ModifiedBy = model.ModifiedBy,
                ModifiedDate = model.ModifiedDate,
                RecordStatus = model.RecordStatus
            };
            return entity;
        }

        public List<StatementOfAccount> ModelsToEntities(List<StatementOfAccountModel> models)
        {
            List<StatementOfAccount> entities = new List<StatementOfAccount>();
            foreach (var item in models)
            {
                entities.Add(ModelToEntity(item));
            }
            return entities;
        }

        public List<PaymentType> GetPaymentTypes()
        {
            return paymentTypeService.FilterActive();
        }

        public new StatementOfAccountModel GetModelById(Guid id)
        {
            StatementOfAccount entity = base.GetById(id);
            if (entity != null)
            {
                StatementOfAccountModel model = EntityToModel(entity);
                model = ComputeSoa(model);
                return model;
            }
            else
            {
                return null;
            }
        }

        public StatementOfAccountModel GetModelBySoaNo(string soaNo)
        {
            StatementOfAccount entity = FilterActiveBy(x => x.StatementOfAccountNo.Equals(soaNo)).OrderByDescending(x => x.CreatedDate)
                    .FirstOrDefault();
            if (entity != null)
            {
                StatementOfAccountModel model = EntityToModel(entity);
                model = ComputeSoa(model);
                return model;
            }
            else
            {
                return null;
            }
        }

        public StatementOfAccountModel GetByStatementOfAccountNo(string soaNo)
        {
            StatementOfAccount entity = FilterActiveBy(x => x.StatementOfAccountNo.Equals(soaNo)).FirstOrDefault();
            if (entity != null)
            {
                StatementOfAccountModel model = EntityToModel(entity);
                model = ComputeSoa(model);
                return model;
            }
            else
            {
                return null;
            }
        }

        public List<StatementOfAccountModel> GetByCompanyId(Guid companyId)
        {
            List<StatementOfAccount> entities = FilterActiveBy(x => x.CompanyId == companyId);
            List<StatementOfAccountModel> models = new List<StatementOfAccountModel>();
            if (entities != null)
            {
                var _models = EntitiesToModels(entities);
                foreach (var item in _models)
                {
                    StatementOfAccountModel model = item;
                    model = ComputeSoa(model);
                    models.Add(model);
                }
                return models;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Compute for the Amount Due, Previous Balance and Outstanding Balance 
        /// </summary>
        /// <param name="StatementOfAccountModel"></param>
        /// <returns></returns>
        /// 
        public StatementOfAccountModel ComputeSoa(StatementOfAccountModel model)
        {
            Logs.AppLogs(LogPath, "SOA BL - ComputeSoa", model.StatementOfAccountNo);
            model.CurrentShipments = shipmentService.EntitiesToModels(shipmentService.FilterActiveBy(x => x.StatementOfAccountId == model.StatementOfAccountId)).OrderByDescending(x => x.DateAccepted).ToList();
            model.PreviousShipments = shipmentService.GetSoaShipmentsByCompanyId(model.CompanyId).Where(x => x.DateAccepted < model.StatementOfAccountPeriodFrom && x.StatementOfAccountId != null).OrderByDescending(x => x.DateAccepted).ToList();

            DateTime soaPaymentStart = new DateTime(model.StatementOfAccountPeriodUntil.Year, model.StatementOfAccountPeriodUntil.Month, model.StatementOfAccountPeriodUntil.Day, 0, 0, 0);
            DateTime soaPaymentEnd = new DateTime(model.SoaDueDate.Year, model.SoaDueDate.Month, model.SoaDueDate.Day, 11, 59, 59);
            model.SoaPayments = soaPaymentService.FilterActiveBy(x => x.StatementOfAccountId == model.StatementOfAccountId && (x.PaymentDate > soaPaymentStart && x.PaymentDate <= soaPaymentEnd));

            DateTime previousSoaStartDate = model.StatementOfAccountPeriodFrom.AddDays(-model.Company.BillingPeriod.NumberOfDays);
            var previousSoa = FilterActiveBy(x => x.StatementOfAccountPeriodFrom.Year == previousSoaStartDate.Year && x.StatementOfAccountPeriodFrom.Month == previousSoaStartDate.Month && x.StatementOfAccountPeriodFrom.Day == previousSoaStartDate.Day).OrderByDescending(x => x.CreatedDate).FirstOrDefault();
            if (previousSoa != null)
            {
                model.PreviousSoaPayments = soaPaymentService.FilterActiveBy(x => x.StatementOfAccount.StatementOfAccountId == previousSoa.StatementOfAccountId);
            }

            List<ShipmentModel> shipments = new List<ShipmentModel>();
            Logs.AppLogs(LogPath, "SOA BL - ComputeSoa - CurrentShipments");
            if (model.CurrentShipments != null && model.CurrentShipments.Count > 0)
            {
                foreach (var item in model.CurrentShipments)
                {
                    ShipmentModel shipmentModel = shipmentService.ComputeBalances(item, model.SoaDueDate);
                    model.TotalCurrentSubTotal = model.TotalCurrentSubTotal + shipmentModel.ShipmentSubTotal;
                    model.TotalCurrentVatAmount = model.TotalCurrentVatAmount + shipmentModel.ShipmentVatAmount;
                    model.TotalCurrentTotal = model.TotalCurrentTotal + shipmentModel.ShipmentTotal;
                    model.TotalCurrentPayments = model.TotalCurrentPayments + shipmentModel.CurrentPayments;
                    model.TotalCurrentAdjustments = model.TotalCurrentAdjustments + shipmentModel.Adjustment;

                    model.TotalSoaPayments = model.TotalSoaPayments + shipmentModel.CurrentPayments;
                    model.TotalSoaAdjustments = model.TotalSoaAdjustments + shipmentModel.Adjustment;

                    shipments.Add(shipmentModel);
                }
            }
            model.CurrentShipments = shipments;

            Logs.AppLogs(LogPath, "SOA BL - ComputeSoa - PreviousShipments");
            shipments = new List<ShipmentModel>();

            //string soaNo = model.StatementOfAccountNo; // ++++ for debugging
            if (model.PreviousShipments != null && model.PreviousShipments.Count > 0)
            {
                foreach (var item in model.PreviousShipments)
                {
                    ShipmentModel shipmentModel = shipmentService.ComputeBalances(item, model.SoaDueDate);
                    model.TotalPreviousAmountDue = model.TotalPreviousAmountDue + shipmentModel.SoaPreviousAmountDue;
                    model.TotalPreviousPayments = model.TotalPreviousPayments + shipmentModel.PreviousPayments;
                    model.TotalPreviousAdjustments = model.TotalPreviousAdjustments + shipmentModel.PreviousAdjustments;
                    model.TotalPreviousBalance = model.TotalPreviousBalance + shipmentModel.PreviousBalance;
                    model.TotalPreviousSurcharge = model.TotalPreviousSurcharge + shipmentModel.Surcharge;

                    model.TotalSoaPayments = model.TotalSoaPayments + shipmentModel.CurrentPayments;
                    model.TotalSoaAdjustments = model.TotalSoaAdjustments + shipmentModel.Adjustment;

                    shipments.Add(shipmentModel);
                }
            }

            model.PreviousShipments = shipments;
            model.TotalCurrentCharges = model.TotalCurrentTotal + model.TotalPreviousSurcharge;
            model.TotalBalancesFromPrevious = model.TotalPreviousAmountDue - model.TotalPreviousPayments + model.TotalPreviousAdjustments;
            model.TotalSoaAmount = model.TotalCurrentCharges + model.TotalBalancesFromPrevious;

            Logs.AppLogs(LogPath, "SOA BL - ComputeSoa - Done");
            return model;
        }

        public List<StatementOfAccountModel> GetHistory(string accountNo)
        {
            decimal totalPreviosBalance = 0;
            List<StatementOfAccountModel> models = GetByCompanyAccountNo(accountNo).OrderBy(x => x.StatementOfAccountDate).ToList();
            foreach (var item in models)
            {
                item.HistoryAmountDueFromPrevious = totalPreviosBalance;
                item.HistoryAdjustments = item.TotalPreviousAdjustments;
                item.HistoryPayments = item.TotalPreviousPayments;
                item.HistoryPreviousBalance = item.TotalBalancesFromPrevious;
                item.HistoryFreightCharges = item.TotalCurrentTotal;
                item.HistorySurcharge = item.TotalBalancesFromPrevious * (Convert.ToDecimal(3) / Convert.ToDecimal(100));
                item.HistoryCurrentCharges = item.HistoryFreightCharges + item.HistorySurcharge;
                item.HistoryAmountDue = item.TotalBalancesFromPrevious + item.HistoryCurrentCharges;

                totalPreviosBalance = item.HistoryAmountDue;
            }
            return models;
        }

        public List<StatementOfAccountModel> ComputeSoas(List<StatementOfAccountModel> models)
        {
            List<StatementOfAccountModel> _models = new List<StatementOfAccountModel>();
            foreach (var item in models)
            {
                _models.Add(ComputeSoa(item));
            }
            return _models;
        }

        public override void Edit(StatementOfAccount entity)
        {
            StatementOfAccount model = base.GetById(entity.StatementOfAccountId);
            if (string.IsNullOrEmpty(model.StatementOfAccountNo))
            {
                model.CiNo = entity.CiNo;
                model.Remarks = model.Remarks;
                model.ModifiedDate = DateTime.Now;
                model.ModifiedBy = entity.ModifiedBy;
                base.Edit(model);
            }
        }

        /// <summary>
        /// Create a New Statement Of Account
        /// </summary>
        /// <param name="accountNo"></param>
        /// <param name="periodStart"></param>
        /// <param name="periodEnd"></param>
        /// <returns></returns>
        public StatementOfAccount CreateNewSoa(string companyAccountNo, DateTime periodStart, DateTime periodEnd, Guid userId)
        {
            Logs.AppLogs(LogPath, "SOA BL - Create SOA", companyAccountNo);
            Company companyModel = companyService.GetByAccountNo(companyAccountNo);
            if (companyModel == null)
            {
                // TODO log error and notify user
                return null;
            }

            // get default SOA values from ApplicationSettings
            int dueDate = Convert.ToInt32(ApplicationSetting.Where(x => x.SettingName.Equals("SOADueDate")).OrderByDescending(x => x.CreatedDate).FirstOrDefault().SettingValue);
            int clientPeriod = Convert.ToInt32(ApplicationSetting.Where(x => x.SettingName.Equals("SOABillingPeriod")).OrderByDescending(x => x.CreatedDate).FirstOrDefault().SettingValue);
            periodStart = periodEnd.AddDays(-(clientPeriod - 1));

            if (companyModel.BillingPeriod != null)
            {
                clientPeriod = companyModel.BillingPeriod.NumberOfDays;
                periodStart = periodEnd.AddDays(-(clientPeriod - 1));
            }

            if (IsExist(x => x.CompanyId == companyModel.CompanyId && (periodEnd > x.StatementOfAccountPeriodFrom && periodEnd < x.StatementOfAccountPeriodUntil)))
            {
                return null;
            }

            Guid newSoaId = Guid.NewGuid();
            // create the SOA
            StatementOfAccount entity = new StatementOfAccount()
            {
                StatementOfAccountId = newSoaId,
                StatementOfAccountNoInt = GetNewSoaNo(newSoaId),
                StatementOfAccountDate = periodEnd,
                StatementOfAccountPeriodFrom = periodStart,
                StatementOfAccountPeriodUntil = periodEnd,
                CompanyId = companyModel.CompanyId,
                Company = companyModel,
                SoaDueDate = periodEnd.AddDays(dueDate),
                CreatedDate = DateTime.Now,
                CreatedBy = userId,
                ModifiedDate = DateTime.Now,
                ModifiedBy = userId,
                RecordStatus = (int)RecordStatus.Active
            };
            try
            {
                Add(entity);
                Logs.AppLogs(LogPath, "SOA BL - Create SOA", "Successfull");
            }
            catch (Exception ex)
            {
                Logs.ErrorLogs(LogPath, "SOA BL - Create SOA", ex);
            }


            // finalize previous SOA if not yet finalized
            StatementOfAccount previousSoa = GetByCompanyIdByPeriod(companyModel.CompanyId, periodStart.AddDays(-(clientPeriod)),
                periodEnd.AddDays(-(clientPeriod)));
            if (previousSoa != null && string.IsNullOrEmpty(previousSoa.StatementOfAccountNo))
            {
                Finalize(EntityToModel(previousSoa));
            }

            shipmentService.LinkToSoa(entity.StatementOfAccountId, companyAccountNo, periodStart, periodEnd);

            return entity;
        }

        /// <summary>
        /// Update SOA with SOANo
        /// Generate PDF and store to file system
        /// Finalized SOA are non-editable
        /// </summary>
        /// <param name="entity"></param>
        public StatementOfAccountModel Finalize(StatementOfAccountModel model)
        {
            Logs.AppLogs(LogPath, "SOA BL - Finalize");
            StatementOfAccount entity = base.GetById(model.StatementOfAccountId);
            if (entity != null && string.IsNullOrEmpty(entity.StatementOfAccountNo))
            {
                //entity.StatementOfAccountNoInt = GetNewSoaNo(entity.StatementOfAccountId);
                entity.ModifiedBy = model.ModifiedBy;
                entity.ModifiedDate = DateTime.Now;
                try
                {
                    base.Edit(entity);
                    //model.StatementOfAccountNo = entity.StatementOfAccountNo;
                    //model.StatementOfAccountNoBarcode = BarcodeGenerator.GetBarcode(model.StatementOfAccountNo);
                    model.ModifiedDate = entity.ModifiedDate;
                    Logs.AppLogs(LogPath, "SOA BL - Finalize", "Successfull");
                }
                catch (Exception ex)
                {
                    Logs.ErrorLogs(LogPath, "SOA BL - Finalize", ex);
                }

            }
            return model;
        }

        //public void CreateSavePdfFile(StatementOfAccountModel model)
        //{
        //    Logs.AppLogs(LogPath, "SOA BL - Create and Save Pdf File");
        //    // create PDF and save to file system
        //    if (model != null)
        //    {
        //        model = ComputeSoa(model);
        //    }
        //    string reportTemplatePath = "";
        //    string soaTemplateFile = "";
        //    string soaReportPath = "";
        //    //string branchName = model.Company.Area.Branch.BranchName;
        //    string bcoName = model.Company.City.BranchCorpOffice.BranchCorpOfficeName;
        //    string soaDate = model.StatementOfAccountDate.ToString("yyyyMMdd");
        //    string soaFileName = soaDate + "_" + model.StatementOfAccountNo + "_" + model.Company.AccountNo;

        //    if (ApplicationSetting != null)
        //    {
        //        reportTemplatePath = ApplicationSetting.SingleOrDefault(x => x.SettingName.Equals("SOAReportTemplatePath")).SettingValue;
        //        soaTemplateFile = ApplicationSetting.SingleOrDefault(x => x.SettingName.Equals("SOATemplateFilename")).SettingValue;
        //        soaReportPath = ApplicationSetting.SingleOrDefault(x => x.SettingName.Equals("SOAReportPath")).SettingValue + soaDate + "\\" + bcoName + "\\";

        //        string soaReportFile = soaFileName + ".pdf";

        //        if (!Directory.Exists(soaReportPath))
        //        {
        //            Directory.CreateDirectory(soaReportPath);
        //        }

        //        // transfer model to dataset for crystal reports
        //        StatementOfAccountDataSet soaDs = new StatementOfAccountDataSet();
        //        // fill SOA Details
        //        // TODO check the assignment of data. Must not add.
        //        DataRow dr = soaDs.Tables["StatementOfAccount"].NewRow();
        //        dr["SoaNo"] = model.StatementOfAccountNo;
        //        dr["SoaDateString"] = model.StatementOfAccountDateString;
        //        dr["SoaDueDate"] = model.SoaDueDateString;
        //        dr["SoaBillingPeriod"] = model.StatementOfAccountPeriod;
        //        dr["AccountNo"] = model.Company.AccountNo;
        //        dr["AccountNoBarcode"] = model.StatementOfAccountNoBarcode;
        //        dr["CompanyName"] = model.Company.CompanyName;
        //        dr["CompanyAddress"] = model.Company.BillingAddress1;
        //        dr["SoaAmountDue"] = model.TotalSoaAmountString;
        //        dr["TotalCurrentBalance"] = model.TotalCurrentChargesString;
        //        dr["TotalPreviousBalance"] = model.TotalPreviousBalanceString;
        //        dr["TotalPreviousSurcharges"] = model.TotalPreviousSurchargeString;
        //        dr["TotalFreightCharges"] = model.TotalCurrentSubTotalString;
        //        dr["TotalVatAmount"] = model.TotalCurrentVatAmountString;
        //        dr["TotalCurrentAmountDue"] = model.TotalCurrentTotalString;
        //        dr["TotalPreviousAmountDue"] = model.TotalPreviousAmountDueString;
        //        dr["TotalPreviousAdjustments"] = model.TotalPreviousAdjustmentsString;
        //        dr["TotalPreviousPayments"] = model.TotalPreviousPaymentsString;
        //        soaDs.Tables["StatementOfAccount"].Rows.Add(dr);

        //        // fill current Shipments
        //        foreach (var item in model.CurrentShipments)
        //        {
        //            dr = soaDs.Tables["CurrentShipments"].NewRow();
        //            dr["DateAccepted"] = item.DateAcceptedString;
        //            dr["AirwayBillNo"] = item.AirwayBillNo;
        //            dr["Origin"] = item.OriginCity.CityCode;
        //            dr["Destination"] = item.DestinationCity.CityCode;
        //            dr["FreightCharges"] = item.ShipmentSubTotalString;
        //            dr["VatAmount"] = item.ShipmentVatAmountString;
        //            dr["AmountDue"] = item.ShipmentTotalString;
        //            soaDs.Tables["CurrentShipments"].Rows.Add(dr);
        //        }

        //        // fill previous Shipments
        //        foreach (var item in model.PreviousShipments)
        //        {
        //            dr = soaDs.Tables["PreviousShipments"].NewRow();
        //            dr["SoaDate"] = item.StatementOfAccount.StatementOfAccountDateString;
        //            dr["SoaNo"] = item.StatementOfAccount.StatementOfAccountNo;
        //            dr["AwbNo"] = item.AirwayBillNo;
        //            dr["PreviousAmountDue"] = item.PreviousAmountDueString;
        //            dr["PreviousPayment"] = item.PreviousPaymentsString;
        //            dr["PreviousAdjustment"] = item.PreviousAdjustmentsString;
        //            dr["PreviousBalance"] = item.PreviousBalanceString;
        //            dr["Surcharge"] = item.SurchargeString;
        //            soaDs.Tables["PreviousShipments"].Rows.Add(dr);
        //        }

        //        // fill SOA Payments
        //        foreach (var item in model.PreviousSoaPayments)
        //        {
        //            dr = soaDs.Tables["Payments"].NewRow();
        //            dr["PaymentDate"] = item.PaymentDateString;
        //            dr["OrPrNo"] = item.OrNo;
        //            dr["Form"] = item.PaymentType.PaymentTypeName;
        //            dr["Remarks"] = item.CheckBankName + " " + item.CheckNo + " " + item.Remarks;
        //            dr["AmountPaid"] = item.AmountString;
        //            soaDs.Tables["Payments"].Rows.Add(dr);
        //        }

        //        try
        //        {
        //            ReportDocument report = new ReportDocument();
        //            report.Load(reportTemplatePath + soaTemplateFile);
        //            report.SetDataSource(soaDs);
        //            report.ExportToDisk(ExportFormatType.PortableDocFormat, soaReportPath + soaReportFile);
        //            Logs.AppLogs(LogPath, "SOA BL - CreateSavePdfFile", "Successfull");
        //        }
        //        catch (Exception ex)
        //        {
        //            Logs.ErrorLogs(LogPath, "SOA BL - CreateSavePdfFile", ex);
        //        }
        //    }
        //    else
        //    {
        //        //Logs.ErrorLogs(LogPath, "SOA BL - CreateSavePdfFile", "Application Settings is null");
        //    }
        //}

        /// <summary>
        /// Bill Run
        /// Create and Finalize SOA
        /// </summary>
        /// <param name="periodStart"></param>
        /// <param name="periodEnd"></param>
        public void GenerateMultipleSoa(DateTime periodStart, DateTime periodEnd)
        {
            List<StatementOfAccountModel> statementOfAccounts = new List<StatementOfAccountModel>();

            // get all Client Account Nos
            List<string> companyAccountNos = companyService.FilterActive().OrderBy(x => x.CompanyName).Select(x => x.AccountNo).ToList();

            // create soa for each account
            foreach (var accoutNo in companyAccountNos)
            {
                statementOfAccounts.Add(EntityToModel(CreateNewSoa(accoutNo, periodStart, periodEnd, Guid.Parse("{00000000-0000-0000-0000-000000000000}"))));
            }

            // finalize each soa
            foreach (var soa in statementOfAccounts)
            {
                Finalize(soa);
            }
        }

        public void Print(StatementOfAccount statementOfAccount)
        {
            // TODO: Print SOA-no code yet
            // find, open and print PDf file
        }

        public void PrintMultipleSoa(DateTime periodStart, DateTime periodEnd)
        {
            //TODO: Print Multiple SOA-get SOA in the given period
            List<StatementOfAccount> statementOfAccounts = new List<StatementOfAccount>();
            foreach (var item in statementOfAccounts)
            {
                Print(item);
            }
        }

        public List<KeyValuePair<string, Guid>> GetPreviousSoaNumbersByCompanyId(Guid companyId)
        {
            DateTime previousDate = DateTime.Now.AddMonths(-11);
            List<KeyValuePair<string, Guid>> previousSoaNumbers = new List<KeyValuePair<string, Guid>>();
            var soas = FilterActiveByAsync(x => x.CompanyId == companyId && (x.StatementOfAccountDate < DateTime.Now && x.StatementOfAccountDate > previousDate)).Result.OrderByDescending(x => x.StatementOfAccountDate);
            foreach (var item in soas)
            {
                previousSoaNumbers.Add(new KeyValuePair<string, Guid>(item.StatementOfAccountNo, item.StatementOfAccountId));
            }
            return previousSoaNumbers;
        }

        /// <summary>
        /// Generates the a 7-day statement of account periods
        /// Period is from Saturday-Friday
        /// </summary>
        /// <returns></returns>
        public List<string> GetBillingPeriods(string companyAccountNo = "")
        {
            int period = 7; // default billing period is 7
            var appSetting = ApplicationSetting.Where(x => x.SettingName.Equals("SOABillingPeriod")).OrderByDescending(x => x.CreatedDate)
                    .FirstOrDefault();
            if (appSetting != null)
            {
                period = Convert.ToInt32(appSetting.SettingValue);
            }

            DateTime startDate = new DateTime(2015, 01, 01);
            DateTime endDate;
            DateTime currentDate = DateTime.Now;
            List<string> periods = new List<string>();

            // Get Company BillingPeriod
            if (!string.IsNullOrEmpty(companyAccountNo))
            {
                if (companyService.IsExist(x => x.AccountNo.Equals(companyAccountNo)))
                {
                    var company = companyService.FilterBy(x => x.AccountNo.Equals(companyAccountNo)).FirstOrDefault();
                    if (company.BillingPeriodId != null)
                    {
                        period = company.BillingPeriod.NumberOfDays;
                    }
                }
            }

            // get the first Saturday of the year
            while (startDate.DayOfWeek != DayOfWeek.Saturday)
            {
                startDate = startDate.AddDays(1);
            }

            // get the next Friday from the current date
            while (currentDate.DayOfWeek != DayOfWeek.Friday)
            {
                currentDate = currentDate.AddDays(1);
            }
            endDate = currentDate;

            while (startDate < endDate)
            {
                periods.Add(startDate.ToString("MMM dd, yyyy") + " - " + startDate.AddDays(period - 1).ToString("MMM dd, yyyy"));
                startDate = startDate.AddDays(period);
            }
            periods.Reverse();
            return periods;
        }

        public int GetNewSoaNo(Guid statementOfAccountId)
        {
            StatementOfAccountNumberBL soaNumberService = new StatementOfAccountNumberBL(_unitOfWork);
            StatementOfAccountNumber model = new StatementOfAccountNumber()
            {
                StatementOfAccountId = statementOfAccountId,
                CreatedDate = DateTime.Now,
                CreatedBy = Guid.Parse("{11111111-1111-1111-1111-111111111111}"),
                ModifiedDate = DateTime.Now,
                ModifiedBy = Guid.Parse("{11111111-1111-1111-1111-111111111111}"),
                RecordStatus = (int)RecordStatus.Active
            };
            soaNumberService.Add(model);
            int newNo = model.SoaNumberId;
            return newNo;
        }

        public StatementOfAccount GetByCompanyIdByPeriod(Guid companyId, DateTime dateFrom, DateTime dateUntil)
        {
            return FilterActiveBy(x => x.CompanyId == companyId && x.StatementOfAccountPeriodFrom == dateFrom && x.StatementOfAccountPeriodUntil == dateUntil).OrderByDescending(x => x.CreatedDate)
                    .FirstOrDefault();
        }

        public Guid GetIdByStatementOfAccountNo(string soaNo)
        {
            var model = FilterActiveBy(x => x.StatementOfAccountNo.Equals(soaNo)).OrderByDescending(x => x.CreatedDate)
                    .FirstOrDefault();
            if (model != null)
            {
                return model.StatementOfAccountId;
            }
            else
            {
                return Guid.Empty;
            }
        }

        public List<StatementOfAccountModel> GetByCompanyAccountNo(string accountNo)
        {
            Logs.AppLogs(LogPath, "SOA BL - GetByCompanyAccountNo");
            var soa = FilterBy(x => x.Company.AccountNo.Equals(accountNo)).ToList();
            List<StatementOfAccountModel> models = new List<StatementOfAccountModel>();
            if (soa != null && soa.Count > 0)
            {
                models = EntitiesToModels(soa);
                models = ComputeSoas(models);
            }
            return models;
        }

        /// <summary>
        /// Payment made to all. Distribute amount to Shipments/AWBs
        /// </summary>
        /// <param name="soaPayment"></param>
        public void MakePayment(StatementOfAccountPayment soaPayment, StatementOfAccountModel model)
        {
            StatementOfAccountPayment _soaPayment = new StatementOfAccountPayment()
            {
                StatementOfAccountPaymentId = Guid.NewGuid(),
                StatementOfAccountId = soaPayment.StatementOfAccountId,
                PaymentDate = soaPayment.PaymentDate,
                OrNo = soaPayment.OrNo,
                Amount = soaPayment.Amount,
                PaymentTypeId = soaPayment.PaymentTypeId,
                CheckBankName = soaPayment.CheckBankName,
                CheckNo = soaPayment.CheckNo,
                CheckDate = soaPayment.CheckDate,
                ReceivedById = soaPayment.ReceivedById,
                Remarks = soaPayment.Remarks,
                CreatedBy = soaPayment.CreatedBy,
                CreatedDate = DateTime.Now,
                ModifiedBy = soaPayment.ModifiedBy,
                ModifiedDate = DateTime.Now,
                RecordStatus = (int)RecordStatus.Active
            };
            soaPaymentService.Add(_soaPayment);


            // TODO move this to CreateSOA
            //if (model.StatementOfAccountId == null || model.StatementOfAccountId == Guid.Empty)
            //{
            //    model = EntityToModel(FilterActiveBy(x => x.StatementOfAccountId == soaPayment.StatementOfAccountId).FirstOrDefault());

            //    if (shipmentService == null)
            //        shipmentService = new ShipmentBL(this._unitOfWork);
            //    model.CurrentShipments = shipmentService.EntitiesToModels(shipmentService.FilterActiveBy(x => x.StatementOfAccountId == model.StatementOfAccountId).OrderByDescending(x => x.DateAccepted).ToList());

            //    var previousSoaIds = FilterActiveBy(x => x.CompanyId == model.CompanyId && x.StatementOfAccountId != model.StatementOfAccountId).OrderBy(x => x.SoaDueDate).Select(x => x.StatementOfAccountId);
            //    if (previousSoaIds != null)
            //    {
            //        foreach (var previousSoaId in previousSoaIds)
            //        {
            //            model.PreviousShipments.AddRange(shipmentService.EntitiesToModels(shipmentService.FilterActiveBy(x => x.StatementOfAccountId == previousSoaId).OrderByDescending(x => x.DateAccepted).ToList()));
            //        }
            //    }
            //}

            //// get current and unpaid Shipments of the current SOA
            //List<ShipmentModel> unpaid = new List<ShipmentModel>();
            //unpaid.AddRange(model.CurrentShipments);
            //unpaid.AddRange(model.PreviousShipments);
            //if (unpaid != null)
            //{
            //    unpaid = shipmentService.ComputeBalances(unpaid, null);
            //    decimal balance = 0;
            //    decimal amountToPay = 0;
            //    decimal availableAmount = soaPayment.Amount;
            //    foreach (var item in unpaid.OrderBy(x => x.DateAccepted).ToList())
            //    {
            //        if (item.DateOfFullPayment == null)
            //        {
            //            balance = item.CurrentBalance;
            //            if (availableAmount >= balance)
            //            {
            //                amountToPay = balance;
            //            }
            //            else
            //            {
            //                amountToPay = availableAmount;
            //            }
            //            Payment paymentModel = new Payment()
            //            {
            //                PaymentId = Guid.NewGuid(),
            //                ShipmentId = item.ShipmentId,
            //                PaymentDate = soaPayment.PaymentDate,
            //                OrNo = soaPayment.OrNo,
            //                Amount = amountToPay,
            //                PaymentTypeId = soaPayment.PaymentTypeId,
            //                CheckBankName = soaPayment.CheckBankName,
            //                CheckNo = soaPayment.CheckNo,
            //                CheckDate = soaPayment.CheckDate,
            //                ReceivedById = soaPayment.ReceivedById,
            //                Remarks = soaPayment.Remarks,
            //                CreatedBy = soaPayment.CreatedBy,
            //                CreatedDate = DateTime.Now,
            //                ModifiedBy = soaPayment.ModifiedBy,
            //                ModifiedDate = DateTime.Now,
            //                RecordStatus = (int)RecordStatus.Active,
            //                StatementOfAccountPaymentId = _soaPayment.StatementOfAccountPaymentId
            //            };
            //            paymentService.Add(paymentModel);
            //            if (balance == amountToPay)
            //            {
            //                var shipment = shipmentService.GetById(item.ShipmentId);
            //                shipment.DateOfFullPayment = DateTime.Now;
            //                shipment.ModifiedBy = soaPayment.ModifiedBy;
            //                shipment.ModifiedDate = DateTime.Now;
            //                shipmentService.Edit(shipment);
            //            }
            //            availableAmount = availableAmount - amountToPay;
            //            if (availableAmount <= 0)
            //            {
            //                break;
            //            }
            //        }
            //    }
            //}
        }

        /// <summary>
        /// Payment made to specified Shipment/AWB
        /// </summary>
        /// <param name="soaPayment"></param>
        /// <param name="shipmentPayments"></param>
        public void MakePayment(StatementOfAccountPayment soaPayment, List<Payment> shipmentPayments)
        {
            StatementOfAccountPayment _soaPayment = new StatementOfAccountPayment()
            {
                StatementOfAccountPaymentId = Guid.NewGuid(),
                StatementOfAccountId = soaPayment.StatementOfAccountId,
                PaymentDate = soaPayment.PaymentDate,
                OrNo = soaPayment.OrNo,
                Amount = soaPayment.Amount,
                PaymentTypeId = soaPayment.PaymentTypeId,
                CheckBankName = soaPayment.CheckBankName,
                CheckNo = soaPayment.CheckNo,
                CheckDate = soaPayment.CheckDate,
                ReceivedById = soaPayment.ReceivedById,
                Remarks = soaPayment.Remarks,
                CreatedBy = soaPayment.CreatedBy,
                CreatedDate = DateTime.Now,
                ModifiedBy = soaPayment.ModifiedBy,
                ModifiedDate = DateTime.Now,
                RecordStatus = (int)RecordStatus.Active
            };
            soaPaymentService.Add(_soaPayment);

            PaymentBL paymentService = new PaymentBL(_unitOfWork);
            foreach (var payment in shipmentPayments)
            {
                payment.PaymentId = Guid.NewGuid();
                payment.CreatedDate = DateTime.Now;
                payment.ModifiedDate = DateTime.Now;
                payment.RecordStatus = (int)RecordStatus.Active;
                payment.StatementOfAccountPaymentId = _soaPayment.StatementOfAccountPaymentId;
                paymentService.Add(payment);
            }

            // update Shipments to paid when no more balance
            List<Shipment> shipments = shipmentService.FilterActiveBy(x => x.StatementOfAccountId == soaPayment.StatementOfAccountId && x.DateOfFullPayment == null).ToList();
            if (shipments != null)
            {
                List<ShipmentModel> models = shipmentService.EntitiesToModels(shipments);
                models = shipmentService.ComputeBalances(models, null);
                foreach (var item in models)
                {
                    // TODO bug not updating
                    if (item.PreviousBalance <= 0)
                    {
                        item.DateOfFullPayment = DateTime.Now;
                        item.ModifiedBy = soaPayment.ModifiedBy;
                        item.ModifiedDate = DateTime.Now;
                        var entity = shipmentService.ModelToEntity(item);
                        shipmentService.Edit(entity);
                    }
                }
            }
        }

        public List<StatementOfAccountPayment> GetStatementOfAccountPaymentsBySoaNo(string soaNo)
        {
            var models = soaPaymentService.FilterBy(x => x.StatementOfAccount.StatementOfAccountNo.Equals(soaNo)).ToList();
            return models;
        }

        public void UpdateAdjustments(List<ShipmentAdjustment> shipments)
        {
            Logs.AppLogs(LogPath, "SOA BL - UpdateAdjustments");
            UserStore userStore = new UserStore(_unitOfWork);

            if (shipments != null)
            {
                foreach (var item in shipments)
                {
                    item.ShipmentAdjustmentId = Guid.NewGuid();
                    item.DateAdjusted = item.DateAdjusted;// DateTime.Now; // ++++ commented for testing
                    item.AdjustmentReasonId = Guid.Parse("{C29D0852-26F9-436D-A5CA-E5F723CFC427}");// TODO AdjustmentReasonId for testing
                    item.AdjustedById = userStore.FindByIdAsync(item.AdjustedById).Result.EmployeeId;
                    item.CreatedBy = item.ModifiedBy;
                    item.CreatedDate = item.ModifiedDate;
                    try
                    {
                        shipmentAdjustmentService.AddEdit(item);
                    }
                    catch (Exception ex)
                    {
                        Logs.ErrorLogs(LogPath, "SOA BL - UpdateAdjustments", ex);
                    }
                }
            }

        }

        public bool IsNextSoaExist(Guid id)
        {
            // TODO implement this
            return false;
        }

    }
}