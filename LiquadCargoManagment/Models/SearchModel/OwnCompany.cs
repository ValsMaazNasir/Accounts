using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static LiquadCargoManagment.Helpers.ApplicationHelper;
namespace LiquadCargoManagment.Models
{
    public class OwnCompanys
    {
        private readonly LCMEntities context;
        public OwnCompanys(LCMEntities _context)
        {
            context = _context;
        }
        public List<OwnCompany> getSearchOwnCompany(DateTime DateFrom, DateTime DateTo)
        {
            return context.OwnCompanies.Where(x => x.CreatedDate >= DateFrom && x.CreatedDate <= DateTo && lstAssignedCompanies.Contains(x.ID)).ToList();
        }
        public List<OwnCompany> getSearchOwnCompany(DateTime Date, string type)
        {
            if (type == "from")
            {
                return context.OwnCompanies.Where(x => x.CreatedDate >= Date && lstAssignedCompanies.Contains(x.ID)).ToList();
            }
            else
            {
                return context.OwnCompanies.Where(x => x.CreatedDate <= Date && lstAssignedCompanies.Contains(x.ID)).ToList();
            }
        }
        public List<OwnCompany> SearchDateIDCodeNameContactEmailWeb(DateTime DateFrom, DateTime DateTo, int? SubcriptionID, int? GroupID , string Code, string Name, string Contact, string ContactOther, string EmailAdd,string WebAdd)
        {
            return context.OwnCompanies.Where(x => x.CreatedDate >= DateFrom && x.CreatedDate <= DateTo && x.SubcriptionID == SubcriptionID && x.GroupID == GroupID && x.Code == Code && x.Name == Name && x.Contact == Contact && x.ContactOther == ContactOther && x.EmailAdd == EmailAdd && x.WebAdd == WebAdd && lstAssignedCompanies.Contains(x.ID)).ToList();
        }
        public List<OwnCompany> SearchDateIDCodeNameContactEmail(DateTime DateFrom, DateTime DateTo, int? SubcriptionID, int? GroupID, string Code, string Name, string Contact, string ContactOther, string EmailAdd)
        {
            return context.OwnCompanies.Where(x => x.CreatedDate >= DateFrom && x.CreatedDate <= DateTo && x.SubcriptionID == SubcriptionID && x.GroupID == GroupID && x.Code == Code && x.Name == Name && x.Contact == Contact && x.ContactOther == ContactOther && x.EmailAdd == EmailAdd  && lstAssignedCompanies.Contains(x.ID)).ToList();
        }
        public List<OwnCompany> SearchDateIDCodeNameContactOther(DateTime DateFrom, DateTime DateTo, int? SubcriptionID, int? GroupID, string Code, string Name, string Contact, string ContactOther)
        {
            return context.OwnCompanies.Where(x => x.CreatedDate >= DateFrom && x.CreatedDate <= DateTo && x.SubcriptionID == SubcriptionID && x.GroupID == GroupID && x.Code == Code && x.Name == Name && x.Contact == Contact && x.ContactOther == ContactOther  && lstAssignedCompanies.Contains(x.ID)).ToList();
        }
        public List<OwnCompany> SearchDateIDCodeNameContact(DateTime DateFrom, DateTime DateTo, int? SubcriptionID, int? GroupID, string Code, string Name, string Contact)
        {
            return context.OwnCompanies.Where(x => x.CreatedDate >= DateFrom && x.CreatedDate <= DateTo && x.SubcriptionID == SubcriptionID && x.GroupID == GroupID && x.Code == Code && x.Name == Name && x.Contact == Contact  && lstAssignedCompanies.Contains(x.ID)).ToList();
        }
        public List<OwnCompany> SearchDateIDCodeName(DateTime DateFrom, DateTime DateTo, int? SubcriptionID, int? GroupID, string Code, string Name)
        {
            return context.OwnCompanies.Where(x => x.CreatedDate >= DateFrom && x.CreatedDate <= DateTo && x.SubcriptionID == SubcriptionID && x.GroupID == GroupID && x.Code == Code && x.Name == Name  && lstAssignedCompanies.Contains(x.ID)).ToList();
        }
        public List<OwnCompany> SearchDateIDCode(DateTime DateFrom, DateTime DateTo, int? SubcriptionID, int? GroupID, string Code)
        {
            return context.OwnCompanies.Where(x => x.CreatedDate >= DateFrom && x.CreatedDate <= DateTo && x.SubcriptionID == SubcriptionID && x.GroupID == GroupID && x.Code == Code && lstAssignedCompanies.Contains(x.ID)).ToList();
        }
        public List<OwnCompany> SearchDateGroupID(DateTime DateFrom, DateTime DateTo, int? SubcriptionID, int? GroupID)
        {
            return context.OwnCompanies.Where(x => x.CreatedDate >= DateFrom && x.CreatedDate <= DateTo && x.SubcriptionID == SubcriptionID && x.GroupID == GroupID && lstAssignedCompanies.Contains(x.ID)).ToList();
        }
        public List<OwnCompany> SearchDateSubscriptionID(DateTime DateFrom, DateTime DateTo, int? SubcriptionID)
        {
            return context.OwnCompanies.Where(x => x.CreatedDate >= DateFrom && x.CreatedDate <= DateTo && x.SubcriptionID == SubcriptionID && lstAssignedCompanies.Contains(x.ID)).ToList();
        }
       
