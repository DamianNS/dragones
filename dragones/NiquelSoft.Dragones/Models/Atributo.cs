using System;
using System.Collections.Generic;
using System.Text;

namespace NiquelSoft.Dragones.Models
{
    internal class Atributo
    {
        public AtributoEnum Tipo { get; set; }
        public int Valor => BaseValor + Modificadores;
        public int BaseValor { get; set; }

        public int Modificadores { get; set; }


    }
}
