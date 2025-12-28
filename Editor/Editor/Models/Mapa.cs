namespace Editor.Models
{
    public class Mapa
    {
        public string Nombre { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public Tile[,] Tiles { get; set; }
        public Mapa(int width, int height)
        {
            Width = width;
            Height = height;
            Tiles = new Tile[width, height];
        }
        public Dictionary<int,Tile> ExampleTiles { get; set; } = new Dictionary<int,Tile>();
    }
}
