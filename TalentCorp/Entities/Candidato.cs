namespace TalentCorp.Entities;

public sealed class Candidato
{
    public int Id { get; set; }

    public string Cédula { get; set; } = null!;

    public string Nombre { get; set; } = null!;

    public string Apellido { get; set; } = null!;

    public DateTime FechaIngreso { get; set; }

    public string Departamento { get; set; } = null!;

    public ICollection<Educacion> Educacions { get; set; } = new List<Educacion>();

    public ICollection<Empleado> Empleados { get; set; } = new List<Empleado>();

    public ICollection<Entrevista> Entrevista { get; set; } = new List<Entrevista>();

    public ICollection<ExperienciaLaboral> ExperienciaLaborals { get; set; } = new List<ExperienciaLaboral>();
}