using LiquadCargoManagment.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LiquadCargoManagment.ViewModels
{
    public class OrdersViewModel
    {
        public List<order> orders { get; set; }
        public List<orders_products> orders_products { get; set; }
    }
}