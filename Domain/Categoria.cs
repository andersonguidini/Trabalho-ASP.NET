using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Categoria
    {
        [Key]
        public int Id { get; set; }
        
        [Display(Name = "Nome da categoria:")]
        [Required(ErrorMessage = "Campo obrigatório!")]
        public string Nome { get; set; }
    }
}
