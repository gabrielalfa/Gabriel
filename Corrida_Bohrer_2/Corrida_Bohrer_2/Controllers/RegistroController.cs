using Corrida_Bohrer.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Corrida_Bohrer.Controllers
{
    public class RegistroController : Controller
    {
        //
        // GET: /Registro/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Registro()
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
                        //rg.Inserir(registro, uploadPath_banco);

                        //arquivo.SaveAs(caminhoArquivo);
                        arquivosSalvos++;
                    }
                    else
                    {
                        uploadPath = "/Content/img/Jogadores/user_icon_blue.png";
                        //string caminhoArquivo = Path.Combine(@uploadPath, Path.GetFileName(arquivo.FileName));

                        var rg = new Aplicacao_Registro();
                        //rg.Inserir(registro, uploadPath);


                        //arquivo.SaveAs(uploadPath); 
                        arquivosSalvos++;
                    }
                }
                return RedirectToAction("Pagamento", "Inscricao", null);
            }


            return RedirectToAction("Logado", "Login", null);
           

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

    }
}
