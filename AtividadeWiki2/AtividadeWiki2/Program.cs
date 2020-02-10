using System;
using System.Collections.Generic;
using System.Linq;

/*
II.Construa um programa Console Application em C# que defina um modelo de ordem de serviço. Considere as informações para OS:
Número(auto incremento)
Data de Abertura
Data de Encerramento(pode aceitar nulo)
Nome do Responsável
Áreas da OS(soma das áreas)

Uma área pode ser composta por:
Código
Tamanho da Área

Utilizando uma List<T>(como um banco de dados em memória), o usuário poderá operar as seguintes informações:

0. Sair do programa.
1. Criar uma nova OS(solicitar data de abertura).
2. Listar todas as OS cadastradas.
3. Encerrar uma OS.
4. Incluir uma nova área na OS.
Dicas: Construa duas classes uma para a OS e outra para a Área e relacione estas através de propriedades (1 para N). 
Solicite as informações e utilizando uma List<T> para simular um banco de dados em memória, realize as operações listadas.
Crie um loop sobre o menu para que o programa não encerre com apenas uma operação.
*/

namespace AtividadeWiki2
{
    class Program
    {
        static void Main(string[] args)
        {
            List<OrdemDeServico> ListaDeOS = new List<OrdemDeServico>();
            int comando = -1;

            while (true)
            {
                InicializarMenu();

                comando = ReceberComando();

                switch (comando)
                {
                    case 0:
                        return;
                        break;

                    case 1:

                        Console.Clear();
                        Console.Write("Digite a data da abertura da OS ou 0 para retornar ao Menu Principal: ");
                        comando = ReceberComando();

                        if (!comando.Equals(0))
                        {
                            OrdemDeServico os = new OrdemDeServico();
                            while (RealizaAberturaDaOS(os, ListaDeOS)) { }
                        }

                        break;

                    default:
                        break;


                }
            }
        }

        static bool RealizaAberturaDaOS(OrdemDeServico os, List<OrdemDeServico> lista)
        {
            try
            {
                if (lista.Count > 0)
                    os.Numero = lista.Max(x => x.Numero) + 1;
                else
                    os.Numero = 1;

                os.Abertura = Convert.ToDateTime(Console.ReadLine());
                os.Responsavel = Console.ReadLine();

                Console.Write("\nOS criada com sucesso. Pressione qualquer tecla para continuar...\n");
                Console.ReadLine();
                return true;
            }
            catch (Exception ex)
            {
                Console.Write("\nOcorreu um erro na abertura da OS.\n" + ex.Message);
                Console.ReadLine();
                return false;
            }

        }

        static int ReceberComando()
        {
            try
            {
                var comando = Convert.ToInt32(Console.ReadLine());
                return comando;
            }
            catch (Exception)
            {
                Console.Write("\nOpção inválida.\n");
            }

            return -1;
        }

        static void InicializarMenu()
        {
            Console.Clear();
            Console.Write("0. Sair do programa.\n" +
                          "1. Criar uma nova OS.\n" +
                          "2. Listar todas as OS cadastradas.\n" +
                          "3. Encerrar uma OS.\n" +
                          "4. Incluir uma nova área na OS.\n\n");

            Console.Write("Digite sua opção: ");
        }
    }

    public class OrdemDeServico
    {
        public OrdemDeServico()
        {

        }

        public long Numero { get; set; }
        public DateTime Abertura { get; set; }
        public DateTime? Encerramento { get; set; }
        public string Responsavel { get; set; }
        public List<Area> Areas { get; set; }

    }

    public class Area
    {
        public Area()
        {

        }

        public long Codigo { get; set; }
        public decimal Tamanho { get; set; }

    }

    public static class Auxiliar
    {
        static long ProximoID(this List<OrdemDeServico> lista)
        {
            return lista.Max(x => x.Numero) + 1;
        }
    }

}
