using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MvcCore.Helpers
{
    public class CypherService
    {

        public static String EncriptarTextoBasico(String contenido)
        {
            //PRIMERO EN BRUTO Y LUEGO HACEMOS INYECCIÓN DE LO NECESARIO
            //NECESITAMOS TRABAJAR A NIVEL DE byte[]
            //DEBEMOS CONVERTIR A byte[] EL CONTENIDO DE ENTRADA
            byte[] entrada;
            //EL CIFRADO SE REALIZA A NIVEL DE byte[]
            //Y DEVOLVERÁ OTRO byte[] DE SALIDA
            byte[] salida;
            //NECESITAMOS UN CONVERSOR PARA TRANSFORMAR byte[]
            //A String Y VICEVERSA
            UnicodeEncoding encoding = new UnicodeEncoding();
            //NECESITAMOS EL OBJETO QUE SE ENCARGARÁ
            //DE REALIZAR EL CIFRADO -> using System.Security.Cryptography;
            SHA1Managed sha = new SHA1Managed();
            //DEBEMOS CONVERTIR EL CONTENIDO DE ENTRADA A byte[]
            entrada = encoding.GetBytes(contenido);
            //EL OBJETO SHA1Managed TIENE UN MÉOTOD
            //PARA DEVOLVER LOS byte[] DE SALIDA REALIZANDO EL CIFRADO
            salida = sha.ComputeHash(entrada);
            String res = encoding.GetString(salida);
            return res;
        }

        //MÉTODO PARA GENERAR SALT, AUNQUE EXISTEN NUGGETS
        public static String GetSalt()
        {
            Random random = new Random();
            String salt = "";
            for (int i = 1; i <= 50; i++)
            {
                int aleat = random.Next(0, 255);
                char letra = Convert.ToChar(aleat);
                salt += letra;
            }
            return salt;
        }

        public static String CifrarContenido(String contenido, int iteraciones, String salt)
        {
            ////PARA EL SALT, PUES SE ALMACENA ENTRE MEDIAS
            ////DEL CONTENIDO, EN POSICIONES QUE YO QUIERO...
            //String contenidosalt = contenido + salt;
            //SHA256Managed sha = new SHA256Managed();
            //byte[] salida;
            //salida = Encoding.UTF8.GetBytes(contenidosalt);
            ////CIFRAMOS EL NÚMERO DE ITERACIONES QUE NOS INDICAN
            //for (int i = 1; i <= 100; i++)
            //{
            //    //REALIZAMOS EL CIFRADO n VECES
            //    salida = sha.ComputeHash(salida);
            //}
            //sha.Clear();
            ////String textosalida = Encoding.UTF8.GetString(salida);
            ////return textosalida;
            //return salida;
            return "";
        }

        public static byte[] CifrarContenido(String contenido, String salt)
        {
            //PARA EL SALT, PUES SE ALMACENA ENTRE MEDIAS
            //DEL CONTENIDO, EN POSICIONES QUE YO QUIERO...
            String contenidosalt = contenido + salt;
            SHA256Managed sha = new SHA256Managed();
            byte[] salida;
            salida = Encoding.UTF8.GetBytes(contenidosalt);
            //CIFRAMOS EL NÚMERO DE ITERACIONES QUE NOS INDICAN
            for (int i = 1; i <= 100; i++)
            {
                //REALIZAMOS EL CIFRADO n VECES
                salida = sha.ComputeHash(salida);
            }
            sha.Clear();
            //String textosalida = Encoding.UTF8.GetString(salida);
            //return textosalida;
            return salida;
        }
    }
}
