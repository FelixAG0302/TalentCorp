namespace TalentCorp.Entities;

public sealed class Educacion
{
    public int Id { get; set; }

    public int CandidatoId { get; set; }

    public string Nivel { get; set; } = null!;

    public string Institucion { get; set; } = null!;

    public string Idiomas { get; set; } = null!;

    public DateTime FechaDesde { get; set; }

    public DateTime FechaHasta { get; set; }

    public Candidato? Candidato { get; set; }
}