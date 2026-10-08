using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using thurston_building.Data;
using thurston_building.Models;

namespace thurston_building.Controllers
{
    public class TenantsController : Controller
    {
        private readonly ThurstonDbContext _context;

        public TenantsController(ThurstonDbContext context)
        {
            _context = context;
        }

		public async Task<IActionResult> Index(
	        string? searchName,
	        string? searchEmail,
	        string? searchPhone,
	        int? roomId)
		{
			var tenants = _context.Tenants
				.Include(t => t.Room)
				.AsQueryable();

			if (!string.IsNullOrWhiteSpace(searchName))
			{
				tenants = tenants.Where(t =>
					t.FirstName.Contains(searchName) ||
					t.LastName.Contains(searchName));
			}

			if (!string.IsNullOrWhiteSpace(searchEmail))
			{
				tenants = tenants.Where(t =>
					t.Email.Contains(searchEmail));
			}

			if (!string.IsNullOrWhiteSpace(searchPhone))
			{
				tenants = tenants.Where(t =>
					t.PhoneNumber.Contains(searchPhone));
			}

			if (roomId.HasValue)
			{
				tenants = tenants.Where(t =>
					t.RoomId == roomId.Value);
			}

			ViewData["RoomId"] = new SelectList(
				await _context.Rooms.ToListAsync(),
				"Id",
				"RoomNumber",
				roomId);

			ViewData["SearchName"] = searchName;
			ViewData["SearchEmail"] = searchEmail;
			ViewData["SearchPhone"] = searchPhone;

			return View(await tenants.ToListAsync());
		}

		// GET: Tenants/Details/5
		public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tenant = await _context.Tenants
                .Include(t => t.Room)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tenant == null)
            {
                return NotFound();
            }

            return View(tenant);
        }

        // GET: Tenants/Create
        public IActionResult Create()
        {
            ViewData["RoomId"] = new SelectList(_context.Rooms, "Id", "RoomNumber");
            return View();
        }

        // POST: Tenants/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FirstName,LastName,Email,PhoneNumber,RoomId")] Tenant tenant)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tenant);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["RoomId"] = new SelectList(_context.Rooms, "Id", "RoomNumber", tenant.RoomId);
            return View(tenant);
        }

        // GET: Tenants/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tenant = await _context.Tenants.FindAsync(id);
            if (tenant == null)
            {
                return NotFound();
            }
            ViewData["RoomId"] = new SelectList(_context.Rooms, "Id", "RoomNumber", tenant.RoomId);
            return View(tenant);
        }

        // POST: Tenants/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FirstName,LastName,Email,PhoneNumber,RoomId")] Tenant tenant)
        {
            if (id != tenant.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tenant);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TenantExists(tenant.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["RoomId"] = new SelectList(_context.Rooms, "Id", "RoomNumber", tenant.RoomId);
            return View(tenant);
        }

        // GET: Tenants/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tenant = await _context.Tenants
                .Include(t => t.Room)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tenant == null)
            {
                return NotFound();
            }

            return View(tenant);
        }

        // POST: Tenants/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tenant = await _context.Tenants.FindAsync(id);
            if (tenant != null)
            {
                _context.Tenants.Remove(tenant);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TenantExists(int id)
        {
            return _context.Tenants.Any(e => e.Id == id);
        }
    }
}
