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
    public class VehicleData : IVehicleData
    {
        public IEnumerable<Vehicle> GetAll()
        {
            return GetVehicles();
        }

        public IEnumerable<Vehicle> GetAllInStock()
        {
            return GetVehicles().Where(x => x.InStock == true).ToList();
        }

        public List<Vehicle> GetVehicles()
        {
            if (!File.Exists(AppDomain.CurrentDomain.BaseDirectory + "App_Data/Vehicles.txt"))
                return new List<Vehicle>();

            List<Vehicle> list = new List<Vehicle>();
            using (StreamReader reader = new StreamReader(AppDomain.CurrentDomain.BaseDirectory + "App_Data/Vehicles.txt"))
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

        public Vehicle FromString(string str)
        {
            if (string.IsNullOrEmpty(str))
                return null;

            var fields = str.Split('|');

            var vehicle = new Vehicle();
            vehicle.ID = int.Parse(fields[0]);
            vehicle.Brand = fields[1];
            vehicle.Model = fields[2];
            vehicle.ChassisDesignation = fields[3];
            vehicle.VehicleColor = (Color)Enum.Parse(typeof(Color), fields[4]);
            vehicle.NumberOfDoors = int.Parse(fields[5]);
            vehicle.Description = fields[6];
            vehicle.TypeOfFuel = (Fuel)Enum.Parse(typeof(Fuel), fields[7]);
            vehicle.Price = double.Parse(fields[8]);
            vehicle.InStock = bool.Parse(fields[9]);

            return vehicle;
        }

        public string ListToString(List<Vehicle> list)
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
