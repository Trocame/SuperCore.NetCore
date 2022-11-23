using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Text.RegularExpressions;

namespace SuperCore
{
    public static class StringExtensions
    {

        /// <summary>
        /// remove caracteres acentuados
        /// </summary>
        /// <param name="texto"></param>
        /// <returns></returns>
        public static string RemoverAcentos(this String texto)
        {
            //if (string.IsNullOrEmpty(texto))
            //    return "";
            //else
            //{
            //    byte[] bytes = System.Text.Encoding.GetEncoding("iso-8859-8").GetBytes(texto);
            //    return System.Text.Encoding.UTF8.GetString(bytes);
            //}

            string t = texto;

            if (!string.IsNullOrEmpty(t))
            {

                string[] expressoes = { "[áàâãä]", "[ÁÀÂÃÄ]", "[éèêë]", "[ÉÈÊË]", "[íìïî]", "[ÍÌÏÎ]", "[óòôõö]", "[ÓÒÔÕÖ]", "[úùûü]", "[ÚÙÛÜ]", "[ç]", "[Ç]" };

                string[] substituicoes = { "a", "A", "e", "E", "i", "I", "o", "O", "u", "U", "c", "C" };

                RegexOptions options = RegexOptions.Multiline;

                for (int i = 0; i < expressoes.Length; i++)
                {
                    Regex r = new Regex(expressoes[i], options);
                    t = r.Replace(t, substituicoes[i]);
                }

            }

            return t;
        }


        /// <summary>
        /// remove caracteres especiais, permanecendo somente letras e numeros
        /// </summary>
        /// <param name="texto">string para remover os caraceters especiais</param>
        /// <returns></returns>
        public static string RemoverCaracteresEspeciais(this String texto)
        {
            if (string.IsNullOrEmpty(texto))
                return string.Empty;

            return Regex.Replace(texto, "[^a-zA-Z0-9 çÇáÁàÀâÂãÃäÄéÉèÈêÊíÍìÌóÓòÒôÔõöÖÕúÚùÙ]", "");

        }

        /// <summary>
        /// remove as tag html do texto
        /// </summary>
        /// <param name="Html">string com tags a serem removidas</param>
        /// <returns></returns>
        public static string RemoverTagHtml(this String Html)
        {
            return Regex.Replace(Html, @"<(.|\n)*?>", string.Empty);

        }

        /// <summary>
        /// remove letras e caracteres especiais matendo somente os numeros
        /// </summary>
        /// <param name="texto">string de onde serão removidas as letras e caracteres especiais</param>
        /// <returns></returns>
        public static string RemoverLetraseCaracteresEspeciais(this String texto)
        {
            string resultString = string.Empty;
            Regex regexObj = new Regex(@"[^\d]");
            resultString = regexObj.Replace(texto, "");
            return resultString;

        }

        /// <summary>
        /// Função Capitaliza o texto mantendo as conjunções principais em formato lowercase 
        /// </summary>
        /// <remarks>
        /// Essa função foi encontrada em: http://channel9.msdn.com/Forums/TechOff/252814-Howto-Capitalize-first-char-of-words-in-a-string-NETC
        /// </remarks>
        /// <param name="entrada">Entrada de texto para converter</param>
        /// <param name="acentuados">Deseja remover a acentuação de caracteres?</param>
        /// <param name="latinos">Deseja substituir caracteres latinos por seus correspondentes?</param>
        /// <returns>Retorna o texto convertido</returns>
        public static string capitalizarNomes(this String entrada, bool acentuados, bool latinos)
        {
            if (string.IsNullOrEmpty(entrada))
            {
                return entrada;
            }
            else
            {

                entrada = entrada.RemoverTagHtml();

                StringBuilder sb = new StringBuilder(entrada.Length);

                sb.Append(char.ToUpper(entrada[0]));

                for (int i = 1; i < entrada.Length; i++)
                {
                    char c = entrada[i];
                    if (char.IsWhiteSpace(entrada[i - 1]) || (entrada[i - 1] == '(') || (entrada[i - 1] == '{') || (entrada[i - 1] == '[') || (entrada[i - 1] == '/') || (entrada[i - 1] == '\\'))
                        c = char.ToUpper(c);
                    else
                        c = char.ToLower(c);

                    sb.Append(c);
                }

                string[] encontrar = { " Da ", " Das ", " De ", " Do ", " Dos ", " Em ", " E ", " Ou ", " Com ", " Na ", " Nas ", " No ", " Nos " };

                string retorno = sb.ToString();

                foreach (string i in encontrar)
                    retorno = retorno.Replace(i, i.ToLower());


                retorno = acentuados ? retorno.RemoverAcentos() : retorno;
                retorno = latinos ? retorno.RemoverAcentos() : retorno;

                return retorno;

            }
        }


