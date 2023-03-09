using ReplicaTpFinal.Dominio.Models;

namespace ReplicaTpFinal.AccesoDatos.Repositories
{
    public interface IVentaRepository
    {
        Venta GetVenta(int id);
        List<Venta> GetAllVentas();
        List<Venta> GetVentasByCliente(int cliente_id);
        List<Venta> GetVentasByProducto(int producto_id);
        List<Venta> InsertarVenta(Venta venta);
    }
}
