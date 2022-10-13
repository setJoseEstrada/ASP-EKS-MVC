using EKS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;


namespace EKS.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
      
        public ActionResult Index() { return View(); }
        // GET: Login

        MLogin enter = new MLogin();
        [HttpPost]
        public ActionResult Login(FormCollection form)
        {
            string correo = Convert.ToString(form["correo"]);
            string contrasena = Convert.ToString(form["contrasena"]);
            Models.Login login = new Models.Login();
            login.contra = contrasena;
            login.correo = correo;
            Respuesta res = enter.Login(login);
            if (res.Exito == 1)
            {

                Session["token"] = res.Data.token;

                return RedirectToAction("Index", "Productos");
            }
            else
            {
                res.Exito = 0;
                res.Mensaje = "Usuario o Contraseña Incorrecta";
            }


            return RedirectToAction("Index", "Login");

        }
    }
}