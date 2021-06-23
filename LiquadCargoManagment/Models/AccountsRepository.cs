using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace LiquadCargoManagment.Models
{
    public class AccountsRepository
    {
        private LCMEntities context;

        public AccountsRepository()
        {
            context = new LCMEntities();
        }

        public int SaveAccounts(account account)
        {
            try
            {
                var entry = context.accounts.Where(x => x.id == account.id).FirstOrDefault();
                if (entry != null)
                {
                    entry.accountId = account.accountId;
                    entry.accountTitle = account.accountTitle;
                    entry.amount = account.amount;
                    entry.modifiedBy = "admin";
                    entry.createdBy = "admin";
                    entry.docDate = account.docDate;
                    entry.narration = account.narration;
                    entry.TranstypeId = account.TranstypeId;
                    entry.vchrStatus = account.vchrStatus;
                    context.Entry(entry).State = System.Data.Entity.EntityState.Modified;
                }
                else
                {
                    account.created = DateTime.Now;
                    account.modified = DateTime.Now;
                    account.createdBy = "admin";
                    account.modifiedBy = "admin";
                    context.accounts.Add(account);
                }
                context.SaveChanges();
                return 1;
            }
             catch(Exception ex)
            {
                return 0;
            }
        }
        public int SaveAccounts_Transaction(int refId,List<accounts_transaction> accounts_transaction)
        {
            try
            {
                var entry = context.accounts_transaction
                    .Where(x => x.refId == refId)
                    .ToList();
                foreach(accounts_transaction e in entry)
                {
                    context.Entry(e).State = EntityState.Deleted;
                }
                context.SaveChanges();

                foreach (accounts_transaction transaction in accounts_transaction)
                {
                    //return 1;
                    if (transaction.customers_id == 0) continue;
                    transaction.refId = refId;
                    transaction.created = DateTime.Now;
                    transaction.modified = DateTime.Now;
                    transaction.modifiedBy = "admin";
                    transaction.createdBy = "admin";
                    context.accounts_transaction.Add(transaction);
                }

                context.SaveChanges();
                return 1;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
        public int DeleteVoucher(int id)
        {
            try
            {
                account acc = context.accounts.Where(x => x.id == id).FirstOrDefault();
                List<accounts_transaction> acc_tr = context.accounts_transaction.Where(x => x.refId == id).ToList();
                context.Entry(acc).State = EntityState.Deleted;
                foreach (accounts_transaction transaction in acc_tr)
                {
                    context.Entry(transaction).State = EntityState.Deleted;
                }
                context.SaveChanges();
                return 1;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
        public int SaveOrder(order order)
        {
            try
            {
                var entry = context.orders.Where(x => x.orders_id == order.orders_id).FirstOrDefault();
                if (entry != null)
                {
                    entry.customers_id = order.customers_id;
                    entry.customers_name = order.customers_name;
                    entry.customers_tel = order.customers_tel;
                    entry.date_purchased = order.date_purchased;
                    entry.orderStatus = order.orderStatus;
                    entry.totalItems = order.totalItems;
                    entry.total_amt = order.total_amt;
                    entry.customers_address = order.customers_address;
                    entry.customers_area = order.customers_area;
                    entry.customers_city = order.customers_city;
                    entry.customers_country = order.customers_country;
                    entry.docTypeId = order.docTypeId;
                    entry.remarks = order.remarks;
                    entry.created = DateTime.Now;
                    entry.modified = DateTime.Now;
                    entry.modifiedBy = "admin";
                    entry.createdBy = "admin";
                    context.Entry(entry).State = System.Data.Entity.EntityState.Modified;
                }
                else
                {
                    order.modifiedBy = "admin";
                    order.createdBy = "admin";
                    order.created = DateTime.Now;
                    order.modified = DateTime.Now;
                    context.orders.Add(order);
                }
                context.SaveChanges();
                return 1;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public int SaveOrders_Products(int orders_id, List<orders_products> orders_products)
        {
            try
            {
                var entry = context.orders_products
                    .Where(x => x.orders_id == orders_id)
                    .ToList();
                foreach (orders_products e in entry)
                {
                    context.Entry(e).State = EntityState.Deleted;
                }
                context.SaveChanges();

                foreach (orders_products transaction in orders_products)
                {
                    //return 1;
                    if (transaction.products_id == 0) continue;
                    transaction.orders_id = orders_id;
                    transaction.created = DateTime.Now;
                    transaction.modified = DateTime.Now;
                    transaction.modifiedBy = "admin";
                    transaction.createdBy = "admin";
                    context.orders_products.Add(transaction);
                }

                context.SaveChanges();
                return 1;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public int DeleteInvoice(int id)
        {
            try
            {
                order acc = context.orders.Where(x => x.orders_id == id).FirstOrDefault();
                List<orders_products> acc_tr = context.orders_products.Where(x => x.orders_id == id).ToList();
                context.Entry(acc).State = EntityState.Deleted;
                foreach (orders_products transaction in acc_tr)
                {
                    context.Entry(transaction).State = EntityState.Deleted;
                }
                context.SaveChanges();
                return 1;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public int SaveGM_ACC(GM_ACC gm_acc)
        {
            try
            {
                GM_ACC acc = context.GM_ACC.Where(x => x.id == gm_acc.id).FirstOrDefault();
                if(acc != null)
                {
                    acc.title = gm_acc.title;
                    acc.CODE = gm_acc.CODE;
                    acc.parent_id = gm_acc.parent_id;
                    acc.description = gm_acc.description;
                    acc.isActive = gm_acc.isActive;
                    acc.modified = DateTime.Now;
                    acc.modifiedBy = "admin";
                    context.Entry(acc).State = System.Data.Entity.EntityState.Modified;
                }
                else
                {
                    gm_acc.created = DateTime.Now;
                    gm_acc.modified = DateTime.Now;
                    gm_acc.modifiedBy = "admin";
                    gm_acc.createdBy = "admin";
                    context.GM_ACC.Add(gm_acc);
                }
                context.SaveChanges();
                return 1;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public int SaveM_FLAG(M_FLAG flag)
        {
            try
            {
                M_FLAG m_flag = context.M_FLAG.Where(x => x.id == flag.id).FirstOrDefault();
                if (m_flag != null)
                {
                    m_flag.flag = flag.flag;
                    m_flag.flagType = flag.flagType.ToUpper();
                    m_flag.flagDesc = flag.flagDesc;
                    m_flag.isActive = flag.isActive;
                    m_flag.modified = DateTime.Now;
                    m_flag.modifiedBy = "admin";
                    context.Entry(m_flag).State = System.Data.Entity.EntityState.Modified;
                }
                else
                {
                    flag.created = DateTime.Now;
                    flag.modified = DateTime.Now;
                    flag.modifiedBy = "admin";
                    flag.createdBy = "admin";
                    flag.flagType = flag.flagType.ToUpper();
                    context.M_FLAG.Add(flag);
                }
                context.SaveChanges();
                return 1;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public int DeleteGM_ACC(int id)
        {
            try
            {
                GM_ACC acc = context.GM_ACC.Where(x => x.id == id).FirstOrDefault();
                context.Entry(acc).State = EntityState.Deleted;
                context.SaveChanges();
                return 1;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public int DeleteM_FLAG(int id)
        {
            try
            {
                M_FLAG flag = context.M_FLAG.Where(x => x.id == id).FirstOrDefault();
                context.Entry(flag).State = EntityState.Deleted;
                context.SaveChanges();
                return 1;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public int SaveCustomer(customer customer)
        {
            try
            {
                customer cust = context.customers.Where(x => x.customers_id == customer.customers_id).FirstOrDefault();
                if (cust != null)
                {
                    cust.customers_firstname = customer.customers_firstname;
                    cust.customers_lastname = customer.customers_lastname;
                    cust.customers_code = customer.customers_code;
                    cust.customers_area = customer.customers_area;
                    cust.customers_city = customer.customers_city;
                    cust.customers_code = customer.customers_code;
                    cust.customers_country = customer.customers_country;
                    cust.customers_tel = customer.customers_tel;
                    cust.customers_address = customer.customers_address;
                    cust.isActive = customer.isActive;
                    cust.modified = DateTime.Now;
                    cust.modifiedBy = "admin";
                    context.Entry(cust).State = System.Data.Entity.EntityState.Modified;
                }
                else
                {
                    customer.created = DateTime.Now;
                    customer.modified = DateTime.Now;
                    customer.modifiedBy = "admin";
                    customer.createdBy = "admin";
                    context.customers.Add(customer);
                }
                context.SaveChanges();
                return 1;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public int DeleteCustomer(int id)
        {
            try
            {
                customer cust = context.customers.Where(x => x.customers_id == id).FirstOrDefault();
                context.Entry(cust).State = EntityState.Deleted;
                context.SaveChanges();
                return 1;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public int SaveProduct(product product)
        {
            try
            {
                product prd = context.products.Where(x => x.products_id == product.products_id).FirstOrDefault();
                if (prd != null)
                {
                    prd.productDescription = product.productDescription;
                    prd.products_name = product.products_name;
                    prd.products_tp = product.products_tp;
                    prd.products_msrp = product.products_msrp;
                    prd.products_model = product.products_model;
                    prd.products_bonus = product.products_bonus;
                    prd.products_scheme = product.products_scheme;
                    prd.products_discAmt = product.products_discAmt;
                    prd.products_discPer = product.products_discPer;
                    prd.isActive = product.isActive;
                    prd.modified = DateTime.Now;
                    prd.modifiedBy = "admin";
                    prd.created = DateTime.Now;
                    prd.createdBy = "admin";
                    context.Entry(prd).State = System.Data.Entity.EntityState.Modified;
                }
                else
                {
                    product.created = DateTime.Now;
                    product.modified = DateTime.Now;
                    product.modifiedBy = "admin";
                    product.createdBy = "admin";
                    context.products.Add(product);
                }
                context.SaveChanges();
                return 1;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public int DeleteProduct(int id)
        {
            try
            {
                product prd = context.products.Where(x => x.products_id == id).FirstOrDefault();
                context.Entry(prd).State = EntityState.Deleted;
                context.SaveChanges();
                return 1;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
    }
}