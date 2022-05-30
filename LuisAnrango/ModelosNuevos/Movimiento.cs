using System;
using System.Collections.Generic;

#nullable disable

namespace LuisAnrango.ModelosNuevos
{
    public partial class Movimiento
    {
        public int MoIdMovimiento { get; set; }
        public string MoNumeroCuenta { get; set; }
        public DateTime MoFecha { get; set; }
        public string MoTipoMovimiento { get; set; }
        public decimal MoValor { get; set; }
        public decimal MoSaldo { get; set; }

        public virtual Cuenta MoNumeroCuentaNavigation { get; set; }
    }
}
