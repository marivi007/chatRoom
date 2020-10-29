using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UnisantaChat.Models
{
    public class Cadastro
    {
        public string Nome { get; set; }

        public string Senha{ get; set; }

        public string Email { get; set; }

        public string Foto { get; set; }

        public int Tipo { get; set; }
    }
}