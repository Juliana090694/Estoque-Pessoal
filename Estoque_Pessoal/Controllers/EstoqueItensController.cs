using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Estoque_Pessoal.Models;

namespace Estoque_Pessoal.Controllers
{
    public class EstoqueItensController : Controller
    {
        private ModelosContainer db = new ModelosContainer();

        // GET: EstoqueItens
        public ActionResult Index()
        {
            return View(db.EstoqueItemSet.ToList());
        }

        // GET: EstoqueItens/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EstoqueItem estoqueItem = db.EstoqueItemSet.Find(id);
            if (estoqueItem == null)
            {
                return HttpNotFound();
            }
            return View(estoqueItem);
        }

        // GET: EstoqueItens/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: EstoqueItens/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Quantidade")] EstoqueItem estoqueItem)
        {
            if (ModelState.IsValid)
            {
                db.EstoqueItemSet.Add(estoqueItem);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(estoqueItem);
        }

        // GET: EstoqueItens/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EstoqueItem estoqueItem = db.EstoqueItemSet.Find(id);
            if (estoqueItem == null)
            {
                return HttpNotFound();
            }
            return View(estoqueItem);
        }

        // POST: EstoqueItens/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Quantidade")] EstoqueItem estoqueItem)
        {
            if (ModelState.IsValid)
            {
                db.Entry(estoqueItem).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index","Estoques");
            }
            return View(estoqueItem);
        }

        // GET: EstoqueItens/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EstoqueItem estoqueItem = db.EstoqueItemSet.Find(id);
            if (estoqueItem == null)
            {
                return HttpNotFound();
            }
            return View(estoqueItem);
        }

        // POST: EstoqueItens/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            EstoqueItem estoqueItem = db.EstoqueItemSet.Find(id);
            db.EstoqueItemSet.Remove(estoqueItem);
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
