public class Reservas
{
    public string Nombre { get; private set; }
    public string Ubicacion { get; private set; }
    public string Planta { get; private set; }
    public string Capacidad { get; private set; }

    public Reservas(string nombre, string ubicacion, string planta, string capacidad)
    {
        this.Nombre = nombre;
        this.Ubicacion = ubicacion;
        this.Planta = planta;
        this.Capacidad = capacidad;
    }

}
