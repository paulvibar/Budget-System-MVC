﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BudgetSystem.Core.Models;
using BudgetSystem.Core.ViewModels;
using BudgetSystem.InMemory;

namespace BudgetSystem.WebUI.Controllers
{
    public class RCManagerController : Controller
    {
        InMemoryRepository<ResponsibilityCenter> context;
        InMemoryRepository<MFOPAP> PAPcontext;
        // GET: RCManager

        public RCManagerController()
        {
            context = new InMemoryRepository<ResponsibilityCenter>();
            PAPcontext = new InMemoryRepository<MFOPAP>();
        }
        public ActionResult Index()
        {
            List<ResponsibilityCenter> RCs = context.Collection().ToList();
            return View(RCs);
        }

        public ActionResult Create()
        {
            RCManagerViewModel viewModel = new RCManagerViewModel();

            viewModel.RC = new ResponsibilityCenter();
            viewModel.PAPs = PAPcontext.Collection();
            return View(viewModel);
        }

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
                return View(viewModel);
            }
        }

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
                return View(RCDelete);
            }

        }
    }
}