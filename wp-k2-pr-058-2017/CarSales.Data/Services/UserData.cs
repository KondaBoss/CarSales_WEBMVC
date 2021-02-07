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
    public class UserData : IUserData
    {
        public IEnumerable<User> GetAll()
        {
            return GetUsers();
        }

        public static List<User> GetUsers()
        {
            if (!File.Exists(AppDomain.CurrentDomain.BaseDirectory + "App_Data/Users.txt"))
                return new List<User>();

            List<User> list = new List<User>();


            using (StreamReader reader = new StreamReader(AppDomain.CurrentDomain.BaseDirectory + "App_Data/Users.txt"))
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

        public static User FromString(string str)
        {
            if (string.IsNullOrEmpty(str))
                return null;

            var fields = str.Split('|');

            var user = new User();
            user.ID = int.Parse(fields[0]);
            user.FirstName = fields[1];
            user.LastName = fields[2];
            user.UserName = fields[3];
            user.Email = fields[4];
            user.Password = fields[5];
            user.Gender = bool.Parse(fields[6]);
            user.BirthDate = DateTime.Parse(fields[7]);
            user.Role = (UserRole)Enum.Parse(typeof(UserRole), fields[8]);
            user.IsDeleted = bool.Parse(fields[9]);
            user.IsLoggedIn = bool.Parse(fields[10]);

            return user;
        }

        public static string ListToString(List<User> list)
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
