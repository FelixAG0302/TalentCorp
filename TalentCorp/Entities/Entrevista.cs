namespace TalentCorp.Entities;

public class Entrevista
{
    public int Id { get; set; }

    public int CandidatoId { get; set; }

    public int PuestoId { get; set; }

    public DateTime FechaEntrevista { get; set; }

    public virtual Candidato Candidato { get; set; } = null!;

    public virtual Puesto Puesto { get; set; } = null!;
}