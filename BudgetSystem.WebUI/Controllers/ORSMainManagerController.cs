using BudgetSystem.Core.Contracts;
using BudgetSystem.Core.Models;
using BudgetSystem.Core.ViewModels;
using ClosedXML.Excel;
using PagedList;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BudgetSystem.WebUI.Controllers
{
    public class ORSMainManagerController : Controller
    {

        IRepository<ORSMainInformation> context;
        IRepository<ORSDetailsInformation> contextDetails;
        IRepository<UACSClass> contextAllotment;
        IRepository<FundSource> contextFundSource;
        IRepository<FundCluster> contextFundCluster;
        IRepository<BoxBSignatory> contextBoxB;
        IRepository<ResponsibilityCenter> contextRC;
        IRepository<MFOPAP> contextPAP;
        IRepository<UACS> contextUACS;



        List<ORSMainInformation> ORSMain;
        List<ORSDetailsInformation> ORSDetails;
        List<UACSClass> AllotmentClass;
        List<FundSource> FundSource;
        List<FundCluster> FundCluster;
        List<BoxBSignatory> BoxBSignatory;
        List<ResponsibilityCenter> ResponsibilityCenter;
        List<MFOPAP> MFOPAP;
        List<UACS> UACS;


        public ORSMainManagerController(IRepository<ORSMainInformation> Context, IRepository<ORSDetailsInformation> ContextDetails, IRepository<UACSClass> ContextAllotment, IRepository<FundSource> ContextFundSource, IRepository<FundCluster> ContextFundCluster, IRepository<BoxBSignatory> ContextBoxB, IRepository<ResponsibilityCenter> ContextRC, IRepository<MFOPAP> ContextPAP, IRepository<UACS> ContextUACS)
        {
            this.context = Context;
            this.contextDetails = ContextDetails;
            this.contextAllotment = ContextAllotment;
            this.contextFundSource = ContextFundSource;
            this.contextFundCluster = ContextFundCluster;
            this.contextBoxB = ContextBoxB;
            this.contextRC = ContextRC;
            this.contextPAP = ContextPAP;
            this.contextUACS = ContextUACS;

            ORSMain = new List<ORSMainInformation>();
            ORSDetails = new List<ORSDetailsInformation>();
            AllotmentClass = new List<UACSClass>();
            FundSource = new List<FundSource>();
            FundCluster = new List<FundCluster>();
            BoxBSignatory = new List<BoxBSignatory>();
            ResponsibilityCenter = new List<ResponsibilityCenter>();
            MFOPAP = new List<MFOPAP>();
            UACS = new List<UACS>();
        }
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {

            ORSMain = context.Collection().ToList();
            ORSDetails = contextDetails.Collection().ToList();
            AllotmentClass = contextAllotment.Collection().ToList();
            FundSource = contextFundSource.Collection().ToList();
            FundCluster = contextFundCluster.Collection().ToList();
            BoxBSignatory = contextBoxB.Collection().ToList();
            
            var result = (from o in ORSMain
                          join ac in AllotmentClass on o.AllotmentCode equals ac.AllotmentCode
                          join fs in FundSource on o.FundSource equals fs.Code
                          join fc in FundCluster on o.FundCluster equals fc.Code
                          join bb in BoxBSignatory on o.BoxBID equals bb.Id
                          select new ORSItemViewModel()
                          {
                              ORSMain = o,
                              UACSClass = ac,
                              FundSource = fs,
                              FundCluster = fc,
                              BoxB = bb,
                              Total = ORSDetails.Where(i => i.ORSId == o.Id).Sum(x => x.Amount).ToString("#,##0.00")
                          });

            ViewBag.CurrentSort = sortOrder;
            ViewBag.IdSortParam = String.IsNullOrEmpty(sortOrder) ? "IdDesc" : "";
            ViewBag.PayeeSortParam = sortOrder == "payee" ? "payeeDesc" : "payee";
            ViewBag.OfficeSortParam = sortOrder == "office" ? "officeDesc" : "office";
            ViewBag.AddressSortParam = sortOrder == "address" ? "addressDesc" : "address";
            ViewBag.ParticularsSortParam = sortOrder == "particulars" ? "particularsDesc" : "particulars";
            ViewBag.DateSortParam = sortOrder == "date" ? "dateDesc" : "date";
            ViewBag.ProcessorSortParam = sortOrder == "processor" ? "processorDesc" : "processor";
            ViewBag.AmountSortParam = sortOrder == "amount" ? "amountDesc" : "amount";
            
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            switch (sortOrder)
            {
                case "IdDesc":
                    result = result.OrderByDescending(r => r.ORSMain.Id);
                    break;
                case "payee":
                    result = result.OrderBy(r => r.ORSMain.Payee);
                    break;
                case "payeeDesc":
                    result = result.OrderByDescending(r => r.ORSMain.Payee);
                    break;
                case "office":
                    result = result.OrderBy(r => r.ORSMain.Office);
                    break;
                case "officeDesc":
                    result = result.OrderByDescending(r => r.ORSMain.Office);
                    break;
                case "address":
                    result = result.OrderBy(r => r.ORSMain.Address);
                    break;
                case "addressDesc":
                    result = result.OrderByDescending(r => r.ORSMain.Address);
                    break;
                case "particulars":
                    result = result.OrderBy(r => r.ORSMain.Particulars);
                    break;
                case "particularsDesc":
                    result = result.OrderByDescending(r => r.ORSMain.Particulars);
                    break;
                case "date":
                    result = result.OrderBy(r => r.ORSMain.Date);
                    break;
                case "dateDesc":
                    result = result.OrderByDescending(r => r.ORSMain.Date);
                    break;
                case "processor":
                    result = result.OrderBy(r => r.ORSMain.Processor);
                    break;
                case "processorDesc":
                    result = result.OrderByDescending(r => r.ORSMain.Processor);
                    break;
                case "amount":
                    result = result.OrderBy(r => r.Total);
                    break;
                case "amountDesc":
                    result = result.OrderByDescending(r => r.Total);
                    break;
                default:
                    result = result.OrderBy(r => r.ORSMain.Id);
                    break;
            }

            if (!String.IsNullOrEmpty(searchString))
            {
                result = result.Where(r => r.ORSMain.Id.ToString().Contains(searchString) ||
                                         r.ORSMain.Payee.Contains(searchString) ||
                                         r.ORSMain.Office.Contains(searchString) ||
                                         r.ORSMain.Address.Contains(searchString) ||
                                         r.ORSMain.Particulars.Contains(searchString) ||
                                         r.ORSMain.Date.ToString().Contains(searchString) ||
                                         r.ORSMain.Processor.Contains(searchString) ||
                                         r.Total.ToString().Contains(searchString));
            }
            int pageSize = 15;
            int pageNumber = (page ?? 1);
            return View(result.ToPagedList(pageNumber, pageSize));
        }
        public IList<ORSItemViewModel> GetORSList()
        {
            ORSMain = context.Collection().ToList();
            ORSDetails = contextDetails.Collection().ToList();
            AllotmentClass = contextAllotment.Collection().ToList();
            FundSource = contextFundSource.Collection().ToList();
            FundCluster = contextFundCluster.Collection().ToList();
            BoxBSignatory = contextBoxB.Collection().ToList();
            ResponsibilityCenter = contextRC.Collection().ToList();
            MFOPAP = contextPAP.Collection().ToList();
            UACS = contextUACS.Collection().ToList();
            var result = (from o in ORSMain
                          join od in ORSDetails on o.Id equals od.ORSId
                          join ac in AllotmentClass on o.AllotmentCode equals ac.AllotmentCode
                          join fs in FundSource on o.FundSource equals fs.Code
                          join fc in FundCluster on o.FundCluster equals fc.Code
                          join bb in BoxBSignatory on o.BoxBID equals bb.Id
                          join rc in ResponsibilityCenter on od.RCId equals rc.Id
                          join p in MFOPAP on od.PAPId equals p.Id
                          join u in UACS on od.UACSId equals u.Id
                          select new ORSItemViewModel()
                          {
                              ORSMain = o,
                              ORSDetails = od,
                              UACSClass = ac,
                              FundSource = fs,
                              FundCluster = fc,
                              BoxB = bb,
                              RC = rc,
                              MFOPAP = p,
                              UACS = u
                          }).ToList();
            return result;
        }

        [HttpPost]
        public FileResult ExportToExcel()
        {
            DataTable dt = new DataTable("All ORS");
            dt.Columns.AddRange(new DataColumn[15]
            {
                new DataColumn("ORS Number"),
                new DataColumn("Payee"),
                new DataColumn("Particulars"),
                new DataColumn("MFOPAP"),
                new DataColumn("UACS Object Code"),
                new DataColumn("Amount"),
                new DataColumn("Date"),
                new DataColumn("UACS Description"),
                new DataColumn("Responsibility Center"),
                new DataColumn("Office Name"),
                new DataColumn("Allotment Code"),
                new DataColumn("MFOPAP Description"),
                new DataColumn("Fund Code"),
                new DataColumn("Fund Description"),
                new DataColumn("Fund Cluster")
            });
            foreach (var result in GetORSList())
            {
                dt.Rows.Add(result.ORSMain.Id, result.ORSMain.Payee, result.ORSMain.Particulars, result.MFOPAP.Code,
                    result.UACS.Code, result.ORSDetails.Amount.ToString("#,##0.00"), result.ORSMain.Date, result.UACS.Description, result.RC.Code,
                    result.RC.Name, result.UACSClass.AllotmentCode, result.MFOPAP.Name, result.FundSource.Code, result.FundSource.Description,
                    result.FundCluster.Code);
            }

            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt);
                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "AllORS.xlsx");
                }
            }
        }
        public ActionResult Create()
        {
            ORSMainManagerViewModel viewModel = new ORSMainManagerViewModel();

            viewModel.ORSMain = new ORSMainInformation();
            viewModel.ORSDetails = new ORSDetailsInformation();
            viewModel.AllotmentClass = contextAllotment.Collection();
            viewModel.FundSource = contextFundSource.Collection();
            viewModel.FundCluster = contextFundCluster.Collection();
            viewModel.BoxB = contextBoxB.Collection();

            return View(viewModel);
            
        }

        [HttpPost]
        public ActionResult Create(ORSMainInformation ORSMain)
        {
            if (!ModelState.IsValid)
            {
                return View(ORSMain);
            }
            else
            {
                context.Insert(ORSMain);
                context.Commit();
                int x = ORSMain.Id;
                return RedirectToAction("Edit", new { id = x });
            }
        }
        public ActionResult Edit(int Id)
        {
            ORSMainInformation ORSMain = context.Find(Id);

            ORSDetails = contextDetails.Collection().ToList();
            ResponsibilityCenter = contextRC.Collection().ToList();
            MFOPAP = contextPAP.Collection().ToList();
            UACS = contextUACS.Collection().ToList();

            if (ORSMain == null)
            {
                return HttpNotFound();
            }
            else
            {
                ORSMainManagerViewModel viewModel = new ORSMainManagerViewModel();

                viewModel.ORSMain = ORSMain;
                //viewModel.ListORSDetails = contextDetails.Collection().Where(i => i.ORSId == Id).ToList();
                var result = (from od in ORSDetails
                              join rc in ResponsibilityCenter on od.RCId equals rc.Id
                              join p in MFOPAP on od.PAPId equals p.Id
                              join u in UACS on od.UACSId equals u.Id
                              select new ORSDetailsItemViewModel()
                              {
                                  ORSDetails = od,
                                  RC = rc,
                                  MFOPAP = p,
                                  UACS = u
                              }).Where(i => i.ORSDetails.ORSId == Id).ToList();
                viewModel.ListORSDetails = result;
                viewModel.AllotmentClass = contextAllotment.Collection();
                viewModel.FundSource = contextFundSource.Collection();
                viewModel.FundCluster = contextFundCluster.Collection();
                viewModel.BoxB = contextBoxB.Collection();

                return View(viewModel);
            }
        }

        [HttpPost]
        public ActionResult Edit(ORSMainInformation ORSMain, int Id)
        {
            ORSMainInformation EditORS = context.Find(Id);
            if (EditORS == null)
            {
                return HttpNotFound();
            }
            else
            {
                if (!ModelState.IsValid)
                {
                    return View(ORSMain);
                }
                else
                {
                    EditORS.Date = ORSMain.Date;
                    EditORS.AllotmentCode = ORSMain.AllotmentCode;
                    EditORS.FundSource = ORSMain.FundSource;
                    EditORS.FundCluster = ORSMain.FundCluster;
                    EditORS.Payee = ORSMain.Payee.ToUpper();
                    EditORS.Office = ORSMain.Office;
                    EditORS.Address = ORSMain.Address;
                    EditORS.Particulars = ORSMain.Particulars;
                    EditORS.BoxASignatory = ORSMain.BoxASignatory;
                    EditORS.BoxAPosition = ORSMain.BoxAPosition;
                    EditORS.BoxBID = ORSMain.BoxBID;
                    EditORS.Processor = ORSMain.Processor;


                    context.Commit();
                    int x = ORSMain.Id;
                    return RedirectToAction("Edit", new { id = x });
                }
            }
        }

        public ActionResult Delete(int Id)
        {
            ORSMainInformation DeleteORS = context.Find(Id);
            if (DeleteORS == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(DeleteORS);
            }

        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult ConfirmDelete(int Id)
        {
            ORSMainInformation DeleteORS = context.Find(Id);
            if (DeleteORS == null)
            {
                return HttpNotFound();
            }
            else
            {
                context.Delete(Id);
                context.Commit();
                return RedirectToAction("Index");
            }

        }

        public ActionResult Details(int Id)
        {
            ORSMainInformation oORSMain = context.Find(Id);

            if (oORSMain == null)
            {
                return HttpNotFound();
            }
            else
            {
                ORSMainManagerViewModel viewModel = new ORSMainManagerViewModel();

                viewModel.ORSMain = oORSMain;

                ORSMain = context.Collection().ToList();
                ORSDetails = contextDetails.Collection().ToList();
                AllotmentClass = contextAllotment.Collection().ToList();
                FundSource = contextFundSource.Collection().ToList();
                FundCluster = contextFundCluster.Collection().ToList();
                BoxBSignatory = contextBoxB.Collection().ToList();

                var resultMain = (from o in ORSMain
                                  join ac in AllotmentClass on o.AllotmentCode equals ac.AllotmentCode
                                  join fs in FundSource on o.FundSource equals fs.Code
                                  join fc in FundCluster on o.FundCluster equals fc.Code
                                  join bb in BoxBSignatory on o.BoxBID equals bb.Id
                                  select new ORSItemViewModel()
                                  {
                                      ORSMain = o,
                                      UACSClass = ac,
                                      FundSource = fs,
                                      FundCluster = fc,
                                      BoxB = bb,
                                      Total = ORSDetails.Where(i => i.ORSId == o.Id).Sum(x => x.Amount).ToString("#,##0.00")
                                  }).Where(i => i.ORSMain.Id == Id).ToList();

                ORSDetails = contextDetails.Collection().ToList();
                ResponsibilityCenter = contextRC.Collection().ToList();
                MFOPAP = contextPAP.Collection().ToList();
                UACS = contextUACS.Collection().ToList();

                var result = (from od in ORSDetails
                              join rc in ResponsibilityCenter on od.RCId equals rc.Id
                              join p in MFOPAP on od.PAPId equals p.Id
                              join u in UACS on od.UACSId equals u.Id
                              select new ORSDetailsItemViewModel()
                              {
                                  ORSDetails = od,
                                  RC = rc,
                                  MFOPAP = p,
                                  UACS = u,
                                  Amount = od.Amount.ToString("#,##0.00")
                              }).Where(i => i.ORSDetails.ORSId == Id).ToList();

                viewModel.ListORSMain = resultMain;
                viewModel.ListORSDetails = result;
                viewModel.AllotmentClass = contextAllotment.Collection();
                viewModel.FundSource = contextFundSource.Collection();
                viewModel.FundCluster = contextFundCluster.Collection();
                viewModel.BoxB = contextBoxB.Collection();

                ViewData["Id"] = Id;
                
                return View(viewModel);
            }
        }
        public IList<ORSItemViewModel> GetORS(int Id)
        {
            ORSMain = context.Collection().ToList();
            ORSDetails = contextDetails.Collection().ToList();
            AllotmentClass = contextAllotment.Collection().ToList();
            FundSource = contextFundSource.Collection().ToList();
            FundCluster = contextFundCluster.Collection().ToList();
            BoxBSignatory = contextBoxB.Collection().ToList();
            ResponsibilityCenter = contextRC.Collection().ToList();
            MFOPAP = contextPAP.Collection().ToList();
            UACS = contextUACS.Collection().ToList();
            var result = (from o in ORSMain
                          join od in ORSDetails on o.Id equals od.ORSId
                          join ac in AllotmentClass on o.AllotmentCode equals ac.AllotmentCode
                          join fs in FundSource on o.FundSource equals fs.Code
                          join fc in FundCluster on o.FundCluster equals fc.Code
                          join bb in BoxBSignatory on o.BoxBID equals bb.Id
                          join rc in ResponsibilityCenter on od.RCId equals rc.Id
                          join p in MFOPAP on od.PAPId equals p.Id
                          join u in UACS on od.UACSId equals u.Id
                          select new ORSItemViewModel()
                          {
                              ORSMain = o,
                              ORSDetails = od,
                              UACSClass = ac,
                              FundSource = fs,
                              FundCluster = fc,
                              BoxB = bb,
                              RC = rc,
                              MFOPAP = p,
                              UACS = u,
                              Total = ORSDetails.Where(i => i.ORSId == o.Id).Sum(x => x.Amount).ToString("#,##0.00"),
                              Date = o.Date.ToString()
                          }).Where(i => i.ORSMain.Id == Id).ToList();
            return result;
        }

        public void GetORSReport(int Id)
        {
            ReportParam objReportParams = new ReportParam();
            var data = GetORSInfo(Id);
            objReportParams.DataSource = data;
            objReportParams.ReportTitle = "ORS Info Report";
            objReportParams.RptFileName = "ORSReport.rdlc";
            objReportParams.ReportType = "ORSInfoReport";
            objReportParams.DataSetName = "dsORSReport";
            this.HttpContext.Session["ReportParam"] = objReportParams;
        }

        public DataTable GetORSInfo(int Id)
        {
            
            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[22]
            {
                new DataColumn("ORSNumber"),
                new DataColumn("Payee"),
                new DataColumn("Office"),
                new DataColumn("Address"),
                new DataColumn("Particulars"),
                new DataColumn("MFOPAP"),
                new DataColumn("UACSObjectCode"),
                new DataColumn("Amount"),
                new DataColumn("Date"),
                new DataColumn("UACSDescription"),
                new DataColumn("ResponsibilityCenter"),
                new DataColumn("OfficeName"),
                new DataColumn("AllotmentCode"),
                new DataColumn("MFOPAPDescription"),
                new DataColumn("FundCode"),
                new DataColumn("FundDescription"),
                new DataColumn("FundCluster"),
                new DataColumn("BoxASignatory"),
                new DataColumn("BoxAPosition"),
                new DataColumn("BoxBSignatory"),
                new DataColumn("BoxBPosition"),
                new DataColumn("Total")
            });
            foreach (var result in GetORS(Id))
            {
                dt.Rows.Add(result.ORSMain.Caption.ToString(), result.ORSMain.Payee.ToString(), result.ORSMain.Office, result.ORSMain.Address, result.ORSMain.Particulars, result.MFOPAP.Code,
                    result.UACS.Code, result.ORSDetails.Amount.ToString("#,##0.00"), result.Date, result.UACS.Description, result.RC.Code,
                    result.RC.Name, result.UACSClass.AllotmentCode, result.MFOPAP.Name, result.FundSource.Code, result.FundSource.Description,
                    result.FundCluster.Code, result.ORSMain.BoxASignatory, result.ORSMain.BoxAPosition, result.BoxB.Name, result.BoxB.Position,
                    result.Total);
            }
            
            return dt;
        }
    }
}