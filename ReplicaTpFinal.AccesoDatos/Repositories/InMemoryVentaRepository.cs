using ReplicaTpFinal.Dominio.Models;

namespace ReplicaTpFinal.AccesoDatos.Repositories
{
    public class InMemoryVentaRepository : IVentaRepository
    {
        private List<Venta> Ventas = new()
        {
        };

        public List<Venta> EliminarVenta(int id)
        {
            foreach (var venta in Ventas)
            {
                if (venta.Id == id)
                {
                    Ventas.Remove(venta);
                }
            }
            return Ventas;
        }

        public Venta GetVenta(int id)
        {
            Venta ventaResult = null;

            foreach (var venta in Ventas)
            {
                if (venta.Id == id)
                {
                    ventaResult = venta;
                }
            }

            return ventaResult;
        }

        public List<Venta> GetAllVentas()
        {
            return Ventas;
        }

        public List<Venta> InsertarVenta(Venta venta)
        {
            Ventas.Add(venta);

            return Ventas;
        }

        public List<Venta> GetVentasByCliente(int cliente_id)
        {
            List<Venta> ventaResult = new List<Venta>();

            foreach (var venta in Ventas)
            {
                if (venta.Cliente_id == cliente_id)
                {
                    ventaResult.Add(venta);
                }
            }

            return ventaResult;
        }

        public List<Venta> GetVentasByProducto(int producto_id)
        {
            List<Venta> ventaResult = new List<Venta>();

            foreach (var venta in Ventas)
            {
                if (venta.Product_id == producto_id)
                {
                    ventaResult.Add(venta);
                }
            }

            return ventaResult;
        }
    }
}
