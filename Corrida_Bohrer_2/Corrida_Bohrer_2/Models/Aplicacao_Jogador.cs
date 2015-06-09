using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Web;

namespace Corrida_Bohrer.Models
{
    public class Aplicacao_Jogador : Jogadores
    {

        public Contexto contexto;





        public void Salvar_Change()
        {
            string pass = "change";
           
            var strQuery = "";
            strQuery += " INSERT INTO Transacao_Pag (status ) ";
            strQuery += string.Format(" VALUES ('{0}') ",
                pass
                );
            //jogador.Data_Nascimento,
            using (contexto = new Contexto())
            {
                contexto.ExecutaComando(strQuery);
            }
        }

        public void Salvar_Trans(string id_transacao)
        {
           
            var strQuery = "";
            strQuery += " INSERT INTO Transacao_Pag (id_transacao ) ";
            strQuery += string.Format(" VALUES ('{0}') ",
                id_transacao
                );
            //jogador.Data_Nascimento,
            using (contexto = new Contexto())
            {
                contexto.ExecutaComando(strQuery);
            }
        }


        public Jogadores Adm(int id)
        {

            using (contexto = new Contexto())
            {
                var strQuery = "";
                strQuery += string.Format("SELECT * FROM Jogador_Bohrer_Corrida  where id = {0} ", id);
                strQuery += "ORDER BY id_Categoria";
                var retornoDataReader = contexto.ExecutaComandoComRetorno(strQuery);
                return TransformaReaderEmListaDeObjeto_email(retornoDataReader).FirstOrDefault();
            }
        }

        public Jogadores Lista_Jogadores_Porid(int cat)
        {

            using (contexto = new Contexto())
            {
                var strQuery = "";
                strQuery += string.Format("SELECT * FROM Jogador_Bohrer_Corrida  where id_Categoria = {0} ", cat);
                strQuery += "ORDER BY id_Categoria";
                var retornoDataReader = contexto.ExecutaComandoComRetorno(strQuery);
                return TransformaReaderEmListaDeObjeto_email(retornoDataReader).FirstOrDefault();
            }
        }

        public List<Jogadores> Listar_Jogadores_Porid(int cat)
        {

            using (contexto = new Contexto())
            {
                var strQuery = "";
                strQuery += string.Format("SELECT * FROM Jogador_Bohrer_Corrida  where id_Categoria = {0} ", cat);
                strQuery += "ORDER BY id_Categoria";
                var retornoDataReader = contexto.ExecutaComandoComRetorno(strQuery);
                return TransformaReaderEmListaDeObjeto_email(retornoDataReader);
            }
        }

        public List<Jogadores> Listar_Jogadores()
        {

            using (contexto = new Contexto())
            {
                var strQuery = "";
                strQuery += "SELECT * FROM Jogador_Bohrer_Corrida ";
                strQuery += "ORDER BY id_Categoria";
                var retornoDataReader = contexto.ExecutaComandoComRetorno(strQuery);
                return TransformaReaderEmListaDeObjeto_email(retornoDataReader);
            }
        }

        private List<Jogadores> TransformaReaderEmListaDeObjeto_email(SqlDataReader reader)
        {
            var users = new List<Jogadores>();

            while (reader.Read())
            {
                var temObjeto = new Jogadores()
                {
                    id_jogador = reader["id"].ToString(),
                    Nome = reader["Nome"].ToString(),
                    id_categoria = int.Parse(reader["id_categoria"].ToString()),
                    adm = reader["adm"].ToString()
                };

                users.Add(temObjeto);

            }


            reader.Close();
            return users;
        }


    }
}