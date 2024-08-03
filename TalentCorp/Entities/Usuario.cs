namespace TalentCorp.Entities;

public sealed class Usuario
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string Correo { get; set; } = null!;

    public string Contrasena { get; set; } = null!;

    public ICollection<UsuariosRole> UsuariosRoles { get; set; } = new List<UsuariosRole>();
}