        public List<OwnCompany> SearchOwnComanyName(DateTime DateFrom, DateTime DateTo, string Name)
        {
            return context.OwnCompanies.Where(x => x.CreatedDate >= DateFrom && x.CreatedDate <= DateTo && x.Name == Name && lstAssignedCompanies.Contains(x.ID)).ToList();
        }
        public List<OwnCompany> SearchOwnComanyCode(DateTime DateFrom, DateTime DateTo, string Code)
        {
            return context.OwnCompanies.Where(x => x.CreatedDate >= DateFrom && x.CreatedDate <= DateTo && x.Code == Code && lstAssignedCompanies.Contains(x.ID)).ToList();
        }
        public List<OwnCompany> SearchOwnComanyIDNameCodeContactAdd(int? SubcriptionID , int? GroupID,  string Code, string Name, string Contact, string ContactOther, string EmailAdd ,string WebAdd)
        {
            return context.OwnCompanies.Where(x => x.SubcriptionID == SubcriptionID && x.GroupID <= GroupID && x.Code == Code && x.Name == Name && x.Contact == Contact && x.ContactOther == ContactOther && x.EmailAdd == EmailAdd && x.WebAdd == WebAdd && lstAssignedCompanies.Contains(x.ID)).ToList();
        }
        public List<OwnCompany> SearchOwnComanyIDNameCodeContactEmailAdd(int? SubcriptionID, int? GroupID, string Code, string Name, string Contact, string ContactOther, string EmailAdd)
        {
            return context.OwnCompanies.Where(x => x.SubcriptionID == SubcriptionID && x.GroupID <= GroupID && x.Code == Code && x.Name == Name && x.Contact == Contact && x.ContactOther == ContactOther && x.EmailAdd == EmailAdd  && lstAssignedCompanies.Contains(x.ID)).ToList();
        }
        public List<OwnCompany> SearchOwnComanyIDNameCodeContactOther(int? SubcriptionID, int? GroupID, string Code, string Name, string Contact, string ContactOther)
        {
            return context.OwnCompanies.Where(x => x.SubcriptionID == SubcriptionID && x.GroupID <= GroupID && x.Code == Code && x.Name == Name && x.Contact == Contact && x.ContactOther == ContactOther && lstAssignedCompanies.Contains(x.ID)).ToList();
        }
        public List<OwnCompany> SearchOwnComanyIDNameCodeContact(int? SubcriptionID, int? GroupID, string Code, string Name, string Contact)
        {
            return context.OwnCompanies.Where(x => x.SubcriptionID == SubcriptionID && x.GroupID <= GroupID && x.Code == Code && x.Name == Name && x.Contact == Contact  && lstAssignedCompanies.Contains(x.ID)).ToList();
        }
        public List<OwnCompany> SearchOwnComanyIDCodeName(int? SubcriptionID, int? GroupID, string Code, string Name)
        {
            return context.OwnCompanies.Where(x => x.SubcriptionID == SubcriptionID && x.GroupID <= GroupID && x.Code == Code && x.Name == Name  && lstAssignedCompanies.Contains(x.ID)).ToList();
        }
        public List<OwnCompany> SearchOwnComanyIDCode(int? SubcriptionID, int? GroupID, string Code)
        {
            return context.OwnCompanies.Where(x => x.SubcriptionID == SubcriptionID && x.GroupID <= GroupID && x.Code == Code  && lstAssignedCompanies.Contains(x.ID)).ToList();
        }
        public List<OwnCompany> SearchOwnComanyID(int? SubcriptionID, int? GroupID)
        {
            return context.OwnCompanies.Where(x => x.SubcriptionID == SubcriptionID && x.GroupID <= GroupID  && lstAssignedCompanies.Contains(x.ID)).ToList();
        }

