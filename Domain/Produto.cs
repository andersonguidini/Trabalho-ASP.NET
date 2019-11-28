using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{ 
    //Annotations ASP.NET Core
    [Table("Produtos")]
    public class Produto
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Nome do produto:")]
        [Required(ErrorMessage = "Campo obrigatório!")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Campo obrigatório!")]
        [Display(Name = "Preço do produto:")]
        public double? Preco { get; set; }

        [Required(ErrorMessage = "Campo obrigatório!")]
        [Range(1, 1000, ErrorMessage = "Quantidade apenas entre 1 e 1000!")]
        [Display(Name = "Quantidade do produto:")]
        public int? Quantidade { get; set; }

        [Required(ErrorMessage = "Campo obrigatório!")]
        [Display(Name = "Unidade de medida do produto:")]
        public string Unidade { get; set; }

        [Required(ErrorMessage = "Campo obrigatório!")]
        [Display(Name = "Prazo de validade do produto:")]
        public DateTime PrazoValidade { get; set; }
    }
}