        /// <summary>
        /// Converte um valor Decimal para String com número em extenso
        /// </summary>
        /// <param name="valor">Valor a ser convertido</param>
        /// <returns>Valor por extenso</returns>
        public static string ConverteParaExtenso(this decimal valor)
        {
            if (valor <= 0 | valor >= 1000000000000000)
                return "Não foi possível converter o valor em extenso.";
            else
            {
                string strValor = valor.ToString("000000000000000.00");
                string valor_por_extenso = string.Empty;

                for (int i = 0; i <= 15; i += 3)
                {
                    valor_por_extenso += GetTresAlgarismos(Convert.ToDecimal(strValor.Substring(i, 3)));
                    if (i == 0 & valor_por_extenso != string.Empty)
                    {
                        if (Convert.ToInt32(strValor.Substring(0, 3)) == 1)
                            valor_por_extenso += " TRILHÃO" + ((Convert.ToDecimal(strValor.Substring(3, 12)) > 0) ? " E " : string.Empty);
                        else if (Convert.ToInt32(strValor.Substring(0, 3)) > 1)
                            valor_por_extenso += " TRILHÕES" + ((Convert.ToDecimal(strValor.Substring(3, 12)) > 0) ? " E " : string.Empty);
                    }
                    else if (i == 3 & valor_por_extenso != string.Empty)
                    {
                        if (Convert.ToInt32(strValor.Substring(3, 3)) == 1)
                            valor_por_extenso += " BILHÃO" + ((Convert.ToDecimal(strValor.Substring(6, 9)) > 0) ? " E " : string.Empty);
                        else if (Convert.ToInt32(strValor.Substring(3, 3)) > 1)
                            valor_por_extenso += " BILHÕES" + ((Convert.ToDecimal(strValor.Substring(6, 9)) > 0) ? " E " : string.Empty);
                    }
                    else if (i == 6 & valor_por_extenso != string.Empty)
                    {
                        if (Convert.ToInt32(strValor.Substring(6, 3)) == 1)
                            valor_por_extenso += " MILHÃO" + ((Convert.ToDecimal(strValor.Substring(9, 6)) > 0) ? " E " : string.Empty);
                        else if (Convert.ToInt32(strValor.Substring(6, 3)) > 1)
                            valor_por_extenso += " MILHÕES" + ((Convert.ToDecimal(strValor.Substring(9, 6)) > 0) ? " E " : string.Empty);
                    }
                    else if (i == 9 & valor_por_extenso != string.Empty)
                        if (Convert.ToInt32(strValor.Substring(9, 3)) > 0)
                            valor_por_extenso += " MIL" + ((Convert.ToDecimal(strValor.Substring(12, 3)) > 0) ? " E " : string.Empty);

                    if (i == 12)
                    {
                        if (valor_por_extenso.Length > 8)
                            if (valor_por_extenso.Substring(valor_por_extenso.Length - 6, 6) == "BILHÃO" | valor_por_extenso.Substring(valor_por_extenso.Length - 6, 6) == "MILHÃO")
                                valor_por_extenso += " DE";
                            else
                                if (valor_por_extenso.Substring(valor_por_extenso.Length - 7, 7) == "BILHÕES" | valor_por_extenso.Substring(valor_por_extenso.Length - 7, 7) == "MILHÕES" | valor_por_extenso.Substring(valor_por_extenso.Length - 8, 7) == "TRILHÕES")
                                valor_por_extenso += " DE";
                            else
                                    if (valor_por_extenso.Substring(valor_por_extenso.Length - 8, 8) == "TRILHÕES")
                                valor_por_extenso += " DE";

                        if (Convert.ToInt64(strValor.Substring(0, 15)) == 1)
                            valor_por_extenso += " REAL";
                        else if (Convert.ToInt64(strValor.Substring(0, 15)) > 1)
                            valor_por_extenso += " REAIS";

                        if (Convert.ToInt32(strValor.Substring(16, 2)) > 0 && valor_por_extenso != string.Empty)
                            valor_por_extenso += " E ";
                    }

                    if (i == 15)
                        if (Convert.ToInt32(strValor.Substring(16, 2)) == 1)
                            valor_por_extenso += " CENTAVO";
                        else if (Convert.ToInt32(strValor.Substring(16, 2)) > 1)
                            valor_por_extenso += " CENTAVOS";
                }
                return valor_por_extenso;
            }
        }



