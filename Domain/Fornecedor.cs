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
    [Table("Fornecedores")]
    public class Fornecedor
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Nome do fornecedor:")]
        [Required(ErrorMessage = "Campo obrigatório!")]
        public string Nome { get; set; }

        [Display(Name = "Produtos do fornecedor:")]
        [Required(ErrorMessage = "Campo obrigatório!")]
        public List<Produto> Produtos { get; set; }
    }
}
