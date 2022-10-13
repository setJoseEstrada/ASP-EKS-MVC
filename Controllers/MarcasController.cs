using EKS.Models;
using EKS.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EKS.Controllers
{
    [ValidarUsuario]
    public class MarcasController : Controller
    {
        // GET: Marcas


        MMarcas _metodos = new MMarcas();
        public ActionResult Index()
        {
            string token = (string)Session["token"];

            List<marca> marca = _metodos.Consultar(token);

            return View(marca);
        }

        // GET: Marcas/Details/5
        public ActionResult Details(int id,string token)
        {

            string tokenn = (string)Session["token"];
            token = tokenn;

            marca marca = _metodos.Consultaruno(id, token);
            return View(marca);
        }

        // GET: Marcas/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Marcas/Create
        [HttpPost]
        public ActionResult Create(marca marcas, string token)
        {
            try
            {
                // TODO: Add insert logic here

                string tokenn =(string) Session["token"];
                token = tokenn;
                marca marcass = _metodos.Crear(marcas,token);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Marcas/Edit/5
        public ActionResult Edit(int id,string token)
        {
            string tokenn = (string)Session["token"];
            token = tokenn;

            marca marcas = _metodos.Consultaruno(id,token);

            return View(marcas);
        }

        // POST: Marcas/Edit/5
        [HttpPost]
        public ActionResult Edit(marca marcas, string token)
        {
            try
            {
                string tokenn = (string)Session["token"];
                token = tokenn;

                _metodos.Modificar(marcas,token);

                // TODO: Add update logic here


                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Marcas/Delete/5
        public ActionResult Delete(int id)
        {
            marca marcas = new marca();
            string token = (string)Session["token"];
            marcas = _metodos.Consultaruno(id,token);
            return View(marcas);
        }

        // POST: Marcas/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                string token = (string)Session["token"];
                _metodos.Eliminar(id,token);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
