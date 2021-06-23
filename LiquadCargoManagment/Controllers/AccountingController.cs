using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using LiquadCargoManagment.Models;
using LiquadCargoManagment.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Core.EntityClient;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using static LiquadCargoManagment.Helpers.ApplicationHelper;

namespace LiquadCargoManagment.Controllers
{
    [Authorize]
    public class AccountingController : Controller
    {
        private AccountsRepository repo = new AccountsRepository();
        private LCMEntities context = new LCMEntities();
        // GET: Accounts
        public ActionResult AddVoucher()
        {
            ViewBag.parentTable = "accounts";

            ViewBag.documentNo = context.accounts.ToList().LastOrDefault() == null ? "1" : 
                (context.accounts.ToList().LastOrDefault().id + 1).ToString();

            ViewBag.transTypes = new SelectList(context.M_FLAG.Where(x => x.flagType.ToLower() == "voucher").OrderBy(x => x.id).ToList(), "id", "flagDesc");

            ViewBag.customers = context.customers
                .Where(x => x.isActive == true)
                .ToDictionary(x => x.customers_id.ToString(), y => y.customers_firstname.ToString());


            ViewBag.gm_acc = context.GM_ACC
                .Where(x => x.isActive == 1 && x.LEVEL == 4)
                .ToDictionary(x => x.id.ToString(), y => y.title.ToString());

            ViewBag.invoices = context.orders
                .ToDictionary(x => x.orders_id.ToString(), y => y.orders_id.ToString() + " - " + y.customers_name.ToString() + " " + y.date_purchased.ToString());

            var accounts = context.accounts.ToList();
            var accounts_transactions = context.accounts_transaction.ToList();
            var view = new AccountsViewModel()
            {
                accounts = accounts,
                accounts_transactions = accounts_transactions
            };

            return View(view);
        }
        public ActionResult BankPaymentVoucher()
        {
            ViewBag.parentTable = "accounts";
            ViewBag.documentNo = context.accounts.ToList().LastOrDefault() == null ? "1" :
                (context.accounts.ToList().LastOrDefault().id + 1).ToString();

            ViewBag.customers = context.customers
                .Where(x => x.isActive == true)
                .ToDictionary(x => x.customers_id.ToString(), y => y.customers_firstname.ToString());

            ViewBag.gm_acc = context.GM_ACC
                .Where(x => x.isActive == 1 && x.LEVEL == 4)
                .ToDictionary(x => x.id.ToString(), y => y.title.ToString());

            ViewBag.invoices = context.orders
                .ToDictionary(x => x.orders_id.ToString(), y => y.orders_id.ToString() + " - " + y.customers_name.ToString() + " " + y.date_purchased.ToString());

            var accounts = context.accounts.Where(x=> x.TranstypeId == 4).ToList();
            var accounts_transactions = context.accounts_transaction.ToList();
            var view = new AccountsViewModel()
            {
                accounts = accounts,
                accounts_transactions = accounts_transactions
            };

            return View(view);
        }
        public ActionResult InvByAccountId(int accountId)
        {
            ViewBag.parentTable = "accounts";
            return View(context.GetCustomersByAcc(accountId).ToList());
        }
        public ActionResult CUST_LEDGER()
        {

            ViewBag.custtypes = new SelectList(context.M_FLAG.Where(x => x.flagType.ToLower() == "custtype").OrderBy(x => x.id).ToList(), "id", "flag");
            ViewBag.customers = context.customers
                .Where(x => x.isActive == true)
                .ToDictionary(x => x.customers_id.ToString(), y => y.customers_firstname.ToString());
            return View();
        }
        public ActionResult AddInvoice()
        {
            ViewBag.parentTable = "orders";
            ViewBag.childTable = "orders_products";

            ViewBag.documentNo = context.orders.ToList().LastOrDefault() == null ? "1" :
                (context.orders.ToList().LastOrDefault().orders_id + 1).ToString();

            ViewBag.transTypes = new SelectList(context.M_FLAG.Where(x => x.flagType.ToLower() == "invoice").OrderBy(x => x.id).ToList(), "id", "flagDesc");

            ViewBag.customers = context.customers
                .Where(x => x.isActive == true)
                .ToDictionary(x => x.customers_id.ToString(), y => y.customers_firstname.ToString());

            ViewBag.products = context.products
                            .Where(x => x.isActive == true)
                            .ToDictionary(x => x.products_id.ToString(), y => y.products_name.ToString());

            ViewBag.gm_acc = context.GM_ACC
                .Where(x => x.isActive == 1)
                .ToDictionary(x => x.id.ToString(), y => y.title.ToString());

            ViewBag.invoices = context.orders
                .ToDictionary(x => x.orders_id.ToString(), y => y.orders_id.ToString() + " - " + y.customers_name.ToString() + " " + y.date_purchased.ToString());

            var view = new OrdersViewModel()
            {
                orders = context.orders.ToList(),
                orders_products = context.orders_products.ToList()
            };

            return View(view);
        }
        public ActionResult ChartOfAccount()
        {
            ViewBag.parentTable = "gm_acc";

            ViewBag.documentNo = context.GM_ACC.ToList().LastOrDefault() == null ? "1" :
                (context.GM_ACC.ToList().LastOrDefault().id + 1).ToString();
            
            ViewBag.parent_acc = new SelectList(context.GM_ACC.Where(x => x.isActive == 1).OrderBy(x => x.id).ToList(), "id", "title");
            
            ViewBag.first_level = context.GM_ACC
            .Where(x => x.LEVEL == 1)
            .AsEnumerable()
            .Select(x => new
            GM_ACCwBal()
            {
                id = x.id,
                isActive = x.isActive,
                parent_id = x.parent_id,
                title = x.title,
                modifiedBy = x.modifiedBy,
                modified = x.modified,
                LEVEL = x.LEVEL,
                description = x.description,
                CODE = x.CODE,
                createdBy = x.createdBy,
                created = x.created,
                balance = ((decimal?)GetAccBalanceByLevel(x.id, (int)x.LEVEL))
            }).OrderBy(x => x.id).ToList();
            
            ViewBag.second_level = context.GM_ACC
            .Where(x => x.LEVEL == 2)
            .AsEnumerable()
            .Select(x => new
            GM_ACCwBal()
            {
                id = x.id,
                isActive = x.isActive,
                parent_id = x.parent_id,
                title = x.title,
                modifiedBy = x.modifiedBy,
                modified = x.modified,
                LEVEL = x.LEVEL,
                description = x.description,
                CODE = x.CODE,
                createdBy = x.createdBy,
                created = x.created,
                balance = GetAccBalanceByLevel(x.id, (int)x.LEVEL)
            }).OrderBy(x => x.id).ToList();
            
            ViewBag.third_level = context.GM_ACC
            .Where(x => x.LEVEL == 3)
            .AsEnumerable()
            .Select(x => new
            GM_ACCwBal()
            {
                id = x.id,
                isActive = x.isActive,
                parent_id = x.parent_id,
                title = x.title,
                modifiedBy = x.modifiedBy,
                modified = x.modified,
                LEVEL = x.LEVEL,
                description = x.description,
                CODE = x.CODE,
                createdBy = x.createdBy,
                created = x.created,
                balance = GetAccBalanceByLevel(x.id, (int)x.LEVEL)
            }).OrderBy(x => x.id).ToList();
            
            ViewBag.fourth_level = context.GM_ACC
            .Where(x => x.LEVEL == 4) 
            .Select(x => new
            GM_ACCwBal()
            {
                id = x.id,
                isActive = x.isActive,
                parent_id = x.parent_id,
                title = x.title,
                modifiedBy = x.modifiedBy,
                modified = x.modified,
                LEVEL = x.LEVEL,
                description = x.description,
                CODE = x.CODE,
                createdBy = x.createdBy,
                created = x.created,
                balance = context.GetCustomersByAcc(x.id).Select(i => i.DueAmount).Sum()
            }).OrderBy(x => x.id).ToList();
            
            return View(context.GM_ACC.OrderByDescending(x => x.id).ToList());
        }

