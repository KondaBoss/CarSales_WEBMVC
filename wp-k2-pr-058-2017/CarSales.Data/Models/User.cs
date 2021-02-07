using CarSales.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarSales.Data.Models
{
    public class User : IUser
    {
        public int ID { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 3)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]

        [StringLength(30, MinimumLength = 3)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        
        [Required]
        [StringLength(30, MinimumLength = 3)]
        [Display(Name = "Username")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 8)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public bool Gender { get; set; }
        
        [DataType(DataType.Date)]
        [Display(Name = "Birth Date")]
        public DateTime? BirthDate { get; set; }


        public UserRole Role { get; set; }

        public bool IsDeleted { get; set; }

        public bool IsLoggedIn { get; set; }


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
                .Append(IsDeleted.ToString())
                .Append("|")
                .Append(IsLoggedIn.ToString());

            return sb.ToString();
        }
    }
}
