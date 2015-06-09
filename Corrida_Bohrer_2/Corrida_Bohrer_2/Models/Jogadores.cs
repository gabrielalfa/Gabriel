using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Corrida_Bohrer.Models
{
    public class Jogadores
    {
        public string id_jogador { get; set; }
        public string Nome { get; set; }
        public int id_categoria { get; set; }
        public string path { get; set; }


        public int cont { get; set; }
        public int cont_total { get; set; }

        public string adm { get; set; }
    }
}