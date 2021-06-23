using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Bilty.Data;
using LiquadCargoManagment.Models;

namespace LiquadCargoManagment.DataAccessLayer
{
    public static class ModelExtension
    {
        public static IEnumerable<SelectListItem> ToOwnCompanySelectList(this List<usp_OwnCompany> collection)
        {
            List<SelectListItem> TList = collection.Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() }).ToList();
            return TList;
        }
        public static IEnumerable<SelectListItem> ToCityModelSelectList(this List<usp_City> collection)
        {
            List<SelectListItem> TList = collection.Select(x => new SelectListItem { Text = x.CityName, Value = x.CityID.ToString() }).ToList();
            return TList;
        }

        public static IEnumerable<SelectListItem> ToProductModelSelectList(this List<usp_Product> collection)
        {
            List<SelectListItem> TList = collection.Select(x => new SelectListItem { Text = x.Code + "-" + x.PackageTypeName + "," + x.Name, Value = x.ID.ToString() }).ToList();
            TList.Insert(0, new SelectListItem { Text = "Select", Value = "0" });
            return TList;
        }


    }
}