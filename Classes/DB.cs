using System.IO;

namespace DIO.bank{
    public class DB{
        private static string caminho = @"D:\Documentos\LocalizaLabs\DIO.bank\Db\database.txt";

        private static bool verificaSeExiste(){
            return File.Exists(caminho);
        }

        public static Conta loadAccount(string nome, string senha){
            if(!verificaSeExiste()){
                File.CreateText(caminho).Close();
            }
            StreamReader db = File.OpenText(caminho);
            while(db.EndOfStream != true){
                string linha = db.ReadLine();
                string[] valores;
                valores = linha.Split(";");
                if(valores[0] == nome && valores[1] == senha){
                    Conta conta = new Conta(valores);
                    db.Close();
                    return conta;
                }
            }
            db.Close();
            return null;
        }

        public static void createAccount(string nome, string senha, string saldo, string credito, string tipoConta){
            StreamWriter db = File.AppendText(caminho);
            db.WriteLine(nome + ";" + senha + ";" + saldo + ";" + credito + ";" + tipoConta);
            db.Close();
        }

        public static void updateAccount(string nome, double saldo){
            string[] file = File.ReadAllLines(caminho);
            int i = 0;
            foreach(string line in file)
            {
                string[] valores;
                valores = line.Split(";");
                if(nome == valores[0]){
                    valores[2] = saldo.ToString();
                    file[i] = vectorToString(valores);
                }
                i++;
            }
            File.WriteAllLines(caminho, file);
        }

        private static string vectorToString(string[] valores){
            string retorno = "";
            retorno += valores[0] + ";";
            retorno += valores[1] + ";";
            retorno += valores[2] + ";";
            retorno += valores[3] + ";";
            retorno += valores[4];
            return retorno;
        }

        public static void transferMoney(string nome, double valor){
            string[] file = File.ReadAllLines(caminho);
            int i = 0;
            foreach(string line in file)
            {
                string[] valores;
                valores = line.Split(";");
                if(nome == valores[0]){
                    double saldoAtual = double.Parse(valores[2]);
                    saldoAtual += valor;
                    valores[2] = saldoAtual.ToString();
                    file[i] = vectorToString(valores);
                }
                i++;
            }
            File.WriteAllLines(caminho, file);
        }
    }
}
