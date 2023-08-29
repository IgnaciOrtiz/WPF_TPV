using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using WPF_LoginForm.Repositories;
using WPF_TPV.Model;

namespace WPF_TPV.Repositories
{
    public class LineaFacturaRepository : RepositoryBase
    {
        public List<LineaFacturaModel> GetLineasFacturaByFacturaId(int facturaId)
        {
            List<LineaFacturaModel> lineasFactura = new List<LineaFacturaModel>();

            using (SqlConnection connection = GetConnection())
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("SELECT * FROM LineaFactura WHERE idFactura = @facturaId", connection))
                {
                    command.Parameters.AddWithValue("@facturaId", facturaId);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            LineaFacturaModel lineaFactura = new LineaFacturaModel
                            {

                            };
                            lineasFactura.Add(lineaFactura);
                        }
                    }
                }
            }

            return lineasFactura;
        }
    }
}
