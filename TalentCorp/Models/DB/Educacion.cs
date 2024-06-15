using System;
using System.Collections.Generic;

namespace TalentCorp.Models.DB;

public partial class Educacion
{
    public int EducacionId { get; set; }

    public int CandidatoId { get; set; }

    public string Nivel { get; set; } = null!;

    public string Institucion { get; set; } = null!;

    public string? Idiomas { get; set; }

    public DateTime FechaDesde { get; set; }

    public DateTime FechaHasta { get; set; }

    public virtual Candidato Candidato { get; set; } = null!;
}
