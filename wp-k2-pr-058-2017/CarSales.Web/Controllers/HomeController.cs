using CarSales.Data.Models;
using CarSales.Data.Services;
using CarSales.Data.ViewModels;
using CarSales.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CarSales.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly DataContext _context;
        public static int userID = 0;
        public static int vehicleID = 0;
        public static int purchaseID = 0;

        public HomeController()
        {
            _context = new DataContext();
            userID = _context.User.GetAll().Count();
            vehicleID = _context.User.GetAll().Count();
            purchaseID = _context.Purchase.GetAll().Count();
        }
        

        public ActionResult CustomerList()
        {
            User user = (User)Session["user"];
            if (user == null || user.UserName.Equals(string.Empty))
                return Content("<h1>Unautorized<h1>");

            if (user.Role != UserRole.Administrator)
                return Content("<h1>Unautorized<h1>");

            var model = _context.Customer.GetAll().Where(x => x.IsDeleted == false);
            return View(model);
        }
        
        public ActionResult Index(
            string brandFilterText, string modelFilterText, string priceFromFilterText, string priceToFilterText,
            string sortOrder)
        {
            ViewBag.BrandSortParm = sortOrder == "Brand" ? "brand_desc" : "Brand";
            ViewBag.ModelSortParm = sortOrder == "Model" ? "model_desc" : "Model";
            ViewBag.PriceSortParm = sortOrder == "Price" ? "price_desc" : "Price";

            var viewmodel = new VehicleListViewModel();

            User user = (User)Session["user"];
            viewmodel.UserRole = user == null ? UserRole.Guest : user.Role;

            IEnumerable<Vehicle> vehicles = new List<Vehicle>();
            if(user == null || user.Role == UserRole.Guest || user.Role == UserRole.Customer)
            {
                vehicles = _context.Vehicle.GetAllInStock();
            }
            else
            {
                vehicles = _context.Vehicle.GetAll();
            }

            var purchases = _context.Purchase.GetAll();
            var customers = _context.Customer.GetAll();

            var vehicleInfo = new List<VehicleInfo>();

            foreach (var vehicle in vehicles)
            {
                string owner = string.Empty;
                var purchase = purchases.SingleOrDefault(x => x.VehicleID == vehicle.ID);
                if(purchase != null)
                {
                    var customer = customers.SingleOrDefault(x => x.ID == purchase.CustomerID);
                    if(customer != null)
                    {
                        owner = customer.FirstName + " " + customer.LastName;
                    }
                }

                vehicleInfo.Add(new VehicleInfo()
                {
                    ID = vehicle.ID,
                    Brand = vehicle.Brand,
                    Model = vehicle.Model,
                    ChassisDesignation = vehicle.ChassisDesignation,
                    VehicleColor = vehicle.VehicleColor,
                    NumberOfDoors = vehicle.NumberOfDoors,
                    Description = vehicle.Description,
                    TypeOfFuel = vehicle.TypeOfFuel,
                    Price = vehicle.Price,
                    InStock = vehicle.InStock,
                    Owner = owner
                });
            }

            #region Filtering

            if(!string.IsNullOrEmpty(brandFilterText))
            {
                vehicleInfo = vehicleInfo.Where(x => x.Brand.ToLower().StartsWith(brandFilterText.ToLower())).ToList();
            }

            if (!string.IsNullOrEmpty(modelFilterText))
            {
                vehicleInfo = vehicleInfo.Where(x => x.Model.ToLower().StartsWith(modelFilterText.ToLower())).ToList();
            }

            if (!string.IsNullOrEmpty(priceFromFilterText) || !string.IsNullOrEmpty(priceToFilterText))
            {
                double priceFrom = string.IsNullOrEmpty(priceFromFilterText) ? 0d : Convert.ToDouble(priceFromFilterText);
                double priceTo = string.IsNullOrEmpty(priceToFilterText) ? double.MaxValue : Convert.ToDouble(priceToFilterText);
                vehicleInfo = vehicleInfo.Where(x => x.Price >= priceFrom && x.Price <= priceTo).ToList();
            }

            #endregion

            #region Sorting

            switch (sortOrder)
            {
                case "Brand":
                    vehicleInfo = vehicleInfo.OrderBy(x => x.Brand).ToList();
                    break;
                case "brand_desc":
                    vehicleInfo = vehicleInfo.OrderByDescending(x => x.Brand).ToList();
                    break;
                case "Model":
                    vehicleInfo = vehicleInfo.OrderBy(x => x.Model).ToList();
                    break;
                case "model_desc":
                    vehicleInfo = vehicleInfo.OrderByDescending(x => x.Model).ToList();
                    break;
                case "Price":
                    vehicleInfo = vehicleInfo.OrderBy(x => x.Price).ToList();
                    break;
                case "price_desc":
                    vehicleInfo = vehicleInfo.OrderByDescending(x => x.Price).ToList();
                    break;
                default:
                    vehicleInfo = vehicleInfo.OrderBy(x => x.Brand).ToList();
                    break;
            }

            #endregion

            viewmodel.Vehicles = vehicleInfo;
            viewmodel.BrandFilterText = brandFilterText;
            viewmodel.ModelFilterText = modelFilterText;
            viewmodel.PriceFromFilterText = priceFromFilterText;
            viewmodel.PriceToFilterText = priceToFilterText;

            return View(viewmodel);
        }

        public ActionResult PurchaseList()
        {
            User user = (User)Session["user"];
            if (user == null || user.UserName.Equals(string.Empty))
                return Content("<h1>Unautorized<h1>");

            if (user.Role != UserRole.Customer)
                return Content("<h1>Only customers can view their purchase list<h1>");


            var viewmodel = new PurchaseListViewModel();


            var purchases = _context.Purchase.GetAll().Where(c => c.CustomerID == user.ID);
            var vehicles = _context.Vehicle.GetAll();

            var purchaseInfo = new List<PurchaseInfo>();

            foreach (var purchase in purchases)
            {
                string vehiclename = string.Empty;

                var vehicle = vehicles.SingleOrDefault(x => x.ID == purchase.VehicleID);

                if (vehicle != null)
                {
                    vehiclename = vehicle.Brand + " " + vehicle.Model;
                }

                purchaseInfo.Add(new PurchaseInfo()
                {
                    ID = purchase.ID,
                    CustomerID = purchase.CustomerID,
                    VehicleID = purchase.VehicleID,
                    PaidPrice = purchase.PaidPrice,
                    DateOfPurchase = purchase.DateOfPurchase,
                    VehicleName = vehiclename
                });

            }

            viewmodel.Purchases = purchaseInfo;


            return View(viewmodel);
        }

        public ActionResult CustomerDetails(int ID)
        {
            User user = (User)Session["user"];
            if (user == null || user.UserName.Equals(string.Empty) || user.Role != UserRole.Administrator)
                return Content("<h1>Unautorized<h1>");

            var model = _context.Customer.GetAll().SingleOrDefault(c => c.ID == ID);

            return View(model);
        }
        
        public ActionResult VehicleDetails(int ID)
        {
            var viewmodel = new VehicleDetailsViewModel();

            User user = (User)Session["user"];
            if (user != null && user.Role == UserRole.Administrator)
                viewmodel.UserRole = UserRole.Administrator;

            else if (user != null && user.Role == UserRole.Customer)
                viewmodel.UserRole = UserRole.Customer;
            else
                viewmodel.UserRole = UserRole.Guest;

            viewmodel.Vehicle = _context.Vehicle.GetAll().SingleOrDefault(c => c.ID == ID);

            if (!viewmodel.Vehicle.InStock && user.Role != UserRole.Administrator)
                return Content("<h1>Unautorized<h1>");


            return View(viewmodel);
        }
        


        [HttpGet]
        public ActionResult EditVehicle(int ID)
        {
            User user = (User)Session["user"];
            if (user == null || user.Role != UserRole.Administrator)
                return Content("<h1>Unautorized<h1>");

            

            var model = _context.Vehicle.GetAll().SingleOrDefault(c => c.ID == ID);

            if (model.InStock == false)
                return Content("<h1>Unautorized<h1>");

            ViewBag.Message = model.ID.ToString();

            return View(model);
        }

        [HttpPost]
        public ActionResult EditVehicle(Vehicle editVehicle)
        {
            List<Vehicle> vehicles = (List<Vehicle>)_context.Vehicle.GetAll();
            

            int editID = editVehicle.ID - 1;

            vehicles[editID].ID = editVehicle.ID;
            vehicles[editID].Brand = editVehicle.Brand;
            vehicles[editID].Model = editVehicle.Model;
            vehicles[editID].ChassisDesignation = editVehicle.ChassisDesignation;
            vehicles[editID].NumberOfDoors = editVehicle.NumberOfDoors;
            vehicles[editID].InStock = true;
            vehicles[editID].VehicleColor = editVehicle.VehicleColor;
            vehicles[editID].TypeOfFuel = editVehicle.TypeOfFuel;
            vehicles[editID].Price = editVehicle.Price;
            vehicles[editID].Description = editVehicle.Description;
            

            Models.Data.SaveVehicles(vehicles);

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public ActionResult CreateNewVehicle()
        {

            User user = (User)Session["user"];
            if (user == null || user.Role != UserRole.Administrator)
                return Content("<h1>Unautorized<h1>");
            

            return View();
        }

        [HttpPost]
        public ActionResult CreateNewVehicle(Vehicle newVehicle)
        {
            Vehicle vehicle = new Vehicle();

            vehicle.ID = ++vehicleID;
            vehicle.Brand = newVehicle.Brand;
            vehicle.Model = newVehicle.Model;
            vehicle.ChassisDesignation = newVehicle.ChassisDesignation;
            vehicle.NumberOfDoors = newVehicle.NumberOfDoors;
            vehicle.InStock = true;
            vehicle.VehicleColor = newVehicle.VehicleColor;
            vehicle.TypeOfFuel = newVehicle.TypeOfFuel;
            vehicle.Price = newVehicle.Price;
            vehicle.Description = newVehicle.Description;
            vehicle.TypeOfFuel = newVehicle.TypeOfFuel;

            Models.Data.SaveVehicle(vehicle);

            return RedirectToAction("Index", "Home");
        }
        
        public ActionResult CreatePurchase(int id)
        {
            User user = (User)Session["user"];
            if (user == null || user.Role != UserRole.Customer)
                return Content("<h1>Unautorized<h1>");


            List<Vehicle> vehicles = (List<Vehicle>)_context.Vehicle.GetAll();
            if (vehicles.Where(x => x.ID == id) == null)
                return Content("<h1>Error<h1>");

            int editID = id - 1;

            vehicles[editID].InStock = false;
            Models.Data.SaveVehicles(vehicles);


            Purchase purchase = new Purchase();

            purchase.ID = ++purchaseID;
            purchase.CustomerID = user.ID;
            purchase.VehicleID = id;
            purchase.DateOfPurchase = DateTime.Now;
            purchase.PaidPrice = vehicles[editID].Price;
            Models.Data.SavePurchase(purchase);
            
            return RedirectToAction("PurchaseList", "Home");
        }

        public ActionResult DeleteVehicle(int id)
        {
            User user = (User)Session["user"];
            if (user == null || user.Role != UserRole.Administrator)
                return Content("<h1>Unautorized<h1>");

            List<Vehicle> vehicles = (List<Vehicle>)_context.Vehicle.GetAll();
            vehicles.Remove(vehicles.SingleOrDefault(x => x.ID == id));

            Models.Data.SaveVehicles(vehicles);

            return RedirectToAction("Index", "Home");
        }

        public ActionResult DeleteCustomer(int id)
        {
            User user = (User)Session["user"];
            if (user == null || user.Role != UserRole.Administrator)
                return Content("<h1>Unautorized<h1>");

            List<User> allUsers = (List<User>)_context.User.GetAll();
            allUsers.SingleOrDefault(x => x.ID == id).IsDeleted = true;

            Models.Data.SaveUsers(allUsers);

            List<User> users = Models.Data.ReadUsers("~/App_Data/Users.txt");
            HttpContext.Application["users"] = users;

            return RedirectToAction("CustomerList", "Home");
        }
    }
}