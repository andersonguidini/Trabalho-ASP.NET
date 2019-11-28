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
    [Table("ProdutosFinais")]
    public class ProdutoFinal
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Nome da receita:")]
        [Required(ErrorMessage = "Campo obrigatório!")]
        public Receita Receita { get; set; }


        [Required(ErrorMessage = "Campo obrigatório!")]
        [Display(Name = "Preço do produto final:")]
        public double? Preco { get; set; }


        [Required(ErrorMessage = "Campo obrigatório!")]
        [Display(Name = "Custo do produto final:")]
        public double? Custo { get; set; }

        [Required(ErrorMessage = "Campo obrigatório!")]
        [Display(Name = "Prazo de validade do produto:")]
        public DateTime PrazoValidade { get; set; }
    }
}
