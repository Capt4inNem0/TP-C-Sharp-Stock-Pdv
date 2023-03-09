using ReplicaTpFinal.Dominio.Models;

namespace ReplicaTpFinal.AccesoDatos.Repositories
{
    public class InMemoryProductoRepository : IProductoRepository
    {
        private List<Producto> Productos = new()
        {
            new Producto
            {
                Id = 0,
                Codigo = "COMBUS2",
                Nombre = "Nafta",
                Cantidad = 256.5m
            },
            new Producto
            {
                Id = 1,
                Codigo = "COMBUS3",
                Nombre = "Gasoil",
                Cantidad = 46.5m
            },
        };

        public List<Producto> EliminarProducto(int id_producto)
        {
            foreach (var producto in Productos)
            {
                if (producto.Id == id_producto)
                {
                    Productos.Remove(producto);
                }
            }
            return Productos;
        }

        public Producto GetProducto(string nombre)
        {
            Producto productoResult = null;

            // Opcion 1
            foreach (var producto in Productos)
            {
                if (producto.Nombre == nombre)
                {
                    productoResult = producto;
                }
            }

            return productoResult;
        }

        public Producto GetProductoById(int id_producto)
        {
            Producto productoResult = null;

            // Opcion 1
            foreach (var producto in Productos)
            {
                if (producto.Id == id_producto)
                {
                    productoResult = producto;
                }
            }

            return productoResult;
        }

        public List<Producto> GetProductos()
        {
            return Productos.OrderBy(o=>o.Id).ToList();
        }

        public List<Producto> ModificarProducto(Producto producto)
        {

            Producto productoEncontrado = null;

            foreach (var p in Productos)
            {
                if (p.Id == producto.Id)
                {
                    productoEncontrado = p;
                    break;
                }
            }

            if (productoEncontrado != null) 
            {
                Productos.Remove(productoEncontrado);

                Productos.Add(producto);
                return GetProductos();
            }

            return Productos;
        }

        public List<Producto> InsertarProducto(Producto producto)
        {
            var max_id = 0;
            foreach (var item in Productos)
            {
                max_id = Math.Max(max_id, item.Id);
            }
            producto.Id = max_id + 1;
            Productos.Add(producto);

            return Productos;
        }

    }
}
