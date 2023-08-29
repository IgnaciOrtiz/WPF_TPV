using System;

namespace WPF_TPV.Model
{
    public class FacturaModel
    {
        public int IdFactura { get; set; }
        public int NumFactura { get; set; }
        public DateTime Fecha { get; set; }
        public int IdCliente { get; set; }
        public decimal Total { get; set; }
    }
}
