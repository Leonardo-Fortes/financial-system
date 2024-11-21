using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dima.Core.Requests.Categories
{
    public class UpdateCategoryRequest : Request
    {
        [Required(ErrorMessage = "Titulo Inválido")]
        [MaxLength(80, ErrorMessage = "O titulo deve ter até 80 caracteres")]
        public string Title { get; set; } = string.Empty;
        [Required(ErrorMessage = "Descrição Inválida")]
        public string Description { get; set; } = string.Empty;
    }
}
