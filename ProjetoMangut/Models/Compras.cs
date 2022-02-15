using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjetoMangut.Models
{   
   
        [Table("Compras")]
        public class Compras
        {
            public int Id { get; set; }

            [Display(Name = "Código Produto")]
            public int Idproduto { get; set; }

            [Display(Name = "Nome Produto")]
            public string NomeProduto { get; set; }

            [Display(Name = "Quantidade")]
            public int Quantidade { get; set; }
        }
}
