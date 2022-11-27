using Newtonsoft.Json;

public class Profesor
{
    public string Especialidad { get; private set; }
    public string Nombre { get; private set; }
    public string Apellidos { get; private set; }
    public string Telefono { get; private set; }
    public string Sexo { get; private set; }
    public string FechaNac { get; private set; }
    public int id { get; private set; }

    [JsonConstructor]
    public Profesor(string especialidad, string nombre, string apellidos, string telefono, string sexo, string fechanac, int id)
    {
        this.Especialidad = especialidad;
        this.Nombre = nombre;
        this.Apellidos = apellidos;
        this.Telefono = telefono;
        this.Sexo = sexo;
        this.FechaNac = fechanac;
        this.id = id;
    }

    public Profesor(string especialidad, string nombre, string apellidos, string telefono, string sexo, string fechanac)
    {
        this.Especialidad = especialidad;
        this.Nombre= nombre;
        this.Apellidos = apellidos;
        this.Telefono = telefono;
        this.Sexo = sexo;
        this.FechaNac = fechanac;
    }

}
