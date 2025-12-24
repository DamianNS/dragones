namespace Editor.Models
{
    public class Mapa
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public Tile[,] Tiles { get; set; }
        public Mapa(int width, int height)
        {
            Width = width;
            Height = height;
            Tiles = new Tile[width, height];
        }
    }
}
