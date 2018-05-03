using CapProj.Models;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CapProj.Controllers
{
    public class ItemController : Controller
    {
        //
        // GET: /Item/

        public ActionResult Index()
        {
            List<ItemModel> objmodel = new List<ItemModel>();
            objmodel = GetItems(0).ToList();
            return View(objmodel);
        }

        //
        // GET: /Item/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Item/Create

        public ActionResult Create(int itemid)
        {
            ItemModel omodel = new ItemModel();
            IList<SelectListItem> Cusineslist=new List<SelectListItem>();
            Cusineslist.Add(new SelectListItem { Text = "Select", Value = "" });
            if (itemid > 0)
            {
                var itemdata = GetItems(itemid).ToList();
                if (itemdata.Count() > 0)
                {
                    omodel.CuisineId = itemdata.ToList().Where(x => x.ItemId == itemid).FirstOrDefault().CuisineId;
                    omodel.Ingredients = itemdata.ToList().Where(x => x.ItemId == itemid).FirstOrDefault().Ingredients;
                    omodel.ItemDetails = itemdata.ToList().Where(x => x.ItemId == itemid).FirstOrDefault().ItemDetails;
                    omodel.ItemId = itemdata.ToList().Where(x => x.ItemId == itemid).FirstOrDefault().ItemId;
                    omodel.ItemImage = itemdata.ToList().Where(x => x.ItemId == itemid).FirstOrDefault().ItemImage;
                    omodel.ItemName = itemdata.ToList().Where(x => x.ItemId == itemid).FirstOrDefault().ItemName;
                    omodel.ItemRate = itemdata.ToList().Where(x => x.ItemId == itemid).FirstOrDefault().ItemRate;
                }
            }
            foreach (var item in GetCuisines(0).ToList())
            {
                Cusineslist.Add(new SelectListItem { Text = item.CuisineName, Value = Convert.ToString(item.CuisineId) });
            }
            omodel.CuisineList=Cusineslist;

            return View(omodel);
        }

        //
        // POST: /Item/Create

        [HttpPost]
        public ActionResult Create(ItemModel objmodel)
        {
            try
            {
                string FileName = string.Empty;
                if (Request.Files.Count > 0)
                {
                    FileName = Convert.ToString(Guid.NewGuid()) + Request.Files["ItemImage"].FileName;
                    string filepath = "/Images/Item/" + FileName;

                    Request.Files["ItemImage"].SaveAs(Server.MapPath(filepath));
                }
                DynamicParameters param = new DynamicParameters();
                if(objmodel.ItemId>0)
                {
                    param.Add("@ItemId", objmodel.ItemId);
                }
                param.Add("@ItemRate", objmodel.ItemRate);
                param.Add("@CuisineId", objmodel.CuisineId);
                param.Add("@ItemImage", FileName);
                param.Add("@ItemDetails", objmodel.ItemDetails);
                param.Add("@Ingredients", objmodel.Ingredients);
                param.Add("@ItemName", objmodel.ItemName);
                SqlHelper.ExecuteSP("AddIteam", param, null);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Item/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Item/Edit/5

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
        // GET: /Item/Delete/5

        public ActionResult Delete(int itemid)
        {
            DynamicParameters param = new DynamicParameters();
            if (itemid > 0)
            {
                param.Add("@ItemId", itemid);
            }
            SqlHelper.ExecuteSP("DeleteItem", param, null);
            return RedirectToAction("Index");
        }

        //
        // POST: /Item/Delete/5

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

        public IEnumerable<ItemModel> GetItems(int itemId)
        {
            try
            {
                DynamicParameters param = new DynamicParameters();
                if (itemId>0)
                    param.Add("@ItemId", itemId);
                return SqlHelper.QuerySP<ItemModel>("usp_GetItems", param, null);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public IEnumerable<CuisineModel> GetCuisines(int CuisineId)
        {
            try
            {
                
                DynamicParameters param = new DynamicParameters();
                if (CuisineId > 0)
                    param.Add("@CuisineId", CuisineId);
                return SqlHelper.QuerySP<CuisineModel>("usp_GetCuisines", param, null);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Tuple<IEnumerable<CuisineModel>,IEnumerable<ItemModel>> GetMenu()
        {
            try
            {
                DynamicParameters param = new DynamicParameters();
                return SqlHelper.QueryMultipleSP<CuisineModel,ItemModel>("usp_GetMenu", param, null);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<OrderMainModel> AddItemMain(int tablenumber,int totalamount)
        {
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@TableNumber",tablenumber);
                 param.Add("@TotalAmount",totalamount);
                DynamicParameters outparam = new DynamicParameters();
                outparam.Add("@OrderMainId");
                return SqlHelper.QuerySP<OrderMainModel>("usp_PlaceOrder", param, outparam);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int AddItemDetails(OrderDetailsModel obj)
        {
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@TableNumber", obj.TableNumber);
                param.Add("@ItemId", obj.ItemId);
                param.Add("@Quantity", obj.Quantity);
                param.Add("@ItemRate", obj.ItemRate);
                param.Add("@Amount", obj.Amount);
                param.Add("@Remarks", obj.Remarks);
                param.Add("@Status", obj.Status);
                param.Add("@ChefStatus", obj.ChefStatus);
                param.Add("@OrderMainId", obj.OrderMainId);
                SqlHelper.QuerySP<OrderMainModel>("usp_PlaceOrderDetails", param, null);
                return 1;
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
