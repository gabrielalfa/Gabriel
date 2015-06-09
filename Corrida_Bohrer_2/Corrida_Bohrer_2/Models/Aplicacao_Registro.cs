using Corrida_Bohrer;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Web;

namespace Corrida_Bohrer.Models
{
    public class Aplicacao_Registro : Registro
    {
        public Contexto contexto;

        public void Inserir(Registro registro, string uploadFile)
        {
            var cpf = registro.CPF;
            var CPF = cpf.Replace(".", "").Replace("-", "");

            string Jogador_Nome = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(registro.Nome);


            var strQuery = "";
            strQuery += " INSERT INTO Jogador_Bohrer_Corrida (Nome, Email, Password, id_Categoria,  Telefone, img_path, Data_Nasc, adm, CPF  ) ";
            strQuery += string.Format(" VALUES ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}') ",
                Jogador_Nome, registro.email, registro.password, registro.id_categoria, registro.telefone, uploadFile, registro.data_nasc,  false, CPF
                );
            //jogador.Data_Nascimento,
            using (contexto = new Contexto())
            {
                contexto.ExecutaComando(strQuery);
            }
        }

        public Registro Procura_Email(string Email)
        {

            using (contexto = new Contexto())
            {
                var strQuery = "";
                strQuery += "select Jogador_Bohrer_Corrida.Email from Jogador_Bohrer_Corrida ";
                strQuery += string.Format("where Jogador_Bohrer_Corrida.Email = '{0}' ", Email);

                var retornoDataReader = contexto.ExecutaComandoComRetorno(strQuery);
                return TransformaReaderEmListaDeObjeto_email(retornoDataReader).FirstOrDefault();
            }
        }

        private List<Registro> TransformaReaderEmListaDeObjeto_email(SqlDataReader reader)
        {
            var users = new List<Registro>();

            while (reader.Read())
            {
                var temObjeto = new Registro()
                {
                    email = reader["email"].ToString(),
                };

                users.Add(temObjeto);

            }


            reader.Close();
            return users;
        }



    }
}