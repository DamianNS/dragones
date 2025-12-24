using Editor.Models.Enums;

namespace Editor.Models
{
    public struct Tile
    {
        public TileTypeEnum Type { get; set; }

        public MuroEnum Muros { get; set; }
    }
}
