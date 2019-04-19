using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LaboratorioAlbum.Models
{
    public class Sticker
    {
        public int Codigo { get; set; }
        public int Cantidad { get; set; }
        public bool Existe { get; set; }

        public Sticker()
        {
            Codigo = 0; Cantidad = 0; Existe = false;
        }
    }
}