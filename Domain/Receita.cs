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
    [Table("Receitas")]
    public class Receita
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Nome da receita:")]
        [Required(ErrorMessage = "Campo obrigatório!")]
        public string Nome { get; set; }

        [Display(Name = "Rendimento da receita:")]
        [Required(ErrorMessage = "Campo obrigatório!")]
        public string Rendimento { get; set; }

        [Display(Name = "Ingredientes da receita:")]
        [Required(ErrorMessage = "Campo obrigatório!")]
        public List<Produto> Ingredientes { get; set; }

        [Required(ErrorMessage = "Campo obrigatório!")]
        [Display(Name = "Tempo de preparo da receita:")]
        public int? TempoDePreparo { get; set; }
    }
}
