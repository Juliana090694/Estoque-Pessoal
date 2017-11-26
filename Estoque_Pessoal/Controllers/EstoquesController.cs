using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Estoque_Pessoal.Models;
using Estoque_Pessoal.DAL;

namespace Estoque_Pessoal.Controllers
{
    [Authorize]
    public class EstoquesController : Controller
    {
        private ModelosContainer db = new ModelosContainer();
        private EstoqueItemDAO estoqueItemDAO = new EstoqueItemDAO();
        private EstoqueDAO estoqueDAO = new EstoqueDAO();

        // GET: Estoques
        public ActionResult Index()
        {
            return View(db.EstoqueSet.Where(x => x.Cliente.Login == User.Identity.Name).ToList());
        }

        // GET: Estoques/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Estoque estoque = db.EstoqueSet.Find(id);
            if (estoque == null)
            {
                return HttpNotFound();
            }
            return View(estoque);
        }

        // GET: Estoques/Create
        public ActionResult Create()
        {
            List<Item> lista = db.ItemSet.ToList();
            if (lista != null)
                ViewBag.lista = new SelectList(lista, "id", "nome", null);

            Estoque viewModel = new Estoque();
            viewModel.EstoqueItem = new EstoqueItem();
            viewModel.EstoqueItem.Item = new Item();
            viewModel.EstoqueItem.Quantidade = 1;
            return View(viewModel);
        }

        // POST: Estoques/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( Estoque estoque)
        {
            if (ModelState.IsValid)
            {
                estoque.Cliente = RetornarLogado();
                if (estoque.Cliente != null)
                {
                    //Verifica se já existe um item de estoque com o mesmo produto para o mesmo usuário
                    if (db.EstoqueSet.Where(x => x.Cliente.Id == estoque.Cliente.Id && x.EstoqueItem.Item.Id == estoque.EstoqueItem.Id).FirstOrDefault() == null)
                    {
                        estoque.EstoqueItem.Id = estoqueItemDAO.Add(estoque.EstoqueItem);
                        estoqueDAO.Add(estoque);
                    }
                    else
                    {
                        TempData["Error"] = "O item já existe no estoque!";
                        ModelState.AddModelError("", "O item já existe no estoque!");
                    }
                }
                else
                {
                    TempData["Error"] = "Usuário inválido!";
                    ModelState.AddModelError("", "Usuário inválido!");
                }
                
                return RedirectToAction("Index");
            }

            return View(estoque);
        }

        public Cliente RetornarLogado()
        {
            Cliente c = null;
            c = db.ClienteSet.Where(x => x.Login == User.Identity.Name).FirstOrDefault();
            return c;
        }

        // GET: Estoques/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Estoque estoque = db.EstoqueSet.Find(id);
            if (estoque == null)
            {
                return HttpNotFound();
            }
            return View(estoque);
        }

        // POST: Estoques/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id")] Estoque estoque)
        {
            if (ModelState.IsValid)
            {
                db.Entry(estoque).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(estoque);
        }

        // GET: Estoques/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Estoque estoque = db.EstoqueSet.Find(id);
            if (estoque == null)
            {
                return HttpNotFound();
            }
            return View(estoque);
        }

        // POST: Estoques/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            
            Estoque estoque = db.EstoqueSet.Find(id);
            EstoqueItem es = db.EstoqueItemSet.Find(estoque.EstoqueItem.Id);
            db.EstoqueSet.Remove(estoque);
            db.EstoqueItemSet.Remove(es);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult EditarQuantidade(int itemEstoqueId, int quantidade)
        {
            EstoqueItem estoque = db.EstoqueItemSet.Where(x => x.Id == itemEstoqueId).FirstOrDefault();
            estoque.Quantidade = quantidade;
            db.Entry(estoque).State = System.Data.Entity.EntityState.Modified;
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
