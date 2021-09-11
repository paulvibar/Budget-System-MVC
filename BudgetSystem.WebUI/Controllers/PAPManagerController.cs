using BudgetSystem.Core.Contracts;
using BudgetSystem.Core.Models;
using BudgetSystem.Core.ViewModels;
using BudgetSystem.InMemory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BudgetSystem.WebUI.Controllers
{
    public class PAPManagerController : Controller
    {
        IRepository<MFOPAP> context;
        IRepository<Identifier> IDcontext;
        // GET: RCManager

        public PAPManagerController(IRepository<MFOPAP> context, IRepository<Identifier> IDcontext)
        {
            this.context = context;
            this.IDcontext = IDcontext;
        }
        public ActionResult Index()
        {
            List<MFOPAP> PAPs = context.Collection().ToList();
            return View(PAPs);
        }

        public ActionResult Create()
        {
            PAPManagerViewModel viewModel = new PAPManagerViewModel();

            viewModel.PAP = new MFOPAP();
            viewModel.Identifiers = IDcontext.Collection();

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Create(MFOPAP PAP)
        {
            if (!ModelState.IsValid)
            {
                return View(PAP);
            }
            else
            {
                context.Insert(PAP);
                context.Commit();

                return RedirectToAction("Index");
            }
        }
        public ActionResult Edit(int Id)
        {
            MFOPAP PAP = context.Find(Id);
            if (PAP == null)
            {
                return HttpNotFound();
            }
            else
            {
                PAPManagerViewModel viewModel = new PAPManagerViewModel();

                viewModel.PAP = PAP;
                viewModel.Identifiers = IDcontext.Collection();

                return View(viewModel);
            }
        }

        [HttpPost]
        public ActionResult Edit(MFOPAP PAP, int Id)
        {
            MFOPAP EditPAP = context.Find(Id);
            if (EditPAP == null)
            {
                return HttpNotFound();
            }
            else
            {
                if (!ModelState.IsValid)
                {
                    return View(PAP);
                }
                else
                {
                    EditPAP.Code = PAP.Code;
                    EditPAP.Name = PAP.Name;
                    EditPAP.Type = PAP.Type;
                    EditPAP.Status = PAP.Status;

                    context.Commit();

                    return RedirectToAction("Index");
                }
            }
        }

        public ActionResult Delete(int Id)
        {
            MFOPAP DeletePAP = context.Find(Id);
            if (DeletePAP == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(DeletePAP);
            }

        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult ConfirmDelete(int Id)
        {
            MFOPAP DeletePAP = context.Find(Id);
            if (DeletePAP == null)
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
    }
}