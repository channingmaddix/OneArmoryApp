using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OneArmoryApp.Models;
using Microsoft.EntityFrameworkCore;


namespace OneArmoryApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly OneArmoryDataContext _context;
        public HomeController(OneArmoryDataContext DB)
        {
            _context = DB;
        }
        public IActionResult Index(List<string> filters)
        {
            var allFilters = new List<string>();
            allFilters = _context.Nomenclature.Select(n => n.Nomenclature1).ToList();
            ViewBag.AllFilters = new List<WeaponReadiness>();
            foreach (var filter in allFilters)
            {
                int readinessLevel = ReadinessLevel(filter);
                ViewBag.AllFilters.Add(new WeaponReadiness { Nomenclature = filter, ReadinessLevel = readinessLevel });
            }


            ViewBag.Weapons = new List<WeaponReadiness>();
            if (filters.Count == 0)
            {
                filters = _context.Nomenclature.Select(n => n.Nomenclature1).ToList();
            }
            foreach (var filter in filters)
            {
                int readinessLevel = ReadinessLevel(filter);
                ViewBag.Weapons.Add(new WeaponReadiness { Nomenclature = filter, ReadinessLevel = readinessLevel });
            }
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
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
                if (weapon.Nomenclature == filter)
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
            }
            int readinessLevel = ((numWeapons - numBrokenWeapons) * 100) / numWeapons;
            return readinessLevel;
        }

    }
}
