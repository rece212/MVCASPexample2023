using Microsoft.AspNetCore.Mvc;
using MVCASPexample2023.Models;
namespace MVCASPexample2023.Controllers
{
    public class StaffController : Controller
    {
        StaffContext db =  new StaffContext();
        public IActionResult Index()
        {
            return View(StaffContext.staffObject);
        }
        public IActionResult AddStaff()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddStaff(Staff sf)
        {
            Staff data = sf;
            if (sf.Name==null||sf.Id<0||sf.Password==null||sf.Dob==null||sf.Title==null)
            {
                ViewBag.Error = "Please enter all the fields or die";
                return View();
            }
            else
            {
                StaffContext.staffObject.Add(data);
                return RedirectToAction("Index");
            }
        }
        public IActionResult EditStaff(int id)
        {
            Staff temp = StaffContext.staffObject.Where(x=>x.Id==id).FirstOrDefault();
            return View(temp);
        }
        [HttpPost]
        public IActionResult EditStaff(Staff sf)
        {
            Staff data = sf;
            if (sf.Name == null || sf.Id < 0 || sf.Password == null 
                || sf.Dob == null || sf.Title == null)
            {
                ViewBag.Error = "Please enter all the fields";
                return View();
            }
            else
            {
                (from p in StaffContext.staffObject
                 where p.Id == sf.Id
                 select p).ToList().ForEach(x =>
                 {
                     x.Password = sf.Password;
                     x.Dob = sf.Dob;
                     x.Name = sf.Name;
                     x.Title = sf.Title;
                 });
                return RedirectToAction("Index");
            }

        }
        public IActionResult ViewStaff(int id)
        {
            Staff temp = StaffContext.staffObject.Where(x => x.Id == id).FirstOrDefault();
            return View(temp);
        }

        public IActionResult DeleteStaff(int id)
        {
            Staff temp = StaffContext.staffObject.Where(x => x.Id == id).FirstOrDefault();
            return View(temp);
        }
        [HttpPost]
        public IActionResult DeleteStaff(Staff sf)
        {
            StaffContext.staffObject = StaffContext.staffObject.Where(x => x.Id != sf.Id).ToList();
            return RedirectToAction("Index");
        }
    }
}
