namespace Ch1_Threads02
{
    using System;
    using System.Threading;
    using static System.Console;
    using static System.Threading.Thread;

    class Program
    {
        // Propriedade única para cada Thread com ThreadStaticAttribute.
        [ThreadStatic]
        static int _campo;

        static void Main(string[] args)
        {
            // Exemplo de cancelamento de Thread com variável compartilhada.
            var parado = false;

            var thread = new Thread(new ThreadStart(() =>
            {
                while (!parado)
                {
                    WriteLine("Rodando...");
                    Sleep(1000);
                }

                WriteLine("Fim da Thread.");
            }));

            thread.Start();

            WriteLine("Pressione uma tecla para sair...");
            ReadKey();

            // Sinaliza a saída da execução.
            parado = true;

            thread.Join();

            new Thread(() =>
            {
                for (int i = 0; i < 10; i++)
                {
                    _campo++;
                    WriteLine($"Thread A: {_campo}");
                }
            }).Start();

            new Thread(() =>
            {
                for (int i = 0; i < 10; i++)
                {
                    _campo++;
                    WriteLine($"Thread B: {_campo}");
                }
            }).Start();

            ReadKey();
        }
    }
}
