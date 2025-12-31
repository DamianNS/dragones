using Editor.Models;
using Editor.Models.Enums;
using Editor.Servicios;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Editor.Components.Pages
{
    public partial class Home
    {
        public int TileSize = 10;
        int MapWidth = 50;
        int MapHeight = 50;
        private DotNetObjectReference<Home>? objRef;
        private Dictionary<int, TileForm> Tiles = new Dictionary<int, TileForm>();
        TileTypeEnum tipoo = TileTypeEnum.Empty;
        int indice = -1;

        private List<Mapa> Mapas = new();
        private Mapa? selectedMapa;
        int CursorX = 0;
        int CursorY = 0;

        // TODO: Modificar que el mapa sea de tamaño dynamico
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                objRef = DotNetObjectReference.Create(this);
                // Pasamos el tamaño del mapa para los límites de navegación
                
                CargarMapas();
                await JS.InvokeVoidAsync("setupTileNavigation", MapWidth, MapHeight, objRef);
            }
        }

        void CargarMapas()
        {
            var f = new FileService();

            f.getMapas().ForEach(m =>
            {
                Mapas.Add(m);
            });           
            selectedMapa = Mapas.FirstOrDefault();
            onCargarMapa();
            StateHasChanged();
        }

        [JSInvokable]
        public void ExecuteZoom(int direction)
        {
            TileSize = Math.Clamp(TileSize + direction, 10, 50); ;
            StateHasChanged();
        }

        [JSInvokable]
        public void ActualizaTile(int x, int y)
        {
            var t = Tiles.FirstOrDefault(tt => tt.Value.IsChecked);
            if(t.Value != null)
            {
                var tt = t.Value.Clone();
                selectedMapa?.Tiles[x,y] = tt;
            }
            StateHasChanged();
        }

        [JSInvokable]
        public void ActualizaCursor(int x, int y)
        {
            CursorX = x;
            CursorY = y;
            StateHasChanged();
        }

        [JSInvokable]
        public void DeleteTile(int x, int y)
        {
            selectedMapa?.Tiles[x, y] = null;
            
            StateHasChanged();
        }

        public void Dispose() => objRef?.Dispose();

        public void onAgregarTile()
        {
            var key = Tiles.Count;
            var t = new TileForm()
            {
                Type = TileTypeEnum.Empty
            };
            Tiles.Add(key, t);
        }

        public void onActualizarTile(TileTypeEnum type)
        {
            if(indice < 0 || indice >= Tiles.Count) return;
            var tile = Tiles[indice];
            tile.Type = type;
        }

        public void onChangeTypeTile(ChangeEventArgs e)
        {
            // Manejar el cambio de tipo de tile si es necesario
            Console.WriteLine($"Tipo de tile cambiado a: {e.Value}");
        }

        public void onChangeCheckBox(int indice, bool valor)
        {
            // Manejar el cambio del checkbox si es necesario
            Console.WriteLine($"Checkbox cambiado a: {valor} - {indice}");


        }

        public bool vuleExpression(int index)
        {
            var tile = Tiles[index];
            tile.IsChecked = !tile.IsChecked;
            bool newValue = tile.IsChecked;
            foreach (var t in Tiles)
            {
                t.Value.IsChecked = false;
            }
            tile.IsChecked = newValue;
            indice = index;
            return tile.IsChecked;
        }

        private async Task HandleCheckboxChange(bool newValue, int index)
        {
            var x = newValue;
            indice = index;
            foreach (var t in Tiles)
            {
                t.Value.IsChecked = t.Key == index ? x : false;
            }
            StateHasChanged();
        }

        void onCargarMapa()
        {
            if (selectedMapa is null) return;
            if( selectedMapa.ExampleTiles != null)
            {
                Tiles = selectedMapa.ExampleTiles.ToDictionary(t => t.Key, t => new TileForm
                {
                    Type = t.Value.Type,
                    IsChecked = false,
                    Muros = t.Value.Muros,
                    Aberturas = t.Value.Aberturas,
                    OtherDesfinition = t.Value.OtherDesfinition
                });
            }
        }

        void nuevoMapa()
        {
            selectedMapa = new Mapa(50,50)
            {
                Nombre = "Nuevo Mapa"                
            };
            Mapas.Add(selectedMapa);
        }

        void SaveMapa()
        {            
            if (selectedMapa is null) return;
            selectedMapa.ExampleTiles = Tiles.ToDictionary(t=> t.Key, t=>(Tile)t.Value);
            //foreach (var tile in Tiles)
            //{
            //    selectedMapa.Tiles[0,0] = tile.Value;
            //}
            var f = new FileService();
            f.SaveMapa(selectedMapa);
        }

        void onPared(DirectionEnum dire)
        {
            if(indice < 0 || indice >= Tiles.Count) return;
            var t = Tiles[indice];
            if (t == null) return;
            
            if(t.Muros.Any(d=> d == dire))
            {
                t.Muros.Remove(dire);
            }
            else
            {
                t.Muros.Add(dire);
            }
            StateHasChanged();
        }

        void onAbertura(DirectionEnum dire)
        {
            if (indice < 0 || indice >= Tiles.Count) return;
            var t = Tiles[indice];
            if (t == null) return;

            if (t.Aberturas.Any(d => d.Direccion == dire))
            {
                t.Aberturas.RemoveAll(a => a.Direccion == dire);                
            }
            else
            {
                var a = new Abertura()
                {
                    Direccion = dire,
                    Tipo = TypeAberturaEnum.Door
                };
                t.Aberturas.Add(a);
            }
            StateHasChanged();
        }
    }
}
