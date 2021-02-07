using CarSales.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarSales.Data.ViewModels
{
    public class VehiclesViewModel
    {
        public List<Vehicle> Vehicles { get; set; }
    }
}
