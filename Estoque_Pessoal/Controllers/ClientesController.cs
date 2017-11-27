using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Estoque_Pessoal.Models;
using System.Web.Security;
using Microsoft.AspNet.Identity;

namespace Estoque_Pessoal.Controllers
{
    [Authorize]
    public class ClientesController : Controller
    {
        private ModelosContainer db = new ModelosContainer();

        // GET: Clientes        
        public ActionResult Index()
        {
            return View(db.ClienteSet.ToList());
        }

        // GET: Clientes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cliente cliente = db.ClienteSet.Find(id);
            if (cliente == null)
            {
                return HttpNotFound();
            }
            return View(cliente);
        }

        [AllowAnonymous]
        // GET: Clientes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Clientes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public ActionResult Create([Bind(Include = "Id,Nome,CEP,Telefone,Login,Senha")] Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                Cliente c = db.ClienteSet.Where(x => x.Login == cliente.Login).FirstOrDefault();
                if (c == null)
                {
                    db.ClienteSet.Add(cliente);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["Error"] = "O login informado já existe!";
                }
            }

            return View(cliente);
        }

        // GET: Clientes/Edit/5
        public ActionResult Edit()
        {
            Cliente c = RetornarLogado();
            if (c == null)
            {
                return HttpNotFound();
            }
            return View(c);
        }

        // POST: Clientes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Nome,CEP,Telefone,Login,Senha")] Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cliente).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                TempData["Error"] = "Alteração realizada com sucesso!";
                return RedirectToAction("Index", "Estoques");
            }
            return View(cliente);
        }

        // GET: Clientes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cliente cliente = db.ClienteSet.Find(id);
            if (cliente == null)
            {
                return HttpNotFound();
            }
            return View(cliente);
        }

        // POST: Clientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Cliente cliente = db.ClienteSet.Find(id);
            db.ClienteSet.Remove(cliente);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Login
        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public ActionResult Login ([Bind(Include = "Login,Senha")] Cliente cliente)
        {
            //Cria o objeto que vai guardar o possível cliente
            Cliente c = null;
            //Se foi enviado corretamente
            if (cliente != null)
            {
                //Se login e senha forem diferente de nulos
                if (cliente.Login != "" && cliente.Senha != "")
                {
                    //Procura no banco um login e senha igual e retorna o usuario
                    c = db.ClienteSet.Where(l => l.Login == cliente.Login && l.Senha == cliente.Senha).FirstOrDefault();

                    //Se o cliente for encontrado
                    if (c != null)
                    {
                        //Cria cookie de autenticação
                        FormsAuthentication.SetAuthCookie(cliente.Login, false);
                        //Guarda nome do usuário na session
                       
                        Session["Nome"] = c.Nome;
                        Session["Login"] = c.Login;
                        return RedirectToAction("Index", "Estoques");
                    }
                }
                //Em caso de algum erro
                TempData["Error"] = "Usuário ou senha incorretos!";
                ModelState.AddModelError("", "Usuário ou senha incorretos!");
                return View();
            }
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }

        public ActionResult Logoff()
        {
            FormsAuthentication.SignOut();
            Session["Nome"] = null;
            Session["Login"] = null;
            return RedirectToAction("Login", "Clientes");
        }

        public Cliente RetornarLogado()
        {
            Cliente c = null;
            c = db.ClienteSet.Where(x => x.Login == User.Identity.Name).FirstOrDefault();
            return c;
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
