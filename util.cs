using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SuperCore
{
    /// <summary>
    /// metodos diversos
    /// </summary>
    public static class Util
    {
       

        /// <summary>
        /// representa a listagem de dias da semana
        /// </summary>        
        public class diaSemana
        {
            public int id { get; set; }
            public string dia { get; set; }

            /// <summary>
            /// representa um dia especifivo
            /// </summary>
            /// <param name="id">codigo do dia da semana começando por domingo=1</param>
            /// <param name="dia">dia da semana por extenso em portugues</param>
            public diaSemana(int d, string s)
            {
                this.id = d;
                this.dia = s;
            }


            /// <summary>
            /// retorna um dia especifico com base em seu id
            /// </summary>
            /// <param name="d">codigo do dia da semana começando por domingo=1</param>   
            /// <returns>retorna um dia especifico</returns>
            public static diaSemana getdia(int d)
            {

                if (d > 0 && d < 8)
                {
                    return diaSemana.get().Where(x => x.id == d).FirstOrDefault();
                }
                else
                    throw new ArgumentException("Valores válidos para o parametro são do 1 ao 7");
            }


            /// <summary>
            /// representa a listagem de dias da semana
            /// </summary>
            /// <returns>uma lista de dias da Semana</returns>
            public static List<diaSemana> get()
            {
                List<diaSemana> dias = new List<diaSemana>();

                diaSemana d1 = new diaSemana(1, "Domingo");
                dias.Add(d1);

                diaSemana d2 = new diaSemana(2, "Segunda");
                dias.Add(d2);

                diaSemana d3 = new diaSemana(3, "Terça");
                dias.Add(d3);

                diaSemana d4 = new diaSemana(4, "Quarta");
                dias.Add(d4);

                diaSemana d5 = new diaSemana(5, "Quinta");
                dias.Add(d5);

                diaSemana d6 = new diaSemana(6, "Sexta");
                dias.Add(d6);

                diaSemana d7 = new diaSemana(7, "Sábado");
                dias.Add(d7);

                return dias;
            }
        }

        /// <summary>
        /// representa a listagem de Anos
        /// </summary>
        public class Anos
        {

            public Anos()
            { }

            /// <summary>
            /// retorna uma listagem de anos
            /// </summary>
            /// <param name="inicio">ano de inicia da listagem</param>
            /// <param name="fim">ano de fim da listagem</param>
            /// <returns>exp: 2001,2002,2003....</returns>
            public static List<int> get(int inicio, int fim)
            {
                List<int> LAnos = new List<int>();

                for (int i = inicio; i < fim + 1; i++)
                {
                    LAnos.Add(i);
                }

                return LAnos;
            }

            /// <summary>
            /// retorna uma listagem de anos apartir do ano atual
            /// </summary>
            /// <param name="num">numero de anos na listagem</param>
            /// <returns>Se o ano atual for 2010 e o parametro num=5 retorna uma listagem de 2010 a 2014</returns>
            public static List<int> get(int num)
            {
                List<int> LAnos = new List<int>();

                int inicio = DateTime.Now.Year;
                int fim = inicio + num;

                for (int i = inicio; i < fim + 1; i++)
                {
                    LAnos.Add(i);
                }

                return LAnos;
            }

        }

        /// <summary>
        /// representa a listagem de meses do ano
        /// </summary>        
        public class Mes
        {
            public int numero { get; set; }
            public string extenso { get; set; }

            /// <summary>
            /// representa um mês do ano especifivo
            /// </summary>
            /// <param name="numero">numero que representa o mês do ano</param>
            /// <param name="extenso">nome do mes por extenso em portugues</param>
            public Mes(int d, string s)
            {
                this.numero = d;
                this.extenso = s;
            }


            /// <summary>
            /// retorna um mes especifico com base em seu numero
            /// </summary>
            /// <param name="d">numero que representa o mes</param>   
            /// <returns>retorna um mes especifico</returns>
            public static Mes getMes(int d)
            {

                if (d > 0 && d < 13)
                {
                    return Mes.get().Where(x => x.numero == d).FirstOrDefault();

                }
                else
                    throw new ArgumentException("Valores válidos para o parametro são do 1 ao 12");
            }


            /// <summary>
            /// representa a listagem dos meses do ano
            /// </summary>
            /// <returns>uma lista dos meses do ano</returns>
            public static List<Mes> get()
            {
                List<Mes> meses = new List<Mes>();

                Mes m1 = new Mes(1, "janeiro");
                meses.Add(m1);

                Mes m2 = new Mes(2, "fevereiro");
                meses.Add(m2);

                Mes m3 = new Mes(3, "março");
                meses.Add(m3);

                Mes m4 = new Mes(4, "abril");
                meses.Add(m4);

                Mes m5 = new Mes(5, "maio");
                meses.Add(m5);

                Mes m6 = new Mes(6, "junho");
                meses.Add(m6);

                Mes m7 = new Mes(7, "julho");
                meses.Add(m7);

                Mes m8 = new Mes(8, "agosto");
                meses.Add(m8);

                Mes m9 = new Mes(9, "setembro");
                meses.Add(m9);

                Mes m10 = new Mes(10, "outubro");
                meses.Add(m10);

                Mes m11 = new Mes(11, "novembro");
                meses.Add(m11);

                Mes m12 = new Mes(12, "dezembro");
                meses.Add(m12);

                return meses;
            }
        }

        /// <summary>
        /// representa a listagem de estados do Brasil
        /// </summary>  
        public class EstadoDoBrasil
        {
            public string sigla { get; set; }
            public string nome { get; set; }

            /// <summary>
            /// representa um estado do Brasil
            /// </summary>
            /// <param name="s">sigla do estado com 2 letras</param>
            /// <param name="n">nome do estado</param>
            public EstadoDoBrasil(string s, string n)
            {
                sigla = s;
                nome = n;
            }

            /// <summary>
            /// retorna um estado do Brasil especifico com base na sua sigla
            /// </summary>
            /// <param name="s">sigla do estado com 2 letras</param>   
            /// <returns>retorna um estado do Brasil especifico</returns>
            public static EstadoDoBrasil getEstado(string s)
            {

                var estado = EstadoDoBrasil.get().Where(x => x.sigla == s);

                if (estado.Any())
                    return estado.FirstOrDefault();
                else
                    throw new ArgumentException("Nenhum estado com sigla informada.");

            }

            /// <summary>
            /// representa a listagem dos estados do Brasil
            /// </summary>
            /// <returns>uma lista dos estados do Brasil</returns>
            public static List<EstadoDoBrasil> get()
            {
                List<EstadoDoBrasil> estados = new List<EstadoDoBrasil>();

                estados.Add(new EstadoDoBrasil("AC", "Acre"));
                estados.Add(new EstadoDoBrasil("AL", "Alagoas"));
                estados.Add(new EstadoDoBrasil("AP", "Amapá"));
                estados.Add(new EstadoDoBrasil("AM", "Amazonas"));
                estados.Add(new EstadoDoBrasil("BA", "Bahia"));
                estados.Add(new EstadoDoBrasil("CE", "Ceará"));
                estados.Add(new EstadoDoBrasil("DF", "Distrito Federal"));
                estados.Add(new EstadoDoBrasil("ES", "Espírito Santo"));
                estados.Add(new EstadoDoBrasil("GO", "Goiás"));
                estados.Add(new EstadoDoBrasil("MA", "Maranhão"));
                estados.Add(new EstadoDoBrasil("MT", "Mato Grosso"));
                estados.Add(new EstadoDoBrasil("MS", "Mato Grosso do Sul"));
                estados.Add(new EstadoDoBrasil("MG", "Minas Gerais"));
                estados.Add(new EstadoDoBrasil("PA", "Pará"));
                estados.Add(new EstadoDoBrasil("PB", "Paraíba"));
                estados.Add(new EstadoDoBrasil("PR", "Paraná"));
                estados.Add(new EstadoDoBrasil("PE", "Pernambuco"));
                estados.Add(new EstadoDoBrasil("PI", "Piauí"));
                estados.Add(new EstadoDoBrasil("RJ", "Rio de Janeiro"));
                estados.Add(new EstadoDoBrasil("RN", "Rio Grande do Norte"));
                estados.Add(new EstadoDoBrasil("RS", "Rio Grande do Sul"));
                estados.Add(new EstadoDoBrasil("RO", "Rondônia"));
                estados.Add(new EstadoDoBrasil("RR", "Roraima"));
                estados.Add(new EstadoDoBrasil("SC", "Santa Catarina"));
                estados.Add(new EstadoDoBrasil("SP", "São Paulo"));
                estados.Add(new EstadoDoBrasil("SE", "Sergipe"));
                estados.Add(new EstadoDoBrasil("TO", "Tocantins"));

                return estados;

            }




        }


    }
}
