using BudgetSystem.Core.Contracts;
using BudgetSystem.Core.Models;
using BudgetSystem.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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

        List<ORSMainInformation> ORSMain;
        List<ORSDetailsInformation> ORSDetails;
        List<UACSClass> AllotmentClass;
        List<FundSource> FundSource;
        List<FundCluster> FundCluster;
        List<BoxBSignatory> BoxBSignatory;


        public ORSMainManagerController(IRepository<ORSMainInformation> Context, IRepository<ORSDetailsInformation> ContextDetails, IRepository<UACSClass> ContextAllotment, IRepository<FundSource> ContextFundSource, IRepository<FundCluster> ContextFundCluster, IRepository<BoxBSignatory> ContextBoxB)
        {
            this.context = Context;
            this.contextDetails = ContextDetails;
            this.contextAllotment = ContextAllotment;
            this.contextFundSource = ContextFundSource;
            this.contextFundCluster = ContextFundCluster;
            this.contextBoxB = ContextBoxB;
        }
        public ActionResult Index()
        {
           
            ORSMain = context.Collection().ToList();
            ORSDetails = contextDetails.Collection().ToList();
            AllotmentClass = contextAllotment.Collection().ToList();
            FundSource = contextFundSource.Collection().ToList();
            FundCluster = contextFundCluster.Collection().ToList();
            BoxBSignatory = contextBoxB.Collection().ToList();

            var result = (from o in ORSMain
                          join od in ORSDetails on o.Id equals od.ORSId
                          join ac in AllotmentClass on o.AllotmentCode equals ac.AllotmentCode
                          join fs in FundSource on o.FundSource equals fs.Code
                          join fc in FundCluster on o.FundCluster equals fc.Code
                          join bb in BoxBSignatory on o.BoxBSignatory equals bb.Name
                          select new ORSItemViewModel()
                          {
                              ORSMain = o,
                              ORSDetail = od,
                              UACSClass = ac,
                              FundSource = fs,
                              FundCluster = fc,
                              BoxB = bb
                              
                          });

            return View(result);
        }

        public ActionResult Create()
        {
            ORSMainManagerViewModel viewModel = new ORSMainManagerViewModel();

            viewModel.ORSMain = new ORSMainInformation();
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

                return RedirectToAction("Index");
            }
        }

        public ActionResult Edit(int Id)
        {
            ORSMainInformation ORSMain = context.Find(Id);
            if (ORSMain == null)
            {
                return HttpNotFound();
            }
            else
            {
                ORSMainManagerViewModel viewModel = new ORSMainManagerViewModel();

                viewModel.ORSMain = new ORSMainInformation();
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
                    EditORS.Payee = ORSMain.Payee;
                    EditORS.Office = ORSMain.Office;
                    EditORS.Address = ORSMain.Address;
                    EditORS.Particulars = ORSMain.Particulars;
                    EditORS.BoxASignatory = ORSMain.BoxASignatory;
                    EditORS.BoxAPosition = ORSMain.BoxAPosition;
                    EditORS.BoxBPosition = ORSMain.BoxBPosition;
                    EditORS.Processor = ORSMain.Processor;

                    context.Commit();

                    return RedirectToAction("Index");
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
                return View(DeleteORS);
            }

        }

        public ActionResult Details()
        {
            ORSMainManagerViewModel viewModel = new ORSMainManagerViewModel();

            viewModel.ORSMain = new ORSMainInformation();
            viewModel.AllotmentClass = contextAllotment.Collection();
            viewModel.FundSource = contextFundSource.Collection();
            viewModel.FundCluster = contextFundCluster.Collection();
            viewModel.BoxB = contextBoxB.Collection();

            return View(viewModel);
        }
    }
}