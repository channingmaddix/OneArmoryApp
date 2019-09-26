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
    public class WeaponsController : Controller
    {
        private readonly OneArmoryDataContext _context;

        public WeaponsController(OneArmoryDataContext context)
        {
            _context = context;
        }

        // GET: Weapons
        public async Task<IActionResult> Index()
        {
            var oneArmoryDataContext = _context.Weapon.Include(w => w.EquipmentTypeNavigation).Include(w => w.NomenclatureNavigation).Include(w => w.PlatoonNavigation);
            return View(await oneArmoryDataContext.ToListAsync());
        }

        // GET: Weapons/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var weapon = await _context.Weapon
                .Include(w => w.EquipmentTypeNavigation)
                .Include(w => w.NomenclatureNavigation)
                .Include(w => w.PlatoonNavigation)
                .FirstOrDefaultAsync(m => m.WeaponId == id);
            if (weapon == null)
            {
                return NotFound();
            }

            return View(weapon);
        }

        // GET: Weapons/Create
        public IActionResult Create()
        {
            ViewData["EquipmentType"] = new SelectList(_context.EquipmentType, "EquipmentType1", "EquipmentType1");
            ViewData["Nomenclature"] = new SelectList(_context.Nomenclature, "Nomenclature1", "Nomenclature1");
            ViewData["Platoon"] = new SelectList(_context.Platoon, "Platoon1", "Platoon1");
            return View();
        }

        // POST: Weapons/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("WeaponId,Serial,Nomenclature,EquipmentType,Platoon,ArrivalDate")] Weapon weapon)
        {
            if (ModelState.IsValid)
            {
                _context.Add(weapon);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EquipmentType"] = new SelectList(_context.EquipmentType, "EquipmentType1", "EquipmentType1", weapon.EquipmentType);
            ViewData["Nomenclature"] = new SelectList(_context.Nomenclature, "Nomenclature1", "Nomenclature1", weapon.Nomenclature);
            ViewData["Platoon"] = new SelectList(_context.Platoon, "Platoon1", "Platoon1", weapon.Platoon);
            return View(weapon);
        }

        // GET: Weapons/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var weapon = await _context.Weapon.FindAsync(id);
            if (weapon == null)
            {
                return NotFound();
            }
            ViewData["EquipmentType"] = new SelectList(_context.EquipmentType, "EquipmentType1", "EquipmentType1", weapon.EquipmentType);
            ViewData["Nomenclature"] = new SelectList(_context.Nomenclature, "Nomenclature1", "Nomenclature1", weapon.Nomenclature);
            ViewData["Platoon"] = new SelectList(_context.Platoon, "Platoon1", "Platoon1", weapon.Platoon);
            return View(weapon);
        }

        // POST: Weapons/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("WeaponId,Serial,Nomenclature,EquipmentType,Platoon,ArrivalDate")] Weapon weapon)
        {
            if (id != weapon.WeaponId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(weapon);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WeaponExists(weapon.WeaponId))
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
            ViewData["EquipmentType"] = new SelectList(_context.EquipmentType, "EquipmentType1", "EquipmentType1", weapon.EquipmentType);
            ViewData["Nomenclature"] = new SelectList(_context.Nomenclature, "Nomenclature1", "Nomenclature1", weapon.Nomenclature);
            ViewData["Platoon"] = new SelectList(_context.Platoon, "Platoon1", "Platoon1", weapon.Platoon);
            return View(weapon);
        }

        // GET: Weapons/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var weapon = await _context.Weapon
                .Include(w => w.EquipmentTypeNavigation)
                .Include(w => w.NomenclatureNavigation)
                .Include(w => w.PlatoonNavigation)
                .FirstOrDefaultAsync(m => m.WeaponId == id);
            if (weapon == null)
            {
                return NotFound();
            }

            return View(weapon);
        }

        // POST: Weapons/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var weapon = await _context.Weapon.FindAsync(id);
            _context.Weapon.Remove(weapon);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WeaponExists(int id)
        {
            return _context.Weapon.Any(e => e.WeaponId == id);
        }

        public int ReadinessLevel(string filter)
        {
            int numWeapons = 0;
            int numBrokenWeapons = 0;

            var oneArmoryWeaponDataContext = _context.Weapon.Include
                (w => w.EquipmentTypeNavigation).Include(w => w.NomenclatureNavigation).
                Include(w => w.PlatoonNavigation);

            var oneArmoryWorkOrderDataContext = _context.WorkOrder.Include(w => w.Weapon).
                Include(w => w.WeaponStatusNavigation).
                Include(w => w.WorkOrderStatusNavigation);
            foreach (Weapon weapon in oneArmoryWeaponDataContext)
            {
                numWeapons++;
                foreach (WorkOrder workOrder in oneArmoryWorkOrderDataContext)
                {
                    if (workOrder.WeaponId == weapon.WeaponId)
                    {
                        numBrokenWeapons++;
                    }
                }
            }
            return (numWeapons - numBrokenWeapons) / numWeapons;
        }
    }
}
