using System;
using System.Collections.Generic;

namespace UniDataHub.Entities;

public partial class Empleado
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

    public virtual Candidato Candidato { get; set; } = null!;

    public virtual Puesto Puesto { get; set; } = null!;
}