        public ActionResult Area()
        {
            ViewBag.title = "Area";
            ViewBag.parentTable = "m_flag";
            ViewBag.type = "AREA";

            ViewBag.documentNo = context.M_FLAG.ToList().LastOrDefault() == null ? "1" :
                (context.M_FLAG.ToList().LastOrDefault().id + 1).ToString();

            return View("M_Flag", context.M_FLAG.Where(x => x.flagType.ToLower() == "area").OrderByDescending(x => x.id).ToList());
        }

        public ActionResult City()
        {
            ViewBag.title = "City";
            ViewBag.parentTable = "m_flag";
            ViewBag.type = "CITY";

            ViewBag.documentNo = context.M_FLAG.ToList().LastOrDefault() == null ? "1" :
                (context.M_FLAG.ToList().LastOrDefault().id + 1).ToString();

            return View("M_Flag", context.M_FLAG.Where(x => x.flagType.ToLower() == "city").OrderByDescending(x => x.id).ToList());
        }

        public ActionResult Dept()
        {
            ViewBag.title = "Department";
            ViewBag.parentTable = "m_flag";
            ViewBag.type = "DEPT";

            ViewBag.documentNo = context.M_FLAG.ToList().LastOrDefault() == null ? "1" :
                (context.M_FLAG.ToList().LastOrDefault().id + 1).ToString();

            return View("M_Flag",context.M_FLAG.Where(x => x.flagType.ToLower() == "dept").OrderByDescending(x => x.id).ToList());
        }

