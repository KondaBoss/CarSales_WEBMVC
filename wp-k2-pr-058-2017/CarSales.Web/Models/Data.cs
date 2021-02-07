using CarSales.Data.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Hosting;

namespace CarSales.Web.Models
{
    public class Data
    {
        //public static List<Product> ReadProducts(string path)
        //{
        //    List<Product> products = new List<Product>();
        //    path = HostingEnvironment.MapPath(path);
        //    FileStream stream = new FileStream(path, FileMode.Open);
        //    StreamReader sr = new StreamReader(stream);
        //    string line = "";
        //    while((line = sr.ReadLine()) != null)
        //    {
        //        string[] tokens = line.Split(';');
        //        Product product = new Product(tokens[0], tokens[1], double.Parse(tokens[2]));
        //        products.Add(product);
        //    }
        //    sr.Close();
        //    stream.Close();

        //    return products;
        //}

        public static List<User> ReadUsers(string path)
        {
            List<User> users = new List<User>();
            path = HostingEnvironment.MapPath(path);
            FileStream stream = new FileStream(path, FileMode.Open);
            StreamReader sr = new StreamReader(stream);
            string line = "";
            while ((line = sr.ReadLine()) != null)
            {
                string[] fields = line.Split('|');
                User user = new User();
                user.ID = int.Parse(fields[0]);
                user.FirstName = fields[1];
                user.LastName = fields[2];
                user.UserName = fields[3];
                user.Email = fields[4];
                user.Password = fields[5];
                user.Gender = bool.Parse(fields[6]);
                user.BirthDate = new DateTime(1999, 1, 19); // Izmeni
                user.Role = (UserRole)Enum.Parse(typeof(UserRole), fields[8]);
                user.IsDeleted = bool.Parse(fields[9]);
                user.IsLoggedIn = bool.Parse(fields[10]);

                users.Add(user);
            }
            sr.Close();
            stream.Close();

            return users;
        }

        public static void SaveUser(User user)
        {
            string path = HostingEnvironment.MapPath("~/App_Data/Users.txt");
            using (StreamWriter sw = File.AppendText(path))
            {
                sw.WriteLine(user.ToString());
            }
        }

        public static void SaveVehicle(Vehicle vehicle)
        {
            string path = HostingEnvironment.MapPath("~/App_Data/Vehicles.txt");
            using (StreamWriter sw = File.AppendText(path))
            {
                sw.WriteLine(vehicle.ToString());
            }
        }

        public static void SavePurchase(Purchase purchase)
        {
            string path = HostingEnvironment.MapPath("~/App_Data/Purchases.txt");
            using (StreamWriter sw = File.AppendText(path))
            {
                sw.WriteLine(purchase.ToString());
            }
        }

        public static void SaveVehicles(List<Vehicle> vehicles)
        {
            string path = HostingEnvironment.MapPath("~/App_Data/Vehicles.txt");
            string temp = string.Empty;

            foreach (Vehicle vehicle in vehicles)
            {
                temp = temp += vehicle.ToString() + "\n";
            }

            System.IO.File.WriteAllText(path, temp);
        }

        public static void SaveUsers(List<User> users)
        {
            string path = HostingEnvironment.MapPath("~/App_Data/Users.txt");
            string temp = string.Empty;

            foreach (User user in users)
            {
                temp = temp += user.ToString() + "\n";
            }

            System.IO.File.WriteAllText(path, temp);
        }
    }
}