using BudgetSystem.Core.Contracts;
using BudgetSystem.Core.Models;
using BudgetSystem.Core.ViewModels;
using BudgetSystem.InMemory;
using PagedList;
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
        [Authorize(Roles = "Admin")]
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            List<MFOPAP> PAPs = context.Collection().ToList();
            var result = PAPs.AsEnumerable();
            ViewBag.CurrentSort = sortOrder;
            ViewBag.CodeSortParam = String.IsNullOrEmpty(sortOrder) ? "codeDesc" : "";
            ViewBag.NameSortParam = sortOrder == "name" ? "nameDesc" : "name";
            ViewBag.TypeSortParam = sortOrder == "type" ? "typeDesc" : "type";
            ViewBag.StatusSortParam = sortOrder == "status" ? "statusDesc" : "status";

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
                case "codeDesc":
                    result = result.OrderByDescending(r => r.Code);
                    break;
                case "name":
                    result = result.OrderBy(r => r.Name);
                    break;
                case "nameDesc":
                    result = result.OrderByDescending(r => r.Name);
                    break;
                case "type":
                    result = result.OrderBy(r => r.Type);
                    break;
                case "typeDesc":
                    result = result.OrderByDescending(r => r.Type);
                    break;
                case "status":
                    result = result.OrderBy(r => r.Status);
                    break;
                case "statusDesc":
                    result = result.OrderByDescending(r => r.Status);
                    break;
                default:
                    result = result.OrderBy(r => r.Code);
                    break;
            }
            if (!String.IsNullOrEmpty(searchString))
            {
                result = result.Where(r => r.Code.ToString().Contains(searchString) ||
                                         r.Name.Contains(searchString) ||
                                         r.Type.Contains(searchString) ||
                                         r.Status.Contains(searchString));
            }
            int pageSize = 15;
            int pageNumber = (page ?? 1);
            return View(result.ToPagedList(pageNumber, pageSize));

        }
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            PAPManagerViewModel viewModel = new PAPManagerViewModel();

            viewModel.PAP = new MFOPAP();
            viewModel.Identifiers = IDcontext.Collection();

            return View(viewModel);
        }
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
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