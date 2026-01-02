using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace NiquelSoft.Dragones.Web.Modulos.Personajes
{
    public partial class CrearPersonaje : ComponentBase
    {
        private readonly Random _rng = new();

        protected PersonajeModel Model { get; set; } = new PersonajeModel();

        protected string[] Razas { get; } = new[] { "Humano", "Elfo", "Enano", "Mediano", "Orco", "Tiefling" };
        protected string[] Clases { get; } = new[] { "Guerrero", "Pícaro", "Mago", "Clérigo", "Bárbaro", "Bardo" };
        protected string[] Alineamientos { get; } = new[]
        {
            "Legal Bueno", "Neutral Bueno", "Caótico Bueno",
            "Legal Neutral", "Neutral Verdadero", "Caótico Neutral",
            "Legal Malvado", "Neutral Malvado", "Caótico Malvado"
        };

        protected string SaveMessage { get; set; } = string.Empty;

        protected Task HandleValidSubmit()
        {
            // Aquí puedes persistir el personaje usando un servicio o API.
            SaveMessage = $"Personaje '{Model.Nombre}' guardado correctamente (simulado).";
            return Task.CompletedTask;
        }

        protected void RollAll()
        {
            Model.Fuerza = Roll4d6DropLowest();
            Model.Destreza = Roll4d6DropLowest();
            Model.Constitucion = Roll4d6DropLowest();
            Model.Inteligencia = Roll4d6DropLowest();
            Model.Sabiduria = Roll4d6DropLowest();
            Model.Carisma = Roll4d6DropLowest();
            SaveMessage = string.Empty;
        }

        protected void ApplyStandardArray()
        {
            // Standard array: 15,14,13,12,10,8
            var array = new[] { 15, 14, 13, 12, 10, 8 };
            // Asignación por defecto: fuerza..carisma
            Model.Fuerza = array[0];
            Model.Destreza = array[1];
            Model.Constitucion = array[2];
            Model.Inteligencia = array[3];
            Model.Sabiduria = array[4];
            Model.Carisma = array[5];
            SaveMessage = string.Empty;
        }

        protected void Reset()
        {
            Model = new PersonajeModel();
            SaveMessage = string.Empty;
            StateHasChanged();
        }

        private int Roll4d6DropLowest()
        {
            var rolls = Enumerable.Range(0, 4)
                .Select(_ => _rng.Next(1, 7))
                .OrderBy(x => x)
                .ToArray();
            return rolls.Skip(1).Sum();
        }

        protected class PersonajeModel
        {
            [Required(ErrorMessage = "El nombre es obligatorio")]
            public string Nombre { get; set; } = string.Empty;

            public string Raza { get; set; } = "Humano";
            public string Clase { get; set; } = "Guerrero";

            [Range(1, 20)]
            public int Nivel { get; set; } = 1;

            public string Trasfondo { get; set; } = string.Empty;
            public string Alineamiento { get; set; } = "Neutral Verdadero";

            [Range(1, 30)]
            public int Fuerza { get; set; }

            [Range(1, 30)]
            public int Destreza { get; set; }

            [Range(1, 30)]
            public int Constitucion { get; set; }

            [Range(1, 30)]
            public int Inteligencia { get; set; }

            [Range(1, 30)]
            public int Sabiduria { get; set; }

            [Range(1, 30)]
            public int Carisma { get; set; }

            public string Notas { get; set; } = string.Empty;
        }
    }
}
