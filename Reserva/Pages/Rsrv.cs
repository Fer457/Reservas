using Newtonsoft.Json;

public class Rsrv
{
    public string FechaYHora { get; private set; }
    public string Profesor { get; private set; }
    public string Aula { get; private set; }
    public int id { get; private set; }

    [JsonConstructor]
    public Rsrv(string fechayhora, string profesor, string aula, int id)
    {
        this.FechaYHora = fechayhora;
        this.Profesor = profesor;
        this.Aula = aula;
        this.id = id;
    }

    public Rsrv(string fechayhora, string profesor, string aula)
    {
        this.FechaYHora = fechayhora;
        this.Profesor = profesor;
        this.Aula = aula;
    }

}