        public List<OwnCompany> SearchOwnComanyIDNameCodeContactAdd(int? GroupID, string Code, string Name, string Contact, string ContactOther, string EmailAdd, string WebAdd)
        {
            return context.OwnCompanies.Where(x => x.SubcriptionID == SubcriptionID && x.GroupID <= GroupID && x.Code == Code && x.Name == Name && x.Contact == Contact && x.ContactOther == ContactOther && x.EmailAdd == EmailAdd && x.WebAdd == WebAdd && lstAssignedCompanies.Contains(x.ID)).ToList();
        }
        public List<OwnCompany> SearchOwnComanyIDNameCodeContact(int? GroupID, string Code, string Name, string Contact, string ContactOther, string EmailAdd)
        {
            return context.OwnCompanies.Where(x => x.SubcriptionID == SubcriptionID && x.GroupID <= GroupID && x.Code == Code && x.Name == Name && x.Contact == Contact && x.ContactOther == ContactOther && x.EmailAdd == EmailAdd  && lstAssignedCompanies.Contains(x.ID)).ToList();
        }
        public List<OwnCompany> SearchOwnComanyIDNameCodeContactOther(int? GroupID, string Code, string Name, string Contact, string ContactOther)
        {
            return context.OwnCompanies.Where(x => x.SubcriptionID == SubcriptionID && x.GroupID <= GroupID && x.Code == Code && x.Name == Name && x.Contact == Contact && x.ContactOther == ContactOther && lstAssignedCompanies.Contains(x.ID)).ToList();
        }
        public List<OwnCompany> SearchOwnComanyIDNameCodeContact(int? GroupID, string Code, string Name, string Contact)
        {
            return context.OwnCompanies.Where(x => x.SubcriptionID == SubcriptionID && x.GroupID <= GroupID && x.Code == Code && x.Name == Name && x.Contact == Contact  && lstAssignedCompanies.Contains(x.ID)).ToList();
        }
        public List<OwnCompany> SearchOwnComanyIDNameCode(int? GroupID, string Code, string Name)
        {
            return context.OwnCompanies.Where(x => x.SubcriptionID == SubcriptionID && x.GroupID <= GroupID && x.Code == Code && x.Name == Name && lstAssignedCompanies.Contains(x.ID)).ToList();
        }
        public List<OwnCompany> SearchOwnComanyIDCode(int? GroupID, string Code)
        {
            return context.OwnCompanies.Where(x => x.SubcriptionID == SubcriptionID && x.GroupID <= GroupID && x.Code == Code && lstAssignedCompanies.Contains(x.ID)).ToList();
        }
       
        public List<OwnCompany> SearchDateFromCode(DateTime DateFrom, string Code)
        {
            return context.OwnCompanies.Where(x => x.CreatedDate >= DateFrom && x.Code == Code && lstAssignedCompanies.Contains(x.ID)).ToList();
        }
        public List<OwnCompany> SearchDateToCode(DateTime DateTo, string Code)
        {
            return context.OwnCompanies.Where(x => x.CreatedDate >= DateTo && x.Code == Code && lstAssignedCompanies.Contains(x.ID)).ToList();
        }
        public List<OwnCompany> SearchDateFromName(DateTime DateFrom, string Name)
        {
            return context.OwnCompanies.Where(x => x.CreatedDate >= DateFrom && x.Name == Name && lstAssignedCompanies.Contains(x.ID)).ToList();
        }
        public List<OwnCompany> SearchDateToName(DateTime DateTo, string Name)
        {
            return context.OwnCompanies.Where(x => x.CreatedDate >= DateTo && x.Name == Name && lstAssignedCompanies.Contains(x.ID)).ToList();
        }
        public List<OwnCompany> SearchNameCode(string Name, string Code)
        {
            return context.OwnCompanies.Where(x => x.Name == Name && x.Code == Code && lstAssignedCompanies.Contains(x.ID)).ToList();
        }
        //public List<OwnCompany> SearchOwnCompanyAllFilter(int? GroupID, int? RoleId, int? SubcriptionID,DateTime DateFrom, DateTime DateTo, string Name, string Code, string Contact, string ContactOther, string EmailAdd, string WebAdd)
        //{
        //    return context.OwnCompanies.Where(x => x.GroupID == GroupID && x.RoleId == RoleId && x.CreatedDate == DateFrom && x.CreatedDate == DateTo && x.PackageTypeName == Name && x.PackageTypeCode == Code && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        //}

