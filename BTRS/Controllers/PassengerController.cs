using BTRS.Data;
using BTRS.Models;
using Microsoft.AspNetCore.Mvc;

namespace BTRS.Controllers
{
    public class PassengerController : Controller
    {
        private SystemDbContext _context;

        public PassengerController(SystemDbContext context)
        {
            this._context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult SignUp()
        {

            return View();
        }

        [HttpPost]
        public IActionResult SignUp(Passenger passenger)
        {
            bool empty = checkEmpty(passenger);
            bool dup = checkEmail(passenger.Email);
            bool dup2 = checkUsername(passenger.Username);
            bool dup3 = checkPhone(passenger.Phone);


            if (empty)
            {
                if (dup)
                {
                    if (dup2)
                    {
                        if (dup3)
                        {
                            _context.passenger.Add(passenger);
                            _context.SaveChanges();
                            TempData["Msg"] = "the data was saved";

                            return View();
                        }
                        else
                        {
                            TempData["Msg"] = "please change PhoneNumber";

                            return View();

                        }
                    }
                    else
                    {
                        TempData["Msg"] = "please change Username";

                        return View();

                    }
                }

                else
                {
                    TempData["Msg"] = "please change Email";

                    return View();
                }
            }
            else
            {
                TempData["Msg"] = "Please fill all input";
                return View();
            }


        }
        public bool checkEmail(string Email)
        {
            Passenger passenger = _context.passenger.Where(u => u.Email.Equals(Email)).FirstOrDefault();
            if (passenger != null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public bool checkUsername(string Username)
        {
            Passenger passenger = _context.passenger.Where(u => u.Username.Equals(Username)).FirstOrDefault();
            if (passenger != null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public bool checkPhone(string Phone)
        {
            Passenger passenger = _context.passenger.Where(u => u.Phone.Equals(Phone)).FirstOrDefault();
            if (passenger != null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public bool checkEmpty(Passenger passenger)
        {
            if (String.IsNullOrEmpty(passenger.Name)) return false;
            else if (string.IsNullOrEmpty(passenger.Password)) return false;
            else if (string.IsNullOrEmpty(passenger.Username)) return false;
            else if (string.IsNullOrEmpty(passenger.Gender)) return false;
            else if (string.IsNullOrEmpty(passenger.Phone)) return false;
            else if (string.IsNullOrEmpty(passenger.Email)) return false;
            else return true;
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(Login passengerLogin)
        {
            if (ModelState.IsValid)
            {
                string username = passengerLogin.username;
                string password = passengerLogin.password;

                Passenger passenger = _context.passenger.Where(
                     u => u.Username.Equals(username) &&
                     u.Password.Equals(password)
                     ).FirstOrDefault();

                Admin admin = _context.admin.Where(
                    u => u.Username.Equals(username) &&
                    u.Password.Equals(password)
                    ).FirstOrDefault();

                if (passenger != null)
                {
                    HttpContext.Session.SetInt32("passengerID", passenger.ID);
                    return RedirectToAction("BusTripList");
                }
                else if (admin != null)
                {
                    HttpContext.Session.SetInt32("adminID", admin.ID);

                    return RedirectToAction("Index", "BusTrip");
                }
                else
                {
                    TempData["Msg"] = "The user Not Found";
                }
            }
            else
            {

            }
            return View();
        }


        public IActionResult BusTripList()
        {
            int passengerID = (int)HttpContext.Session.GetInt32("passengerID");

            List<int> lst_user_passenger = _context.passenger_Users.Where(
                u => u.passenger.ID == passengerID
                ).Select(t => t.busTrip.ID).ToList();

            List<BusTrip> lst_passenger = _context.busTrip.Where(
                 t => lst_user_passenger.Contains(t.ID) == false
                 ).ToList();

            return View(lst_passenger);
        }

        public IActionResult AddBooking(int id)
        {
            int busTripID = id;

            int passengerID = (int)HttpContext.Session.GetInt32("passengerID");

            Passenger_User passenger_User = new Passenger_User();
            passenger_User.passenger = _context.passenger.Find(passengerID);
            passenger_User.busTrip = _context.busTrip.Find(busTripID);

            _context.passenger_Users.Add(passenger_User);
            _context.SaveChanges();

            return RedirectToAction("BookingList");
        }

        public IActionResult BookingList()
        {

            int passengerID = (int)HttpContext.Session.GetInt32("passengerID");

            List<int> lst_booking = _context.passenger_Users.Where(
                t => t.passenger.ID == passengerID
                ).Select(s => s.busTrip.ID).ToList();

            List<BusTrip> lst = _context.busTrip.Where(
                t => lst_booking.Contains(t.ID)
                ).ToList();

            return View(lst);
        }

        public IActionResult Delete(int id)
        {
            int passengerID = (int)HttpContext.Session.GetInt32("passengerID");

            Passenger_User passenger_User = _context.passenger_Users.Where(
                t => t.passenger.ID == passengerID && t.busTrip.ID == id
              ).FirstOrDefault();



            _context.passenger_Users.Remove(passenger_User);
            _context.SaveChanges();


            return RedirectToAction("BookingList");
        }
    }
}
