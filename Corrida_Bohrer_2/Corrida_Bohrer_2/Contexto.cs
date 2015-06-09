using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Corrida_Bohrer
{
    public class Contexto:IDisposable
    {
       

        private readonly SqlConnection myCn;

        public Contexto()
        {
            myCn = new SqlConnection(ConfigurationManager.ConnectionStrings["IntelectoAlphaConfig"].ConnectionString);
            myCn.Open();
        }

        public void ExecutaComando(string strQuery)
        {
            var cmdComando = new SqlCommand
                                {
                                    CommandText = strQuery,
                                    CommandType = CommandType.Text,
                                    Connection = myCn
                                };
            cmdComando.ExecuteNonQuery();
        }


        public SqlDataReader ExecutaComandoComRetorno(string strQuery)
        {
            var cmdComando  = new SqlCommand(strQuery, myCn);
            return cmdComando.ExecuteReader();
        }


        public void Dispose()
        {
            if (myCn.State == ConnectionState.Open)
            {
                myCn.Close();
            }
        }
    }
    }
