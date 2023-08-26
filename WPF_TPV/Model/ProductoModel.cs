namespace WPF_TPV.Model
{
    public class ProductoModel
    {
        public int idProducto { get; set; }
        public string Nombre { get; set; }
        public decimal Precio { get; set; }
        public string Descripcion { get; set; }
        public int TipoIVA { get; set; }
        public int idFamiliaProd { get; set; }
        public int idProveedor { get; set; }
        public int Stock { get; set; }
    }
}
