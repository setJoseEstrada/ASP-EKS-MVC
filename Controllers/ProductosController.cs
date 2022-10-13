using EKS.Models;
using EKS.Security;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EKS.Controllers
{
    [ValidarUsuario]
    public class ProductosController : Controller
    {
        // GET: Productos
        MProductos _metodos = new MProductos();
        public ActionResult Index()
        {
            string token = (string)Session["token"];

          
            List<productos> productos = _metodos.Consultar(token);
            return View(productos);
        }

        // GET: Productos/Details/5
        public ActionResult Details(int id,string token)
        {
            string tokenn = (string)Session["token"];
            token = tokenn;

            productos productos = _metodos.Consultaruno(id,token);

            return View(productos);
        }

        // GET: Productos/Create
        public ActionResult Create()
        {
            string token = (string)Session["token"];


            MMarcas mMarcas = new MMarcas();
            List<marca> list = mMarcas.Consultar(token);
            ViewBag.Vmarcas = list;

            return View();
        }

        // POST: Productos/Create
        [HttpPost]
        public ActionResult Create(productos objproductos, string token)
        {
            try
            {
                // TODO: Add insert logic here
                string tokenn = (string)Session["token"];
                token=tokenn;


                MMarcas mMarcas = new MMarcas();
                List<marca> list = mMarcas.Consultar(token);
                ViewBag.Vmarcas = list;

                productos productos =_metodos.Crear(objproductos,token);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Productos/Edit/5
        public ActionResult Edit(int id,string token)
        {
            string tokenn = (string)Session["token"];
            token = tokenn;

            MMarcas mMarcas = new MMarcas();
            List<marca> list = mMarcas.Consultar(token);
            ViewBag.Vmarcas = list;
            productos productos   =_metodos.Consultaruno(id,token);

            return View(productos);
        }

        // POST: Productos/Edit/5
        [HttpPost]
        public ActionResult Edit(productos objpro, string token)
        {
            try
            {
                // TODO: Add update logic here
                string tokenn = (string)Session["token"];
                token = tokenn;

                MMarcas mMarcas = new MMarcas();
                List<marca> list = mMarcas.Consultar(token);
                ViewBag.Vmarcas = list;
                _metodos.Modificar(objpro,token);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Productos/Delete/5
        public ActionResult Delete(int id)
        {
            productos productos = new productos();
            string token = (string)Session["token"];
            productos = _metodos.Consultaruno(id,token);
            return View(productos);
        }

        // POST: Productos/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {   string token = (string)Session["token"];
                // TODO: Add delete logic here
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
