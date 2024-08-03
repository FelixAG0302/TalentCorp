namespace TalentCorp.Entities;

public sealed class Empleado
{
    public int Id { get; set; }

    public string Cedula { get; set; } = null!;

    public string Nombre { get; set; } = null!;

    public string Apellido { get; set; } = null!;

    public DateTime FechaIngreso { get; set; }

    public string Departamento { get; set; } = null!;

    public string Estado { get; set; } = null!;

    public int PuestoId { get; set; }

    public int CandidatoId { get; set; }

    public Candidato Candidato { get; set; } = null!;

    public Puesto Puesto { get; set; } = null!;
}