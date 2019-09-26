using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OneArmoryApp.Models;

namespace OneArmoryApp.Controllers
{
    public class SoldiersController : Controller
    {
        private readonly OneArmoryDataContext _context;

        public SoldiersController(OneArmoryDataContext context)
        {
            _context = context;
        }

        // GET: Soldiers
        public async Task<IActionResult> Index()
        {
            var oneArmoryDataContext = _context.Soldier.Include(s => s.PaygradeNavigation).Include(s => s.Weapon);
            return View(await oneArmoryDataContext.ToListAsync());
        }

        // GET: Soldiers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var soldier = await _context.Soldier
                .Include(s => s.PaygradeNavigation)
                .Include(s => s.Weapon)
                .FirstOrDefaultAsync(m => m.SoldierId == id);
            if (soldier == null)
            {
                return NotFound();
            }

            return View(soldier);
        }

        // GET: Soldiers/Create
        public IActionResult Create()
        {
            ViewData["Paygrade"] = new SelectList(_context.Paygrade, "Paygrade1", "Paygrade1");
            ViewData["WeaponId"] = new SelectList(_context.Weapon, "WeaponId", "WeaponId");
            return View();
        }

        // POST: Soldiers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SoldierId,FirstName,LastName,Paygrade,WeaponId,DoDid,ArrivalDate")] Soldier soldier)
        {
            if (ModelState.IsValid)
            {
                _context.Add(soldier);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Paygrade"] = new SelectList(_context.Paygrade, "Paygrade1", "Paygrade1", soldier.Paygrade);
            ViewData["WeaponId"] = new SelectList(_context.Weapon, "WeaponId", "WeaponId", soldier.WeaponId);
            return View(soldier);
        }

        // GET: Soldiers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var soldier = await _context.Soldier.FindAsync(id);
            if (soldier == null)
            {
                return NotFound();
            }
            ViewData["Paygrade"] = new SelectList(_context.Paygrade, "Paygrade1", "Paygrade1", soldier.Paygrade);
            ViewData["WeaponId"] = new SelectList(_context.Weapon, "WeaponId", "WeaponId", soldier.WeaponId);
            return View(soldier);
        }

        // POST: Soldiers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SoldierId,FirstName,LastName,Paygrade,WeaponId,DoDid,ArrivalDate")] Soldier soldier)
        {
            if (id != soldier.SoldierId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(soldier);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SoldierExists(soldier.SoldierId))
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
            ViewData["Paygrade"] = new SelectList(_context.Paygrade, "Paygrade1", "Paygrade1", soldier.Paygrade);
            ViewData["WeaponId"] = new SelectList(_context.Weapon, "WeaponId", "WeaponId", soldier.WeaponId);
            return View(soldier);
        }

        // GET: Soldiers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var soldier = await _context.Soldier
                .Include(s => s.PaygradeNavigation)
                .Include(s => s.Weapon)
                .FirstOrDefaultAsync(m => m.SoldierId == id);
            if (soldier == null)
            {
                return NotFound();
            }

            return View(soldier);
        }

        // POST: Soldiers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var soldier = await _context.Soldier.FindAsync(id);
            _context.Soldier.Remove(soldier);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SoldierExists(int id)
        {
            return _context.Soldier.Any(e => e.SoldierId == id);
        }
    }
}
