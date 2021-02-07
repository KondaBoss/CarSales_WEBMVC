using CarSales.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarSales.Data.ViewModels
{
    public class VehicleListViewModel
    {
        public IEnumerable<VehicleInfo> Vehicles { get; set; }

        public UserRole UserRole { get; set; }
        public string BrandFilterText { get; set; }
        public string ModelFilterText { get; set; }
        public string PriceFromFilterText { get; set; }
        public string PriceToFilterText { get; set; }
    }
}
