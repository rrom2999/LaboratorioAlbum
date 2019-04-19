using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel;

namespace LaboratorioAlbum.Models
{
    public class Dic2
    {
        [DisplayName("Codigo de Sticker")]
        public string Num { get; set; }
        [DisplayName("Equipo")]
        public string Equipo { get; set; }

        public Dic2()
        {
            Equipo = "";
            Num = "";
        }
    }
}