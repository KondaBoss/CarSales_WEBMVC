using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarSales.Data.Models
{
    public class Purchase
    {
        public int ID { get; set; }

        [Required]
        public int CustomerID { get; set; }

        [Required]
        public int VehicleID { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        public double PaidPrice { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime? DateOfPurchase { get; set; }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb
                .Append(ID.ToString())
                .Append("|")
                .Append(CustomerID.ToString())
                .Append("|")
                .Append(VehicleID.ToString())
                .Append("|")
                .Append(DateOfPurchase?.ToString("yyyy-MM-dd") ?? string.Empty)
                .Append("|")
                .Append(PaidPrice.ToString());

            return sb.ToString();
        }
    }
}
