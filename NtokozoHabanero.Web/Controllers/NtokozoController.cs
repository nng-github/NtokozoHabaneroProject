using System.Web.Mvc;

namespace NtokozoHabanero.Web.Controllers
{
    public class NtokozoController : Controller
    {
        // GET: Ntokozo
        public ActionResult Index()
        {
            return View();
        }

        // GET: Ntokozo/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Ntokozo/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Ntokozo/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Ntokozo/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Ntokozo/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Ntokozo/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Ntokozo/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
