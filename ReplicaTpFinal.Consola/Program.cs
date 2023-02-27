using ReplicaTpFinal.AccesoDatos.Repositories;
using ReplicaTpFinal.Dominio.Models;

namespace ReplicaTpFinal.Consola
{
    internal class Program
    {
        public static InMemoryPersonaRepository personasInMemory = new();

        static void Main(string[] args)
        {
            //MostrarMenu();
            var sqlRepository = new SqlPersonaRepository();

            var personas = sqlRepository.GetPersonas();
            var persona = sqlRepository.GetPersona("Pablo");

            Console.WriteLine("GET PERSONAS");
            foreach (var item in personas)
            {
                Console.WriteLine(item.ToString());
            }

            Console.WriteLine("GET Persona");
            Console.WriteLine(persona.ToString());

            var personaNueva = new Persona
            {
                Id = 100,
                Altura = 1.76m,
                Nombre = "Martin",
                Apellido = "ApellidoPrueba",
                Ciudad = "Cordoba",
                Edad = 3,
                Email = "l@gmail.com",
                Nacionalidad = "Argentina",
                FechaDeNacimiento = DateTime.Now//new DateTime(2022, 01, 17, 19, 09, 00)
            };

            personas = sqlRepository.InsertarPersona(personaNueva);

            Console.WriteLine("GET PERSONAS Nuevas");
            foreach (var item in personas)
            {
                Console.WriteLine(item.ToString());
            }
        }

        private static void MostrarMenu()
        {
            string? resp;

            do
            {
                //Aqui mostraremos los mensajes que apareceran en nuestra consola igual que el menu de seleccion.
                string menu = "1: Crear Persona \n" +
                            "2: Mostrar Persona \n" +
                            // Modificar
                            // Eliminar (string nombre)
                            "3: Mostrar todas las Personas \n" +
                            "4: Salir del programa \n";

                Console.WriteLine(menu);

                Console.Write("Eliga Una Opcion: "); //Aqui es donde indicaremos que operacion vamos a realizar
                resp = Console.ReadLine();

                string? eleccion = Convert.ToString(resp);

                Console.WriteLine(); // Linea de separacion.

                switch (eleccion)
                {
                    case "1":
                        //MostrarMenuCrearPersona();
                        Console.WriteLine($"Opcion ingresada: {eleccion} \n");
                        break;
                    case "2":
                        Console.WriteLine($"Opcion ingresada: {eleccion} \n");
                        break;
                    case "3":
                        Console.WriteLine($"Opcion ingresada: {eleccion} \n");
                        var personas = personasInMemory.GetPersonas();
                        Console.WriteLine($"Lista de personas: {eleccion} \n");
                        foreach (var persona in personas)
                        {
                            Console.WriteLine(persona.ToString());
                        }
                        Console.WriteLine($"Fin Lista de personas \n");
                        break;
                    case "4":
                        break;
                    default:
                        Console.WriteLine("No se reconoce la opcion ingresada");
                        break;
                }
            }
            while (resp != "4");
        }
    }
}