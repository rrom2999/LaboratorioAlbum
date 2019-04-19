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
                direccion = Server.MapPath("~/EXCEL/") + archivo.FileName;

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

                foreach (var estampa in Datos.Instancia.Diccionario1[Equipo].Completo) //Metodo para buscar con el codigo para cambiar a existente en diccionarios
                {
                    if (estampa.Codigo == int.Parse(ID))
                    {
                        estampa.Cantidad = 1;
                        estampa.Existe = true;
                        Datos.Instancia.Diccionario2[Llave] = true;
                        Datos.Instancia.Diccionario1[Equipo].Tiene.Add(estampa);
                        break;
                    }
                }
                foreach (var estampa in Datos.Instancia.Diccionario1[Equipo].Falta) //Metodo para remover en faltantes de diccionario1
                {
                    if (estampa.Codigo == int.Parse(ID))
                    {
                        Datos.Instancia.Diccionario1[Equipo].Falta.Remove(estampa);
                        break;
                    }
                }
                foreach (var estampa in Datos.Instancia.ListadoFaltantes) //Metodo
                {
                    if (estampa.Num == ID)
                    {
                        Datos.Instancia.ListadoFaltantes.Remove(estampa);
                        break;
                    }
                }
                return View();
            }else if (Datos.Instancia.Diccionario2.ContainsKey(Llave) == false)
            {
                ViewBag.Msg = "Sticker no se encuentra disponible";
                return View("Index");
            }
            else if (Datos.Instancia.Diccionario2[Llave] == true)
            {
                ViewBag.Mensaje = "El sticker ya se encuentra agregado";
                return View();
            }
            else
            {
                return View("Index");
            }
        }


        public ActionResult MostrarDiccionario(int Listado)
        {
            try
            {
                if (Listado == 1)
                {
                    ViewBag.Msg = "FALTANTES:";
                    return View(Datos.Instancia.Diccionario1[key].Falta);
                }
                else if (Listado == 2)
                {
                    ViewBag.Msg = "OBTENNIDAS";
                    return View(Datos.Instancia.Diccionario1[key].Tiene);
                }
                else if (Listado == 3)
                {
                    ViewBag.Msg = "REPETIDAS";
                    return View(Datos.Instancia.Diccionario1[key].Repetidas);
                }
                return View();
            }
            catch
            {
                return View();
            } 
        }
    }
}