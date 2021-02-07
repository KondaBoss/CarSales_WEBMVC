using CarSales.Data.Interfaces;
using CarSales.Data.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarSales.Data.Services
{
    public class PurchaseData : IPurchaseData
    {
        public IEnumerable<Purchase> GetAll()
        {
            return GetPurchases();
        }

        public List<Purchase> GetPurchases()
        {
            if (!File.Exists(AppDomain.CurrentDomain.BaseDirectory + "App_Data/Purchases.txt"))
                return new List<Purchase>();

            List<Purchase> list = new List<Purchase>();
            using (StreamReader reader = new StreamReader(AppDomain.CurrentDomain.BaseDirectory + "App_Data/Purchases.txt"))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    if (FromString(line) != null)
                        list.Add(FromString(line));
                }
            }
            return list;
        }

        public Purchase FromString(string str)
        {
            if (string.IsNullOrEmpty(str))
                return null;


            var fields = str.Split('|');

            var purchase = new Purchase();
            purchase.ID = int.Parse(fields[0]);
            purchase.CustomerID = int.Parse(fields[1]);
            purchase.VehicleID = int.Parse(fields[2]);
            purchase.DateOfPurchase = DateTime.Parse(fields[3]);
            purchase.PaidPrice = double.Parse(fields[4]);

            return purchase;
        }

        public string ListToString(List<Purchase> list)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < list.Count; i++)
            {
                sb.Append(list[i].ToString());
            }
            return sb.ToString();
        }
    }
}
