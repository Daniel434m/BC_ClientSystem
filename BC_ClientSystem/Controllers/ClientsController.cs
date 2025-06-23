using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using BC_ClientSystem.DAL;
using BC_ClientSystem.Models;

namespace BC_ClientSystem.Controllers
{
    public class ClientsController : Controller
    {
        private ClientContext db = new ClientContext();

        public ActionResult Index()
        {
            return View(db.Clients.Include(c => c.Contacts).ToList());
        }

        private string GenerateClientCode(string name)
        {
            string prefix = new string(name.ToUpper().Where(char.IsLetter).Take(3).ToArray());
            if (prefix.Length < 3) prefix = prefix.PadRight(3, 'A');
            int suffix = 1;
            string clientCode;
            do
            {
                clientCode = $"{prefix}{suffix.ToString("D3")}";
                suffix++;
            } while (db.Clients.Any(c => c.ClientCode == clientCode));
            return clientCode;
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Name")] Client client)
        {
            if (ModelState.IsValid)
            {
                client.ClientCode = GenerateClientCode(client.Name);
                db.Clients.Add(client);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(client);
        }


        [HttpGet]
        public JsonResult GetClientCode(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return Json(string.Empty, JsonRequestBehavior.AllowGet);
            var code = GenerateClientCode(name);
            return Json(code, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Details(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            Client client = db.Clients.Find(id);
            if (client == null) return HttpNotFound();
            return View(client);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            Client client = db.Clients.Find(id);
            if (client == null) return HttpNotFound();
            return View(client);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ClientId,Name,ClientCode")] Client client)
        {
            if (ModelState.IsValid)
            {
                db.Entry(client).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(client);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            Client client = db.Clients.Find(id);
            if (client == null) return HttpNotFound();
            return View(client);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Client client = db.Clients.Find(id);
            db.Clients.Remove(client);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) db.Dispose();
            base.Dispose(disposing);
        }
    }
}


