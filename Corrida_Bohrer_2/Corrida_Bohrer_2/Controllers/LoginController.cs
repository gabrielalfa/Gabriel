using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Corrida_Bohrer.Controllers
{
    public class LoginController : Controller
    {
        //
        // GET: /Login/

        public ActionResult Index()
        {
            return View();
        }


        public ActionResult Login()
        {

            List<SelectListItem> Jogadores = new List<SelectListItem>();
            using (var contexto = new Contexto())
            {
                string stringQuerySelect = "Select * From Categorias_Bohrer";
                SqlDataReader dados = contexto.ExecutaComandoComRetorno(stringQuerySelect);
                Jogadores.Add(new SelectListItem { Text = "Escolha uma Categoria", Value = "" });
                while (dados.Read())
                {
                    Jogadores.Add(new SelectListItem { Text = dados["Categoria"].ToString() , Value = dados["id"].ToString() });
                }
                ViewData["categorias"] = Jogadores;
            }

            return View();
        }

        public ActionResult Logado()
        {
            return View();
        }
    }
}
