using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PLA.Communications.Database;
using Microsoft.AspNet.SignalR;
using SignalRChat;

namespace PLA.Controllers
{
    public class DemoController : Controller
    {
        private db_PLAEntities db = new db_PLAEntities();

        //
        // GET: /Demo/

        public ActionResult Index()
        {
            return View();
        }
        public PartialViewResult _PartialIndex()
        {
            return PartialView(db.DevTests.ToList());
        }

        //
        // GET: /Demo/Details/5

        public ActionResult Details(int id = 0)
        {
            DevTest devtest = db.DevTests.Find(id);
            if (devtest == null)
            {
                return HttpNotFound();
            }
            return View(devtest);
        }

        //
        // GET: /Demo/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Demo/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(DevTest devtest)
        {
            if (ModelState.IsValid)
            {
                db.DevTests.Add(devtest);
                db.SaveChanges();

                var context = GlobalHost.ConnectionManager.GetHubContext<ChatHub>();
                context.Clients.All.getalldata();
                return RedirectToAction("Index");
            }

            return View(devtest);
        }

        //
        // GET: /Demo/Edit/5

        public ActionResult Edit(int id = 0)
        {
            DevTest devtest = db.DevTests.Find(id);
            if (devtest == null)
            {
                return HttpNotFound();
            }
            return View(devtest);
        }

        //
        // POST: /Demo/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(DevTest devtest)
        {
            if (ModelState.IsValid)
            {
                db.Entry(devtest).State = EntityState.Modified;
                db.SaveChanges();
                var context = GlobalHost.ConnectionManager.GetHubContext<ChatHub>();
                context.Clients.All.getalldata();
                return RedirectToAction("Index");
            }
            return View(devtest);
        }

        //
        // GET: /Demo/Delete/5

        public ActionResult Delete(int id = 0)
        {
            DevTest devtest = db.DevTests.Find(id);
            if (devtest == null)
            {
                return HttpNotFound();
            }
            return View(devtest);
        }

        //
        // POST: /Demo/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DevTest devtest = db.DevTests.Find(id);
            db.DevTests.Remove(devtest);
            db.SaveChanges();
            var context = GlobalHost.ConnectionManager.GetHubContext<ChatHub>();
            context.Clients.All.getalldata();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}