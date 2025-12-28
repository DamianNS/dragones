using Editor.Models;

namespace Editor.Servicios
{
    public class FileService
    {
        string path = Path.Combine(Directory.GetCurrentDirectory(), "Mapas");

        public List<Mapa> getMapas()
        {
            if(path == null) throw new ArgumentNullException("path");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            var archivos = Directory.GetFiles(path, "*.mapa");
            var mapas = new List<Mapa>();
            var options = new System.Text.Json.JsonSerializerOptions
            {
                WriteIndented = true
            };
            options.Converters.Add(new Editor.Models.Tile2DArrayConverter());
            foreach (var archivo in archivos)
            {
                var contenido = File.ReadAllText(archivo);
                var mapa = System.Text.Json.JsonSerializer.Deserialize<Mapa>(contenido, options);
                if(mapa != null)
                {
                    mapas.Add(mapa);
                }
            }
            return mapas;
        }

        internal void SaveMapa(Mapa selectedMapa)
        {
            if (path == null) throw new ArgumentNullException("path");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            var archivo = Path.Combine(path, $"{selectedMapa.Nombre}.mapa");
            var options = new System.Text.Json.JsonSerializerOptions
            {
                WriteIndented = true
            };
            options.Converters.Add(new Editor.Models.Tile2DArrayConverter());
            var contenido = System.Text.Json.JsonSerializer.Serialize(selectedMapa, options);
            File.WriteAllText(archivo, contenido);
        }
    }
}
