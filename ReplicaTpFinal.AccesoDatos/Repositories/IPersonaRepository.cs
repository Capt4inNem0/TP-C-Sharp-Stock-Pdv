using ReplicaTpFinal.Dominio.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReplicaTpFinal.AccesoDatos.Repositories
{
    public interface IPersonaRepository
    {
        Persona GetPersona(string nombre);
        List<Persona> GetPersonas();
        List<Persona> EliminarPersona(string nombre);
        List<Persona> ModificarPersona(Persona persona);
        List<Persona> InsertarPersona(Persona persona);
    }
}
