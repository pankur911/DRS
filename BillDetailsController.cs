using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CapProj.Controllers
{
    public class BillDetailsController : Controller
    {
        //
        // GET: /BillDetails/

        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /BillDetails/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /BillDetails/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /BillDetails/Create

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

        //
        // GET: /BillDetails/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /BillDetails/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
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

        //
        // GET: /BillDetails/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /BillDetails/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
