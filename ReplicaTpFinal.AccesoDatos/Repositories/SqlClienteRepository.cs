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
    public class SqlClienteRepository : IClienteRepository
    {
        private string connectionString = "Data Source=DESKTOP-GGP2IK1;Integrated Security=True;Encrypt=false;Trust Server Certificate=true";

        public List<Cliente> EliminarCliente(int id_cliente)
        {
            using SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();

            var query = "DELETE FROM Clientes WHERE Id = @Id";

            using var command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@Id", id_cliente);

            var s = command.ExecuteNonQuery();
            connection.Close();

            return GetClientes();
        }

        public Cliente GetCliente(string nombre)
        {
            string query = "SELECT * FROM Clientes WHERE Nombre = @Nombre";

            using (SqlConnection connection = new(connectionString))
            {
                connection.Open();

                var command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@Nombre", nombre);

                using SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    var cliente = new Cliente
                    {
                        Id = reader.GetInt32(nameof(Producto.Id)),
                        Activo = reader.GetFieldValue<int>("Activo") == 1,
                        Nombre = reader.GetFieldValue<string>("Nombre")
                    };

                    if (!reader.IsDBNull(reader.GetOrdinal(nameof(Cliente.FechaCreacion))))
                    {
                        cliente.FechaCreacion = reader.GetFieldValue<DateTime>(nameof(Cliente.FechaCreacion));
                    }

                    return cliente;
                }
            }

            return null;
        }

        public Cliente GetClienteById(int id)
        {
            string query = "SELECT * FROM Clientes WHERE Id = @id";

            using (SqlConnection connection = new(connectionString))
            {
                connection.Open();

                var command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@Id", id);

                using SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    var cliente = new Cliente
                    {
                        Id = reader.GetInt32(nameof(Producto.Id)),
                        Activo = reader.GetFieldValue<int>("Activo") == 1,
                        Nombre = reader.GetFieldValue<string>("Nombre")
                    };

                    if (!reader.IsDBNull(reader.GetOrdinal(nameof(Cliente.FechaCreacion))))
                    {
                        cliente.FechaCreacion = reader.GetFieldValue<DateTime>(nameof(Cliente.FechaCreacion));
                    }

                    return cliente;
                }
            }

            return null;
        }

        public List<Cliente> GetClientes()
        {
            var clientes = new List<Cliente>();

            string query = "SELECT * FROM Clientes";

            using (SqlConnection connection = new(connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand(query, connection);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var cliente = new Cliente
                        {
                            Id = reader.GetInt32(nameof(Cliente.Id)),
                            Activo = reader.GetFieldValue<int>(nameof(Cliente.Activo)) == 1,
                            Nombre = reader.GetFieldValue<string>(nameof(Cliente.Nombre))
                        };

                        if (!reader.IsDBNull(reader.GetOrdinal(nameof(Cliente.FechaCreacion))))
                        {
                            cliente.FechaCreacion = reader.GetFieldValue<DateTime>(nameof(Cliente.FechaCreacion));
                        }

                        clientes.Add(cliente);
                    }
                }

                connection.Close();
            }

            return clientes;
        }

        public List<Cliente> InsertarCliente(Cliente cliente)
        {
            using SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();

            var query = "INSERT INTO Clientes (Activo, Nombre, FechaCreacion) VALUES (@Activo, @Nombre, @Fecha)";
            
            using var command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@Activo", cliente.Activo);
            command.Parameters.AddWithValue("@Nombre", cliente.Nombre);
            command.Parameters.AddWithValue("@Fecha", cliente.FechaCreacion);
            var s = command.ExecuteNonQuery();
            connection.Close();

            return GetClientes();
        }

        public List<Cliente> ModificarCliente(Cliente cliente)
        {
            using SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();

            var query = "UPDATE Clientes SET Activo = @Activo, Nombre = @Nombre WHERE Id = @Id";

            using var command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@Id", cliente.Id);
            command.Parameters.AddWithValue("@Activo", cliente.Activo);
            command.Parameters.AddWithValue("@Nombre", cliente.Nombre);
            var s = command.ExecuteNonQuery();
            connection.Close();
            return GetClientes();
        }
    }
}
