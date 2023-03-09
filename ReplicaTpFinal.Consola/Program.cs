using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;
using ReplicaTpFinal.AccesoDatos.Repositories;
using ReplicaTpFinal.Dominio.Models;
using System.Data;

namespace ReplicaTpFinal.Consola
{
    internal class Program
    {
        //private static SqlProductoRepository productoRepository = new();
        //private static SqlClienteRepository clienteRepository = new();
        private static InMemoryProductoRepository productoRepository = new InMemoryProductoRepository();
        private static InMemoryClienteRepository clienteRepository = new InMemoryClienteRepository();
        private static InMemoryVentaRepository puntoDeVenta = new();

        static void Main(string[] args)
        {
            MostrarMenu();
        }

        private static void MostrarMenu()
        {
            string? resp;

            do
            {
                string menu = "0: Registrar un nuevo Producto \t" + // C
                            "1: Ver lista de productos \t" +        // R
                            "2: Modificar producto \t" +            // U
                            "3: Eliminar producto \n" +             // D
                            "4: Registrar un cliente\t\t" +           // C
                            "5: Ver lista de clientes\t" +          // R
                            "6: Modificar cliente\t" +              // U
                            "7: Eliminar cliente\n" +               // D
                            "8: Registrar venta \t\t" +
                            "9: Listar ventas\t\t" +
                            "10: Salir";

                Console.WriteLine(menu);

                Console.Write("Elija Una Opcion: ");
                resp = Console.ReadLine();

                string? eleccion = Convert.ToString(resp);

                Console.WriteLine();

                Console.WriteLine($"Opcion ingresada: {eleccion} \n");
                switch (eleccion)
                {
                    case "0":   // Crea un nuevo producto
                        var producto = InputProducto();
                        if (producto != null)
                        {
                            Console.WriteLine("Registrando producto");
                            productoRepository.InsertarProducto(producto);
                            break;
                        }
                        Console.WriteLine("Error al registrar producto, abortando.");
                        break;
                    case "1":   // Lee productos
                        var productos = productoRepository.GetProductos();
                        Console.WriteLine("Listado productos disponibles:");
                        Console.WriteLine(" Id | Codigo | Nombre | Cantidad");
                        foreach (var item in productos)
                        {
                            Console.WriteLine($"{item}");
                        }
                        break;
                    case "2":
                        try
                        {
                            Console.WriteLine("Ingrese id del producto a modificar: ");
                            var id_producto_a_modificar = Convert.ToInt32(Console.ReadLine());
                            var producto_existente = productoRepository.GetProductoById(id_producto_a_modificar);
                            if (producto_existente == null)
                            {
                                Console.WriteLine("Producto no encontrado.");
                                break;
                            }
                            Console.WriteLine("Escriba los datos a modificar del producto");
                            var nuevo_producto = InputProducto();
                            productoRepository.ModificarProducto(nuevo_producto);
                            break;
                        } catch
                        {
                            Console.WriteLine("Se produjo un error durante el proceso, vuelva a intentar");
                        }
                        break;
                    case "3":
                        Console.WriteLine("Ingrese id del producto a eliminar: ");
                        try
                        { 
                            var id_producto_a_eliminar = Convert.ToInt32(Console.ReadLine());
                            var producto_eliminar = productoRepository.GetProductoById(id_producto_a_eliminar);
                            if (producto_eliminar == null)
                            {
                                Console.WriteLine("Producto no encontrado.");
                                break;
                            }
                            productoRepository.EliminarProducto(id_producto_a_eliminar);
                            break;
                        }
                        catch
                        {
                            Console.WriteLine("Se produjo un error durante el proceso, vuelva a intentar");
                        }
                        break;
                    case "4":  // Crear cliente
                        var cliente_nuevo = InputCliente(null);
                        if (cliente_nuevo != null)
                        {
                            Console.WriteLine("Registrando cliente");
                            clienteRepository.InsertarCliente(cliente_nuevo);
                            break;
                        }
                        Console.WriteLine("Error al registrar producto, abortando.");
                        break;
                    case "5":
                        var clientes = clienteRepository.GetClientes();
                        Console.WriteLine("Listado clientes disponibles:");
                        Console.WriteLine(" Id | Activo | Nombre | Fecha de creacion");
                        foreach (var item in clientes)
                        {
                            Console.WriteLine($"{item}");
                        }
                        break;
                    case "6":
                        Console.WriteLine("Ingrese id de cliente a modificar: ");
                        try
                        {
                            var id_cliente_a_modificar = Convert.ToInt32(Console.ReadLine());
                            var cliente_existente = clienteRepository.GetClienteById(id_cliente_a_modificar);
                            if (cliente_existente == null)
                            {
                                Console.WriteLine("Cliente no encontrado.");
                                break;
                            }
                            Console.WriteLine("Escriba los datos a modificar del cliente");
                            var nuevo_cliente = InputCliente(cliente_existente);
                            clienteRepository.ModificarCliente(nuevo_cliente);
                        } catch
                        {
                            Console.WriteLine("Se produjo un error durante el proceso, vuelva a intentar");
                        }
                        break;
                    case "7":
                        Console.WriteLine("Ingrese id de cliente a eliminar: ");
                        try
                        {
                            var id_cliente_a_eliminar = Convert.ToInt32(Console.ReadLine());
                            var cliente_eliminar = clienteRepository.GetClienteById(id_cliente_a_eliminar);
                            if (cliente_eliminar == null)
                            {
                                Console.WriteLine("Cliente no encontrado.");
                                break;
                            }
                            clienteRepository.EliminarCliente(id_cliente_a_eliminar);
                        } catch
                        {
                            Console.WriteLine("Se produjo un error durante el proceso, vuelva a intentar");
                        }
                        break;
                    case "8":
                        var venta = InputVenta();
                        puntoDeVenta.InsertarVenta(venta);
                        Console.WriteLine("Orden generada.");
                        break;
                    case "9":
                        var ventas = puntoDeVenta.GetAllVentas();
                        Console.WriteLine("Listado de ventas: ");

                        foreach (var item in ventas)
                        {
                            Console.WriteLine($"{item}");
                        }
                        break;
                    case "10":
                        break;
                    default:
                        Console.WriteLine("No se reconoce la opcion ingresada");
                        break;
                }
            }
            while (resp != "10");
        }

