using System;
using System.Collections.Generic;
using System.Text;

namespace NiquelSoft.Dragones.Models
{
    public class Raza
    {
        public String Nombre { get; set; }
        public List<Atributo> ModificadoresAtributos { get; set; } = new List<Atributo>();
    }
}
