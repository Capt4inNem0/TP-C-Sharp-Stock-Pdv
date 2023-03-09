namespace ReplicaTpFinal.Dominio.Models
{
    public class Producto
    {
        public int Id { get; set; }
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public decimal Cantidad { get; set; }

        public override string ToString()
        {
            return $"{Id} | {Codigo} | {Nombre} | {Cantidad}";
        }
    }
}
