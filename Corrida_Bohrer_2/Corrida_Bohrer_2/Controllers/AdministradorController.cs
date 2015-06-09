using Corrida_Bohrer.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Corrida_Bohrer.Controllers
{
    public class AdministradorController : Controller
    {
        //
        // GET: /Administrador/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Logar(string msg)
        {
            ViewBag.msg = msg;
            return View();
        }

        public ActionResult Redirecionamento(string msg)
        {
            ViewBag.msg = msg;
            return View();
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session["ID_Jogador"] = "";
            Session["User"] = "";
            Session["path"] = "";
            Session["adm"] = false;

            return RedirectToAction("Index", "Home");
        }



        [HttpPost]
        public ActionResult Logar(string email, string pass)
        {
            if (IsValid(email, pass))
            {
                FormsAuthentication.SetAuthCookie(email, false);
                if (Convert.ToBoolean(Session["adm"]))
                {
                    HttpCookie myCookie = new HttpCookie("User");
                    myCookie.Expires = DateTime.Now.AddDays(-1d);
                    Response.Cookies.Add(myCookie);

                    return RedirectToAction("DashBoard", "Administrador");
                }
                else
                {
                    string msg = "Este login não tem privilégios administrativos no momento.";
                    return RedirectToAction("Redirecionamento", "Administrador", new { msg });
                }

            }
            else
            {
                if (Request.Cookies["UserSettings"] != null)
                {
                    HttpCookie myCookie = new HttpCookie("User");
                    myCookie.Expires = DateTime.Now.AddDays(-1d);
                    Response.Cookies.Add(myCookie);
                }
                string msg = "Este login não tem privilégios administrativos no momento.";
                //return View(msg);
                return RedirectToAction("Redirecionamento", "Administrador", msg);
            }

            return View();
        }

        private bool IsValid(string email, string password)
        {
            //var crypto = new SimpleCrypto.PBKDF2();
            bool isValid = false;
            var user = "";
            var pass = "";
            var login = "";
            var path = "";
            bool adm;
            int id = 0;
            using (var contexto = new Contexto())
            {

                string stringQuerySelect = "Select * From Jogador_Bohrer_Corrida";
                SqlDataReader dados = contexto.ExecutaComandoComRetorno(stringQuerySelect);

                while (dados.Read())
                {
                    id = Convert.ToInt32(dados["ID"]);
                    user = string.Format("{0}", dados["Email"]);
                    pass = string.Format("{0}", dados["Password"]);
                    //login = string.Format("{0}", dados["login"]);

                    if (user != null)
                    {
                        if (user == email)
                        {
                            if (pass == password)
                            {
                                isValid = true;
                                break;
                            }
                        }
                    }

                }
            }

            try
            {
                var appJogador = new Aplicacao_Jogador();
                var listaJogador = appJogador.Adm(id);
                adm = Convert.ToBoolean(listaJogador.adm);
                Session["adm"] = adm;
            }
            catch (Exception)
            {

            }

            Session["path"] = path;
            Session["ID_Jogador"] = id;
            Session["User"] = user;

            return isValid;
        }

        public ActionResult DashBoard()
        {
            if (Session["adm"] != null)
            
            {

                var Info_Jogos = new Aplicacao_Jogos();
                var lista_jogos = Info_Jogos.Listar_Jogos_Inseridos();


                return View(lista_jogos);
            }
            else
            {
                return RedirectToAction("Logar", "Administrador");
            }

        }


        [HttpPost]
        public ActionResult Listagem_Jogos(int id)
        {

            int mes = id;
            var Info_Jogos = new Aplicacao_Jogos();
            var lista_jogos = Info_Jogos.Listar_Jogos_Por_mes(mes);


            return View(lista_jogos);
        }



        public ActionResult Listagem_Jogos(string id)
        {

            int mes = int.Parse(id);
            var Info_Jogos = new Aplicacao_Jogos();
            var lista_jogos = Info_Jogos.Listar_Jogos_Por_mes(mes);


            return View(lista_jogos);
        }


        public ActionResult Listar_Jogadores(string jog)
        {


            List<SelectListItem> stateList = new List<SelectListItem>();
            using (var contexto = new Contexto())
            {
                string stringQuerySelect = "";


                stringQuerySelect = string.Format("Select * From Jogador_Bohrer_Corrida order by Nome");


                SqlDataReader dados = contexto.ExecutaComandoComRetorno(stringQuerySelect);
                stateList.Add(new SelectListItem { Text = "Escolha uma avaliação", Value = "" });
                while (dados.Read())
                {
                    stateList.Add(new SelectListItem { Text = dados["Nome"].ToString(), Value = dados["id"].ToString() });
                }

            }


            return Json(stateList, JsonRequestBehavior.AllowGet);
        }



        [HttpPost]
        public ActionResult Editar_Jogos(int id)
        {

            var appJogo = new Aplicacao_Jogos();
            var jogo = appJogo.Listar_Jogo_Edicao(id);


            List<SelectListItem> stateList = new List<SelectListItem>();
            using (var contexto = new Contexto())
            {
                string stringQuerySelect = "";


                stringQuerySelect = string.Format("Select * From Jogador_Bohrer_Corrida order by Nome");


                SqlDataReader dados = contexto.ExecutaComandoComRetorno(stringQuerySelect);
                stateList.Add(new SelectListItem { Text = "Substituição", Value = "" });
                while (dados.Read())
                {
                    stateList.Add(new SelectListItem { Text = dados["Nome"].ToString(), Value = dados["id"].ToString() });
                }

            }
            ViewData["Jogadores"] = stateList;




            return View(jogo);
        }

        [HttpPost]
        public ActionResult Salvar_Edicao(Jogos jogo)
        {

            var appJogo = new Aplicacao_Jogos();
            appJogo.Editar(jogo);

            return RedirectToAction("Listagem_Jogos", "Administrador", new { @id = jogo.mes });
        }




    }
}
