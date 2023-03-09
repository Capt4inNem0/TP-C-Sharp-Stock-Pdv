namespace ReplicaTpFinal.Dominio.Models
{
    public class Cliente
    {
        public int Id { get; set; }
        public bool Activo { get; set; }
        public string Nombre { get; set; }
        public DateTime FechaCreacion { get; set; }

        public override string ToString()
        {
            return $"{Id} | {Activo} | {Nombre} | {FechaCreacion}";
        }
    }
}
