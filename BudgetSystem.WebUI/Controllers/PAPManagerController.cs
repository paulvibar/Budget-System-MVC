using BudgetSystem.Core.Models;
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
        InMemoryRepository<MFOPAP> context;
        // GET: RCManager

        public PAPManagerController()
        {
            context = new InMemoryRepository<MFOPAP>();
        }
        public ActionResult Index()
        {
            List<MFOPAP> PAPs = context.Collection().ToList();
            return View(PAPs);
        }

        public ActionResult Create()
        {
            MFOPAP PAP = new MFOPAP();

            return View(PAP);
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
                return View(PAP);
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
                    EditPAP.Id = PAP.Id;
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
                return View(DeletePAP);
            }

        }
    }
}