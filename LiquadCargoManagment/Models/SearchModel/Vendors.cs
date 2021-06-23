using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static LiquadCargoManagment.Helpers.ApplicationHelper;
namespace LiquadCargoManagment.Models
{
    public class Vendors
    {
        private readonly LCMEntities context;
        public Vendors(LCMEntities _context)
        {
            context = _context;
        }

    }
}