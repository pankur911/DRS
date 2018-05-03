using CapProj.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CapProj.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            ItemController ic = new ItemController();
            MenuModel objmenumodel = new MenuModel();
            var resultdata=ic.GetMenu();
            if (resultdata.Item1.Count()>0)
            {
                objmenumodel.CuisineModelList = resultdata.Item1.ToList();
            }
            if (resultdata.Item2.Count() > 0)
            {
                objmenumodel.ItemModelList = resultdata.Item2.ToList();
            }
            return View(objmenumodel);
        }
        public ActionResult Cart()
        {
            List<OrderDetailsModel> objitemlist=new List<OrderDetailsModel>();
            if (Session["CartData"] != null)
            {
                objitemlist = (List<OrderDetailsModel>)Session["CartData"];
            }
            return View(objitemlist);
        }
        [HttpPost]
        public ActionResult AddItemToCart(int itemId,int Quantity )
        {
            List<OrderDetailsModel> objitemlist ;
            TableInformationModel omodel=new TableInformationModel();
            ItemController ic = new ItemController();
            ItemModel objItemModel = new ItemModel();
            var itemdata=ic.GetItems(itemId);
            if(itemdata.Count()>0){
                objItemModel = itemdata.ToList().FirstOrDefault();
            }
            int intAmount = 0;
            intAmount = Quantity * objItemModel.ItemRate;
            omodel=(TableInformationModel)Session["TableInfo"];
            if(Session["CartData"]!=null)
            {
                objitemlist = (List<OrderDetailsModel>)Session["CartData"];
                if (objitemlist.Where(x => x.ItemId == itemId).Count()>0)
                {
                    var objitemalreadyexists = objitemlist.Where(x => x.ItemId == itemId).FirstOrDefault();
                    objitemalreadyexists.Quantity =objitemalreadyexists.Quantity+ Quantity;
                    objitemalreadyexists.Amount = objitemalreadyexists.Quantity * objItemModel.ItemRate;
                    //objitemlist.Add(new OrderDetailsModel { ItemId = itemId, Quantity = Quantity, ItemRate = objItemModel.ItemRate, TableNumber = omodel.TableNumber, Amount = intAmount, ItemModel = objItemModel });
                }
                else
                {
                    objitemlist.Add(new OrderDetailsModel { ItemId = itemId, Quantity = Quantity, ItemRate = objItemModel.ItemRate, TableNumber = omodel.TableNumber, Amount = intAmount, ItemModel = objItemModel });
                }
                
            }
            else
            {
                objitemlist = new List<OrderDetailsModel>();
                objitemlist.Add(new OrderDetailsModel { ItemId = itemId, Quantity = Quantity, ItemRate = objItemModel.ItemRate, TableNumber = omodel.TableNumber, Amount = intAmount, ItemModel = objItemModel });
            }
            Session["CartData"] = objitemlist;
            return Json("Item Added to cart", JsonRequestBehavior.DenyGet);
        }
        [HttpPost]
        public JsonResult GetCartCount()
        {
            int count = 0;
            List<OrderDetailsModel> objitemlist;
            if (Session["CartData"] != null)
            {
                objitemlist = (List<OrderDetailsModel>)Session["CartData"];
                count = objitemlist.Count();
            }
            return Json(count, JsonRequestBehavior.DenyGet);
        }

        [HttpGet]
        public ActionResult RemoveItem(int ItemId)
        {
            List<OrderDetailsModel> objitemlist;
            if (Session["CartData"] != null)
            {
                objitemlist = (List<OrderDetailsModel>)Session["CartData"];
                var objitem=objitemlist.Where(x=>x.ItemId==ItemId).FirstOrDefault();
                objitemlist.Remove(objitem);
                Session["CartData"] = objitemlist;
            }
            return RedirectToAction("Cart");
        }
        [HttpGet]
        public ActionResult PlaceOrder()
        {
            List<OrderDetailsModel> objitemlist;
            TableInformationModel omodel = new TableInformationModel();
            ItemController ic = new ItemController();
            int Ordermainid = 0;
            if (Session["CartData"] != null)
            {
                objitemlist = (List<OrderDetailsModel>)Session["CartData"];
                omodel = (TableInformationModel)Session["TableInfo"];
               var data=ic.AddItemMain(omodel.TableNumber,objitemlist.Sum(x=>x.Amount));
                if(data.Count()>0)
                {
                    Ordermainid = data.ToList().FirstOrDefault().OrderMainId;

                    foreach(var item in objitemlist)
                    {
                        item.OrderMainId = Ordermainid;
                        ic.AddItemDetails(item);
                    }
                }
            }
            return RedirectToAction("OrderPlaced",new {OrderId=Ordermainid});
        }
        public ActionResult OrderPlaced(int OrderId)
        {
            return View();
        }
    }
}
