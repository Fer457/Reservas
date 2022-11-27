using Newtonsoft.Json;

public class Aula
{
    public string Nombre { get; private set; }
    public string Ubicacion { get; private set; }
    public string Planta { get; private set; }
    public string Capacidad { get; private set; }
    public int id { get; private set; }

    [JsonConstructor]
    public Aula(string nombre, string ubicacion, string planta, string capacidad, int id)
    {
        this.Nombre = nombre;
        this.Ubicacion = ubicacion;
        this.Planta = planta;
        this.Capacidad = capacidad;
        this.id = id;
    }

    public Aula(string nombre, string ubicacion, string planta, string capacidad)
    {
        this.Nombre = nombre;
        this.Ubicacion = ubicacion;
        this.Planta = planta;
        this.Capacidad = capacidad;
    }

}
