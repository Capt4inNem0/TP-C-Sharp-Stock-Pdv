using ReplicaTpFinal.Dominio.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReplicaTpFinal.AccesoDatos.Repositories
{
    public class InMemoryPersonaRepository : IPersonaRepository
    {
        private List<Persona> Personas = new()
        {
            new Persona
            {
                Altura = 1.76m,
                Nombre = "Leo",
                Apellido = "Ferreyra",
                Ciudad = "Cordoba",
                Edad = 30,
                Email = "leo.e.ferreyra@gmail.com",
                Nacionalidad = "Argentina",
                FechaDeNacimiento = DateTime.Now//new DateTime(2022, 01, 17, 19, 09, 00)
            },
            new Persona()
            {
                Altura = 1.46m,
                Nombre = "Luz",
                Apellido = "Gomez",
                Ciudad = "Cordoba",
                Edad = 32,
                Email = "luz@gmail.com",
                Nacionalidad = "Argentina",
                FechaDeNacimiento = DateTime.Now
            }
        };

        public List<Persona> EliminarPersona(string nombre)
        {
            // Opcion 1
            foreach (var persona in Personas)
            {
                if (persona.Nombre == nombre)
                {
                    Personas.Remove(persona);
                }
            }

            return Personas;

            // Opcion 2 (LINQ)
            //Personas.RemoveAll(p => p.Nombre == nombre);
            //return Personas;
        }

        public Persona GetPersona(string nombre)
        {
            Persona personaResult = null;

            // Opcion 1
            foreach (var persona in Personas)
            {
                if (persona.Nombre == nombre)
                {
                    personaResult = persona;
                }
            }

            return personaResult;

            // Opcion 2 (LINQ)
            //return Personas.FirstOrDefault(p => p.Nombre == nombre);
        }

        public List<Persona> GetPersonas()
        {
            return Personas;
        }

        public List<Persona> ModificarPersona(Persona persona)
        {
            // Opcion 1
            Persona personaEncontrada = null;

            foreach (var p in Personas)
            {
                if (p.Nombre == persona.Nombre)
                {
                    personaEncontrada = p;
                    break;
                }
            }

            if (personaEncontrada != null) 
            {
                Personas.Remove(personaEncontrada);

                return InsertarPersona(persona);
            }

            return Personas;
        }

        public List<Persona> InsertarPersona(Persona persona)
        {
            Personas.Add(persona);

            return Personas;
        }
    }
}
