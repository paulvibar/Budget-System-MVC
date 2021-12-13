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
    public class ORSDetailsManagerController : Controller
    {
        // GET: ORSDetailsManager
        IRepository<ORSDetailsInformation> context;
        IRepository<ORSMainInformation> ORScontext;
        IRepository<ResponsibilityCenter> RCcontext;
        IRepository<MFOPAP> PAPcontext;
        IRepository<UACS> UACS;
        // GET: RCManager

        List<ResponsibilityCenter> ResponsibilityCenter;
        List<MFOPAP> MFOPAP;
        public ORSDetailsManagerController(IRepository<ORSDetailsInformation> Context, IRepository<ORSMainInformation> ORSContext,IRepository<ResponsibilityCenter> RCContext, IRepository<MFOPAP> PAPContext, IRepository<UACS> UACSContext)
        {
            context = Context;
            ORScontext = ORSContext;
            RCcontext = RCContext;
            PAPcontext = PAPContext;
            UACS = UACSContext;
        }
        public ActionResult Index(int Id)
        {
            List<ORSDetailsInformation> ORSDetails = context.Collection().ToList();
            return View(ORSDetails);
        }

        public ActionResult Create(int ORSId)
        {
            ORSDetailsManagerViewModel viewModel = new ORSDetailsManagerViewModel();
            ResponsibilityCenter = RCcontext.Collection().ToList();
            MFOPAP = PAPcontext.Collection().ToList();
            viewModel.ORSdetails = new ORSDetailsInformation();
            viewModel.ORSNumber = ORSId;
            var result = (from r in ResponsibilityCenter
                          join m in MFOPAP on r.PAP equals m.Id
                          select new RCItemViewModel()
                          {
                              RC = r,
                              MFOPAP = m,
                              Id = r.Id,
                              Caption = r.Code + " - " + r.Name + " (" + m.Name + ")"
                          }).AsEnumerable();
            viewModel.RCs = result;
            viewModel.PAP = PAPcontext.Collection();
            viewModel.UACS = UACS.Collection();
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Create(ORSDetailsInformation ORSDetails, int ORSId)
        {
            if (!ModelState.IsValid)
            {
                return View(ORSDetails);
            }
            else
            {
                context.Insert(ORSDetails);
                context.Commit();

                return RedirectToAction("Edit","ORSMainManager", new { id = ORSId });
            }
        }
        public ActionResult Edit(int Id, int ORSId)
        {
            ORSDetailsInformation ORSDetails = context.Find(Id);

            ResponsibilityCenter = RCcontext.Collection().ToList();
            MFOPAP = PAPcontext.Collection().ToList();
            if (ORSDetails == null)
            {
                return HttpNotFound();
            }
            else
            {

                ORSDetailsManagerViewModel viewModel = new ORSDetailsManagerViewModel();

                viewModel.ORSdetails = ORSDetails;
                viewModel.ORSNumber = ORSId;
                var result = (from r in ResponsibilityCenter
                              join m in MFOPAP on r.PAP equals m.Id
                              select new RCItemViewModel()
                              {
                                  RC = r,
                                  MFOPAP = m,
                                  Id = r.Id,
                                  Caption = r.Code + " - " + r.Name + " (" + m.Name + ")"
                              }).AsEnumerable();
                viewModel.RCs = result;
                viewModel.PAP = PAPcontext.Collection();
                viewModel.UACS = UACS.Collection();
                
                return View(viewModel);
            }
        }

        [HttpPost]
        public ActionResult Edit(ORSDetailsInformation ORSDetails, int Id, int ORSId)
        {
            ORSDetailsInformation EditORSDetails = context.Find(Id);
            if (EditORSDetails == null)
            {
                return HttpNotFound();
            }
            else
            {
                if (!ModelState.IsValid)
                {
                    return View(EditORSDetails);
                }
                else
                {
                    EditORSDetails.RCId = ORSDetails.RCId;
                    //EditORSDetails.PAPId = ORSDetails.PAPId;
                    EditORSDetails.UACSId = ORSDetails.UACSId;
                    EditORSDetails.Amount = ORSDetails.Amount;

                    context.Commit();

                    return RedirectToAction("Edit", "ORSMainManager", new { id = ORSId });
                }
            }
        }

        public ActionResult Delete(int Id)
        {
            ORSDetailsInformation DeleteORSDetails = context.Find(Id);
            if (DeleteORSDetails == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(DeleteORSDetails);
            }

        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult ConfirmDelete(int Id, int ORSId)
        {
            ORSDetailsInformation DeleteORSDetails = context.Find(Id);
            if (DeleteORSDetails == null)
            {
                return HttpNotFound();
            }
            else
            {
                context.Delete(Id);
                context.Commit();
                return RedirectToAction("Edit", "ORSMainManager", new { id = ORSId });
            }

        }

    }
}