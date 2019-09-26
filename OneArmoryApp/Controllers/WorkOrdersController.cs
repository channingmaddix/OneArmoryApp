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
    public class WorkOrdersController : Controller
    {
        private readonly OneArmoryDataContext _context;

        public WorkOrdersController(OneArmoryDataContext context)
        {
            _context = context;
        }

        // GET: WorkOrders
        public async Task<IActionResult> Index()
        {
            var oneArmoryDataContext = _context.WorkOrder.Include(w => w.Weapon).Include(w => w.WeaponStatusNavigation).Include(w => w.WorkOrderStatusNavigation);
            return View(await oneArmoryDataContext.ToListAsync());
        }

        // GET: WorkOrders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var workOrder = await _context.WorkOrder
                .Include(w => w.Weapon)
                .Include(w => w.WeaponStatusNavigation)
                .Include(w => w.WorkOrderStatusNavigation)
                .FirstOrDefaultAsync(m => m.WorkOrderId == id);
            if (workOrder == null)
            {
                return NotFound();
            }

            return View(workOrder);
        }

        // GET: WorkOrders/Create
        public IActionResult Create()
        {
            ViewData["WeaponId"] = new SelectList(_context.Weapon, "WeaponId", "WeaponId");
            ViewData["WeaponStatus"] = new SelectList(_context.WeaponStatus, "WeaponStatus1", "WeaponStatus1");
            ViewData["WorkOrderStatus"] = new SelectList(_context.WorkOrderStatus, "WorkOrderStatus1", "WorkOrderStatus1");
            return View();
        }

        // POST: WorkOrders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("WorkOrderId,WeaponId,FaultDesc,WeaponStatus,WorkOrderStatus,StartDate,EndDate")] WorkOrder workOrder)
        {
            if (ModelState.IsValid)
            {
                _context.Add(workOrder);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["WeaponId"] = new SelectList(_context.Weapon, "WeaponId", "WeaponId", workOrder.WeaponId);
            ViewData["WeaponStatus"] = new SelectList(_context.WeaponStatus, "WeaponStatus1", "WeaponStatus1", workOrder.WeaponStatus);
            ViewData["WorkOrderStatus"] = new SelectList(_context.WorkOrderStatus, "WorkOrderStatus1", "WorkOrderStatus1", workOrder.WorkOrderStatus);
            return View(workOrder);
        }

        // GET: WorkOrders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var workOrder = await _context.WorkOrder.FindAsync(id);
            if (workOrder == null)
            {
                return NotFound();
            }
            ViewData["WeaponId"] = new SelectList(_context.Weapon, "WeaponId", "WeaponId", workOrder.WeaponId);
            ViewData["WeaponStatus"] = new SelectList(_context.WeaponStatus, "WeaponStatus1", "WeaponStatus1", workOrder.WeaponStatus);
            ViewData["WorkOrderStatus"] = new SelectList(_context.WorkOrderStatus, "WorkOrderStatus1", "WorkOrderStatus1", workOrder.WorkOrderStatus);
            return View(workOrder);
        }

        // POST: WorkOrders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("WorkOrderId,WeaponId,FaultDesc,WeaponStatus,WorkOrderStatus,StartDate,EndDate")] WorkOrder workOrder)
        {
            if (id != workOrder.WorkOrderId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(workOrder);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WorkOrderExists(workOrder.WorkOrderId))
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
            ViewData["WeaponId"] = new SelectList(_context.Weapon, "WeaponId", "WeaponId", workOrder.WeaponId);
            ViewData["WeaponStatus"] = new SelectList(_context.WeaponStatus, "WeaponStatus1", "WeaponStatus1", workOrder.WeaponStatus);
            ViewData["WorkOrderStatus"] = new SelectList(_context.WorkOrderStatus, "WorkOrderStatus1", "WorkOrderStatus1", workOrder.WorkOrderStatus);
            return View(workOrder);
        }

        // GET: WorkOrders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var workOrder = await _context.WorkOrder
                .Include(w => w.Weapon)
                .Include(w => w.WeaponStatusNavigation)
                .Include(w => w.WorkOrderStatusNavigation)
                .FirstOrDefaultAsync(m => m.WorkOrderId == id);
            if (workOrder == null)
            {
                return NotFound();
            }

            return View(workOrder);
        }

        // POST: WorkOrders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var workOrder = await _context.WorkOrder.FindAsync(id);
            _context.WorkOrder.Remove(workOrder);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WorkOrderExists(int id)
        {
            return _context.WorkOrder.Any(e => e.WorkOrderId == id);
        }
    }
}
