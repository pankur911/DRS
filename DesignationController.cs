using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CapProj.Controllers
{
    public class DesignationController : Controller
    {
        //
        // GET: /Designation/

        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /Designation/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Designation/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Designation/Create

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
        // GET: /Designation/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Designation/Edit/5

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
        // GET: /Designation/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Designation/Delete/5

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
