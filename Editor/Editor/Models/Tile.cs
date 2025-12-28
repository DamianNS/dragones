using Editor.Models.Enums;

namespace Editor.Models
{
    public class Tile
    {
        public TileTypeEnum Type { get; set; }
        public String OtherDesfinition { get; set; } = String.Empty;


        public List<DirectionEnum> Muros { get; set; } = new List<DirectionEnum>();


        public List<Aberturas> Aberrturas { get; set; } = new List<Aberturas>();
    }
}
