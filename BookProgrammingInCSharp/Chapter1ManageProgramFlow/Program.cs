namespace Ch1_Threads01
{
    using System.Threading;
    using static System.Console;
    using static System.Threading.Thread;

    class Program
    {
        public static void ThreadMethod()
        {
            for (int i = 0; i < 10; i++)
            {
                WriteLine($"ThreadProc: {i}");
                Sleep(1000);
            }
        }

        public static void ThreadMethodComParametro(object obj)
        {
            for (int i = 0; i < (int) obj; i++)
            {
                WriteLine($"ThreadProc: {i}");
                Sleep(0);
            }
        }

        static void Main(string[] args)
        {
            // Instancia uma nova Thread para executar o método definido.
            var thread = new Thread(new ThreadStart(ThreadMethod));

            thread.Start();

            for (int i = 0; i < 4; i++)
            {
                WriteLine("Método principal");
                Sleep(0);
            }

            // Aguarda a Worker Thread invocada terminar a execução para prosseguir.
            thread.Join();

            ReadKey();

            // Com Background = false, essa nova Thread será executada sobre a Main Thread que já estará finalizada.
            // ParameterizedThreadStart = Permite que um parâmetro seja passado para o método da Thread invocada.
            var outraThread = new Thread(new ParameterizedThreadStart(ThreadMethodComParametro))
            {
                IsBackground = false
            };

            // Envio de parâmetro (contador para o loop na Thread).
            outraThread.Start(3);

            ReadKey();
        }
    }
}
