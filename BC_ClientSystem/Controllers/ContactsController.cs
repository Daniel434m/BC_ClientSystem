using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Web.Mvc;
using BC_ClientSystem.DAL;
using BC_ClientSystem.Models;

namespace BC_ClientSystem.Controllers
{
    public class ContactsController : Controller
    {
        private ClientContext db = new ClientContext();

        public ActionResult Index()
        {
            var contacts = db.Contacts.Include(c => c.Clients);
            return View(contacts.Include(c => c.Clients).ToList());
        }

        public ActionResult Create()
        {
            ViewBag.ClientList = db.Clients.ToList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Contact contact, int[] selectedClientIds)
        {
            // Validate email
            if (!IsValidEmail(contact.Email))
            {
                ModelState.AddModelError("Email", "Invalid email format.");
            }

            // Check for duplicates
            if (db.Contacts.Any(c => c.Email == contact.Email))
            {
                ModelState.AddModelError("Email", "Email already exists.");
            }

            if (ModelState.IsValid)
            {
                if (selectedClientIds != null)
                {
                    contact.Clients = db.Clients.Where(c => selectedClientIds.Contains(c.ClientId)).ToList();
                }

                db.Contacts.Add(contact);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ClientList = db.Clients.ToList();
            return View(contact);
        }

        public ActionResult UnlinkAllClients(int id)
        {
            var contact = db.Contacts.Include(c => c.Clients).FirstOrDefault(c => c.ContactId == id);
            if (contact == null) return HttpNotFound();

            contact.Clients.Clear();
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        private bool IsValidEmail(string email)
        {
            var regex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");
            return regex.IsMatch(email);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) db.Dispose();
            base.Dispose(disposing);
        }
    }
}
