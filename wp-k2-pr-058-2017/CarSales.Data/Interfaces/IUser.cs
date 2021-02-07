using CarSales.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarSales.Data.Interfaces
{
    public interface IUser
    {
        int ID { get; set; }
        string FirstName { get; set; }
        string LastName { get; set; }
        string UserName { get; set; }
        string Email { get; set; }
        string Password { get; set; }
        bool Gender { get; set; }
        DateTime? BirthDate { get; set; }
        UserRole Role { get; set; }
    }
}
