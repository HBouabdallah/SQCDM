using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IndustryIncident.Controllers
{
    public class NotFoundController : Controller
    {
        // GET: NotFoundController
        public ActionResult Index()
        {
            return View();
        }

        // GET: NotFoundController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: NotFoundController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: NotFoundController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: NotFoundController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: NotFoundController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: NotFoundController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: NotFoundController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
