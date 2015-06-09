using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Corrida_Bohrer.Models
{
    public class Aplicacao_Jogos
    {

        public Contexto contexto;

        //--------------------------------------------------------------------
        //controle tabelas temporarias
        public void Deletar_Jogador_randon()
        {

            var strQuery = "";
            strQuery += "delete From Randon_Jogadores";
            using (contexto = new Contexto())
            {
                contexto.ExecutaComando(strQuery);
            }
        }

        public void Deletar_Jogos_randon(int mes)
        {

            var strQuery = "";
            strQuery += "delete From randon_jogos ";
            strQuery += string.Format("where mes = {0}", mes);
            using (contexto = new Contexto())
            {
                contexto.ExecutaComando(strQuery);
            }
        }

        public void InserirJogador_randon(int id_jog, int id_cat)
        {

            var strQuery = "";
            strQuery += " INSERT INTO Randon_Jogadores (id, id_categoria)";
            strQuery += string.Format(" VALUES ('{0}','{1}') ",
                    id_jog, id_cat
                );

            using (contexto = new Contexto())
            {
                contexto.ExecutaComando(strQuery);
            }
        }

        //*fim controle tableas temporarias*
        //---------------------------------------------------------------------

        public void Editar(Jogos jogo)
        {

            
            var strQuery = "";
            strQuery += " UPDATE randon_jogos SET ";
            strQuery += string.Format(" randon_jogos.id_1  = '{0}', ", jogo.idj_1);
            strQuery += string.Format(" randon_jogos.id_2 = '{0}', ", jogo.idj_2);
            strQuery += string.Format(" randon_jogos.hora = '{0}', ", jogo.hora);
            strQuery += string.Format(" randon_jogos.dia = '{0}' ", jogo.dia);

            strQuery += string.Format(" where randon_jogos.id = {0} ", jogo.id);
            
            using (contexto = new Contexto())
            {
                contexto.ExecutaComando(strQuery);
            }
        }


        public void InserirJogos(int id_1, int id_2, int c, int mes)
        {

            var strQuery = "";
            strQuery += " INSERT INTO randon_jogos (id_1, id_2, id_categoria, mes)";
            strQuery += string.Format(" VALUES ('{0}','{1}','{2}','{3}') ",
                    id_1, id_2, c, mes
                );
            
            using (contexto = new Contexto())
            {
                contexto.ExecutaComando(strQuery);
            }
        }

        public Jogos Listar_Jogo_Edicao(int id)
        {

            using (contexto = new Contexto())
            {
                var strQuery = "";

                strQuery += "select Hj.id, Hj.id_1,Hj.id_2, Hj.id_categoria, j.Nome as Nome_j1, js.Nome as Nome_j2, ";
                strQuery += "j.img_path as path_j1,js.img_path as path_j2, Hj.dia, hj.hora, hj.mes  ";
                strQuery += "from randon_jogos as Hj ";
                strQuery += "join Jogador_Bohrer_Corrida as j on j.ID = Hj.id_1  ";
                strQuery += "join Jogador_Bohrer_Corrida as js on js.ID = Hj.id_2 ";
                strQuery += string.Format("where Hj.id = {0}", id);
                var retornoDataReader = contexto.ExecutaComandoComRetorno(strQuery);
                return TransformaReaderEmListaDeObjeto_jogos(retornoDataReader).FirstOrDefault();
            }
        }

        public List<Jogos> Listar_Jogos_Por_mes(int mes)
        {

            using (contexto = new Contexto())
            {
                var strQuery = "";

                strQuery += "select Hj.id, Hj.id_1,Hj.id_2, Hj.id_categoria, j.Nome as Nome_j1, js.Nome as Nome_j2, ";
                strQuery += "j.img_path as path_j1,js.img_path as path_j2, Hj.dia, hj.hora, hj.mes  ";
                strQuery += "from randon_jogos as Hj ";
                strQuery += "join Jogador_Bohrer_Corrida as j on j.ID = Hj.id_1  ";
                strQuery += "join Jogador_Bohrer_Corrida as js on js.ID = Hj.id_2 ";
                strQuery += string.Format("where mes = {0} ", mes);
                strQuery += "order by Hj.dia, hj.hora ";
                var retornoDataReader = contexto.ExecutaComandoComRetorno(strQuery);
                return TransformaReaderEmListaDeObjeto_jogos(retornoDataReader);
            }
        }

        public List<Jogos> Listar_Jogos()
        {

            using (contexto = new Contexto())
            {
                var strQuery = "";

                strQuery += "select Hj.id, Hj.id_1,Hj.id_2, Hj.id_categoria, j.Nome as Nome_j1, js.Nome as Nome_j2, ";
                strQuery += "j.img_path as path_j1,js.img_path as path_j2 ";
                strQuery += "from randon_jogos as Hj ";
                strQuery += "join Jogador_Bohrer_Corrida as j on j.ID = Hj.id_1  ";
                strQuery += "join Jogador_Bohrer_Corrida as js on js.ID = Hj.id_2 ";
                var retornoDataReader = contexto.ExecutaComandoComRetorno(strQuery);
                return TransformaReaderEmListaDeObjeto_jogos(retornoDataReader);
            }
        }


        public List<Jogos> Listar_Jogos_Inseridos()
        {

            using (contexto = new Contexto())
            {
                var strQuery = "";

                strQuery += " select  COUNT(randon_jogos.id) as contagem_mes, mes from randon_jogos group by randon_jogos.mes ";
                var retornoDataReader = contexto.ExecutaComandoComRetorno(strQuery);
                return TransformaReaderEmListaDeObjeto_inseridos(retornoDataReader);
            }
        }


         private List<Jogos> TransformaReaderEmListaDeObjeto_inseridos(SqlDataReader reader)
        {
            var users = new List<Jogos>();

            while (reader.Read())
            {
                var temObjeto = new Jogos()
                {
                    
                    contagem_group_mes = reader["contagem_mes"].ToString(), 
                    mes = reader["mes"].ToString(),
                    
                };

                users.Add(temObjeto);

            }


            reader.Close();
            return users;
        }

        private List<Jogos> TransformaReaderEmListaDeObjeto_jogos(SqlDataReader reader)
        {
            var users = new List<Jogos>();

            while (reader.Read())
            {
                var temObjeto = new Jogos()
                {
                    id = int.Parse(reader["id"].ToString()),
                    idj_1 = reader["id_1"].ToString(),
                    idj_2 = reader["id_2"].ToString(),
                    id_categoria = reader["id_categoria"].ToString(),
                    Nome_j1 = reader["Nome_j1"].ToString(),
                    Nome_j2 = reader["Nome_j2"].ToString(),
                    path_j1 =reader["path_j1"].ToString(),
                    path_j2 = reader["path_j2"].ToString(), 
                    dia =reader["dia"].ToString(),
                    hora = reader["hora"].ToString(),
                    mes = reader["mes"].ToString(),
                   
                };

                users.Add(temObjeto);

            }


            reader.Close();
            return users;
        }

        public List<Jogadores> Listar_Jogadores()
        {

            using (contexto = new Contexto())
            {
                var strQuery = "";
                strQuery += "SELECT TOP 1 id FROM Randon_Jogadores ORDER BY NEWID() ";

                var retornoDataReader = contexto.ExecutaComandoComRetorno(strQuery);
                return TransformaReaderEmListaDeObjeto_email(retornoDataReader);
            }
        }

        public List<Jogadores> Listar_Jogadores2(int id)
        {

            using (contexto = new Contexto())
            {
                var strQuery = "";
                strQuery += string.Format("SELECT TOP 1 id FROM Randon_Jogadores where id != {0} ", id);
                strQuery += "ORDER BY NEWID()";

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
                };

                users.Add(temObjeto);

            }


            reader.Close();
            return users;
        }


        public void Deletar_Jogador(int id)
        {

            var strQuery = "";
            strQuery += string.Format("delete From Randon_Jogadores where id = {0} ", id);         
            using (contexto = new Contexto())
            {
                contexto.ExecutaComando(strQuery);
            }
        }
  
        public Jogadores Contagem_PorID(int mes)
        {

            using (contexto = new Contexto())
            {
                var strQuery = "";
                strQuery += string.Format( "select count(id_1) as cont from Randon_jogos where mes = {0}", mes);


                var retornoDataReader = contexto.ExecutaComandoComRetorno(strQuery);
                return TransformaReaderEmListaDeObjeto_cont(retornoDataReader).FirstOrDefault();
            }
        }

        public Jogadores Contagem2(int id)
        {

            using (contexto = new Contexto())
            {
                var strQuery = "";
                strQuery += string.Format("select count(id_2) as cont from Randon_jogos where id_2 = {0}", id);


                var retornoDataReader = contexto.ExecutaComandoComRetorno(strQuery);
                return TransformaReaderEmListaDeObjeto_cont(retornoDataReader).FirstOrDefault();
            }
        }

        public Jogadores Contagem3(int id)
        {

            using (contexto = new Contexto())
            {
                var strQuery = "";
                strQuery += string.Format("select count(id_1) as cont from Randon_jogos where id_2 = {0}", id);


                var retornoDataReader = contexto.ExecutaComandoComRetorno(strQuery);
                return TransformaReaderEmListaDeObjeto_cont(retornoDataReader).FirstOrDefault();
            }
        }

        public Jogadores Contagem4(int id)
        {

            using (contexto = new Contexto())
            {
                var strQuery = "";
                strQuery += string.Format("select count(id_2) as cont from Randon_jogos where id_2 = {0}", id);


                var retornoDataReader = contexto.ExecutaComandoComRetorno(strQuery);
                return TransformaReaderEmListaDeObjeto_cont(retornoDataReader).FirstOrDefault();
            }
        }

        public Jogadores Contagem_total()
        {

            using (contexto = new Contexto())
            {
                var strQuery = "";
                strQuery += "select count(id) as cont_total from Randon_Jogadores ";


                var retornoDataReader = contexto.ExecutaComandoComRetorno(strQuery);
                return TransformaReaderEmListaDeObjeto_cont_geral(retornoDataReader).FirstOrDefault();
            }
        }

        private List<Jogadores> TransformaReaderEmListaDeObjeto_cont_geral(SqlDataReader reader)
        {
            var users = new List<Jogadores>();

            while (reader.Read())
            {
                var temObjeto = new Jogadores()
                {
                    cont_total = int.Parse(reader["cont_total"].ToString()),
                };

                users.Add(temObjeto);

            }


            reader.Close();
            return users;
        }

        private List<Jogadores> TransformaReaderEmListaDeObjeto_cont(SqlDataReader reader)
        {
            var users = new List<Jogadores>();

            while (reader.Read())
            {
                var temObjeto = new Jogadores()
                {
                    cont = int.Parse(reader["cont"].ToString()),
                };

                users.Add(temObjeto);

            }


            reader.Close();
            return users;
        }

    }
}