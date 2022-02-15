using System.ComponentModel.DataAnnotations;

namespace ProjetoMangut.Models
{
    public class Categoria
    {
        [Key]
        public int IdCategoria { get; set; }
        public string Tipo { get; set; }
        //public List<Produto> Produtos { get; set; }
    }
}
