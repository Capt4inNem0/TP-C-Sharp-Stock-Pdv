using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ReplicaTpFinal.AccesoDatos.Repositories;
using ReplicaTpFinal.Dominio.Models;

namespace ReplicaTpFinal.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductoController : Controller
    {
        private SqlProductoRepository productoRepository = new();
        
        [HttpGet]
        [Route("Get")]
        public ActionResult GetProducto(int id) 
        { 
            var producto = productoRepository.GetProductoById(id);
            return producto != null ? Ok(producto) : NotFound();
        }

        [HttpGet]
        [Route("GetAll")]
        public ActionResult GetAllProductos()
        {
            var productos = productoRepository.GetProductos();
            return Ok(productos);
        }

        [HttpPost]
        [Route("InsertarProducto")]
        public ActionResult InsertarProducto([FromForm] Producto producto) 
        {
            var productos = productoRepository.InsertarProducto(producto);
            return Ok(productos);
        }

        [HttpPut]
        [Route("ModificarProducto")]
        public ActionResult ModificarProducto([FromForm] Producto producto)
        {
            var producto_existente = productoRepository.GetProductoById(producto.Id);
            if (producto_existente == null)
            {
                return NotFound();
            }
            var productos = productoRepository.ModificarProducto(producto);
            return Ok(productos);
        }

        [HttpDelete]
        [Route("EliminarProducto")]
        public ActionResult EliminarProducto(int producto_id)
        {
            var producto_existente = productoRepository.GetProductoById(producto_id);
            if (producto_existente == null)
            {
                return NotFound();
            }
            return Ok(productoRepository.EliminarProducto(producto_id));
        }


    }
}
