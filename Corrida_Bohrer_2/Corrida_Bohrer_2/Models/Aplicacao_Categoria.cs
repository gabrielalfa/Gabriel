using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Corrida_Bohrer.Models
{
    public class Aplicacao_Categoria : Categorias
    {


        public Contexto contexto;

        public List<Categorias> Listar_Categorias()
        {

            using (contexto = new Contexto())
            {
                var strQuery = "";
                strQuery += "SELECT * FROM Categorias_Bohrer ";

                var retornoDataReader = contexto.ExecutaComandoComRetorno(strQuery);
                return TransformaReaderEmListaDeObjeto_cat(retornoDataReader);
            }
        }

        private List<Categorias> TransformaReaderEmListaDeObjeto_cat(SqlDataReader reader)
        {
            var users = new List<Categorias>();

            while (reader.Read())
            {
                var temObjeto = new Categorias()
                {
                    id = int.Parse(reader["id"].ToString())
                };

                users.Add(temObjeto);

            }


            reader.Close();
            return users;
        }

    }
}