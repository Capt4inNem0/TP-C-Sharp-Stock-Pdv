using ReplicaTpFinal.Dominio.Models;

namespace ReplicaTpFinal.AccesoDatos.Repositories
{
    public interface IProductoRepository
    {
        Producto GetProducto(string nombre);
        Producto GetProductoById(int id_producto);
        List<Producto> GetProductos();
        List<Producto> EliminarProducto(int id_producto);
        List<Producto> ModificarProducto(Producto producto);
        List<Producto> InsertarProducto(Producto producto);
    }
}
