using System.ComponentModel.DataAnnotations;
using ToDoApp.Client.Validators;

namespace ToDoApp.Client.Models
{
    public class BaseModel
    {
        [Required(ErrorMessage = "El titulo es requerido")]
        [StringLength(80, ErrorMessage = "El titulo debe ser igual o menor a 80 caracteres")]
        [UniqueValue]
        public required string Title { get; set; }
    }
}
