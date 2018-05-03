using CapProj.Models;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CapProj.Controllers
{
    public class CuisineController : Controller
    {
        //
        // GET: /Cuisine/

        public ActionResult Index()
        {
            List<CuisineModel> objmodel = new List<CuisineModel>();
            ItemController ic = new ItemController();
            objmodel=ic.GetCuisines(0).ToList();
            return View(objmodel);
        }

        //
        // GET: /Cuisine/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Cuisine/Create

        public ActionResult Create(int id)
        {
            CuisineModel omodel = new CuisineModel();
           
            if (id > 0)
            {
                ItemController ic = new ItemController();
                var itemdata = ic.GetCuisines(id).ToList();
                if (itemdata.Count() > 0)
                {
                    omodel.CuisineId = itemdata.ToList().Where(x => x.CuisineId == id).FirstOrDefault().CuisineId;
                    omodel.CuisineName = itemdata.ToList().Where(x => x.CuisineId == id).FirstOrDefault().CuisineName;
                    omodel.CuisineImage = itemdata.ToList().Where(x => x.CuisineId == id).FirstOrDefault().CuisineImage;
                    omodel.Status = itemdata.ToList().Where(x => x.CuisineId == id).FirstOrDefault().Status;
                }
            }
            return View(omodel);
        }

        //
        // POST: /Cuisine/Create

        [HttpPost]
        public ActionResult Create(CuisineModel objmodel)
        {
            try
            {
                string FileName = string.Empty;
                if(Request.Files.Count>0)
                {
                    FileName = Convert.ToString(Guid.NewGuid()) + Request.Files["CuisineImage"].FileName;
                    string filepath = "/Images/Cuisine/" + FileName;
                    
                    Request.Files["CuisineImage"].SaveAs(Server.MapPath(filepath));
                }
                DynamicParameters param=new DynamicParameters();
                if (objmodel.CuisineId > 0)
                {
                    param.Add("@CrusineId", objmodel.CuisineId);
                }
                param.Add("@CrusineName", objmodel.CuisineName);
                param.Add("@CrusineImage", FileName);
                SqlHelper.ExecuteSP("C1", param, null);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Cuisine/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Cuisine/Edit/5

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
        // GET: /Cuisine/Delete/5

        public ActionResult Delete(int id)
        {
            DynamicParameters param = new DynamicParameters();
            if (id > 0)
            {
                param.Add("@CuisineId", id);
                param.Add("@Status", 0);
            }
            SqlHelper.ExecuteSP("DeleteCuisine", param, null);
            return RedirectToAction("Index");
        }

        //
        // POST: /Cuisine/Delete/5

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
