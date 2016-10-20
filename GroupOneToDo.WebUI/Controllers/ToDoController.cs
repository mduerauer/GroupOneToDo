using GroupOneToDo.Model;
using GroupOneToDo.Service.Repository;
using NLog;
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

        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

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

            var todo = await _todoRepository.GetById(id);

            return View(todo);
        }

        // GET: ToDo/Create
        public ActionResult Create()
        {
            var template = new ToDo()
            {
                CreatedBy = User.Identity.Name,
                CreatedWhen = DateTime.Now

            };

            return View(template);
        }

        // POST: ToDo/Create
        [HttpPost]
        public async Task<ActionResult> Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                ToDo todo = new ToDo(Guid.NewGuid())
                {
                    Task = collection["Task"],
                    DueDateTime = DateTime.Parse(collection["DueDateTime"]),
                    CreatedBy = User.Identity.Name,
                    CreatedWhen = DateTime.Now,
                    Done = false,
                    AssignedTo = User.Identity.Name
                };

                await _todoRepository.Create(todo);
                
                return RedirectToAction("Index");
            }
            catch(Exception e)
            {
                Logger.Error(e, "Exception occured.");

                return View();
            }
        }

        // GET: ToDo/Edit/5
        public async Task<ActionResult> Edit(Guid id)
        {
            var todo = await _todoRepository.GetById(id);

            return View(todo);
        }

        // POST: ToDo/Edit/5
        [HttpPost]
        public async Task<ActionResult> Edit(Guid id, FormCollection collection)
        {
            try
            {

                var existing = await _todoRepository.GetById(id);

                existing.Task = collection["Task"];
                existing.DueDateTime = DateTime.Parse(collection["DueDateTime"]);
                existing.Done = Boolean.Parse(collection["Done"].Split(',')[0]);

                await _todoRepository.Update(existing);
 
                return RedirectToAction("Index");
            }
            catch(Exception e)
            {
                Logger.Error(e, "Can't save edited ToDo.");

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
