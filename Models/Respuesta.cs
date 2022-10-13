using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EKS.Models
{
    public class Respuesta
    {
        public int Exito { get; set; }
        public string Mensaje { get; set; }
        public Data Data { get; set; }
    }
}