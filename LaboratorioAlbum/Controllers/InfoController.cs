using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LaboratorioAlbum.Models;
using LaboratorioAlbum.S;

namespace LaboratorioAlbum.Controllers
{
    public class InfoController : Controller
    {
        public ActionResult Index()
        {
            return View(Datos.Instancia.ListadoEquipos);
        }

        public ActionResult Falta()
        {
            return View(Datos.Instancia.ListadoFaltantes);
        }

        static int X = 0;
        public ActionResult CargarArchivo()
        {
            if (X > 0)
            {
                ViewBag.Msg = "Error al cargar el archivo";
            }
            X++;
            return View();
        }

        [HttpPost]
        public ActionResult CargarArchivo(HttpPostedFileBase archivo)
        {
            if (archivo != null)
            {
                Subir(archivo);
                return RedirectToAction("Subir");
            }
            else
            {
                ViewBag.Msg = "ERROR AL CARGAR EL ARCHIVO, INTENTE DE NUEVO";
                return View();
            }

        }

        public ActionResult Subir(HttpPostedFileBase archivo)
        {
            string direccion = "";
            if (archivo != null &&  archivo.ContentLength > 0)
            {
                direccion = Server.MapPath("~/Upload/") + archivo.FileName;

                archivo.SaveAs(direccion);
                Datos.Instancia.LlenadodeAlbum(direccion);
                ViewBag.Msg = "Carga de archivo correcta";
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.Msg = "Error en carga de archivo";
                return RedirectToAction("CargarArchivos");
            }

        }


        static string key = "";
        public ActionResult MostrarListas(string Key)
        {
            key = Key;
            ViewBag.Nombre = Key;

            return View(Datos.Instancia.Diccionario1[Key].Completo);
        }

        public ActionResult Agregar(string ID, string Equipo)
        {
            ID = ID.Trim();
            Equipo = Equipo.Trim();

            string Llave = Equipo + "|" + ID;

            if (Datos.Instancia.Diccionario2.ContainsKey(Llave) == true && Datos.Instancia.Diccionario2[Llave] == false)
            {
                ViewBag.Mensaje = "Datos actualizados, sticker agregado.";
                ViewBag.Msg = "";

            }
        }
    }
}