using AdoteUmAmigo.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AdoteUmAmigo.Models
{    
    public class Pet
    {
        public int PetId { get; set; }

        [Required]
        [Display(Name = "Nome do Pet")]
        [StringLength(50, ErrorMessage = "Digite o nome do Pet")]
        public string NomeDoPet { get; set; }

        [Required]
        [Display(Name = "Descrição do Pet")]
        [StringLength(1000, ErrorMessage = "Digite alguma descrição do Pet")]
        public string Descricao { get; set; }

        [Required]
        [Display(Name = "Nome do Doador")]
        [StringLength(50, ErrorMessage = "Digite o nome do Doador")]
        public string NomeDoDoador { get; set; }

        [Required]
        [StringLength(20, ErrorMessage = "Digite um número de telefone para contato")]
        public string Telefone { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Digite um e-mail para contato")]
        [Display(Name = "e-mail")]
        public string Email { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Digite o nome de sua cidade")]
        public string Cidade { get; set; }

        [Display(Name = "Espécie")]
        public int EspecieId { get; set; }

        public Especie Especie { get; set; }

        public string userId { get; set; }

        public string Foto { get; set; }        
    }
}