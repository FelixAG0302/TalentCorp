using System;
using System.Collections.Generic;

namespace TalentCorp.Models.DB;

public partial class Puesto
{
    public int PuestoId { get; set; }

    public string Nombre { get; set; } = null!;

    public string Descripción { get; set; } = null!;

    public string? NivelRiesgo { get; set; }

    public decimal SalarioMin { get; set; }

    public decimal SalarioMax { get; set; }

    public string Estado { get; set; } = null!;

    public virtual ICollection<Empleado> Empleados { get; set; } = new List<Empleado>();

    public virtual ICollection<Entrevista> Entrevista { get; set; } = new List<Entrevista>();
}
