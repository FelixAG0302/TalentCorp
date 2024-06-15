namespace TalentCorp.Entities;

public class UsuariosRole
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public int RoleId { get; set; }

    public virtual Role Role { get; set; } = null!;

    public virtual Usuario User { get; set; } = null!;
}