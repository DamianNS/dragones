using Editor.Models.Enums;

namespace Editor.Models
{
    public class Tile
    {
        public TileTypeEnum Type { get; set; }
        public String OtherDesfinition { get; set; } = String.Empty;


        public List<DirectionEnum> Muros { get; set; } = new List<DirectionEnum>();


        public List<Abertura> Aberturas { get; set; } = new List<Abertura>();

        public Tile Clone()
        {
            return new Tile()
            {
                Type = this.Type,
                OtherDesfinition = this.OtherDesfinition + "",
                Muros = new List<DirectionEnum>(this.Muros),
                Aberturas = this.Aberturas.Select(a => a.Clone()).ToList()
            };
        }
    }
}
