using LuisAnrango.Controllers;
using LuisAnrango.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TDD_LuisAnrango
{
    [TestClass]
    public class UnitTestClienteController : UnitTestBase
    {
        [TestMethod]
        public async Task ObtenerCliente()
        {
            //prepacion
            string nombreBD = Guid.NewGuid().ToString();
            var contexto = CrearContext(nombreBD);

            contexto.Clientes.Add(new Cliente()
            {
                Identificacion = "1002003001",
                ClContrasenia = "1234",
                ClEstado = true,
                Nombre = "Jose Perez",
                Genero = "Masculino",
                Edad = 33,
                Direccion = "La colon",
                Telefono = "0992201122"
            });
            contexto.Clientes.Add(new Cliente()
            {
                Identificacion = "1233456442",
                ClContrasenia = "1111",
                ClEstado = true,
                Nombre = "Maria Guerra",
                Genero = "Femenina",
                Edad = 33,
                Direccion = "La Y",
                Telefono = "0983873222"
            });
            await contexto.SaveChangesAsync();

            var contextoAux = CrearContext(nombreBD);

            //Prueba
            var controlador = new ClientesController(contextoAux);
            var respuesta = await controlador.GetClientes();
            //verificacion
            var cliente = respuesta.Value;
            Assert.AreEqual(2, cliente.Count);

        }
        [TestMethod]
        public async Task ObtenerMovimiento()
        {
            //prepacion
            string nombreBD = Guid.NewGuid().ToString();
            var contexto = CrearContext(nombreBD);

            contexto.Movimientos.Add(new Movimiento()
            {
                MoNumeroCuenta = "2345677",
                MoFecha = DateTime.Now,
                MoTipoMovimiento = "Deposito",
                MoValor = 100,
                MoSaldo = 1100

            });
            contexto.Movimientos.Add(new Movimiento()
            {
                MoNumeroCuenta = "2345677",
                MoFecha = DateTime.Now,
                MoTipoMovimiento = "Deposito",
                MoValor = 100,
                MoSaldo = 1100

            });
            await contexto.SaveChangesAsync();

            var contextoAux = CrearContext(nombreBD);

            //Prueba
            var controlador = new MovimientosController(contextoAux);
            var respuesta = await controlador.GetMovimientos();
            //verificacion
            var cliente = respuesta.Value;
            Assert.AreEqual(2, cliente.Count);
        }
    }
}
