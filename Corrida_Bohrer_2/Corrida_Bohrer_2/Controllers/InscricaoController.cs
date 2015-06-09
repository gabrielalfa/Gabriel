using Corrida_Bohrer.Models;
using Corrida_Bohrer_2.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Uol.PagSeguro.Domain;
using Uol.PagSeguro.Exception;
using Uol.PagSeguro.Resources;
using Uol.PagSeguro.Service;


namespace Corrida_Bohrer.Controllers
{
    public class InscricaoController : Controller
    {
        //
        // GET: /Inscricao/


        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Registro(Registro registro, string uploadFile)
        {
            if (ModelState.IsValid)
            {
                int arquivosSalvos = 0;
                var uploadPath = "";
                for (int i = 0; i < Request.Files.Count; i++)
                {
                    HttpPostedFileBase arquivo = Request.Files[i];
                    //Suas validações ...... //Salva o arquivo 
                    if (arquivo.ContentLength > 0)
                    {
                        uploadPath = Server.MapPath("~/Content/img/Jogadores");
                        var uploadPath_banco = "/Content/img/Jogadores/" + arquivo.FileName;
                        string caminhoArquivo = Path.Combine(@uploadPath, Path.GetFileName(arquivo.FileName));

                        var rg = new Aplicacao_Registro();
                        rg.Inserir(registro, uploadPath_banco);

                        arquivo.SaveAs(caminhoArquivo);
                        arquivosSalvos++;
                    }
                    else
                    {
                        uploadPath = "/Content/img/Jogadores/user_icon_blue.png";
                        string caminhoArquivo = Path.Combine(@uploadPath, Path.GetFileName(arquivo.FileName));

                        var rg = new Aplicacao_Registro();
                        rg.Inserir(registro, uploadPath);


                        arquivo.SaveAs(uploadPath); 
                        arquivosSalvos++;
                    }
                }
                return RedirectToAction("Pagamento", "Inscricao", null);
            }


            return RedirectToAction("Logado", "Login", null);
        }

        public ActionResult Inscricao()
        {


            List<SelectListItem> Jogadores = new List<SelectListItem>();
            using (var contexto = new Contexto())
            {
                string stringQuerySelect = "Select * From Categorias_Bohrer";
                SqlDataReader dados = contexto.ExecutaComandoComRetorno(stringQuerySelect);
                Jogadores.Add(new SelectListItem { Text = "Escolha uma Categoria", Value = "" });
                while (dados.Read())
                {
                    Jogadores.Add(new SelectListItem { Text = dados["categoria"].ToString() , Value = dados["id"].ToString() });
                }
                ViewData["categoria"] = Jogadores;

            }


            return View();
        }

     
        public ActionResult Pagamento()
        {


            return View();
        }

        [HttpPost]
        public ActionResult Pagamento(FormCollection collection, string id_transaction)
        {

    
            return View();
        }


        public ActionResult Retorno(FormCollection pag_itens,  string id_transaction)
        {
            if (id_transaction != "")
            {
                ViewBag.teste = id_transaction;

                var appInscricao = new Aplicacao_Jogador();
                appInscricao.Salvar_Trans(id_transaction);


            } else {
                ViewBag.teste = "Vazio";
            }

            return View();
        }

        [HttpPost]
        public ActionResult Retorno(FormCollection colecao)
        {

            ViewBag.debug = colecao;

            return View();
        }

        public ActionResult Espera()
        {
            var appInscricao = new Aplicacao_Jogador();
            appInscricao.Salvar_Change();


            return View();
        }

        [HttpPost]
        public ActionResult Espera (FormCollection collecao, string id_transaction)
        {


            var appInscricao = new Aplicacao_Jogador();
            appInscricao.Salvar_Change();

            return View();
        }



        public JsonResult Procura_Email_Reg(string Email_reg)
        {
            var appEmail = new Aplicacao_Registro();
            var Email_Login = appEmail.Procura_Email(Email_reg);

            bool mail = false;
            if (Email_Login == null)
            {
                mail = false;
            }
            else
            {
                mail = true;
            }

            return Json(mail);
        }

        public ActionResult Depuracao()
        {

            var app = new ReceiveNotification();
            app.teste();
            
            return View();
        }

        [HttpPost]
        public ActionResult Depuracao(FormCollection colecao)
        {

            ViewBag.debug = colecao;

            return View();
        }

       

    }






}
