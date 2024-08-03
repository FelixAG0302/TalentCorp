namespace TalentCorp.Entities;

public sealed class Role
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string Descripcion { get; set; } = null!;

    public ICollection<UsuariosRole> UsuariosRoles { get; set; } = new List<UsuariosRole>();
}