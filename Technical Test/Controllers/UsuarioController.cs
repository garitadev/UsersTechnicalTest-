using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Technical_Test.Entities;
using Technical_Test.Models;

namespace Technical_Test.Controllers
{
    public class UsuarioController : Controller
    {
        UsuarioModel model = new UsuarioModel();
        // GET: Usuario
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult ConsularUsuario(int? pos)
        {
            

            ViewBag.listaUsuarios = model.ConsultarUsuariosCombo();
            var usuario = model.mostrarInformacion(pos);
            return View(usuario);
        }
        [HttpPost]
        public JsonResult consultarUsuarioAjax(int? pos)
        {
            return Json(model.mostrarInformacion(pos));
            
        }

        [HttpPost]
        public ActionResult ActualizarUsuarioFrm(int pos)
        {
            try
            {
                var respuesta = model.mostrarInformacion(pos);
                if (respuesta != null)
                {
                    return View("ActualizarUsuario", respuesta);
                }
                else
                {
                    return View("../Shared/Error");

                }
            }
            catch (Exception)
            {

                return View("../Shared/Error");
            }
        }

        [HttpPost]
        public ActionResult ActualizarUsuario(UsuarioObj usuario)
        {
            var respuesta = model.ActualizarUsuario(usuario);

            if (respuesta != null)
            {
                return RedirectToAction("ConsularUsuario", "Usuario");
            }
            else
            {
                return View("../Shared/Error");
            }
        }

        [HttpGet]
        public ActionResult MostarDatos()
        {
            var datos = model.consultarInformacionLista();
            
            return View(datos);

        }

        [HttpPost]
        public ActionResult EliminarUsuario(int pos)
        {
            var datos = model.EliminarUsuario(pos);

            return RedirectToAction("MostarDatos", "Usuario");

        }

    }
}