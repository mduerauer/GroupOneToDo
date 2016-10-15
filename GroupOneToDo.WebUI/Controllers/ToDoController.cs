using GroupOneToDo.Service.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace GroupOneToDo.WebUI.Controllers
{
    public class ToDoController : Controller
    {
        private readonly IToDoRepository _todoRepository;

        public ToDoController(IToDoRepository todoRepository)
        {
            this._todoRepository = todoRepository;
        }

        // GET: ToDo
        public ActionResult Index()
        {
            return RedirectToAction("List");
        }

        public async Task<ActionResult> List()
        {
            return View(await _todoRepository.FindAll());
        }

        // GET: ToDo/Details/5
        public async Task<ActionResult> Details(Guid id)
        {
            return View(await _todoRepository.GetById(id));
        }

        // GET: ToDo/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ToDo/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: ToDo/Edit/5
        public async Task<ActionResult> Edit(Guid id)
        {
            return View(await _todoRepository.GetById(id));
        }

        // POST: ToDo/Edit/5
        [HttpPost]
        public ActionResult Edit(Guid id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: ToDo/Delete/5
        public async Task<ActionResult> Delete(Guid id)
        {
            return View(await _todoRepository.GetById(id));
        }

        // POST: ToDo/Delete/5
        [HttpPost]
        public async Task<ActionResult> Delete(Guid id, FormCollection collection)
        {
            try
            {
                await _todoRepository.DeleteById(id);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
