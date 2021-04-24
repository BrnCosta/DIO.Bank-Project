using System;
using System.IO;

namespace DIO.bank{
    public class Conta{
        private TipoConta TipoConta { get; set; }

        private double Saldo { get; set; }

        private double Credito { get; set; }

        private string Nome { get; set; }

        public Conta(TipoConta tipoConta, double salarioMensal, string nome, string senha){
            this.TipoConta = tipoConta;
            this.Saldo = 0;
            this.Nome = nome;
            this.Credito = calcularCredito(salarioMensal);
            
            DB.createAccount(this.Nome, senha, this.Saldo.ToString(), this.Credito.ToString(), this.TipoConta.ToString());
        }

        public Conta(string[] valores){
            TipoConta conta = valores[4] == "PessoaFisica" ? (TipoConta)1 : (TipoConta)2;
            this.TipoConta = conta;
            this.Saldo = double.Parse(valores[2]);
            this.Nome = valores[0];
            this.Credito = double.Parse(valores[3]);
        }

        public bool Sacar(double valorSaque){
            //Validação de saldo
            if(this.Saldo - valorSaque < (this.Credito *-1)){
                Console.WriteLine("Saldo Insuficiente!");
                return false;
            }

            this.Saldo -= valorSaque;

            Console.WriteLine("Saldo atual da conta de {0} é de {1}", this.Nome, this.Saldo);

            return true;
        }

        public void Depositar(double valorDeposito){
            this.Saldo += valorDeposito;

            Console.WriteLine("Saldo atual da conta de {0} é de {1}", this.Nome, this.Saldo);
        }

        public void Transferir(double valorTransferencia, string nome){
            if(this.Sacar(valorTransferencia)){
                DB.transferMoney(nome, valorTransferencia);
            }
        }

        public override string ToString()
        {
            string retorno = "";
            retorno += "TipoConta " + this.TipoConta + " | ";
            retorno += "Nome " + this.Nome + " | ";
            retorno += "Saldo " + this.Saldo + " | ";
            retorno += "Credito " + this.Credito + " | ";
            return retorno;
        }

        public void atualizarConta(){
            DB.updateAccount(this.Nome, this.Saldo);
        }

        private double calcularCredito(double salarioMensal){
            return salarioMensal * 0.2;
        }
    }
}