using System;
using System.Collections.Generic;

namespace TalentCorp.Models.DB;

public partial class Candidato
{
    public int CandidatoId { get; set; }

    public string Cédula { get; set; } = null!;

    public string Nombre { get; set; } = null!;

    public string Apellido { get; set; } = null!;

    public DateTime FechaIngreso { get; set; }

    public string Departamento { get; set; } = null!;

    public string Estado { get; set; } = null!;

    public virtual ICollection<Educacion> Educacions { get; set; } = new List<Educacion>();

    public virtual ICollection<Empleado> Empleados { get; set; } = new List<Empleado>();

    public virtual ICollection<Entrevista> Entrevista { get; set; } = new List<Entrevista>();

    public virtual ICollection<ExperienciaLaboral> ExperienciaLaborals { get; set; } = new List<ExperienciaLaboral>();
}
