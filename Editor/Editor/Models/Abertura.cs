using Editor.Models.Enums;

namespace Editor.Models
{
    public class Abertura
    {
        public DirectionEnum Direccion { get; set; }
        public TypeAberturaEnum Tipo { get; set; }

        
        public Abertura Clone() => (Abertura)this.MemberwiseClone();
    }
}