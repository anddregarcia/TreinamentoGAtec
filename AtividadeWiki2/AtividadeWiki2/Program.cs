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

        #region Main

        static void Main(string[] args)
        {
            OpcaoMenu menu = OpcaoMenu.Menu_Principal;
            ListaDeOS listaOS = new ListaDeOS();

            listaOS.ListaOS.Add(new OrdemDeServico { Numero = 1, Abertura = DateTime.Now, Encerramento = null, Areas = new List<Area>(), Responsavel = "André" });
            listaOS.ListaOS.Add(new OrdemDeServico { Numero = 2, Abertura = DateTime.Now.AddDays(-1), Encerramento = null, Areas = new List<Area>(), Responsavel = "André" });
            listaOS.ListaOS.Add(new OrdemDeServico { Numero = 3, Abertura = DateTime.Now.AddDays(-2), Encerramento = null, Areas = new List<Area>(), Responsavel = "André" });

            while (true)
            {
                InicializarMenu();

                var comando = Console.ReadLine();

                try
                {
                    menu = (OpcaoMenu)Convert.ToInt32(comando);
                }
                catch (Exception)
                {
                    Console.Write("Opção inválida");
                    Console.Write("\nPressione 'enter' para continuar...\n");
                    Console.ReadLine();
                }

                switch (menu)
                {
                    case OpcaoMenu.Sair:
                        return;

                    case OpcaoMenu.CriarNovaOS:

                        Console.Clear();
                        Console.Write("Digite a data da abertura da OS ou 0 para retornar ao Menu Principal: ");
                        comando = Console.ReadLine();

                        try
                        {
                            if (comando != "0")
                            {
                                RealizaAberturaOS(listaOS, comando);
                            }
                        }
                        catch (Exception)
                        {
                            Console.Write("Opção inválida");
                            Console.Write("\nPressione 'enter' para continuar...\n");
                            Console.ReadLine();
                        }

                        break;

                    case OpcaoMenu.ListaOS:

                        Console.Clear();
                        Console.Write("Lista de OS cadastradas no sistema\n\n");

                        foreach (var item in listaOS.ListaOS)
                        {
                            Console.Write($"Código da OS: {item.Numero}, " +
                                          $"Data de abertura: {item.Abertura}, " +
                                          $"Responsável: {item.Responsavel}, " +
                                          $"Encerramento: {item.Encerramento} " +
                                          $"Area: {item.SomaAreas(item)} \n");
                        }

                        Console.Write("\n\n\nPressione 'enter' para continuar...\n");
                        Console.ReadLine();

                        break;

                    case OpcaoMenu.EncerrarOS:

                        Console.Clear();
                        Console.Write("Digite o número da OS a ser encerrada ou 0 para retornar ao Menu Principal: ");
                        comando = Console.ReadLine();

                        try
                        {
                            if (comando != "0")
                            {
                                RealizaEncerramentoOS(listaOS, comando);
                            }
                        }
                        catch (Exception)
                        {
                            Console.Write("Opção inválida");
                            Console.Write("\nPressione 'enter' para continuar...\n");
                            Console.ReadLine();
                        }

                        break;

                    case OpcaoMenu.IncluirNovaArea:

                        Console.Clear();
                        Console.Write("Digite o número da OS para adicionar a área ou 0 para retornar ao Menu Principal: ");
                        comando = Console.ReadLine();

                        try
                        {
                            if (comando != "0")
                            {
                                AdicionarNovaArea(listaOS, comando);
                            }
                        }
                        catch (Exception)
                        {
                            Console.Write("Opção inválida");
                            Console.Write("\nPressione 'enter' para continuar...\n");
                            Console.ReadLine();
                        }

                        break;

                    default:
                        break;

                }
            }
        }

        #endregion

        #region RealizaAberturaOS
        static void RealizaAberturaOS(ListaDeOS listaOS, string data)
        {
            OrdemDeServico os = new OrdemDeServico();

            if (listaOS.ListaOS.Count > 0)
                os.Numero = listaOS.ListaOS.Max(x => x.Numero) + 1;
            else
                os.Numero = 1;

            try
            {
                os.Abertura = Convert.ToDateTime(data);
                Console.Write("\nInforme o nome do responsável: ");
                os.Responsavel = Console.ReadLine();

                listaOS.ListaOS.Add(os);

                Console.Write("\nOS criada com sucesso. Pressione qualquer tecla para continuar...\n");
                Console.ReadLine();
            }
            catch (Exception)
            {
                Console.Write("\nOcorreu um erro na abertura da OS.");
                Console.Write("\nPressione qualquer tecla para continuar...\n");
                Console.ReadLine();
            }
        }

        #endregion

        #region RealizaEncerramentoOS
        static void RealizaEncerramentoOS(ListaDeOS listaOS, string numero)
        {
            OrdemDeServico os = new OrdemDeServico();

            os = listaOS.ListaOS.Find(x => x.Numero.ToString() == numero);

            if (os==null)
            {
                Console.Write("\nOS não encontrada no sistema. Pressione qualquer tecla para continuar...\n");
                Console.ReadLine();
                return;
            }

            try
            {
                Console.Write("\nInforme a data de encerramento: \n");
                var data = Console.ReadLine();

                os.Encerramento = Convert.ToDateTime(data);

                Console.Write("\nOS encerrada com sucesso. Pressione qualquer tecla para continuar...\n");
                Console.ReadLine();
            }
            catch (Exception)
            {
                Console.Write("\nOcorreu um erro na abertura da OS.");
                Console.Write("\nPressione qualquer tecla para continuar...\n");
                Console.ReadLine();
            }
        }

        #endregion

        #region AdicionarNovaArea
        static void AdicionarNovaArea(ListaDeOS listaOS, string numero)
        {
            OrdemDeServico os = new OrdemDeServico();

            os = listaOS.ListaOS.Find(x => x.Numero.ToString() == numero);

            if (os == null)
            {
                Console.Write("\nOS não encontrada no sistema. Pressione qualquer tecla para continuar...\n");
                Console.ReadLine();
                return;
            }

            try
            {
                Console.Write("\nInforme o tamanho da área: \n");
                var tamanho = Console.ReadLine();

                Area area = new Area();

                if (os.Areas != null)
                {
                    if (os.Areas.Count > 0)
                        area.Codigo = os.Areas.Max(x => x.Codigo) + 1;
                    else
                        area.Codigo = 1;
                } else
                {
                    area.Codigo = 1;
                }
                

                area.Tamanho = Convert.ToDecimal(tamanho);

                os.Areas.Add(area);

                Console.Write("\nÁrea adicionada com sucesso. Pressione qualquer tecla para continuar...\n");
                Console.ReadLine();
            }
            catch (Exception)
            {
                Console.Write("\nOcorreu um erro ao adicionar a área.");
                Console.Write("\nPressione qualquer tecla para continuar...\n");
                Console.ReadLine();
            }
        }

        #endregion

        #region InicializarMenu
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
        #endregion
    }

    #region Classes
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

        public decimal SomaAreas(OrdemDeServico os)
        {
            decimal total = 0;

            foreach (var item in os.Areas)
            {
                total += item.Tamanho;    
            }

            return total;
        }

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

    public class ListaDeOS
    {
        public ListaDeOS()
        {
            ListaOS = new List<OrdemDeServico>();
        }

        public List<OrdemDeServico> ListaOS { get; set; }
    }

    #endregion

    public enum OpcaoMenu
    {
        Menu_Principal = -1,
        Sair = 0,
        CriarNovaOS = 1,
        ListaOS = 2,
        EncerrarOS = 3,
        IncluirNovaArea = 4
    }

}