        private static Producto InputProducto()
        {
            bool error = false;
            Console.Write("Ingrese un nombre: ");
            var nombre = Console.ReadLine();
            Console.Write("Ingrese el codigo de producto: ");
            var codigo = Console.ReadLine();
            decimal cantidad = -1;
            while (!error && cantidad < 0)
            {
                try
                {
                    Console.Write("Ingrese cantidad inicial: ");
                    cantidad = Convert.ToDecimal(Console.ReadLine());
                }
                catch (Exception ex)
                {
                    var want_continue = ContinueOrExitMenu("Por favor ingrese una cantidad valida.");
                    error = !want_continue;
                }
            }


            if (!error)
            {
                var producto = new Producto
                {
                    Codigo = Convert.ToString(codigo),
                    Nombre = Convert.ToString(nombre),
                    Cantidad = cantidad
                };

                return producto;
            }
            return null;        
        }

        private static Cliente InputCliente(Cliente? cliente_a_modificar)
        {

            bool error = false;
            Console.Write("Ingrese un nombre: ");
            var nombre = "";

            while (nombre.Length <= 0)
            {
                nombre = Convert.ToString(Console.ReadLine());
                if (nombre.Length <= 0)
                {
                    error = !ContinueOrExitMenu("Debe ingresar un nombre");
                }
            }

            if (!error)
            {
                var fecha_creacion = DateTime.Now;
                var id = 0;

                if (cliente_a_modificar != null)
                {   
                    fecha_creacion = cliente_a_modificar.FechaCreacion;
                    id = cliente_a_modificar.Id;
                }
                
                var cliente = new Cliente
                {
                    Id = id,
                    Activo = true,
                    Nombre = Convert.ToString(nombre),
                    FechaCreacion = fecha_creacion
                };

                return cliente;
            }
            return null;
        }

        private static Venta? InputVenta()
        {
            var error = false;
            Producto producto = null;
            while (producto == null && !error)
            {
                Console.Write("Ingrese nombre de producto: ");
                var nombre = Console.ReadLine();
                producto = productoRepository.GetProducto(nombre);
                if (producto == null)
                {
                    var want_continue = ContinueOrExitMenu("El producto ingresado no existe.");
                    error = !want_continue;
                }
                else
                {
                    Console.WriteLine($"Quedan {producto.Cantidad}u disponibles de este producto.");
                }
            }

            Cliente cliente = null;
            while (cliente == null && !error)
            {
                Console.Write("Ingrese nombre del cliente: ");
                var nombre = Console.ReadLine();
                cliente = clienteRepository.GetCliente(nombre);
                if (cliente == null)
                {
                    var want_continue = ContinueOrExitMenu("El cliente ingresado no existe.");
                    error = !want_continue;
                }
            }

            decimal cantidad = -1;
            do
            {
                try
                {
                    Console.Write("Ingrese cantidad: ");
                    cantidad = Convert.ToDecimal(Console.ReadLine());
                    if (cantidad <= 0)
                    {
                        throw new Exception("Cantidad invalida");
                    }
                    if (producto.Cantidad < cantidad)
                    {
                        var want_continue = ContinueOrExitMenu("No hay suficientes existencias en stock, ingrese una cantidad valida.");
                        error = !want_continue;
                    }
                }
                catch (Exception ex)
                {
                    var want_continue = ContinueOrExitMenu("Por favor ingrese una cantidad valida.");
                    error = !want_continue;
                }
            } while (error || cantidad <= 0 || producto.Cantidad < cantidad);


            decimal total = -1;
            do
            {
                try
                {

                    Console.Write("Ingrese importe total: ");
                    total = Convert.ToDecimal(Console.ReadLine());
                }
                catch (Exception ex)
                {
                    var want_continue = ContinueOrExitMenu("Por favor ingrese un numero valido.");
                    error = !want_continue;
                }
            } while (error || total <= 0);

            if (!error)
            {
                Venta venta = new Venta { 
                    Product_id = producto.Id,
                    Cliente_id = cliente.Id,
                    Cantidad = cantidad,
                    Fecha = DateTime.Now,
                    Importe = total
                };

                producto.Cantidad -= venta.Cantidad;
                productoRepository.ModificarProducto(producto);

                return venta;
            }
            return null;
        }

        private static bool ContinueOrExitMenu(string message)
        {
            Console.WriteLine($"{message} Presione 0 para reintentar, 1 para salir.");
            var resp = Convert.ToString(Console.ReadLine());
            return resp == "0";
        }
    }
}