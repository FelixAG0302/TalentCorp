using System;
using System.Collections.Generic;

namespace TalentCorp.Models.DB;

public partial class ExperienciaLaboral
{
    public int ExperienciaId { get; set; }

    public int CandidatoId { get; set; }

    public string Empresa { get; set; } = null!;

    public string PuestoOcupado { get; set; } = null!;

    public DateTime FechaDesde { get; set; }

    public DateTime FechaHasta { get; set; }

    public decimal Salario { get; set; }

    public virtual Candidato Candidato { get; set; } = null!;
}
