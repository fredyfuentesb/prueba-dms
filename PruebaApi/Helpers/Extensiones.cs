using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;

namespace PruebaApi.Helpers
{
    static class Extensiones
    {
        /// <summary>
        /// Permite comparar los cambios que tengan los atributos de un objeto
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="val1"></param>
        /// <param name="val2"></param>
        /// <returns></returns>
        public static List<Variacion> ComparacionDetallada<T>(this T val1, T val2)
        {
            List<Variacion> variaciones = new List<Variacion>();
            ///Se obtienen todos los atributos del objeto
            PropertyInfo[] atributos = val1.GetType().GetProperties();
            foreach (PropertyInfo atributo in atributos)
            {
                if (!atributo.Name.Equals("fecha_ultima_modificacion"))
                {
                    Variacion variacion = new Variacion();
                    variacion.propiedad = atributo.Name;
                    variacion.valorA = atributo.GetValue(val1);
                    variacion.valorB = atributo.GetValue(val2);
                    if (!variacion.valorA.Equals(variacion.valorB))
                        variaciones.Add(variacion);
                }
            }
            return variaciones;
        }
    }

    public class Variacion
    {
        public string propiedad { get; set; }
        public object valorA { get; set; }
        public object valorB { get; set; }
    }
}