using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LaboratorioAlbum.Models
{
    public class Dic1
    {

        public List<Sticker> Falta { get; set; }
        public List<Sticker> Tiene { get; set; }
        public List<Sticker> Repetidas { get; set; }
        public List<Sticker> Completo { get; set; }

        public Dic1()
        {
            Falta = new List<Sticker>();
            Tiene = new List<Sticker>();
            Repetidas = new List<Sticker>();
            Completo = new List<Sticker>();
        }
    }
}