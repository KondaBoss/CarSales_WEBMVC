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
    public class VehicleDetailsViewModel
    {
        public Vehicle Vehicle { get; set; }

        public UserRole UserRole { get; set; }

    }
}
