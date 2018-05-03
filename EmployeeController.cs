using CapProj.Models;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CapProj.Controllers
{
    public class EmployeeController : Controller
    {
        //
        // GET: /Employee/

        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /Employee/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Employee/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Employee/Create

        [HttpPost]
        public ActionResult Create(EmployeeModel objmodel)
        {
            try
            {
                // TODO: Add insert logic here
                DynamicParameters param = new DynamicParameters();
                param.Add("@EmployeeName", objmodel.EmployeeName);
                param.Add("@DepartmentId", objmodel.DepartmentId);
                param.Add("@DesignationId", objmodel.DesignationId);
                param.Add("@EmployeeUserName", objmodel.EmployeeUserName);
                param.Add("@EmployeePassword", objmodel.EmployeePassword);
                param.Add("@Sequrityname", objmodel.SecurityName);
                param.Add("@Question", objmodel.QuestionId);
                SqlHelper.ExecuteSP("AddEmployee", param, null);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Employee/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Employee/Edit/5

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
        // GET: /Employee/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Employee/Delete/5

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
