using CarSales.Data.Models;
using CarSales.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CarSales.Web.Controllers
{
    public class AccountController : Controller
    {
        public static int userID = 0;

        public AccountController()
        {
            HomeController homeController = new HomeController();
        }

        [HttpGet]
        public ActionResult Register()
        {
            User user = (User)Session["user"];
            if (user != null && !user.UserName.Equals(string.Empty))
                return Content("<h1>Error<h1>");

            return View();
        }

        [HttpPost]
        public ActionResult Register(RegisterViewModel model)
        {
            User user = new User();
            
            user.ID = ++HomeController.userID;

            user.FirstName = model.FirstName ?? string.Empty;
            user.LastName = model.LastName ?? string.Empty;
            user.UserName = model.UserName ?? string.Empty;
            user.Email = model.Email ?? string.Empty;
            user.Password = model.Password ?? string.Empty;
            user.Gender = model.Gender;
            user.BirthDate = model.BirthDate ?? null;

            user.Role = UserRole.Customer;
            user.IsDeleted = false;
            user.IsLoggedIn = false;

            if (
                user.FirstName == string.Empty ||
                user.LastName == string.Empty ||
                user.UserName == string.Empty ||
                user.Email == string.Empty ||
                user.Password == string.Empty ||
                user.BirthDate == null
                )
            {
                ViewBag.Message = $"All fields must be filled!";
                return View();
            }



            List<User> users = (List<User>)HttpContext.Application["users"];

            for (int i = 0; i < users.Count; i++)
            {
                if (users[i].UserName == user.UserName || users[i].Email == user.Email)
                {
                    ViewBag.Message = $"Username or email is already taken!";
                    return View();
                }
            }

            users.Add(user);
            Models.Data.SaveUser(user);

            return RedirectToAction("Login", "Account");
        }

        [HttpGet]
        public ActionResult Login()
        {
            User user = (User)Session["user"];
            if (user != null && !user.UserName.Equals(string.Empty))
                return Content("<h1>Error<h1>");

            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel model)
        {
            List<User> users = (List<User>)HttpContext.Application["users"];
            User user = users.Find(u => u.Email.Equals(model.Email) && u.Password.Equals(model.Password));
            if (user == null)
            {
                ViewBag.Message = $"User with credentials doesn't exists!";
                return View();
            }

            if (user.IsDeleted)
            {
                ViewBag.Message = $"User with username: {user.UserName} has been deleted, and cannot log in.";
                return View();
            }

            Session["user"] = user;
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Logout()
        {
            User user = (User)Session["user"];
            if (user == null || user.UserName.Equals(string.Empty))
                return Content("<h1>Error<h1>");

            Session["user"] = null;

            return RedirectToAction("Login", "Account");
        }
    }
}