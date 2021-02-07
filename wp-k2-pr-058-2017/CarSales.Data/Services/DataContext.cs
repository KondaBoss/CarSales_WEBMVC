using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarSales.Data.Services
{
    public class DataContext
    {
        public DataContext()
        {
            User = new UserData();
            Customer = new CustomerData();
            Purchase = new PurchaseData();
            Vehicle = new VehicleData();
        }

        public UserData User { get; private set; }

        public CustomerData Customer { get; private set; }

        public PurchaseData Purchase { get; private set; }

        public VehicleData Vehicle { get; private set; }
    }
}
