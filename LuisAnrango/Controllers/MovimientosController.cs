using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LuisAnrango.Context;
using LuisAnrango.Model;

namespace LuisAnrango.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovimientosController : ControllerBase
    {
        private readonly BaseContext _context;

        public MovimientosController(BaseContext context)
        {
            _context = context;
        }

        // GET: api/Movimientos
        [HttpGet]
        public async Task<ActionResult<List<Movimiento>>> GetMovimientos()
        {
            return await _context.Movimientos.ToListAsync();
        }

        // GET: api/Movimientos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Movimiento>> GetMovimiento(int id)
        {
            var movimiento = await _context.Movimientos.FindAsync(id);

            if (movimiento == null)
            {
                return NotFound();
            }

            return movimiento;
        }

        // PUT: api/Movimientos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMovimiento(int id, Movimiento movimiento)
        {
            if (id != movimiento.MoIdMovimiento)
            {
                return BadRequest();
            }

            _context.Entry(movimiento).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MovimientoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Movimientos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<Response> PostMovimiento(Movimiento oMovimiento)
        {
            Response response = new Response();
            oMovimiento.MoFecha = DateTime.Now;
            #region Validacion campos vacios
            if (oMovimiento.MoValor <= 0)
            {
                response.Message = "No se permite valores 0";
                return response;
            }
            #endregion

            Movimiento ultimoMovimiento = await _context.Movimientos.Where(x => x.MoNumeroCuenta == oMovimiento.MoNumeroCuenta).OrderByDescending(x => x.MoFecha).FirstOrDefaultAsync();

            #region Validacion de Cupos
            if (string.Equals(oMovimiento.MoTipoMovimiento, "Retiro"))
            {
                response = await ValidarCupos(oMovimiento.MoNumeroCuenta, oMovimiento.MoValor);
                if (!response.IsSuccess)
                {
                    return response;
                }
            }
            #endregion

            #region Validacion de Saldos y Registro de movimientos

            if (ultimoMovimiento.MoSaldo < Math.Abs(oMovimiento.MoValor))
            {
                response.Message = "Saldo no disponible";
            }
            else
            {
                if (string.Equals(oMovimiento.MoTipoMovimiento, "Retiro"))
                {
                    oMovimiento.MoSaldo = ultimoMovimiento.MoSaldo - oMovimiento.MoValor;
                    oMovimiento.MoValor = Math.Abs(oMovimiento.MoValor) * (-1);
                }
                else
                    oMovimiento.MoSaldo = ultimoMovimiento.MoSaldo + oMovimiento.MoValor;
                response.IsSuccess = true;
                _context.Movimientos.Add(oMovimiento);
                await _context.SaveChangesAsync();
            }
            #endregion

            response.Data= CreatedAtAction("GetPMovimiento", new { id = oMovimiento.MoIdMovimiento }, oMovimiento);
            return response;
        }

        // DELETE: api/Movimientos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMovimiento(int id)
        {
            var movimiento = await _context.Movimientos.FindAsync(id);
            if (movimiento == null)
            {
                return NotFound();
            }

            _context.Movimientos.Remove(movimiento);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MovimientoExists(int id)
        {
            return _context.Movimientos.Any(e => e.MoIdMovimiento == id);
        }

        [HttpPost("{strNumeroCuenta},{decMontoTransaccion}")]
        public async Task<Response> ValidarCupos(string strNumeroCuenta, decimal decMontoTransaccion)
        {
            Response response = new Response();
            DateTime diaActual = DateTime.Now;

            string dia = diaActual.ToString("dd-MM-yyyy");
            DateTime InicioDeDia = DateTime.ParseExact(dia, "dd-MM-yyyy", null);
            DateTime FinalDeDia = DateTime.ParseExact(dia + " 23:59:59", "dd-MM-yyyy HH:mm:ss", null);

            decimal valoresRetiro = await _context.Movimientos
                .Where(x => x.MoNumeroCuenta == strNumeroCuenta
                && x.MoFecha >= InicioDeDia
                && x.MoFecha <= FinalDeDia
                && x.MoTipoMovimiento == "Retiro")
                .SumAsync(a => a.MoValor);

            decimal totalsupuesto = Math.Abs(valoresRetiro) + Math.Abs(decMontoTransaccion);

            response.Data = Math.Abs(totalsupuesto);
            if (totalsupuesto > 1000 || totalsupuesto > 1000)
            {
                response.Message = "Cupo diario excedido";
                response.IsSuccess = false;
                response.Data = Math.Abs(valoresRetiro);
            }
            else
                response.IsSuccess = true;
            return response;
        }

        [HttpGet("{strIdentificacion}&{dtFechaInicio}&{dtFechaFin}")]
        public async Task<Response> MovimientosRango(string strIdentificacion, DateTime dtFechaInicio, DateTime dtFechaFin)
        {
            List<Movimiento> lstPMovimiento = new List<Movimiento>();
            Response response = new Response();
            try
            {
                response.Data = await _context.Movimientos.Select(x => new MovimientosCliente
                {
                    Fecha = x.MoFecha,
                    Nombre = x.MoNumeroCuentaNavigation.CuIdClienteNavigation.Nombre,
                    NumeroCuenta = x.MoNumeroCuentaNavigation.CuNumeroCuenta,
                    Tipo = x.MoNumeroCuentaNavigation.CuTipoCuenta,
                    Estado = x.MoNumeroCuentaNavigation.CuIdClienteNavigation.ClEstado,
                    SaldoInicial = x.MoNumeroCuentaNavigation.CuSaldoInicial,
                    Movimiento = x.MoValor,
                    SaldoDisponible = x.MoSaldo,
                    Identificacion = x.MoNumeroCuentaNavigation.CuIdClienteNavigation.Identificacion,

                }).Where(s => s.Identificacion == strIdentificacion && s.Fecha >= dtFechaInicio && s.Fecha <= dtFechaFin).ToListAsync();
                response.IsSuccess = true;
            }
            catch (Exception e)
            {
                response.Message = "Error: " + e.StackTrace;
            }
            return response;
        }
    }
}
