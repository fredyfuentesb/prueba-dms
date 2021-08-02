using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PruebaWeb.Helpers
{
    public static class StringHelper
    {
        public static Dictionary<string, string> ObtenerDiccionario(string parametro)
        {
            string[] paramSplit1 = parametro.Split('&');
            return paramSplit1.Select(par => par.Split('=')).ToDictionary(paramSplit2 => paramSplit2[0], paramSplit2 => paramSplit2[1]);
        }
    }
}