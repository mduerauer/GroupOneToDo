﻿using GroupOneToDo.Service.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GroupOneToDo.WebUI.Controllers
{
    public class HomeController : Controller
    {

        private readonly IToDoRepository _todoRepository;

        public HomeController(IToDoRepository todoRepository)
        {
            this._todoRepository = todoRepository;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}