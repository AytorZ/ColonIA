using System.ComponentModel.DataAnnotations.Schema;

namespace ColonIA.Models
{
    public partial class Usuario
    {
        [NotMapped]
        public string ConfirmacionContrasenna { get; set; } = null!;

    }
}
