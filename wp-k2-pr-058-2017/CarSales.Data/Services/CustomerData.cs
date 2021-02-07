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
    public class CustomerData : IUserData
    {
        public IEnumerable<User> GetAll()
        {
            return UserData.GetUsers().Where(x => x.Role == UserRole.Customer).ToList();
        }
    }
}
