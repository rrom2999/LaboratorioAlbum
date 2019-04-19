using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LaboratorioAlbum.Models;
using System.IO;

namespace LaboratorioAlbum.S
{
    public class Datos
    {
        private static Datos instancia = null;
        public static Datos Instancia
        {
            get
            {
                if (instancia == null)
                {
                    instancia = new Datos();
                }
                return instancia;
            }
        }

        public Dictionary<string, Dic1> Diccionario1 = new Dictionary<string, Dic1>();
        public Dictionary<string, bool> Diccionario2 = new Dictionary<string, bool>();
        public List<string> ListadoEquipos = new List<string>();
        public List<Dic2> ListadoFaltantes = new List<Dic2>();

        public void LlenadodeAlbum(string Direccion)
        {
            string[] lineas = File.ReadAllLines(Direccion);
            int C = 0;

            foreach (var linea in lineas)
            {
                Dic1 Informacion = new Dic1();
                if (C > 0)
                {
                    string[] InfoSeparada = linea.Split(';');
                    for (int i = 12; i < 23; i++)
                    {
                        Dic2 dato = new Dic2();


                        Sticker sticker1 = new Sticker();
                        Sticker sticker2 = new Sticker(); //Para repetidas
                        Sticker sticker3 = new Sticker(); //Por equipo

                        //Los mismos datos para sticker 1 , 2 , 3 para luego agregar segun el caso
                        sticker1.Cantidad = int.Parse(InfoSeparada[i]);
                        sticker2.Cantidad = int.Parse(InfoSeparada[i]);
                        sticker3.Cantidad = int.Parse(InfoSeparada[i]);
                        sticker1.Codigo = int.Parse(InfoSeparada[i - 11]);
                        sticker2.Codigo = int.Parse(InfoSeparada[i - 11]); 
                        sticker3.Codigo = int.Parse(InfoSeparada[i - 11]);



                        string llave1 = $"{InfoSeparada[0]}"; //Llave para 1er diccionario
                        string llave2 = $"{InfoSeparada[0]}{"|"}{InfoSeparada[i - 11]}"; //Llave para 2o diccionario
                        if (sticker1.Cantidad == 0)
                        {
                            sticker1.Existe = false;
                            sticker3.Existe = false;

                            Informacion.Falta.Add(sticker1);
                            Diccionario2.Add(llave2, sticker1.Existe);
                            dato.Equipo = InfoSeparada[0];
                            dato.Num = InfoSeparada[i - 11];
                            ListadoFaltantes.Add(dato); //Para lista de faltantes
                        }
                        else if (sticker1.Cantidad == 1)
                        {
                            sticker1.Existe = true;
                            sticker3.Existe = true;
                            Informacion.Tiene.Add(sticker1);
                            Diccionario2.Add(llave2, sticker1.Existe);
                        }
                        else if (int.Parse(InfoSeparada[i]) > 1)
                        {
                            sticker1.Existe = true;
                            sticker2.Existe = true;
                            sticker3.Existe = true;
                            sticker2.Cantidad--;
                            sticker1.Cantidad = 1; 
                            Informacion.Tiene.Add(sticker1);
                            Informacion.Repetidas.Add(sticker2);
                            Diccionario2.Add(llave2, sticker1.Existe);
                        }
                        Informacion.Completo.Add(sticker3);

                    }
                    ListadoEquipos.Add(InfoSeparada[0]);
                    Diccionario1.Add(InfoSeparada[0], Informacion);
                }
                else { C++; }
            
            }
        }

    }
}