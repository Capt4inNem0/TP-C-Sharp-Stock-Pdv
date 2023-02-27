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
    public class SqlPersonaRepository : IPersonaRepository
    {
        private string connectionString = "Data Source=LEO-PC;Initial Catalog=Personas;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        public List<Persona> EliminarPersona(string nombre)
        {
            throw new NotImplementedException();
        }

        public Persona GetPersona(string nombre)
        {
            string query = "SELECT * FROM Personas WHERE Nombre = @Nombre";

            using (SqlConnection connection = new(connectionString))
            {
                connection.Open();

                var command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@Nombre", nombre);

                using SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    var persona = new Persona
                    {
                        Id = reader.GetInt32(nameof(Persona.Id)),
                        Edad = reader.GetFieldValue<int>("Edad"),
                        Nombre = reader.GetFieldValue<string>("Nombre"),
                        Altura = reader.GetFieldValue<decimal>("Altura"),
                        Email = reader.GetFieldValue<string>("email"),
                        Nacionalidad = reader.GetString(nameof(Persona.Nacionalidad)),
                        Ciudad = reader.GetString(nameof(Persona.Ciudad))
                    };

                    if (!reader.IsDBNull(reader.GetOrdinal(nameof(Persona.FechaDeNacimiento))))
                    {
                        persona.FechaDeNacimiento = reader.GetFieldValue<DateTime>(nameof(Persona.FechaDeNacimiento));
                    }

                    return persona;
                }
            }

            return null;
        }

        public List<Persona> GetPersonas()
        {
            var personas = new List<Persona>();

            string query = "SELECT * FROM Personas";

            using (SqlConnection connection = new(connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand(query, connection);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var persona = new Persona
                        {
                            Id = reader.GetInt32(nameof(Persona.Id)),
                            Edad = reader.GetFieldValue<int>("Edad"),
                            Nombre = reader.GetFieldValue<string>("Nombre"),
                            Altura = reader.GetFieldValue<decimal>("Altura"),
                            Email = reader.GetFieldValue<string>("email"),
                            Nacionalidad = reader.GetString(nameof(Persona.Nacionalidad)),
                            Ciudad = reader.GetString(nameof(Persona.Ciudad))
                        };

                        if (!reader.IsDBNull(nameof(Persona.FechaDeNacimiento)))
                        {
                            persona.FechaDeNacimiento = reader.GetFieldValue<DateTime>(nameof(Persona.FechaDeNacimiento));
                        }

                        personas.Add(persona);
                    }
                }

                connection.Close();
            }

            return personas;
        }

        public List<Persona> InsertarPersona(Persona persona)
        {
            using SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();

            var query = "INSERT INTO Personas (Id, Edad, Nombre, Apellido, Altura, Email, Nacionalidad, Ciudad) VALUES (@Id, @Edad, @Nombre, @Apellido, @Altura, @Email, @Nacionalidad, @Ciudad)";
            
            using var command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@Id", persona.Id);
            command.Parameters.AddWithValue("@Edad", persona.Edad);
            command.Parameters.AddWithValue("@Nombre", persona.Nombre);
            command.Parameters.AddWithValue("@Apellido", persona.Apellido);
            command.Parameters.AddWithValue("@Altura", persona.Altura);
            command.Parameters.AddWithValue("@Email", persona.Email);
            command.Parameters.AddWithValue("@Nacionalidad", persona.Nacionalidad);
            command.Parameters.AddWithValue("@Ciudad", persona.Ciudad);
            var s = command.ExecuteNonQuery();

            return GetPersonas();
        }

        public List<Persona> ModificarPersona(Persona persona)
        {
            throw new NotImplementedException();
        }
    }
}
