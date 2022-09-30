using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AdoteUmAmigo.Models
{
    public class Especie
    {
        public int EspecieId { get; set; }

        [Required]
        [Display(Name = "Espécie")]
        [StringLength(50, ErrorMessage = "Digite o nome da Espécie")]
        public string Nome { get; set; }

        public ICollection<Pet> Pets { get; set; }
    }
}