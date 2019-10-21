using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BACKEND.Models
{
    public partial class Categoria
    {
        public Categoria()
        {
            Eventos = new HashSet<Eventos>();
        }

        [Key]
        public int IdCategoria { get; set; }
        [Required]
        [StringLength(255)]
        public string Titulo { get; set; }

        [InverseProperty("IdCategoriaNavigation")]
        public virtual ICollection<Eventos> Eventos { get; set; }
    }
}
