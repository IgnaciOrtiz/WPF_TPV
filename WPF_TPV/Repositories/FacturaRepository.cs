using Microsoft.Data.SqlClient;
using System;
using WPF_LoginForm.Repositories;
using WPF_TPV.Model;

namespace WPF_TPV.Repositories
{
    public class FacturaRepository : RepositoryBase
    {
        public int GuardarFactura(FacturaModel factura)
        {
            using (var connection = GetConnection())
            {
                connection.Open();

                // Insertar factura en BBDD
                using (var cmd = new SqlCommand(
                    "INSERT INTO [gst_Facturas] (numFactura, fecha, idCliente, Total) VALUES (@NumFactura, @Fecha, @IdCliente, @Total); SELECT SCOPE_IDENTITY();",
                    connection))
                {
                    cmd.Parameters.AddWithValue("@NumFactura", factura.NumFactura);
                    cmd.Parameters.AddWithValue("@Fecha", factura.Fecha);
                    cmd.Parameters.AddWithValue("@IdCliente", factura.IdCliente);
                    cmd.Parameters.AddWithValue("@Total", factura.Total);

                    int facturaId = Convert.ToInt32(cmd.ExecuteScalar());
                    return facturaId;
                }
            }
        }

        public void GuardarLineaFactura(int facturaId, LineaFacturaModel lineaFactura)
        {
            using (var connection = GetConnection())
            {
                connection.Open();

                // Insertar líneaFactura en BBDD
                using (var cmd = new SqlCommand(
                    "INSERT INTO [gst_LineaFacturas] (idFactura, Cantidad, Precio, idProducto) VALUES (@IdFactura, @Cantidad, @Precio, @IdProducto);",
                    connection))
                {
                    cmd.Parameters.AddWithValue("@IdFactura", facturaId);
                    cmd.Parameters.AddWithValue("@Cantidad", lineaFactura.Cantidad);
                    cmd.Parameters.AddWithValue("@Precio", lineaFactura.Precio);
                    cmd.Parameters.AddWithValue("@IdProducto", lineaFactura.IdProducto);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public int ObtenerNuevoNumeroFactura()
        {
            int ultimoNumeroFactura = ObtenerUltimoNumeroFacturaDesdeBD();
            int nuevoNumeroFactura = ultimoNumeroFactura + 1;
            return nuevoNumeroFactura;
        }

        public int ObtenerUltimoNumeroFacturaDesdeBD()
        {
            using (var connection = GetConnection())
            {
                connection.Open();

                using (var cmd = new SqlCommand("SELECT MAX(numFactura) FROM [gst_Facturas];", connection))
                {
                    object result = cmd.ExecuteScalar();
                    if (result != null && result != DBNull.Value)
                    {
                        return Convert.ToInt32(result);
                    }
                    return 0;
                }
            }
        }
        public int ObtenerIdCliente()
        {

            int idClienteContado = 1;

            return idClienteContado;
        }
    }
}
