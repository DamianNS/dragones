using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace NiquelSoft.Dragones.Storage
{
    [Table("Libros")]
    public class Libro
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        [Required]
        public int Id { get; set; }

        [MaxLength(200)]
        [Required]
        public string Nombre { get; set; } = null!;

        [MaxLength(200)]
        public string? Autor { get; set; }

        [MaxLength(4000)]
        public string? Descr { get; set; }
    }
}
