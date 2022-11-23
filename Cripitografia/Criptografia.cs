using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace SuperCore
{
    
   

    /// <summary>
    /// calculo e/ou comparação de digito verificador utilizando o modulo 11
    /// </summary>
    public class Mod11
    {
        /// <summary>
        /// compara se o digito verificado é valido
        /// </summary>
        /// <param name="number">string com numero completo contendo o digito verificador a ser validado</param>
        /// <returns></returns>
        public static bool isValid(string number)
        {

            if (!number.eNumero())
                return false;

            string str = number.Substring(0, number.Length - 1);

            if (number == str + AdDigitoVerificador(str))
                return true;
            else
                return false;

        }

        /// <summary>
        /// retorna o digito verificador
        /// </summary>
        /// <param name="number">string que representa o numero para o qual sera gerado o digito verificador</param>
        /// <returns></returns>
        public static string AdDigitoVerificador(string number)
        {
            int sum = 0;
            for (int i = number.Length - 1, multiplier = 2; i >= 0; i--)
            {
                sum += (int)char.GetNumericValue(number[i]) * multiplier;
                if (++multiplier > 9) multiplier = 2;
            }
            int mod = (sum % 11);
            if (mod == 0 || mod == 1) return "0";
            return (11 - mod).ToString();
        }
    }


    /// <summary>
    /// calculo e/ou comparação de digito verificador utilizando o modulo 10
    /// </summary>
    public class Mod10
    {

        /// <summary>
        /// compara se o digito verificado é valido
        /// </summary>
        /// <param name="number">string com numero completo contendo o digito verificador a ser validado</param>
        /// <returns></returns>
        public static bool isValid(string number)
        {

            if (!number.eNumero())
                return false;

            string str = number.Substring(0, number.Length - 1);

            if (number == str + AdDigitoVerificador(str))
                return true;
            else
                return false;

        }

        /// <summary>
        /// retorna o digito verificador
        /// </summary>
        /// <param name="number">string que representa o numero para o qual sera gerado o digito verificador</param>
        /// <returns></returns>
        public static string AdDigitoVerificador(string str)
        {
            int i = 2;
            int sum = 0;
            int res = 0;
            foreach (char c in str.ToCharArray())
            {
                res = Convert.ToInt32(c.ToString()) * i;
                sum += res > 9 ? (res - 9) : res;
                i = i == 2 ? 1 : 2;
            }
            return (10 - (sum % 10)).ToString();
        }

    }


    /// <summary>
    /// funcoes de criptografia e descriptografia
    /// </summary>
    public class TDESAlgorithm
    {

        /// <summary>
        /// criptografa uma string
        /// </summary>
        /// <param name="s">string a ser criptografada</param>
        /// <returns>string ja criptografada</returns>
        public static string Criptografia(string s, string chave)
        {
            if (chave == null)
                throw new ArgumentException("A chave para criptografia não foi definida.");

            byte[] Results;
            System.Text.UTF8Encoding UTF8 = new System.Text.UTF8Encoding();
            MD5CryptoServiceProvider HashProvider = new MD5CryptoServiceProvider();
            byte[] TDESKey = HashProvider.ComputeHash(UTF8.GetBytes(chave));
            TripleDESCryptoServiceProvider TDESAlgorithm = new TripleDESCryptoServiceProvider();
            TDESAlgorithm.Key = TDESKey;
            TDESAlgorithm.Mode = CipherMode.ECB;
            TDESAlgorithm.Padding = PaddingMode.PKCS7;
            byte[] DataToEncrypt = UTF8.GetBytes(s);
            try
            {
                ICryptoTransform Encryptor = TDESAlgorithm.CreateEncryptor();
                Results = Encryptor.TransformFinalBlock(DataToEncrypt, 0, DataToEncrypt.Length);
            }

            finally
            {
                TDESAlgorithm.Clear();
                HashProvider.Clear();
            }
            return Convert.ToBase64String(Results);

        }


        /// <summary>
        /// descriptografa uma string
        /// </summary>
        /// <param name="s">string a ser descripyografada</param>
        /// <returns>string ja descriptografada</returns>
        public static string DesCriptografia(string s, string chave)
        {
            if (chave == null)
                throw new ArgumentException("A chave para criptografia não foi definida.");

            byte[] Results;
            System.Text.UTF8Encoding UTF8 = new System.Text.UTF8Encoding();
            MD5CryptoServiceProvider HashProvider = new MD5CryptoServiceProvider();
            byte[] TDESKey = HashProvider.ComputeHash(UTF8.GetBytes(chave));
            TripleDESCryptoServiceProvider TDESAlgorithm = new TripleDESCryptoServiceProvider();
            TDESAlgorithm.Key = TDESKey;
            TDESAlgorithm.Mode = CipherMode.ECB;
            TDESAlgorithm.Padding = PaddingMode.PKCS7;
            byte[] DataToDecrypt = Convert.FromBase64String(s);
            try
            {
                ICryptoTransform Decryptor = TDESAlgorithm.CreateDecryptor();
                Results = Decryptor.TransformFinalBlock(DataToDecrypt, 0, DataToDecrypt.Length);
            }
            finally
            {
                TDESAlgorithm.Clear();
                HashProvider.Clear();
            }
            return UTF8.GetString(Results);

        }      

    }


    public static class Criptografiarijndael
    {


        private static Rijndael CriarInstanciaRijndael(
            string chave, string vetorInicializacao)
        {
            if (!(chave != null &&
                  (chave.Length == 16 ||
                   chave.Length == 24 ||
                   chave.Length == 32)))
            {
                throw new Exception(
                    "A chave de criptografia deve possuir " +
                    "16, 24 ou 32 caracteres.");
            }

            if (vetorInicializacao == null ||
                vetorInicializacao.Length != 16)
            {
                throw new Exception(
                    "O vetor de inicialização deve possuir " +
                    "16 caracteres.");
            }

            Rijndael algoritmo = Rijndael.Create();
            algoritmo.Key =
                Encoding.ASCII.GetBytes(chave);
            algoritmo.IV =
                Encoding.ASCII.GetBytes(vetorInicializacao);

            return algoritmo;
        }

        /// <summary>
        /// Metodo de criptografia uma string utilizando Rijndael
        /// </summary>
        /// <param name="chave">A chave de criptografia deve possuir 16, 24 ou 32 caracteres</param>
        /// <param name="vetorInicializacao">O vetor de inicialização deve possuir 16 caracteres.</param>
        /// <param name="textoNormal">Texto para ser encriptado</param>
        /// <returns>retorna uma string encriptada</returns>
        public static string Encriptar(
            string chave,
            string vetorInicializacao,
            string textoNormal)
        {
            if (String.IsNullOrWhiteSpace(textoNormal))
            {
                throw new Exception(
                    "O conteúdo a ser encriptado não pode " +
                    "ser uma string vazia.");
            }

            using (Rijndael algoritmo = CriarInstanciaRijndael(
                chave, vetorInicializacao))
            {
                ICryptoTransform encryptor =
                    algoritmo.CreateEncryptor(
                        algoritmo.Key, algoritmo.IV);

                using (MemoryStream streamResultado =
                       new MemoryStream())
                {
                    using (CryptoStream csStream = new CryptoStream(
                        streamResultado, encryptor,
                        CryptoStreamMode.Write))
                    {
                        using (StreamWriter writer =
                            new StreamWriter(csStream))
                        {
                            writer.Write(textoNormal);
                        }
                    }

                    return ArrayBytesToHexString(
                        streamResultado.ToArray());
                }
            }
        }

        private static string ArrayBytesToHexString(byte[] conteudo)
        {
            string[] arrayHex = Array.ConvertAll(
                conteudo, b => b.ToString("X2"));
            return string.Concat(arrayHex);
        }

        /// <summary>
        /// Metodo de descriptografia de uma string utilizando Rijndael
        /// </summary>
        /// <param name="chave">A chave de criptografia deve possuir 16, 24 ou 32 caracteres</param>
        /// <param name="vetorInicializacao">O vetor de inicialização deve possuir 16 caracteres.</param>
        /// <param name="textoNormal">Texto encriptado para ser descriptografado</param>
        /// <returns>retorna uma string descriptografada</returns>
        public static string Decriptar(
            string chave,
            string vetorInicializacao,
            string textoEncriptado)
        {
            if (String.IsNullOrWhiteSpace(textoEncriptado))
            {
                throw new Exception(
                    "O conteúdo a ser decriptado não pode " +
                    "ser uma string vazia.");
            }

            if (textoEncriptado.Length % 2 != 0)
            {
                throw new Exception(
                    "O conteúdo a ser decriptado é inválido.");
            }


            using (Rijndael algoritmo = CriarInstanciaRijndael(
                chave, vetorInicializacao))
            {
                ICryptoTransform decryptor =
                    algoritmo.CreateDecryptor(
                        algoritmo.Key, algoritmo.IV);

                string textoDecriptografado = null;
                using (MemoryStream streamTextoEncriptado =
                    new MemoryStream(
                        HexStringToArrayBytes(textoEncriptado)))
                {
                    using (CryptoStream csStream = new CryptoStream(
                        streamTextoEncriptado, decryptor,
                        CryptoStreamMode.Read))
                    {
                        using (StreamReader reader =
                            new StreamReader(csStream))
                        {
                            textoDecriptografado =
                                reader.ReadToEnd();
                        }
                    }
                }

                return textoDecriptografado;
            }
        }

        private static byte[] HexStringToArrayBytes(string conteudo)
        {
            int qtdeBytesEncriptados =
                conteudo.Length / 2;
            byte[] arrayConteudoEncriptado =
                new byte[qtdeBytesEncriptados];
            for (int i = 0; i < qtdeBytesEncriptados; i++)
            {
                arrayConteudoEncriptado[i] = Convert.ToByte(
                    conteudo.Substring(i * 2, 2), 16);
            }

            return arrayConteudoEncriptado;
        }
    }


   

}