        public ActionResult Group()
        {
            ViewBag.title = "Group";
            ViewBag.parentTable = "m_flag";
            ViewBag.type = "GROUP";

            ViewBag.documentNo = context.M_FLAG.ToList().LastOrDefault() == null ? "1" :
                (context.M_FLAG.ToList().LastOrDefault().id + 1).ToString();

            return View("M_Flag", context.M_FLAG.Where(x => x.flagType.ToLower() == "group").OrderByDescending(x => x.id).ToList());
        }

        public ActionResult Customer()
        {
            ViewBag.parentTable = "customers";

            ViewBag.custtypes = new SelectList(context.M_FLAG.Where(x => x.flagType.ToLower() == "custtype").OrderBy(x => x.id).ToList(), "id", "flag");

            ViewBag.areas = new SelectList(context.M_FLAG.Where(x => x.flagType.ToLower() == "area").OrderBy(x => x.id).ToList(), "id", "flag");
            ViewBag.city = new SelectList(context.M_FLAG.Where(x => x.flagType.ToLower() == "city").OrderBy(x => x.id).ToList(), "id", "flag");
            ViewBag.group = new SelectList(context.M_FLAG.Where(x => x.flagType.ToLower() == "group").OrderBy(x => x.id).ToList(), "id", "flag");
            ViewBag.dept = new SelectList(context.M_FLAG.Where(x => x.flagType.ToLower() == "dept").OrderBy(x => x.id).ToList(), "id", "flag");

            ViewBag.documentNo = context.customers.ToList().LastOrDefault() == null ? "1" :
                (context.customers.ToList().LastOrDefault().customers_id + 1).ToString();

            return View(context.customers.OrderByDescending(x => x.customers_id).ToList());
        }
        public ActionResult Product()
        {
            ViewBag.parentTable = "products";

            ViewBag.documentNo = context.products.ToList().LastOrDefault() == null ? "1" :
                (context.products.ToList().LastOrDefault().products_id + 1).ToString();

            return View(context.products.OrderByDescending(x => x.products_id).ToList());
        }
        [HttpPost]
        public JsonResult CreateVoucher(account accounts,List<accounts_transaction> accounts_transactions)
        {
            AjaxResponse response = new AjaxResponse();
            response.Type = "Response Message";
            response.FieldName = "Accounts Voucher";
            try
            {
                repo.SaveAccounts(accounts);
                repo.SaveAccounts_Transaction(accounts.id,accounts_transactions);
                response.Message = "New Record Saved Successfully!";
                return Json(response, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                return Json(response, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public JsonResult DeleteVoucher(int id)
        {
            AjaxResponse response = new AjaxResponse();
            response.Type = "Response Message";
            response.FieldName = "Accounts Voucher";
            try
            {
                repo.DeleteVoucher(id);
                response.Message = "Record has been deleted successfully!";
                return Json(response, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                return Json(response, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public JsonResult CreateInvoice(order orders,List<orders_products> orders_products)
        {
            AjaxResponse response = new AjaxResponse();
            response.Type = "Response Message";
            response.FieldName = "Accounts Voucher";
            try
            {
                repo.SaveOrder(orders);
                repo.SaveOrders_Products(orders.orders_id,orders_products);
                response.Message = "New Record Saved Successfully!";
                return Json(response, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                return Json(response, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public JsonResult DeleteInvoice(int id)
        {
            AjaxResponse response = new AjaxResponse();
            response.Type = "Response Message";
            response.FieldName = "Accounts Voucher";
            try
            {
                repo.DeleteInvoice(id);
                response.Message = "Record has been deleted successfully!";
                return Json(response, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                return Json(response, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public JsonResult GetProductDetails(int id)
        {
            var response = context.products.Where(x => x.products_id == id).FirstOrDefault();
            return Json(response, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult GetCustomerDetails(int id)
        {
            var response = context.customers.Where(x => x.customers_id == id).FirstOrDefault();
            return Json(response, JsonRequestBehavior.AllowGet);
        }
        
        [HttpPost]
        public JsonResult CreateChartOfAccount(GM_ACC gm_acc)
        {
            AjaxResponse response = new AjaxResponse();
            response.Type = "Response Message";
            response.FieldName = "Chart Of Account";
            try
            {
                repo.SaveGM_ACC(gm_acc);
                response.Message = "New Record Saved Successfully!";
                return Json(response, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                return Json(response, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult CreateM_FLAG(M_FLAG m_flag)
        {
            AjaxResponse response = new AjaxResponse();
            response.Type = "Response Message";
            response.FieldName = "Chart Of Account";
            try
            {
                repo.SaveM_FLAG(m_flag);
                response.Message = "New Record Saved Successfully!";
                return Json(response, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                return Json(response, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult DeleteChartOfAccount(int id)
        {
            AjaxResponse response = new AjaxResponse();
            response.Type = "Response Message";
            response.FieldName = "Accounts Voucher";
            try
            {
                repo.DeleteGM_ACC(id);
                response.Message = "Record has been deleted successfully!";
                return Json(response, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                return Json(response, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult DeleteM_FLAG(int id)
        {
            AjaxResponse response = new AjaxResponse();
            response.Type = "Response Message";
            response.FieldName = "Accounts Voucher";
            try
            {
                repo.DeleteM_FLAG(id);
                response.Message = "Record has been deleted successfully!";
                return Json(response, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                return Json(response, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult CreateCustomer(customer customers)
        {
            AjaxResponse response = new AjaxResponse();
            response.Type = "Response Message";
            response.FieldName = "Chart Of Account";
            try
            {
                repo.SaveCustomer(customers);
                response.Message = "New Record Saved Successfully!";
                return Json(response, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                return Json(response, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult DeleteCustomer(int id)
        {
            AjaxResponse response = new AjaxResponse();
            response.Type = "Response Message";
            response.FieldName = "Accounts Voucher";
            try
            {
                repo.DeleteCustomer(id);
                response.Message = "Record has been deleted successfully!";
                return Json(response, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                return Json(response, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult CreateProduct(product products)
        {
            AjaxResponse response = new AjaxResponse();
            response.Type = "Response Message";
            response.FieldName = "Chart Of Account";
            try
            {
                repo.SaveProduct(products);
                response.Message = "New Record Saved Successfully!";
                return Json(response, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                return Json(response, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult DeleteProduct(int id)
        {
            AjaxResponse response = new AjaxResponse();
            response.Type = "Response Message";
            response.FieldName = "Accounts Voucher";
            try
            {
                repo.DeleteProduct(id);
                response.Message = "Record has been deleted successfully!";
                return Json(response, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                return Json(response, JsonRequestBehavior.AllowGet);
            }
        }
        
        public HttpResponseBase Cust_Ledger_Report(Nullable<int> customerId, DateTime frmDt, DateTime toDt, int typeId, string dst)
        {
            customerId = customerId == null ? 0 : customerId;
            string sql = $"SELECT '{dst}' AS dst,* FROM CUST_LEDGER({customerId}, '{frmDt.ToString("yyyy-MM-dd")}', '{toDt.ToString("yyyy-MM-dd")}', {typeId})";
            return Report(
                sql,
                "CUST_LEDGER",
                "CustomerLedger"
            );
        }

        public HttpResponseBase Report(string sql, string reportName, string pdfName)
        {
            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/Reports"), reportName+".rpt"));
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(new LCMEntities().Database.Connection.ConnectionString);
            SqlConnection conn = new SqlConnection(new LCMEntities().Database.Connection.ConnectionString);
            conn.Open();
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = sql;
            cmd.CommandTimeout = 0;
            DataTable dataSource = new DataTable();
            dataSource.Load(cmd.ExecuteReader());
            rd.SetDataSource(dataSource);
            Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            var pdfByteArray = new byte[stream.Length - 1];
            stream.Position = 0;
            stream.Read(pdfByteArray, 0, Convert.ToInt32(stream.Length - 1));
            Response.ClearContent();
            Response.ClearHeaders();
            Response.AddHeader("content-disposition", "filename="+pdfName + ".pdf");
            Response.ContentType = "application/pdf";
            Response.AddHeader("content-length", pdfByteArray.Length.ToString());
            Response.BinaryWrite(pdfByteArray);
            return Response;
        }
        private decimal GetAccBalanceByLevel(int id, int level)
        {
            decimal? balance = 0.00M;
            if (level == 4)
            {
                balance = (decimal)context.GetCustomersByAcc(id).Select(i => i.DueAmount).Sum();
            }
            else if(level == 3)
            {
                var acc3 = context.GM_ACC.Where(x => x.parent_id == id).ToList();
                foreach (var item in acc3)
                {
                    var val = context
                            .GetCustomersByAcc(item.id)
                            .Select(i =>
                            i.DueAmount).Sum();
                    if (val != null) balance += val;
                }
            }
            else if (level == 2)
            {
                var acc2 = context.GM_ACC.Where(x => x.parent_id == id).ToList();
                foreach (var item in acc2)
                {
                    var acc3 = context.GM_ACC.Where(x => x.parent_id == item.id).ToList();
                    foreach (var subitem in acc3)
                    {
                        var val = context
                            .GetCustomersByAcc(subitem.id)
                            .Select(i =>
                            i.DueAmount).Sum();
                        if (val != null) balance += val;
                    }
                }
            }
            else if (level == 1)
            {
                var acc1 = context.GM_ACC.Where(x => x.parent_id == id).ToList();
                foreach (var item in acc1)
                {
                    var acc2 = context.GM_ACC.Where(x => x.parent_id == item.id).ToList();
                    foreach (var subitem in acc2)
                    {
                        var acc3 = context.GM_ACC.Where(x => x.parent_id == subitem.id).ToList();
                        foreach (var subitem_item in acc3)
                        {
                            var val = context
                                .GetCustomersByAcc(subitem_item.id)
                                .Select(i => 
                                i.DueAmount).Sum();
                            if (val != null) balance += val;
                        }
                    }
                }
            }
            return (decimal)balance;
        }

        public ActionResult Bank()
        {
            ViewBag.fourth_level = context.GM_ACC
.Where(x => x.LEVEL == 4)
.Select(x => new
GM_ACCwBal()
{
    id = x.id,
    isActive = x.isActive,
    parent_id = x.parent_id,
    title = x.title,
    modifiedBy = x.modifiedBy,
    modified = x.modified,
    LEVEL = x.LEVEL,
    description = x.description,
    CODE = x.CODE,
    createdBy = x.createdBy,
    created = x.created,
    balance = context.GetCustomersByAcc(x.id).Select(i => i.DueAmount).Sum()
}).OrderBy(x => x.id).ToList();
            return View(context.GM_ACC.Where(x => x.title.Contains("Bank")).ToList());
        }
        public ActionResult Cash()
        {
            var list = context.GM_ACC.Where(x => x.title.Contains("Cash")).ToList();
            return View(list);
        }
    }
}