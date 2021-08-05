using System;

namespace MileniaGame
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