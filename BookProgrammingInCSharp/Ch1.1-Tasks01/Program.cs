namespace Ch1._1_Tasks01
{
    using System.Threading.Tasks;
    using static System.Console;

    class Program
    {
        static void Main(string[] args)
        {
            goto Inicio;

            // Criando uma nova Task e iniciando ela imediatamente.
            // Pg 11.
            var task = Task.Run(() =>
            {
                for (int x = 0; x < 100; x++)
                {
                    WriteLine(x);
                }
            });

            // Equivalente a Join na Thread que esperará com que a Task finalize para prosseguir
            // na execução das linhas abaixo.
            task.Wait();

            Inicio:

            // Task que retorna valor.
            // Pg 12.
            Task<string> taskRetornaValor = Task.Run(() =>
            {
                return "Olá";
            });

            // Printa o resultado da Task executada.
            WriteLine(taskRetornaValor.Result);

            ReadLine();
        }
    }
}