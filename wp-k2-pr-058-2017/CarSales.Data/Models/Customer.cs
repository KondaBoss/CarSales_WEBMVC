using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarSales.Data.Models
{
    public class Customer : User
    {
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb
                .Append(ID.ToString())
                .Append("|")
                .Append(FirstName ?? string.Empty)
                .Append("|")
                .Append(LastName ?? string.Empty)
                .Append("|")
                .Append(UserName ?? string.Empty)
                .Append("|")
                .Append(Email ?? string.Empty)
                .Append("|")
                .Append(Password ?? string.Empty)
                .Append("|")
                .Append(Gender.ToString())
                .Append("|")
                .Append(BirthDate?.ToString("yyyy-MM-dd") ?? string.Empty)
                .Append("|")
                .Append(Role.ToString())
                .Append("|")
                .Append(IsDeleted.ToString());

            return sb.ToString();
        }
    }
}
