﻿using BudgetSystem.Core.Contracts;
using BudgetSystem.Core.Models;
using BudgetSystem.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BudgetSystem.WebUI.Controllers
{
    public class UACSManagerController : Controller
    {
        IRepository<UACS> context;
        IRepository<UACSClassification> contextClassification;
        IRepository<UACSClass> contextClass;
        IRepository<UACSGroup> contextGroup;
        IRepository<UACSObject> contextObject;

        List<UACS> UACS;
        List<UACSObject> UACSObject;
        List<UACSGroup> UACSGroup;
        List<UACSClass> UACSClass;
        List<UACSClassification> UACSClassification;

        public UACSManagerController(IRepository<UACS> Context, IRepository<UACSClassification> ContextClassification, IRepository<UACSClass> ContextClass, IRepository<UACSGroup> ContextGroup, IRepository<UACSObject> ContextObject)
        {
            this.context = Context;
            this.contextClassification = ContextClassification;
            this.contextClass = ContextClass;
            this.contextGroup = ContextGroup;
            this.contextObject = ContextObject;

            UACS = new List<UACS>();
            UACSGroup = new List<UACSGroup>();
            UACSObject = new List<UACSObject>();
            UACSClass = new List<UACSClass>();
            UACSClassification = new List<UACSClassification>();
        }
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            UACS = context.Collection().ToList();
            UACSObject = contextObject.Collection().ToList();
            UACSGroup = contextGroup.Collection().ToList();
            UACSClass = contextClass.Collection().ToList();
            UACSClassification = contextClassification.Collection().ToList();
            var result = (from o in UACS
                          join uo in UACSObject on o.ObjectId equals uo.Id
                          join g in UACSGroup on o.GroupId equals g.Id
                          join c in UACSClass on o.ClassId equals c.Id
                          join cc in UACSClassification on o.ClassificationId equals cc.Id
                          select new UACSItemViewModel()
                          {
                              UACS = o,
                              UACSObject = uo,
                              UACSGroup = g,
                              UACSClass = c,
                              UACSClassification = cc
                          });


            return View(result);
        }
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            UACSManagerViewModel viewModel = new UACSManagerViewModel();

            viewModel.UACS = new UACS();
            viewModel.Objects = contextObject.Collection();
            viewModel.Groups = contextGroup.Collection();
            viewModel.Classes = contextClass.Collection();
            viewModel.Classifications = contextClassification.Collection();

            return View(viewModel);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult Create(UACS UACS)
        {
            if (!ModelState.IsValid)
            {
                return View(UACS);
            }
            else
            {
                context.Insert(UACS);
                context.Commit();

                return RedirectToAction("Index");
            }
        }
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int Id)
        {
            UACS UACS = context.Find(Id);
            if (UACS == null)
            {
                return HttpNotFound();
            }
            else
            {
                UACSManagerViewModel viewModel = new UACSManagerViewModel();

                viewModel.UACS = UACS;
                viewModel.Objects = contextObject.Collection();
                viewModel.Groups = contextGroup.Collection();
                viewModel.Classes = contextClass.Collection();
                viewModel.Classifications = contextClassification.Collection();

                return View(viewModel);
            }
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult Edit(UACS UACS, int Id)
        {
            UACS EditUACS = context.Find(Id);
            if (EditUACS == null)
            {
                return HttpNotFound();
            }
            else
            {
                if (!ModelState.IsValid)
                {
                    return View(UACS);
                }
                else
                {
                    EditUACS.Code = UACS.Code;
                    EditUACS.Description = UACS.Description;
                    EditUACS.ClassificationId = UACS.ClassificationId;
                    EditUACS.ClassId= UACS.ClassId;
                    EditUACS.GroupId = UACS.GroupId;
                    EditUACS.ObjectId = UACS.ObjectId;

                    context.Commit();

                    return RedirectToAction("Index");
                }
            }
        }
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int Id)
        {
            UACS DeleteUACS = context.Find(Id);
            if (DeleteUACS == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(DeleteUACS);
            }

        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ActionName("Delete")]
        public ActionResult ConfirmDelete(int Id)
        {
            UACS DeleteUACS = context.Find(Id);
            if (DeleteUACS == null)
            {
                return HttpNotFound();
            }
            else
            {
                context.Delete(Id);
                context.Commit();
                return View(DeleteUACS);
            }

        }

    }
}