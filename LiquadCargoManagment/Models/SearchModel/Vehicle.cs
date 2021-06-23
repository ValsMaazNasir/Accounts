using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static LiquadCargoManagment.Helpers.ApplicationHelper;
namespace LiquadCargoManagment.Models
{
    public class Vehicles
    {
        private readonly LCMEntities context;
        public Vehicles(LCMEntities _context)
        {
            context = _context;
        }
        public List<Vehicle> getSearchVehicle(DateTime DateFrom, DateTime DateTo)
        {
            return context.Vehicles.Where(x => x.CreatedDate >= DateFrom && x.CreatedDate <= DateTo).ToList();
        }
        public List<Vehicle> getSearchVehicle(DateTime Date, string type)
        {
            if (type == "from")
            {
                return context.Vehicles.Where(x => x.CreatedDate >= Date).ToList();
            }
            else
            {
                return context.Vehicles.Where(x => x.CreatedDate <= Date).ToList();
            }
        }
        public List<Vehicle> SearchVehicleIDRegNo(int? VehicleTypeID, string RegNo)
        {
            return context.Vehicles.Where(x => x.VehicleTypeID == VehicleTypeID && x.RegNo == RegNo && lstAssignedCompanies.Contains(x.OwnCompanyId)).ToList();
        }
        public List<Vehicle> SearchVehicleDateFromRegNo(DateTime DateFrom, string RegNo)
        {
            return context.Vehicles.Where(x => x.CreatedDate == DateFrom && x.RegNo == RegNo && lstAssignedCompanies.Contains(x.OwnCompanyId) ).ToList();
        }
        public List<Vehicle> SearchDateToRegNo(DateTime DateTo, string RegNo)
        {
            return context.Vehicles.Where(x => x.CreatedDate == DateTo && x.RegNo == RegNo && lstAssignedCompanies.Contains(x.OwnCompanyId)).ToList();
        }
        public List<Vehicle> SearchVehicleIDDateFromDateTo(int? VehicleTypeID,DateTime DateFrom, DateTime DateTo)
        {
            return context.Vehicles.Where(x => x.VehicleTypeID == VehicleTypeID  && x.CreatedDate >= DateFrom && x.CreatedDate <= DateTo  && lstAssignedCompanies.Contains(x.OwnCompanyId)).ToList();
        }
        public List<Vehicle> SearchVehicleDateFromDateToReg(DateTime DateFrom, DateTime DateTo, string RegNo)
        {
            return context.Vehicles.Where(x =>  x.CreatedDate == DateFrom && x.CreatedDate == DateTo && x.RegNo == RegNo && lstAssignedCompanies.Contains(x.OwnCompanyId)).ToList();
        }

        public List<Vehicle> SearchVehicleAllFilter(int? VehicleTypeID ,DateTime DateFrom, DateTime DateTo, string RegNo)
        {
            return context.Vehicles.Where(x => x.VehicleTypeID == VehicleTypeID && x.CreatedDate == DateFrom && x.CreatedDate == DateTo && x.RegNo == RegNo && lstAssignedCompanies.Contains(x.OwnCompanyId)).ToList();
        }
      


    }
}