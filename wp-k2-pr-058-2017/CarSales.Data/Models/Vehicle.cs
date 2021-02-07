using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarSales.Data.Models
{
    public class Vehicle
    {
        public int ID { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 3)]
        public string Brand { get; set; }

        [Required]
        public string Model { get; set; }

        [Required]
        [StringLength(17, MinimumLength = 17)]
        [Display(Name = "Chassis designation")]
        public string ChassisDesignation { get; set; }

        [Required]
        [Display(Name = "Vehicle color")]
        public Color VehicleColor { get; set; }   // System.Drawing.dll reference

        [Required]
        [Display(Name = "Number of doors")]
        public int NumberOfDoors { get; set; }

        [StringLength(256)]
        [DataType(DataType.Text)]
        public string Description { get; set; }
        
        [Required]
        [Display(Name ="Type of fuel")]
        public Fuel TypeOfFuel { get; set; }
        
        [Required]
        [DataType(DataType.Currency)]
        public double Price { get; set; }

        [Display(Name = "In stock")]
        public bool InStock { get; set; }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb
                .Append(ID.ToString())
                .Append("|")
                .Append(Brand ?? string.Empty)
                .Append("|")
                .Append(Model ?? string.Empty)
                .Append("|")
                .Append(ChassisDesignation ?? string.Empty)
                .Append("|")
                .Append(VehicleColor.ToString())
                .Append("|")
                .Append(NumberOfDoors.ToString())
                .Append("|")
                .Append(Description ?? string.Empty)
                .Append("|")
                .Append(TypeOfFuel.ToString())
                .Append("|")
                .Append(Price.ToString())
                .Append("|")
                .Append(InStock.ToString());

            return sb.ToString();
        }
    }
}
