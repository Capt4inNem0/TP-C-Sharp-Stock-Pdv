using ReplicaTpFinal.Dominio.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Net;

namespace ReplicaTpFinal.AccesoDatos.Repositories
{
    public class SqlProductoRepository : IProductoRepository
    {
        private string connectionString = "Data Source=DESKTOP-GGP2IK1;Integrated Security=True;Encrypt=false;Trust Server Certificate=true";

        public List<Producto> EliminarProducto(int id_producto)
        {
            using SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();

            var query = "DELETE FROM Productos WHERE Id = @Id";

            using var command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@Id", id_producto);

            var s = command.ExecuteNonQuery();
            connection.Close();

            return GetProductos();
        }

        public Producto GetProducto(string nombre)
        {
            string query = "SELECT * FROM Productos WHERE Nombre = @Nombre";

            using (SqlConnection connection = new(connectionString))
            {
                connection.Open();

                var command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@Nombre", nombre);

                using SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    var prodcuto = new Producto
                    {
                        Id = reader.GetInt32(nameof(Producto.Id)),
                        Codigo = reader.GetFieldValue<string>("Codigo"),
                        Nombre = reader.GetFieldValue<string>("Nombre"),
                        Cantidad = reader.GetFieldValue<decimal>("Cantidad")
                    };

                    return prodcuto;
                }
            }

            return null;
        }

        public Producto GetProductoById(int id_producto)
        {
            string query = "SELECT * FROM Productos WHERE Id = @Id";

            using (SqlConnection connection = new(connectionString))
            {
                connection.Open();

                var command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@Id", id_producto);

                using SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    var prodcuto = new Producto
                    {
                        Id = reader.GetInt32(nameof(Producto.Id)),
                        Codigo = reader.GetFieldValue<string>("Codigo"),
                        Nombre = reader.GetFieldValue<string>("Nombre"),
                        Cantidad = reader.GetFieldValue<decimal>("Cantidad")
                    };

                    return prodcuto;
                }
            }

            return null;
        }
        public List<Producto> GetProductos()
        {
            var productos = new List<Producto>();

            string query = "SELECT * FROM Productos";

            using (SqlConnection connection = new(connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand(query, connection);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var prodcuto = new Producto
                        {
                            Id = reader.GetInt32(nameof(Producto.Id)),
                            Codigo = reader.GetFieldValue<string>("Codigo"),
                            Nombre = reader.GetFieldValue<string>("Nombre"),
                            Cantidad = reader.GetFieldValue<decimal>("Cantidad")
                        };

                        productos.Add(prodcuto);
                    }
                }

                connection.Close();
            }

            return productos;
        }

        public List<Producto> InsertarProducto(Producto producto)
        {
            using SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();

            var query = "INSERT INTO Productos (Codigo, Nombre, Cantidad) VALUES (@Codigo, @Nombre, @Cantidad)";
            
            using var command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@Codigo", producto.Codigo);
            command.Parameters.AddWithValue("@Nombre", producto.Nombre);
            command.Parameters.AddWithValue("@Cantidad", producto.Cantidad);
            var s = command.ExecuteNonQuery();
            connection.Close();
            return GetProductos();
        }

        public List<Producto> ModificarProducto(Producto producto)
        {
            using SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();

            var query = "UPDATE Productos SET Codigo = @Codigo, Nombre = @Nombre, Cantidad = @Cantidad WHERE Id = @Id";

            using var command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@Id", producto.Id);
            command.Parameters.AddWithValue("@Codigo", producto.Codigo);
            command.Parameters.AddWithValue("@Nombre", producto.Nombre);
            command.Parameters.AddWithValue("@Cantidad", producto.Cantidad);
            var s = command.ExecuteNonQuery();
            connection.Close();
            return GetProductos();
        }
    }
}
