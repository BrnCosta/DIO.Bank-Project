using System;
using System.Collections.Generic;
using System.IO;

namespace DIO.bank
{
    class Program
    {
        static List<Conta> listaContas = new List<Conta>();
        static void Main(string[] args)
        {
            string opcaoUsuario = ObterOpcaoUsuarioDeslogado();

            while(opcaoUsuario.ToUpper() != "X"){
                switch(opcaoUsuario){
                    case "1":
                        Entrar();
                        break;
                    case "2":
                        CriarConta();
                        break;
                    default:
                        Console.WriteLine("Por Favor, selecione uma opção válida.");
                        break;
                }

                opcaoUsuario = ObterOpcaoUsuarioDeslogado();
            }

            Console.WriteLine("Obrigado por utilizar nossos serviços.");
            Console.ReadLine();

        }

        private static void Entrar()
        {
            Console.WriteLine("Digite seu nome:");
            string nome = Console.ReadLine();
            
            Console.WriteLine("Digite sua senha:");
            string senha = Console.ReadLine();

            Conta contaLogada = DB.loadAccount(nome, senha);
            if(contaLogada == null){
                Console.WriteLine("Conta não encontrada.");
                Console.WriteLine();
                return;
            }

            Console.Clear();

            string opcaoUsuario = ObterOpcaoUsuarioLogado();

            while(opcaoUsuario.ToUpper() != "X"){
                switch(opcaoUsuario){
                    case "1":
                        Detalhes(contaLogada);
                        break;
                    case "4":
                        Transferir(contaLogada);
                        break;
                    case "2":
                        Sacar(contaLogada);
                        break;
                    case "3":
                        Depositar(contaLogada);
                        break;
                    case "C":
                        Console.Clear();
                        break;
                    default:
                        Console.WriteLine("Por Favor, selecione uma opção válida.");
                        break;
                }

                opcaoUsuario = ObterOpcaoUsuarioLogado();
            }

            AtualizarConta(contaLogada);
        }

        private static void AtualizarConta(Conta contaLogada)
        {
            contaLogada.atualizarConta();
        }

        private static void Detalhes(Conta contaLogada)
        {
            Console.WriteLine(contaLogada.ToString());
            Console.WriteLine();
            Console.WriteLine("Pressione Enter para finalizar...");
            Console.ReadLine();
            Console.Clear();
        }

        private static void Transferir(Conta contaLogada)
        {
            Console.WriteLine("Digite o nome da conta de destino: ");
            string contaDestino = Console.ReadLine();

            Console.WriteLine("Digite o valor a ser transferido: ");
            double valorTransferencia = double.Parse(Console.ReadLine());

            contaLogada.Transferir(valorTransferencia, contaDestino);
        }

        private static void Depositar(Conta contaLogada)
        {

            Console.WriteLine("Digite o valor a ser depositado: ");
            double valorDeposito = double.Parse(Console.ReadLine());

            contaLogada.Depositar(valorDeposito);
            Console.WriteLine();
            Console.WriteLine("Pressione Enter para finalizar...");
            Console.ReadLine();
            Console.Clear();
        }

        private static void Sacar(Conta contaLogada)
        {
            Console.WriteLine("Digite o valor a ser sacado: ");
            double valorSaque = double.Parse(Console.ReadLine());

            contaLogada.Sacar(valorSaque);
            Console.WriteLine();
            Console.WriteLine("Pressione Enter para finalizar...");
            Console.ReadLine();
            Console.Clear();
        }

        private static void CriarConta()
        {
            Console.WriteLine("Inserir nova conta");

            Console.WriteLine("Digite 1 para Conta Física ou 2 para Conta Jurídica: ");
            int entradaTipoConta = int.Parse(Console.ReadLine());

            Console.WriteLine("Digite o Nome do Cliente: ");
            string entradaNome = Console.ReadLine();

            Console.WriteLine("Digite o seu salário mensal: ");
            double entradaSalario = double.Parse(Console.ReadLine());

            Console.WriteLine("Digite sua senha: ");
            string entradaSenha = Console.ReadLine();

            Conta novaConta = new Conta(
                tipoConta: (TipoConta)entradaTipoConta,
                salarioMensal: entradaSalario,
                nome: entradaNome,
                senha: entradaSenha
            );

            Console.WriteLine("Conta criada com sucesso!");
            Console.WriteLine();
            Console.WriteLine("Pressione Enter para finalizar...");
            Console.ReadLine();
            Console.Clear();
        }

        private static string ObterOpcaoUsuarioDeslogado(){
            Console.WriteLine();
            Console.WriteLine("DIO Bank a seu dispor!!");
            Console.WriteLine("Informe a opção desejada:");

            Console.WriteLine("1- Entrar");
            Console.WriteLine("2- Criar Conta");
            Console.WriteLine("X- Sair");
            Console.WriteLine();

            string opcaoUsuario = Console.ReadLine().ToUpper();
            Console.WriteLine();
            return opcaoUsuario;            
        }

        private static string ObterOpcaoUsuarioLogado(){
            Console.WriteLine();
            Console.WriteLine("DIO Bank a seu dispor!!");
            Console.WriteLine("Informe a opção desejada:");

            Console.WriteLine("1- Listar detalhes da conta");
            Console.WriteLine("2- Sacar");
            Console.WriteLine("3- Depositar");
            Console.WriteLine("4- Transferir");
            Console.WriteLine("C- Limpar Tela");
            Console.WriteLine("X- Sair");
            Console.WriteLine();

            string opcaoUsuario = Console.ReadLine().ToUpper();
            Console.WriteLine();
            return opcaoUsuario;            
        }
    }
}
