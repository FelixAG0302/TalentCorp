namespace TalentCorp.Entities;

public sealed class Entrevista
{
    public int Id { get; set; }

    public int CandidatoId { get; set; }

    public DateTime FechaEntrevista { get; set; }

    public Candidato? Candidato { get; set; } = null!;
}