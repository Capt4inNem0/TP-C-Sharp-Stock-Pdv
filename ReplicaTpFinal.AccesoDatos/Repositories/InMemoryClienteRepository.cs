using ReplicaTpFinal.Dominio.Models;

namespace ReplicaTpFinal.AccesoDatos.Repositories
{
    public class InMemoryClienteRepository : IClienteRepository
    {
        private List<Cliente> Clientes = new()
        {
            new Cliente
            {
                Id = 0,
                Activo = true,
                Nombre = "John Doe",
                FechaCreacion = new DateTime(2021, 06, 24, 20, 09, 00)
            },
            new Cliente
            {
                Id = 1,
                Activo = false,
                Nombre = "Erica Doe",
                FechaCreacion = new DateTime(2020, 09, 20, 23, 06, 00)
            },
        };

        public List<Cliente> EliminarCliente(int id_cliente)
        {
            foreach (var cliente in Clientes)
            {
                if (cliente.Id == id_cliente)
                {
                    Clientes.Remove(cliente);
                }
            }
            return Clientes;
        }

        public Cliente GetCliente(string nombre)
        {
            Cliente clienteResult = null;

            // Opcion 1
            foreach (var cliente in Clientes)
            {
                if (cliente.Nombre == nombre)
                {
                    clienteResult = cliente;
                }
            }

            return clienteResult;
        }

        public List<Cliente> GetClientes()
        {
            return Clientes;
        }

        public List<Cliente> ModificarCliente(Cliente cliente)
        {

            Cliente clienteEncontrado = null;

            foreach (var c in Clientes)
            {
                if (c.Id == cliente.Id)
                {
                    clienteEncontrado = c;
                    break;
                }
            }

            if (clienteEncontrado != null) 
            {
                Clientes.Remove(clienteEncontrado);
                Clientes.Add(cliente);
            }

            return Clientes;
        }

        public List<Cliente> InsertarCliente(Cliente cliente)
        {
            var max_id = 0;
            foreach (var item in Clientes)
            {
                max_id = Math.Max(max_id, item.Id);
            }
            cliente.Id = max_id + 1;
            Clientes.Add(cliente);

            return Clientes;
        }

        public Cliente GetClienteById(int id)
        {
            Cliente clienteResult = null;

            // Opcion 1
            foreach (var cliente in Clientes)
            {
                if (cliente.Id == id)
                {
                    clienteResult = cliente;
                }
            }

            return clienteResult;
        }
    }
}
