using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using prjToDo.Models;

namespace prjToDo.Controllers
{
    public class HomeController : Controller
    {
        dbToDoEntities db = new dbToDoEntities();
        // GET: Home
        public ActionResult Index()
        {
            var todos = db.tToDo.OrderBy(m => m.fDate).ToList();

            return View(todos);
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(string fTitle, string fLevel, DateTime fDate)
        {
            tToDo todo = new tToDo();
            todo.fTitle = fTitle;
            todo.fLevel = fLevel;
            todo.fDate = fDate;
            db.tToDo.Add(todo);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Delete(int id)
        {
            var todo = db.tToDo.Where(m => m.fId == id).FirstOrDefault();
            tDone done = new tDone();
            done.fId = todo.fId;
            done.fTitle = todo.fTitle;
            done.fLevel = todo.fLevel;
            done.fDate = todo.fDate;
            done.fDeleDate = DateTime.Now;

            db.tDone.Add(done); // 存到完成
            db.tToDo.Remove(todo);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Log()
        {
            var doneList = db.tDone.OrderByDescending(m => m.fDeleDate).ToList();

            return View(doneList);
        }
    }
}