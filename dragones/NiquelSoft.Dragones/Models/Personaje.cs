using System;
using System.Collections.Generic;
using System.Text;

namespace NiquelSoft.Dragones.Models
{
    internal class Personaje
    {
        public String Nombre { get; set; }

        public Raza Raza { get; set; }

        public List<Atributo> Atributos { get; set; } = [
            new Atributo { },
        ];
    }
}
