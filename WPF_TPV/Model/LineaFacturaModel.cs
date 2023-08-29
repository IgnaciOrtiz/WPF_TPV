namespace WPF_TPV.Model
{
    public class LineaFacturaModel
    {
        public int IdLineaFactura { get; set; }
        public int IdFactura { get; set; }
        public int Cantidad { get; set; }
        public decimal Precio { get; set; }
        public int IdProducto { get; set; }
    }
}