        private static string GetTresAlgarismos(decimal valor)
        {
            if (valor <= 0)
                return string.Empty;
            else
            {
                string montagem = string.Empty;
                if (valor > 0 & valor < 1)
                {
                    valor *= 100;
                }
                string strValor = valor.ToString("000");
                int a = Convert.ToInt32(strValor.Substring(0, 1));
                int b = Convert.ToInt32(strValor.Substring(1, 1));
                int c = Convert.ToInt32(strValor.Substring(2, 1));

                if (a == 1) montagem += (b + c == 0) ? "CEM" : "CENTO";
                else if (a == 2) montagem += "DUZENTOS";
                else if (a == 3) montagem += "TREZENTOS";
                else if (a == 4) montagem += "QUATROCENTOS";
                else if (a == 5) montagem += "QUINHENTOS";
                else if (a == 6) montagem += "SEISCENTOS";
                else if (a == 7) montagem += "SETECENTOS";
                else if (a == 8) montagem += "OITOCENTOS";
                else if (a == 9) montagem += "NOVECENTOS";

                if (b == 1)
                {
                    if (c == 0) montagem += ((a > 0) ? " E " : string.Empty) + "DEZ";
                    else if (c == 1) montagem += ((a > 0) ? " E " : string.Empty) + "ONZE";
                    else if (c == 2) montagem += ((a > 0) ? " E " : string.Empty) + "DOZE";
                    else if (c == 3) montagem += ((a > 0) ? " E " : string.Empty) + "TREZE";
                    else if (c == 4) montagem += ((a > 0) ? " E " : string.Empty) + "QUATORZE";
                    else if (c == 5) montagem += ((a > 0) ? " E " : string.Empty) + "QUINZE";
                    else if (c == 6) montagem += ((a > 0) ? " E " : string.Empty) + "DEZESSEIS";
                    else if (c == 7) montagem += ((a > 0) ? " E " : string.Empty) + "DEZESSETE";
                    else if (c == 8) montagem += ((a > 0) ? " E " : string.Empty) + "DEZOITO";
                    else if (c == 9) montagem += ((a > 0) ? " E " : string.Empty) + "DEZENOVE";
                }
                else if (b == 2) montagem += ((a > 0) ? " E " : string.Empty) + "VINTE";
                else if (b == 3) montagem += ((a > 0) ? " E " : string.Empty) + "TRINTA";
                else if (b == 4) montagem += ((a > 0) ? " E " : string.Empty) + "QUARENTA";
                else if (b == 5) montagem += ((a > 0) ? " E " : string.Empty) + "CINQUENTA";
                else if (b == 6) montagem += ((a > 0) ? " E " : string.Empty) + "SESSENTA";
                else if (b == 7) montagem += ((a > 0) ? " E " : string.Empty) + "SETENTA";
                else if (b == 8) montagem += ((a > 0) ? " E " : string.Empty) + "OITENTA";
                else if (b == 9) montagem += ((a > 0) ? " E " : string.Empty) + "NOVENTA";

                if (strValor.Substring(1, 1) != "1" & c != 0 & montagem != string.Empty) montagem += " E ";

                if (strValor.Substring(1, 1) != "1")
                    if (c == 1) montagem += "UM";
                    else if (c == 2) montagem += "DOIS";
                    else if (c == 3) montagem += "TRÊS";
                    else if (c == 4) montagem += "QUATRO";
                    else if (c == 5) montagem += "CINCO";
                    else if (c == 6) montagem += "SEIS";
                    else if (c == 7) montagem += "SETE";
                    else if (c == 8) montagem += "OITO";
                    else if (c == 9) montagem += "NOVE";

                return montagem;
            }
        }




        /// <summary>
        /// Verificar se uma string contem somente numeros
        /// </summary>
        /// <param name="numero">string a verificar</param>
        /// <returns></returns>
        public static bool eNumero(this string numero)
        {
            return numero.All(char.IsDigit);

        }

        /// <summary>
        /// Verificar se uma string é uma data valida
        /// </summary>
        /// <param name="data">string a verificar</param>
        /// <returns></returns>
        public static bool eData(this string data)
        {
            DateTime dt = new DateTime();
            return DateTime.TryParse(data, out dt);

        }



    }


    /// <summary>
    /// funcoes para serem utilizadas com secure string
    /// </summary>
    public static class SecureStringExtensions
    {
        /// <summary>
        /// converte uma string segura para uma string
        /// </summary>
        /// <param name="secureString">string segura a ser convertida</param>
        /// <returns>retorna uma string</returns>
        public static string ToUnsecureString(this SecureString secureString)
        {
            if (secureString == null) throw new ArgumentNullException("secureString");

            var unmanagedString = IntPtr.Zero;
            try
            {
                unmanagedString = Marshal.SecureStringToGlobalAllocUnicode(secureString);
                return Marshal.PtrToStringUni(unmanagedString);
            }
            finally
            {
                Marshal.ZeroFreeGlobalAllocUnicode(unmanagedString);
            }
        }


        /// <summary>
        /// converte uma string para uma string segura
        /// </summary>
        /// <param name="unsecureString">string a ser convertida</param>
        /// <returns>retorna uma string segura</returns>
        public static SecureString ToSecureString(this string unsecureString)
        {
            if (unsecureString == null) throw new ArgumentNullException("unsecureString");

            return unsecureString.Aggregate(new SecureString(), AppendChar, MakeReadOnly);
        }


        private static SecureString MakeReadOnly(SecureString ss)
        {
            ss.MakeReadOnly();
            return ss;
        }

        private static SecureString AppendChar(SecureString ss, char c)
        {
            ss.AppendChar(c);
            return ss;
        }
    }
}
