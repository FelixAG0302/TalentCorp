namespace TalentCorp.Entities;

public sealed class UsuariosRole
{
    public int Id { get; set; }

    public int UsuarioId { get; set; }

    public int RolId { get; set; }

    public Role Rol { get; set; } = null!;

    public Usuario Usuario { get; set; } = null!;
}