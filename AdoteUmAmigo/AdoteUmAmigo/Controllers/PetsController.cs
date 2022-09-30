using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using AdoteUmAmigo.Models;
using System.Web;
using System;
using Microsoft.AspNet.Identity;

namespace AdoteUmAmigo.Controllers
{
    public class PetsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Pets/Área Restrita Admin
        [Authorize(Roles = "View")]
        public ActionResult Admin()
        {
            var pet = db.Pets.Include(p => p.Especie);
            return View(pet.ToList());
        }

        // GET: Pets/Adote Um Amigo
        [AllowAnonymous]
        public ActionResult IndexHome()
        {            
            var pet = db.Pets.Include(p => p.Especie);
            return View(pet.ToList());            
        }

        // GET: Pets/Doar Pet
        [Authorize]
        public ActionResult Index()
        {
            string userId = User.Identity.GetUserId();

            db.Pets.Where(p => p.userId == userId).ToList();
            var pet = db.Pets.Include(p => p.Especie);
            return View(pet.Where(p => p.userId == userId).ToList());
        }

        // GET: Pets/Details/5
        [Authorize]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pet pet = db.Pets.Find(id);
            if (pet == null)
            {
                return HttpNotFound();
            }
            return View(pet);
        }

        // GET: Pets/Create
        [Authorize]
        public ActionResult Create()
        {
            ViewBag.EspecieId = new SelectList(db.Especies, "EspecieId", "Nome");
            return View();
        }

        // POST: Pets/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PetId,EspecieId,NomeDoPet,Descricao,NomeDoDoador,Telefone,Email,Cidade,userId")] Pet pet, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                pet.userId = User.Identity.GetUserId();               
                db.Pets.Add(pet);            

                if (file != null)
                {
                    String[] strName = file.FileName.Split('.');
                    String strExt = strName[strName.Count() - 1];
                    string pathSave = String.Format("{0}{1}.{2}", Server.MapPath("~/Imagens/"), pet.PetId, strExt);
                    String pathBase = String.Format("/Imagens/{0}.{1}", pet.PetId, strExt);
                    file.SaveAs(pathSave);
                    pet.Foto = pathBase;
                    db.SaveChanges();
                }

                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.EspecieId = new SelectList(db.Especies, "EspecieId", "Nome", pet.EspecieId);
            return View(pet);
        }

        // GET: Pets/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pet pet = db.Pets.Find(id);
            if (pet == null)
            {
                return HttpNotFound();
            }
            ViewBag.EspecieId = new SelectList(db.Especies, "EspecieId", "Nome", pet.EspecieId);
            return View(pet);
        }

        // POST: Pets/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PetId,EspecieId,NomeDoPet,Descricao,NomeDoDoador,Telefone,Email,Cidade,userId")] Pet pet, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                pet.userId = User.Identity.GetUserId();
                db.Pets.Add(pet);
                db.Entry(pet).State = EntityState.Modified;
                db.SaveChanges();
                if (file != null)
                {
                    if (pet.Foto != null)
                    {
                        if (System.IO.File.Exists(Server.MapPath("~/" + pet.Foto)))
                        {
                            System.IO.File.Delete(Server.MapPath("~/" + pet.Foto));
                        }
                    }
                    String[] strName = file.FileName.Split('.');
                    String strExt = strName[strName.Count() - 1];
                    string pathSave = String.Format("{0}{1}.{2}", Server.MapPath("~/Imagens/"), pet.PetId, strExt);
                    String pathBase = String.Format("/Imagens/{0}.{1}", pet.PetId, strExt);
                    file.SaveAs(pathSave);
                    pet.Foto = pathBase;
                    db.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            ViewBag.EspecieId = new SelectList(db.Especies, "EspecieId", "Nome", pet.EspecieId);
            return View(pet);
        }

        // GET: Pets/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pet pet = db.Pets.Find(id);
            if (pet == null)
            {
                return HttpNotFound();
            }
            return View(pet);
        }

        // POST: Pets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Pet pet = db.Pets.Find(id);
            db.Pets.Remove(pet);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
