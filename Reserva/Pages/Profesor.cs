public class Profesor
{
    public string Especialidad { get; private set; }
    public string Nombre { get; private set; }
    public string Apellidos { get; private set; }
    public string Telefono { get; private set; }

    public Profesor(string especialidad, string nombre, string apellidos, string telefono)
    {
        this.Especialidad = especialidad;
        this.Nombre= nombre;
        this.Apellidos = apellidos;
        this.Telefono = telefono;
    }

}
