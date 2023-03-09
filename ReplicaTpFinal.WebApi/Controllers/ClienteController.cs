using Microsoft.AspNetCore.Mvc;
using ReplicaTpFinal.AccesoDatos.Repositories;
using ReplicaTpFinal.Dominio.Models;

namespace ReplicaTpFinal.WebApi.Controllers
{
    [ApiController]
    public class ClienteController : Controller
    {

        private SqlClienteRepository clienteRepository = new();

        [HttpGet]
        [Route("Get")]
        public ActionResult GetCliente(int id)
        {
            var cliente = clienteRepository.GetClienteById(id);
            return cliente != null ? Ok(cliente) : NotFound();
        }

        [HttpGet]
        [Route("GetAll")]
        public ActionResult GetAllClientes()
        {
            var productos = clienteRepository.GetClientes();
            return Ok(productos);
        }

        [HttpPost]
        [Route("InsertarCliente")]
        public ActionResult InsertarCliente([FromForm] Cliente cliente)
        {
            var productos = clienteRepository.InsertarCliente(cliente);
            return Ok(productos);
        }

        [HttpPut]
        [Route("ModificarCliente")]
        public ActionResult ModificarCliente([FromForm] Cliente cliente)
        {
            var producto_existente = clienteRepository.GetClienteById(cliente.Id);
            if (producto_existente == null)
            {
                return NotFound();
            }
            var productos = clienteRepository.ModificarCliente(cliente);
            return Ok(productos);
        }

        [HttpDelete]
        [Route("EliminarCliente")]
        public ActionResult EliminarProducto(int cliente_id)
        {
            var producto_existente = clienteRepository.GetClienteById(cliente_id);
            if (producto_existente == null)
            {
                return NotFound();
            }
            return Ok(clienteRepository.EliminarCliente(cliente_id));
        }
    }
}
