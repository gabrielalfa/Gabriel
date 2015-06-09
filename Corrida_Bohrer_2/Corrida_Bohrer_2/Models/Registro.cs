using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Corrida_Bohrer.Models
{
    public class Registro
    {
        public int id { get; set; }
       

        [Required (ErrorMessage="Nome Requerido")]
        public string Nome { get; set; }
      
        public string email { get; set; }
     
        public int id_categoria { get; set; }
        
        public string data_nasc { get; set; }
      
        public string telefone { get; set; }
     
        public string password { get; set; }

        public string CPF { get; set; }

    }
}