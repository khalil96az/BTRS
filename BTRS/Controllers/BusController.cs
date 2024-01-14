using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BTRS.Data;
using BTRS.Models;

namespace BTRS.Controllers
{
    public class BusController : Controller
    {
        private readonly SystemDbContext _context;

        public BusController(SystemDbContext context)
        {
            _context = context;
        }

        // GET: Buses
        public async Task<IActionResult> Index()
        {
            return _context.bus != null ?
                        View(await _context.bus.ToListAsync()) :
                        Problem("Entity set 'SystemDbContext.bus'  is null.");
        }

        // GET: Buses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.bus == null)
            {
                return NotFound();
            }

            var bus = await _context.bus
                .FirstOrDefaultAsync(m => m.ID == id);
            if (bus == null)
            {
                return NotFound();
            }

            return View(bus);
        }

        // GET: Buses/Create
        public IActionResult Create()
        {
            ViewBag.BusTrip = _context.busTrip.ToList();
            return View();
        }

        // POST: Buses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(IFormCollection form)
        {
            string CaptainName = form["CaptainName"];
            int Seats = int.Parse(form["Seats"]);
            int TripId = int.Parse(form["TripId"]);

            Bus bus = new Bus();
            bus.CaptainName = CaptainName;
            bus.Seats = Seats;
            bus.busTrip = _context.busTrip.Find(TripId);

            _context.bus.Add(bus);
            _context.SaveChanges();

            return RedirectToAction("Index");

        }

        // GET: Buses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            ViewBag.BusTrip = _context.busTrip.ToList();

            if (id == null || _context.bus == null)
            {
                return NotFound();
            }
            var players = await _context.bus.FindAsync(id);
            if (players == null)
            {
                return NotFound();
            }
            return View(players);
        }

        // POST: Buses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(IFormCollection form)
        {
            string CaptainName = form["CaptainName"];
            int Seats = int.Parse(form["Seats"]);
            int TripId = int.Parse(form["TripId"]);
            int id = int.Parse(form["ID"]);

            Bus bus = _context.bus.Find(id);
            bus.CaptainName = CaptainName;
            bus.Seats = Seats;

            bus.busTrip = _context.busTrip.Find(TripId);


            _context.bus.Update(bus);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }


        // GET: Buses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.bus == null)
            {
                return NotFound();
            }

            var bus = await _context.bus
                .FirstOrDefaultAsync(m => m.ID == id);
            if (bus == null)
            {
                return NotFound();
            }

            return View(bus);
        }

        // POST: Buses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.bus == null)
            {
                return Problem("Entity set 'SystemDbContext.bus'  is null.");
            }
            var bus = await _context.bus.FindAsync(id);
            if (bus != null)
            {
                _context.bus.Remove(bus);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BusExists(int id)
        {
            return (_context.bus?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}