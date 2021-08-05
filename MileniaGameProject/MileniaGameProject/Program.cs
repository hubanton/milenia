using System;

namespace MileniaGameProject
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            using (var game = new Milenia())
                game.Run();
        }
    }
}