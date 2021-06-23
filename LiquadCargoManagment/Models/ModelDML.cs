using LiquadCargoManagment.Controllers;
using LiquadCargoManagment.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static LiquadCargoManagment.Helpers.ApplicationHelper;
namespace LiquadCargoManagment.Models
{
    public class ModelDML 
    {       
        private readonly LCMEntities context;
        public ModelDML(LCMEntities _context)
        {
            context = _context;
        }
        
        public List<VendorType> getVendorType()
        {
            return context.VendorTypes.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyId)).ToList();
        }
        public VendorType getVendorType(long Id)
        {
            return context.VendorTypes
                .Where(x => x.ID == Id && lstAssignedCompanies.Contains(x.OwnCompanyId)).FirstOrDefault();
        }
        public List<VendorType> getVendorType(string Code, string Name)
        {
            return context.VendorTypes
                .Where(x => x.Code == Code && x.Name == Name && lstAssignedCompanies.Contains(x.OwnCompanyId)).ToList();
        }



        public List<Vendor> getVendor()
        {
            return context.Vendors.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyId)).ToList();
        }
        public Vendor getVendor(long Id)
        {
            return context.Vendors
                .Where(x => x.ID == Id && lstAssignedCompanies.Contains(x.OwnCompanyId)).FirstOrDefault();
        }
        public List<Vendor> getVendor(string Code, string Name)
        {
            return context.Vendors
                .Where(x => x.Code == Code && lstAssignedCompanies.Contains(x.OwnCompanyId)).ToList();
        }




        public List<OwnCompany> getOwnCompany()
        {
            return context.OwnCompanies.Where(x=>x.SubcriptionID == ApplicationHelper.SubcriptionID).ToList();
        }
        public OwnCompany getOwnCompany(long Id)
        {
            return context.OwnCompanies.Where(x => x.ID == Id && x.SubcriptionID == ApplicationHelper.SubcriptionID).FirstOrDefault();
        }
        public List<OwnCompany> getOwnCompany(string Code, string Name)
        {
            return context.OwnCompanies
                .Where(x => x.Code == Code && x.Name == Name && x.SubcriptionID == ApplicationHelper.SubcriptionID).ToList();
        }


        public List<CustomerGroup> getCustomerGroup()
        {
            return context.CustomerGroups.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyId)).ToList();
        }
        public CustomerGroup getCustomerGroup(long Id)
        {
            return context.CustomerGroups
                .Where(x => x.GroupID == Id && lstAssignedCompanies.Contains(x.OwnCompanyId)).FirstOrDefault();
        }
        public List<CustomerGroup> getCustomerGroup(string Code, string Name)
        {
            return context.CustomerGroups
                .Where(x => x.Code == Code && x.Name == Name && lstAssignedCompanies.Contains(x.OwnCompanyId)).ToList();
        }

        public List<CustomerCompany> getCustomerCompany()
        {
            return context.CustomerCompanies.Where(x => lstAssignedCompanies.Contains(x.OwnCompanyId)).ToList();
        }
        public CustomerCompany getCustomerCompany(long Id)
        {
            return context.CustomerCompanies
                .Where(x => x.ID == Id && lstAssignedCompanies.Contains(x.OwnCompanyId)).FirstOrDefault();
        }
        public List<CustomerCompany> getCustomerCompany(string Code, string Name)
        {
            return context.CustomerCompanies
                .Where(x => x.Code == Code && x.Name == Name && lstAssignedCompanies.Contains(x.OwnCompanyId)).ToList();
        }

    }
}