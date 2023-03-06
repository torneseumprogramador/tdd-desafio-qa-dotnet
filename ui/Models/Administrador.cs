using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace tdd_desafio_qa_dotnet.Models;

public class Administrador
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [MaxLength(100)]
    public string Nome { get; set; } = default!;

    [MaxLength(160)]
    public string Email { get; set; } = default!;

    [MaxLength(20)]
    public string Senha { get; set; } = default!;
}