using CapProj.Models;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CapProj.Controllers
{
    public class AccountController : Controller
    {
        //
        // GET: /Account/

        public ActionResult Index()
        {
            Session.Abandon();
            return View();
        }

        //
        // GET: /Account/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Account/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Account/Create

        [HttpPost]
        public ActionResult Create(EmployeeModel objemployeemodel)
        {
            try
            {
                DynamicParameters param=new DynamicParameters();
                param.Add("@UserId", objemployeemodel.EmployeeUserName);
                param.Add("@Pass", objemployeemodel.EmployeePassword);
                IEnumerable<EmployeeModel> Employeedata=SqlHelper.QuerySP<EmployeeModel>("AdminLogIn", param, null);
                if (Employeedata != null && Employeedata.ToList().Count() > 0)
                {
                    if(Employeedata.Where(x => x.EmployeeUserName == objemployeemodel.EmployeeUserName && x.EmployeePassword == objemployeemodel.EmployeePassword).Count()>0)
                    {
                        EmployeeModel objemp = new EmployeeModel();
                        objemp = Employeedata.Where(x => x.EmployeeUserName == objemployeemodel.EmployeeUserName && x.EmployeePassword == objemployeemodel.EmployeePassword).FirstOrDefault();
                        if (objemp.DesignationId == 1)
                        {
                            Session["Employee"] = objemp;
                            //Response.Redirect("~/Admin/Admin.aspx");
                            TempData["ErrorMessage"] = null;
                            return RedirectToAction("Index","Dashboard");

                        }
                        else if (objemp.DesignationId == 2)
                        {
                            Session["Employee"] = objemp;
                            //Response.Redirect("~/Manager/Manager.aspx");
                            TempData["ErrorMessage"] = null;
                            return RedirectToAction("Index", "Dashboard");
                        }
                        else if (objemp.DesignationId == 3)
                        {
                            Session["Employee"] = objemp;
                            //Response.Redirect("./Chef.aspx");
                            TempData["ErrorMessage"] = null;
                            return RedirectToAction("Index", "Dashboard");
                        }
                        else
                        {
                            TempData["ErrorMessage"] = "Error Encountered.";
                            return View("Index");
                        }
                    }
                    else
                    {
                        TempData["ErrorMessage"] = "Invalid Username or Password.";
                        return View("Index");
                        
                    }
                   
                }
                else
                {
                    //TempData["ErrorMessage"] = "Not a single user is registered.";
                    //return View("Index");
                    DynamicParameters param1 = new DynamicParameters();
                    IEnumerable<TableInformationModel> tabledata = SqlHelper.QuerySP<TableInformationModel>("usp_GetTableInfo", param1, null);
                    if (tabledata.Count() > 0)
                    {
                        if (tabledata.ToList().Where(x => x.TableName == objemployeemodel.EmployeeUserName && x.TablePassword == objemployeemodel.EmployeePassword).Count() > 0)
                        {
                            Session["TableInfo"] = tabledata.ToList().Where(x => x.TableName == objemployeemodel.EmployeeUserName && x.TablePassword == objemployeemodel.EmployeePassword).FirstOrDefault();
                            return RedirectToAction("Index", "Home");
                        }
                        else
                        {
                            TempData["ErrorMessage"] = "Not a single user is registered.";
                            return View("Index");
                        }
                    }
                    else
                    {
                        TempData["ErrorMessage"] = "Not a single user is registered.";
                        return View("Index");
                    }
                }
               
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Account/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Account/Edit/5

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
        // GET: /Account/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Account/Delete/5

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

        public ActionResult Logout()
        {
            Session.Abandon();
            return View("Index");
        }

        public ActionResult TableInfo()
        {
            return View();
        }

        public ActionResult TotalOrders()
        {
            return View();
        }

        public ActionResult GenerateBills()
        {
            return View();
        }

        public ActionResult MonthlyReport()
        {
            return View();
        }



    }
}
