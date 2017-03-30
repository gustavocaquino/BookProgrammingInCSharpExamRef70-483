namespace Ch1_Threads02
{
    using System;
    using System.Threading;
    using static System.Console;
    using static System.Threading.Thread;

    class Program
    {
        // Propriedade única para cada Thread com ThreadStaticAttribute.
        // Pg 7.
        [ThreadStatic]
        static int _campo;

        // Pg 8.
        static ThreadLocal<int> _campoThreadLocal =
            new ThreadLocal<int>(() =>
            {
                return CurrentThread.ManagedThreadId;
            });

        static void Main(string[] args)
        {
            goto Inicio;

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

            // ThreadLocal<T> = Informação local inicializada em cada Thread.
            // Pg 8. 
            new Thread(() =>
            {
                for (int x = 0; x < _campoThreadLocal.Value; x++)
                {
                    WriteLine($"Thread A: {x}");
                }
            }).Start();

            new Thread(() =>
            {
                for (int x = 0; x < _campoThreadLocal.Value; x++)
                {
                    WriteLine($"Thread B: {x}");
                }
            }).Start();

            // ThreadPool - Pool de threads gerenciadas para execuções de tasks, assincronismo e etc.
            // A ThreadPool reutiliza as Threads facilitando o gerenciamento da mesma pela aplicação.
            // Pg. 9

            Inicio:

            ThreadPool.QueueUserWorkItem((callBack) =>
            {
                WriteLine("Rodando a partir da ThreadPool");
            });

            ReadKey();
        }
    }
}
