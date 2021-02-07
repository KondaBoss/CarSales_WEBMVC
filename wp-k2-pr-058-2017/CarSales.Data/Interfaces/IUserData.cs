using CarSales.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarSales.Data.Interfaces
{
    public interface IUserData
    {
        IEnumerable<User> GetAll();
    }
}
