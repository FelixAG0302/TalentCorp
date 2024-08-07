namespace TalentCorp.Entities;

public sealed class Empleado
{
    public int Id { get; set; }

    public string Cedula { get; set; } = null!;

    public string Nombre { get; set; } = null!;

    public string Apellido { get; set; } = null!;

    public DateTime FechaIngreso { get; set; }

    public string Estado { get; set; } = null!;

    public int PuestoId { get; set; }

    public Puesto? Puesto { get; set; }

    public Empleado()
    {
    }

    public Empleado(string cedula, string nombre, string apellido, DateTime fechaIngreso,
        string estado, int puestoId, Puesto puesto)
    {
        Cedula = cedula;
        Nombre = nombre;
        Apellido = apellido;
        FechaIngreso = fechaIngreso;
        Estado = estado;
        PuestoId = puestoId;
        Puesto = puesto;
    }
}