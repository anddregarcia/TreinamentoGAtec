using System;
using System.Collections.Generic;

/*I.Construa um programa Console Application em C# que leia uma operação dentre as possíveis operações:

+ soma
- subtração
* multiplicação
/ divisão
^ potência
% resto de divisão
.Sair do programa
Caso o usuário informe a operação.encerre o programa.Caso ele informe uma das demais operações, leia valores no formato decimal e realize 
tal operação informada.

Considerações:

Implemente regras para aceitar apenas as operações listadas acima e caso não seja uma delas apresente um erro.
Implemente uma tratativa para aceitar apenas números nos valores (decimal) e ao digitar letras ou texto apresente um erro.
Evolua o programa para um loop infinito encerrando o loop apenas quando o usuário digitar.para a operação.Limpe a tela a cada nova operação.
Dicas:
Utilize blocos try/catch para tratativas e loops para insistir até que o usuário informe um valor válido.*/

namespace AtividadeWiki1
{
    class Program
    {
        static void Main(string[] args)
        {
            var expression = "";

            while (true)
            {
                Console.WriteLine("Digite uma expressão matematica no formato {número [+] número} (Digite enter quando terminar):\n");

                expression = Console.ReadLine();

                if (expression == ".")
                    return;

                try
                {
                    if (Validation(expression))
                    {
                        var result = new Operators(expression).ReadExpression();
                        Console.WriteLine(result.ToString());
                    }
                    else
                        Console.WriteLine("Expressão inválida");
                    
                } catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

                Console.ReadLine();
                Console.Clear();
            }
        }

        static public bool Validation(string expression)
        {
            var args = expression.Split(" ");
            List<string> operators = new List<string> { "+", "-", "*", "/", "^", "%" };

            try
            {
                decimal op1 = Convert.ToDecimal(args[0].Trim());
                decimal op2 = Convert.ToDecimal(args[2].Trim());
            }
            catch (Exception)
            {
                throw new System.ArgumentException("O primeiro ou o segundo parâmetro da expressão não é decimal.");
            }

            if (!operators.Exists (x => x==args[1]))
                throw new System.ArgumentException("A operação matemática utilizada não é válida. Utilize '+', '-', '*', '/', '^', '%'");
            
            return true;
        }
    }

    class Operators
    {
        public Operators()
        {

        }

        public Operators(string expression)
        {
            this.Expression = expression;
        }

        private Operators(decimal value)
        {
            this.Value = value;
        }

        public decimal Value { get; set; }
        public decimal? Result { get; set; }
        public string Expression { get; set; }

        public Operators ReadExpression()
        {
            string[] args;

            try
            {
                args = this.Expression.Split(" ");
                Operators op1 = new Operators(Convert.ToDecimal(args[0]));
                Operators op2 = new Operators(Convert.ToDecimal(args[2]));

                if (args[1] == "+")
                    return op1 + op2;

                if (args[1] == "-")
                    return op1 - op2;

                if (args[1] == "*")
                    return op1 * op2;

                if (args[1] == "/")
                    return op1 / op2;

                if (args[1] == "^")
                    return op1 ^ op2;

                if (args[1] == "%")
                    return op1 % op2;
            }
            catch(Exception)
            {
                throw new System.ArgumentException("Operação inválida");
            }
            
            return null;
        }

        public static Operators operator +(Operators op1, Operators op2)
        {
            Operators op = new Operators();
            op.Result = op1.Value + op2.Value;
            return op;
        }

        public static Operators operator -(Operators op1, Operators op2)
        {
            Operators op = new Operators();
            op.Result = op1.Value - op2.Value;
            return op;
        }

        public static Operators operator *(Operators op1, Operators op2)
        {
            Operators op = new Operators();
            op.Result = op1.Value * op2.Value;
            return op;
        }

        public static Operators operator /(Operators op1, Operators op2)
        {
            Operators op = new Operators();
            op.Result = op1.Value / op2.Value;
            return op;
        }

        public static Operators operator ^(Operators op1, Operators op2)
        {
            Operators op = new Operators();
            op.Result = (decimal)Math.Pow((double)op1.Value, (double)op2.Value);
            return op;
        }

        public static Operators operator %(Operators op1, Operators op2)
        {
            Operators op = new Operators();
            op.Result = op1.Value % op2.Value;
            return op;
        }

        public override string ToString()
        {
            return $"= { this.Result }";
        }

    }
}
