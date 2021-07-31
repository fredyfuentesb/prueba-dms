using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Transversal.Helpers
{
    public static class HashHelper
    {
        /// <summary>
        /// Este metodo recibe una cadena de texto y la devuelve en MD5
        /// </summary>
        /// <param name="word">Cadena de texto a convertir en MD5</param>
        /// <returns>Cadena de texto convertida en MD5</returns>
        public static string MD5(string word)
        {
            MD5 md5 = MD5CryptoServiceProvider.Create();
            ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] stream = null;
            StringBuilder sb = new StringBuilder();
            stream = md5.ComputeHash(encoding.GetBytes(word));
            for (int i = 0; i < stream.Length; i++) sb.AppendFormat("{0:x2}", stream[i]);
            return sb.ToString();
        }

        /// <summary>
        /// Este metodo genera un  valor unico encriptado en MD5, cada vez que se llame el metodo generará un nuevo id
        /// </summary>
        /// <returns>Valor unico generado</returns>
        public static string Token()
        {
            long i = 1;
            foreach (byte b in Guid.NewGuid().ToByteArray()) i *= ((int)b + 1);
            return MD5(string.Format("{0:x}", i - DateTime.Now.Ticks));
        }

        /// <summary>
        /// Permite encriptar en base64 una cadena de texto ingresada 
        /// </summary>
        /// <param name="word">Cadena de texto a encriptar en base64</param>
        /// <returns>Cadena de texto encriptada en Base64</returns>
        public static string Base64Encode(string word)
        {
            byte[] byt = System.Text.Encoding.UTF8.GetBytes(word);
            return Convert.ToBase64String(byt);
        }

        /// <summary>
        /// Permite desencriptar una cadena en base64
        /// </summary>
        /// <param name="word">Cadena en base64 que se va a desencriptar</param>
        /// <returns>Cadena de texto desencriptada</returns>
        public static string Base64Decode(string word)
        {
            byte[] b = Convert.FromBase64String(word);
            return System.Text.Encoding.UTF8.GetString(b);
        }

        /// <summary>
        /// Permite encriptar una cadena de texto usando SHA1
        /// </summary>
        /// <param name="str">Cadena de texto a encriptar</param>
        /// <returns>Cadena de texto encriptada</returns>
        public static string SHA1(string str)
        {
            SHA1 sha1 = SHA1Managed.Create();
            ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] stream = null;
            StringBuilder sb = new StringBuilder();
            stream = sha1.ComputeHash(encoding.GetBytes(str));
            for (int i = 0; i < stream.Length; i++) sb.AppendFormat("{0:x2}", stream[i]);
            return sb.ToString();
        }

        /// <summary>
        /// Permite encriptar una cadena de texto usando SHA256
        /// </summary>
        /// <param name="str">Cadena de texto a encriptar</param>
        /// <returns>Cadena de texto encriptada</returns>
        public static string SHA256(string str)
        {
            SHA256 sha256 = SHA256Managed.Create();
            ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] stream = null;
            StringBuilder sb = new StringBuilder();
            stream = sha256.ComputeHash(encoding.GetBytes(str));
            for (int i = 0; i < stream.Length; i++) sb.AppendFormat("{0:x2}", stream[i]);
            return sb.ToString();
        }

        /// <summary>
        /// Permite encriptar una cadena de texto usando SHA384
        /// </summary>
        /// <param name="str">Cadena de texto a encriptar</param>
        /// <returns>Cadena de texto encriptada</returns>
        public static string SHA384(string str)
        {
            SHA384 sha384 = SHA384Managed.Create();
            ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] stream = null;
            StringBuilder sb = new StringBuilder();
            stream = sha384.ComputeHash(encoding.GetBytes(str));
            for (int i = 0; i < stream.Length; i++) sb.AppendFormat("{0:x2}", stream[i]);
            return sb.ToString();
        }

        /// <summary>
        /// Permite encriptar una cadena de texto usando SHA512
        /// </summary>
        /// <param name="str">Cadena de texto a encriptar</param>
        /// <returns>Cadena de texto encriptada</returns>
        public static string SHA512(string str)
        {
            SHA512 sha512 = SHA512Managed.Create();
            ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] stream = null;
            StringBuilder sb = new StringBuilder();
            stream = sha512.ComputeHash(encoding.GetBytes(str));
            for (int i = 0; i < stream.Length; i++) sb.AppendFormat("{0:x2}", stream[i]);
            return sb.ToString();
        }
    }
}
