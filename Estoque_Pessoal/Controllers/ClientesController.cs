﻿using System;
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
        public ActionResult Create([Bind(Include = "Id,Nome,CEP,Telefone,Login,Senha")] Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                db.ClienteSet.Add(cliente);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(cliente);
        }

        // GET: Clientes/Edit/5
        public ActionResult Edit(int? id)
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
                return RedirectToAction("Index");
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
        public ActionResult Login (LoginViewModel login)
        {
            //Cria o objeto que vai guardar o possível cliente
            Cliente c = null;
            //Se foi enviado corretamente
            if (login != null)
            {
                //Se login e senha forem diferente de nulos
                if (login.Login != "" && login.Senha != "")
                {
                    //Procura no banco um login e senha igual e retorna o usuario
                     c = db.ClienteSet.Where(l => l.Login == login.Login && l.Senha == login.Senha).FirstOrDefault();

                    //Se o cliente for encontrado
                    if (c != null)
                    {
                        //Cria cookie de autenticação
                        FormsAuthentication.SetAuthCookie(login.Login, false);
                        //Guarda nome do usuário na session
                        Session["Nome"] = c.Nome;
                        return RedirectToAction("Index", "Clientes");
                    }
                }
                //Em caso de algum erro
                ModelState.AddModelError("", "Usuário ou senha incorretos!");
                return View();
            }
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }

        public ActionResult Logoff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Clientes");
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
