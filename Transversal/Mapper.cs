/*MyS Mapper
 *Clase que permite mapear de un objeto origen a un objeto destino, las propiedades de cada objeto deben tenr el mismo nombre
 *Esta Clase fue generada por medio de una herramienta de generacion automatica de codigo, si usted edita la funcionalidad de la misma por favor indiquelo en los comentarios.
 *<autor>Fredy Fuentes</autor>
 *<Fecha>30/07/2021 20:29:20</Fecha>
 *<Cambios>Indique su Nombre, la Fecha y el cambio realizado</Cambios>
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
namespace Transversal
{
    /// <summary>
    /// Clase estatica de tipo generico 
    /// </summary>
    /// <typeparam name="T">Tipo de parametro con el cual se realizara el mapeo</typeparam>
    public static class Mapper<T> where T : class
    {
        /// <summary>
        /// Metodo estatico que realiza el mapeo entre un objeto de origen y un objeto de destino
        /// Las propiedades de los objetos de origen y destino deben llamarse de la misma forma
        /// </summary>
        /// <param name="origen">Objeto de origne que contiene los datos a mapear</param>
        /// <param name="destino">Objeto de destino vacio que contendra los datos del objeto origen</param>
        /// <returns>Objeto destino con los datos mapeados del objeto de origen</returns>
        public static T Map(object origen, T destino)
        {
            foreach (MemberInfo miOrigen in origen.GetType().GetMembers())
            {
                if (miOrigen.MemberType == MemberTypes.Property)
                {
                    PropertyInfo piOrigen = miOrigen as PropertyInfo;
                    if (piOrigen != null)
                    {
                        foreach (MemberInfo miDestino in destino.GetType().GetMembers())
                        {
                            if (miDestino.MemberType == MemberTypes.Property)
                            {
                                PropertyInfo piDestino = miDestino as PropertyInfo;
                                if (piDestino != null)
                                {
                                    if (piOrigen.Name == piDestino.Name)
                                    {
                                        piDestino.SetValue(destino, piOrigen.GetValue(origen, null));
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return destino;
        }
    }
}
