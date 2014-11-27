using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AdventurousContacts.Models.Repository;
using System.Net;

namespace AdventurousContacts.Models
{
    public class contactController : Controller
    {
         private IRepository _repository;

        public contactController() : this(new EFRepository()) { }

        public contactController(IRepository repository)
        {
            _repository = repository;
        }

        //
        // GET: /contact/

        public ActionResult Index()
        {
            return View(_repository.GetContacts().Skip(Math.Max(0, _repository.GetContacts().Count() - 20)).Take(20));
        }
        public ActionResult NoContact()
        {
            return View();
        }

        //
        // GET: /contact/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /contact/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "FirstName,LastName,EmailAddress")]Contact contact)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _repository.InsertContact(contact);
                    _repository.Save();
                    TempData["success"] = "Person tillagd.";
                    return RedirectToAction("Index");
                }
            }
            catch (DataException)
            {
                TempData["error"] = "Misslyckades att spara personen. Försök igen, och kvarstår problemet kontakta systemadministratören.";
            }

            return View(contact);
        }
        //
        // GET: /contact/Edit/

        public ActionResult Edit(int? id)
        {
            if (!id.HasValue)
            {
                return RedirectToAction("NoContact");
            }

            Contact contact = _repository.GetContactsById(id.Value);
            if (contact == null)
            {
                TempData["error"] = "Misslyckades att spara ändringarna. Försök igen, och kvarstår problemet kontakta systemadministratören.";
                return RedirectToAction("NoContact");
            }

            return View(contact);
        }
        // POST: /contact/Edit/
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult EditPost(int id)
        {
            var contactToUpdate = _repository.GetContactsById(id);

            if (contactToUpdate == null)
            {
                TempData["error"] = "Misslyckades att spara ändringarna. Försök igen, och kvarstår problemet kontakta systemadministratören.";
                return RedirectToAction("NoContact");
            }

            if (TryUpdateModel(contactToUpdate, String.Empty, new string[] { "FirstName","LastName","EmailAddress" }))
            {
                try
                {
                    _repository.UpdateContact(contactToUpdate);
                    _repository.Save();
                    TempData["success"] = "Ändringarna sparade.";
                    return RedirectToAction("Index");
                }
                catch (DataException)
                {
                    TempData["error"] = "Misslyckades att spara ändringarna. Försök igen, och kvarstår problemet kontakta systemadministratören.";
                    //return RedirectToAction("Edit", new { id = id });
                    return RedirectToAction("NoContact");
                }
            }

            return View(contactToUpdate);
        }
        // GET: /contect/Delete/
        public ActionResult Delete(int? id)
        {
            if (!id.HasValue)
            {
                return RedirectToAction("NoContact");
            }

            var contact = _repository.GetContactsById(id.Value);
            if (contact == null)
            {
                return RedirectToAction("NoContact");
            }

            return View(contact);
        }

        //
        // POST: /contact/Delete/

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                var contactToDelete = new Contact { ContactID = id };
                _repository.DeleteContact(contactToDelete);
                _repository.Save();
                TempData["success"] = "Kontakt togs bort.";
            }
            catch (DataException)
            {
                TempData["error"] = "Misslyckades att ta bort Kontakt. Försök igen, och kvarstår problemet kontakta systemadministratören.";
                return RedirectToAction("Delete", new { id = id });
            }

            return RedirectToAction("Index");
        }
        //
        // POST: /contact/Details/
        protected override void Dispose(bool disposing)
        {
            _repository.Dispose();
            base.Dispose(disposing);
        }

        public ActionResult Details(int? id)
        {
            if (!id.HasValue)
            {
                return RedirectToAction("NoContact");
            }

            Contact contact = _repository.GetContactsById(id.Value);
            if (contact == null)
            {
                TempData["error"] = "Kontakten finns inte.";
                return RedirectToAction("NoContact");
            }

            return View(contact);
        }
    }
}