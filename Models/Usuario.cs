using System.ComponentModel.DataAnnotations;

namespace CrudNpN.Models;

public class Usuario
{
    [Key]
    public int Id { get; set; }

    [Required, StringLength(120)]
    public string Nome { get; set; } = string.Empty;

    [EmailAddress, StringLength(120)]
    public string? Email { get; set; }

    [StringLength(20)]
    public string? Telefone { get; set; }

    [StringLength(20), Required]
    public string? Senha { get; set; }

}