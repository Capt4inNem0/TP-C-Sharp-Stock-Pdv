using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReplicaTpFinal.Dominio.Models
{
    public class Persona
    {
        public int Id { get; set; }
        public int Edad { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public decimal Altura { get; set; }
        public string Nacionalidad { get; set; }
        public string Email { get; set; }
        public string Ciudad { get; set; }
        public DateTime? FechaDeNacimiento { get; set; }

        public override string ToString()
        {
            return $"Nombre: {Nombre} - Edad: {Edad} - FechaNacimiento: {FechaDeNacimiento}";
        }
    }
}
