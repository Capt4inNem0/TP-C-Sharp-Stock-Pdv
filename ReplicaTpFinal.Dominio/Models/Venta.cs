namespace ReplicaTpFinal.Dominio.Models
{
    public class Venta
    {
        public int Id { get; set; }
        public int Product_id { get; set; }
        public int Cliente_id { get; set; }
        public decimal Cantidad { get; set; }
        public DateTime Fecha { get; set; }
        public decimal Importe { get; set; }

        public override string ToString()
        {
            return $"{Id} | {Fecha} | Id Cliente: {Cliente_id} | Id Producto: {Product_id} | {Cantidad}u | Total: ${Importe}";
        }
    }
}