        //Multiple Selected Search
        //public List<Bilty> SearchBiltyDateVehicleBillTo(DateTime DateFrom, DateTime DateTo, int? Vehicle, int? BillTo)
        //{
        //    return context.Bilties.Where(x => x.OrderDate >= DateFrom && x.OrderDate <= DateTo && x.VehicleID == Vehicle && x.BillTo == BillTo && x.IsDeleted != true && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        //}
        //public List<Bilty> SearchBiltyDateVehicleBillToReceiver(DateTime DateFrom, DateTime DateTo, int? Vehicle, int? BillTo, int? Receiver)
        //{
        //    return context.Bilties.Where(x => x.OrderDate >= DateFrom && x.OrderDate <= DateTo && x.VehicleID == Vehicle && x.BillTo == BillTo && x.Reciever == Receiver && x.IsDeleted != true && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        //}
        //public List<Bilty> SearchBiltyDateVehicleBillToReceiverOwnCompany(DateTime DateFrom, DateTime DateTo, int? Vehicle, int? BillTo, int? Receiver, int? OwnCompany)
        //{
        //    return context.Bilties.Where(x => x.OrderDate >= DateFrom && x.OrderDate <= DateTo && x.VehicleID == Vehicle && x.BillTo == BillTo && x.Reciever == Receiver && x.OwnCompany == OwnCompany && x.IsDeleted != true && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        //}
        //public List<Bilty> SearchBiltyAllFilter(DateTime DateFrom, DateTime DateTo, DateTime DeliveryDate, int? Vehicle, int? BillTo, int? Receiver, int? OwnCompany, int? OrderNo)
        //{
        //    return context.Bilties.Where(x => x.OrderDate == DateFrom && x.OrderDate == DateTo && x.DeliveryDate == DeliveryDate && x.VehicleID == Vehicle && x.BillTo == BillTo && x.Reciever == Receiver && x.OwnCompany == OwnCompany && x.OrderNo == OrderNo && x.IsDeleted != true && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        //}

        //public List<Bilty> SearchBiltyDropDownVBD(int? Vehicle, int? BillTo,)
        //{
        //    return context.Bilties.Where(x => x.VehicleID == Vehicle && x.BillTo == BillTo && x.IsDeleted != true && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        //}
        //public List<Bilty> SearchBiltyDropDownVR(int? Vehicle, int? Receiver)
        //{
        //    return context.Bilties.Where(x => x.VehicleID == Vehicle && x.Reciever == Receiver && x.IsDeleted != true && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        //}
        //public List<Bilty> SearchBiltyDropDownVO(int? Vehicle, int? OwnCompany)
        //{
        //    return context.Bilties.Where(x => x.VehicleID == Vehicle && x.OwnCompany == OwnCompany && x.IsDeleted != true && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        //}
        //public List<Bilty> SearchBiltyDropDownVBR(int? Vehicle, int? BillTo, int? Reciever)
        //{
        //    return context.Bilties.Where(x => x.VehicleID == Vehicle && x.BillTo == BillTo && x.Reciever == Reciever && x.IsDeleted != true && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        //}

        //public List<Bilty> SearchBiltyDropDownBR(int? BillTo, int? Receiver)
        //{
        //    return context.Bilties.Where(x => x.BillTo == BillTo && x.Reciever == Receiver && x.IsDeleted != true && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        //}
        //public List<Bilty> SearchBiltyDropDownBO(int? BillTo, int? OwnCompany)
        //{
        //    return context.Bilties.Where(x => x.BillTo == BillTo && x.OwnCompany == OwnCompany && x.IsDeleted != true && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        //}
        //public List<Bilty> SearchBiltyDropDownRO(int? Receiver, int? OwnCompany)
        //{
        //    return context.Bilties.Where(x => x.Reciever == Receiver && x.OwnCompany == OwnCompany && x.IsDeleted != true && lstAssignedCompanies.Contains(x.OwnCompanyID)).ToList();
        //}
    }
}