using System;
using System.Collections.Generic;

#nullable disable

namespace LuisAnrango.Model
{
    public partial class Cuenta
    {
        public Cuenta()
        {
            Movimientos = new HashSet<Movimiento>();
        }

        public string CuNumeroCuenta { get; set; }
        public int CuIdCliente { get; set; }
        public string CuTipoCuenta { get; set; }
        public bool CuEstado { get; set; }
        public decimal CuSaldoInicial { get; set; }

        public virtual Cliente CuIdClienteNavigation { get; set; }
        public virtual ICollection<Movimiento> Movimientos { get; set; }
    }
}
