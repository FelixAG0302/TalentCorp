namespace TalentCorp.Entities;

public sealed class Puesto
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string Descripcion { get; set; } = null!;

    public string NivelRiesgo { get; set; } = null!;

    public decimal SalarioMin { get; set; }

    public decimal SalarioMax { get; set; }

    public string Estado { get; set; } = null!;
    
    public ICollection<Candidato> Candidatos { get; set; } = new List<Candidato>();

    public ICollection<Empleado> Empleados { get; set; } = new List<Empleado>();
}