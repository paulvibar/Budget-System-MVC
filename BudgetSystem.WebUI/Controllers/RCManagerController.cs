using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BudgetSystem.Core.Contracts;
using BudgetSystem.Core.Models;
using BudgetSystem.Core.ViewModels;
using BudgetSystem.InMemory;
using PagedList;

namespace BudgetSystem.WebUI.Controllers
{
    public class RCManagerController : Controller
    {
        IRepository<ResponsibilityCenter> context;
        IRepository<MFOPAP> PAPcontext;
        IRepository<Identifier> Identifiercontext;
        // GET: RCManager

        public RCManagerController(IRepository<ResponsibilityCenter> RCContext, IRepository<MFOPAP> PAPContext, IRepository<Identifier> Status)
        {
            context = RCContext;
            PAPcontext = PAPContext;
            Identifiercontext = Status;
        }
        [Authorize(Roles = "Admin")]
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            List<ResponsibilityCenter> RCs = context.Collection().ToList();
            List<MFOPAP> PAPs = PAPcontext.Collection().ToList();

            var result = (from r in RCs
                          join p in PAPs on r.PAP equals p.Id
                          select new RCItemViewModel()
                          {
                              RC = r,
                              MFOPAP = p
                          });

            ViewBag.CurrentSort = sortOrder;
            ViewBag.CodeSortParam = String.IsNullOrEmpty(sortOrder) ? "codeDesc" : "";
            ViewBag.NameSortParam = sortOrder == "name" ? "nameDesc" : "name";
            ViewBag.AcronymSortParam = sortOrder == "acronym" ? "acronymDesc" : "acronym";
            ViewBag.PAPSortParam = sortOrder == "pap" ? "papDesc" : "pap";
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
                    result = result.OrderByDescending(r => r.RC.Code);
                    break;
                case "name":
                    result = result.OrderBy(r => r.RC.Name);
                    break;
                case "nameDesc":
                    result = result.OrderByDescending(r => r.RC.Name);
                    break;
                case "acronym":
                    result = result.OrderBy(r => r.RC.Acronym);
                    break;
                case "acronymDesc":
                    result = result.OrderByDescending(r => r.RC.Acronym);
                    break;
                case "pap":
                    result = result.OrderBy(r => r.MFOPAP.Name);
                    break;
                case "papDesc":
                    result = result.OrderByDescending(r => r.MFOPAP.Name);
                    break;
                case "status":
                    result = result.OrderBy(r => r.RC.Status);
                    break;
                case "statusDesc":
                    result = result.OrderByDescending(r => r.RC.Status);
                    break;
                default:
                    result = result.OrderBy(r => r.RC.Code);
                    break;
            }
            if (!String.IsNullOrEmpty(searchString))
            {
                result = result.Where(r => r.RC.Code.ToString().Contains(searchString) ||
                                         r.RC.Name.Contains(searchString) ||
                                         r.RC.Acronym.Contains(searchString) ||
                                         r.MFOPAP.Name.Contains(searchString) ||
                                         r.RC.Status.Contains(searchString));
            }
            int pageSize = 15;
            int pageNumber = (page ?? 1);
            return View(result.ToPagedList(pageNumber, pageSize));
        }
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            RCManagerViewModel viewModel = new RCManagerViewModel();

            viewModel.RC = new ResponsibilityCenter();
            viewModel.PAPs = PAPcontext.Collection();
            viewModel.Identifiers = Identifiercontext.Collection();
            return View(viewModel);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult Create(ResponsibilityCenter RC)
        {
            if(!ModelState.IsValid)
            {
                return View(RC);
            }
            else
            {
                context.Insert(RC);
                context.Commit();

                return RedirectToAction("Index");
            }
        }
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int Id)
        {
            ResponsibilityCenter RC = context.Find(Id);
            if(RC == null)
            {
                return HttpNotFound();
            }
            else
            {
                RCManagerViewModel viewModel = new RCManagerViewModel();

                viewModel.RC = RC;
                viewModel.PAPs = PAPcontext.Collection();
                viewModel.Identifiers = Identifiercontext.Collection();
                return View(viewModel);
            }
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult Edit(ResponsibilityCenter RC, int Id)
        {
            ResponsibilityCenter RCEdit = context.Find(Id);
            if (RCEdit == null)
            {
                return HttpNotFound();
            }
            else
            {
                if(!ModelState.IsValid)
                {
                    return View(RC);
                }
                else
                {
                    RCEdit.Name = RC.Name;
                    RCEdit.PAP = RC.PAP;
                    RCEdit.Status = RC.Status;

                    context.Commit();

                    return RedirectToAction("Index");
                }
            }
        }
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int Id)
        {
            ResponsibilityCenter RCDelete = context.Find(Id);
            if (RCDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(RCDelete);
            }
            
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ActionName("Delete")]
        public ActionResult ConfirmDelete(int Id)
        {
            ResponsibilityCenter RCDelete = context.Find(Id);
            if (RCDelete == null)
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