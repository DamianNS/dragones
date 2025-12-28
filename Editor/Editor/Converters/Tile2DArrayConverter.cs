using System.Text.Json;
using System.Text.Json.Serialization;

namespace Editor.Models
{
    public class Tile2DArrayConverter : JsonConverter<Tile[,]>
    {
        public override Tile[,] Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            // Deserializar como List<List<Tile>> y convertir a Tile[,]
            var lists = JsonSerializer.Deserialize<List<List<Tile>>>(ref reader, options) ?? new List<List<Tile>>();
            int w = lists.Count;
            int h = w > 0 ? lists[0].Count : 0;
            var arr = new Tile[w, h];
            for (int i = 0; i < w; i++)
            {
                for (int j = 0; j < h; j++)
                {
                    arr[i, j] = lists[i][j];
                }
            }
            return arr;
        }

        public override void Write(Utf8JsonWriter writer, Tile[,] value, JsonSerializerOptions options)
        {
            int w = value.GetLength(0);
            int h = value.GetLength(1);

            writer.WriteStartArray();
            for (int i = 0; i < w; i++)
            {
                writer.WriteStartArray();
                for (int j = 0; j < h; j++)
                {
                    JsonSerializer.Serialize(writer, value[i, j], options);
                }
                writer.WriteEndArray();
            }
            writer.WriteEndArray();
        }
    }
}