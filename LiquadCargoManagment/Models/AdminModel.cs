using Bilty.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LiquadCargoManagment.Models
{
    public class AdminModel
    {
        public List<Group> lstGroup;
        public Group _group;
        public List<usp_OwnGroups> lstOwnGroup;
        public usp_OwnGroups _ownGroup;
        //public List<GetCompanyList_Result> lstCompany;
        public usp_OwnCompany _ownCompany;
        public List<usp_OwnCompany> lstOwnCompany;
        public List<usp_Company> CompanyList;
        public usp_OwnDepartment _ownDeptartment;
        public List<usp_OwnDepartment> lstOwnDepartment;
       // public List<usp_Organization_Result> lstOragnaization;
        public List<Menu> lstMenu;

        //public List<usp_NavMenu_Result> lstNavMenuResult;
        public usp_UserAccounts userAccount;
        public List<usp_UserAccounts> lstUserAccount;
        public List<Designation> lstDesignation;
        public List<Role> lstRole;
        public List<Category> lstCategory;
        public List<Nature> lstNature;
        public List<Gener> lstGener;
        public List<usp_NavMenu> lstForm;
        public usp_NavMenu form;
        public List<usp_Menu> lstNavMenu;
        public List<usp_Product> lstPrduct;
        public List<usp_City> lstCity;
        public usp_CustomerProfile CustomerProfile;
        public List<usp_CustomerProfile> CustomerProfileList;
        public List<usp_CustomerProfileDetail> CustomerProfileDetailList;
        public List<usp_VehicleType> lstVehicleType;
        public List<usp_Vehicle> lstVehicle;
        public List<usp_PackageType> lstPackageType;
        public List<usp_Stations> lstStation;
        public List<usp_PickDropLocation> lstPickDrop;
        public List<usp_Groups> lstGroups;
        public List<usp_Department> lstDepart;
        public List<usp_ExpensesType> lstExpenseType;
        public List<usp_DocumentType> lstDocumentType;
        public List<usp_LocationType> lstLocationType;
        public List<usp_Brokers> lstBroker;
        public List<usp_RoleBaseFormRight> lstRoleBaseForm;
        public List<usp_MCBBranches> lstmcbBranches;
        public AdminModel()
        {
            lstmcbBranches = new List<usp_MCBBranches>();
            lstRoleBaseForm = new List<usp_RoleBaseFormRight>();
            lstDocumentType = new List<usp_DocumentType>();
            lstGroups = new List<usp_Groups>();
            lstDepart = new List<usp_Department>();
            lstLocationType = new List<usp_LocationType>();
            lstPickDrop = new List<usp_PickDropLocation>();
            lstStation = new List<usp_Stations>();
            lstPackageType = new List<usp_PackageType>();
            lstVehicleType = new List<usp_VehicleType>();
            lstVehicle = new List<usp_Vehicle>();
            CustomerProfile = new usp_CustomerProfile();
            CustomerProfileList = new List<usp_CustomerProfile>();
            CustomerProfileDetailList = new List<usp_CustomerProfileDetail>();
            CompanyList = new List<usp_Company>();
            lstCity = new List<usp_City>();
            lstPrduct = new List<usp_Product>();
            lstGroup = new List<Group>();
            _group = new Group();
            lstOwnCompany = new List<usp_OwnCompany>();
            _ownCompany = new usp_OwnCompany();
           // lstCompany = new List<GetCompanyList_Result>();
            _ownDeptartment = new usp_OwnDepartment();
            lstOwnDepartment = new List<usp_OwnDepartment>();
          //  lstOragnaization = new List<usp_Organization_Result>();
            lstMenu = new List<Menu>();

          //  lstNavMenuResult = new List<usp_NavMenu_Result>();
            userAccount = new usp_UserAccounts();
            lstUserAccount = new List<usp_UserAccounts>();
            lstDesignation = new List<Designation>();
            lstRole = new List<Role>();
            lstForm = new List<usp_NavMenu>();
            form = new usp_NavMenu();
            lstNavMenu = new List<usp_Menu>();
            lstCategory = new List<Category>();
            lstGener = new List<Gener>();
            lstNature = new List<Nature>();

        }
    }
}