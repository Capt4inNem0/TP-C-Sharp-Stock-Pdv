using ReplicaTpFinal.Dominio.Models;

namespace ReplicaTpFinal.AccesoDatos.Repositories
{
    public interface IClienteRepository
    {
        Cliente GetCliente(string nombre);
        List<Cliente> GetClientes();
        List<Cliente> EliminarCliente(int id_cliente);
        List<Cliente> ModificarCliente(Cliente cliente);
        List<Cliente> InsertarCliente(Cliente cliente);
        Cliente GetClienteById(int id);
    }
}
