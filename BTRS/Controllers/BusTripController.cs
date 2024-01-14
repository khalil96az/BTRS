using BTRS.Data;
using BTRS.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BTRS.Controllers
{
    public class BusTripController : Controller
    {

        private SystemDbContext _context;
        public BusTripController(SystemDbContext context)
        {
            _context = context;
        }

        // GET: BusTripController1
        public ActionResult Index()
        {
            return View(_context.busTrip.ToList());
        }

        // GET: BusTripController1/Details/5
        public ActionResult Details(int id)
        {
            BusTrip busTrip = _context.busTrip.Find(id);
            return View(busTrip);
        }

        // GET: BusTripController1/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BusTripController1/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(BusTrip busTrip)
        {
            try
            {
                int adminid = (int)HttpContext.Session.GetInt32("adminID");

                Admin admin = _context.admin.Where(
                a => a.ID == adminid
                ).FirstOrDefault();

                busTrip.Admin = admin;

                _context.busTrip.Add(busTrip);
                _context.SaveChanges();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: BusTripController1/Edit/5
        public ActionResult Edit(int id)
        {
            BusTrip busTrip = _context.busTrip.Find(id);
            return View(busTrip);
        }

        // POST: BusTripController1/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, BusTrip busTrip)
        {
            try
            {


                _context.busTrip.Update(busTrip);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));


            }
            catch
            {
                return View();
            }
        }

        // GET: BusTripController1/Delete/5
        public ActionResult Delete(int id)
        {
            BusTrip busTrip = _context.busTrip.Find(id);

            return View(busTrip);
        }

        // POST: BusTripController1/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, BusTrip busTrip)
        {
            try
            {
                _context.busTrip.Remove(busTrip);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
