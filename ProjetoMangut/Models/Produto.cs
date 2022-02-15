using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjetoMangut.Models
{
    [Table("Produto")]
    public class Produto
    {
        [Display(Name = "Código")]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Nome produto")]
        public string NomeProduto { get; set; }

        [Required]
        [Display(Name = "Preço")]
        public decimal Preco { get; set; }

        [NotMapped]

        public IFormFile Imagem { get; set; }

        public string url { get; set; }
    }
}
