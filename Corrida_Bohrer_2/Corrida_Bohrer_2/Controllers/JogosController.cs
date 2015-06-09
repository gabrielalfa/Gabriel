using Corrida_Bohrer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Corrida_Bohrer.Controllers
{
    public class JogosController : Controller
    {
        //
        // GET: /Jogos/

        public ActionResult Index()
        {
            return View();
        }


        public ActionResult Atual()
        {
            int mes = DateTime.Today.Month;
            var Info_Jogos = new Aplicacao_Jogos();
            var lista_jogos = Info_Jogos.Listar_Jogos_Por_mes(mes);

            return View(lista_jogos);
        }

        public ActionResult Resultados()
        {
            return View();
        }

        public ActionResult Exmplo(string msg)
        {
            var Info_Jogos = new Aplicacao_Jogos();
            var lista_jogos = Info_Jogos.Listar_Jogos_Inseridos();

            ViewBag.msg = msg;

            return View(lista_jogos);
        }

        [HttpPost]
        public ActionResult Exmplo(int mes)
        {

            var Info_Jogos = new Aplicacao_Jogos();
            var jog_1 = "";
            var jog_2 = "";

            //verifica existencia
            var result_contagem = Info_Jogos.Contagem_PorID(mes);
            var msg = "";

            if (result_contagem.cont != 0)
            {
                return RedirectToAction("Exmplo", "Jogos", new { msg = "Listagem Existente" });
            }


            //deletar jogos randon_jogos
            Info_Jogos.Deletar_Jogos_randon(mes);

            var lc = new Aplicacao_Categoria();
            var lista_categorias = lc.Listar_Categorias();

            for (int c = 1; c < lista_categorias.Count; c++)
            {
                var lj = new Aplicacao_Jogador();
                var lista_jogadores = lj.Listar_Jogadores_Porid(c);

                //deletar jogador randon_jogadores
                Info_Jogos.Deletar_Jogador_randon();

                for (int i = 0; i < lista_jogadores.Count; i++)
                {
                    var id_jog = int.Parse(lista_jogadores[i].id_jogador);
                    var nm = lista_jogadores[i].Nome;
                    var id_cat = lista_jogadores[i].id_categoria;

                    //insert jogos randon_jogadores
                    Info_Jogos.InserirJogador_randon(id_jog, id_cat);

                }


                var cont_total = Info_Jogos.Contagem_total().cont_total;

                for (int i = 0; i < cont_total; i++)
                {

                    try
                    {
                        var lista = Info_Jogos.Listar_Jogadores();
                        jog_1 = lista.FirstOrDefault().id_jogador;

                        var lista2 = Info_Jogos.Listar_Jogadores2(int.Parse(jog_1));
                        jog_2 = lista2.FirstOrDefault().id_jogador;
                    }
                    catch (Exception)
                    {
                        break;
                    }

                    int Insert1 = 0;
                    int Insert2 = 0;

                    Insert1 = int.Parse(jog_1);
                    Insert2 = int.Parse(jog_2);

                    if (Insert1 != 0 && Insert2 != 0)
                    {
                        Info_Jogos.InserirJogos(Insert1, Insert2, c, mes);
                        Info_Jogos.Deletar_Jogador(Insert1);
                        Info_Jogos.Deletar_Jogador(Insert2);

                    }

                }

            }

            var lista_jogos = Info_Jogos.Listar_Jogos();


            return RedirectToAction("DashBoard", "Administrador", null);
        }

        public ActionResult criar_jogos() 
        {

            return View();
        }

        public ActionResult deletar_jogos()
        {


            return View();
        }

        [HttpPost]
        public ActionResult deletar_jogos(int mes)
        {

            var Info_Jogos = new Aplicacao_Jogos();

            //deletar jogos randon_jogos
            Info_Jogos.Deletar_Jogos_randon(mes);

            return RedirectToAction("DashBoard", "Administrador", null);
        }


    }
}
