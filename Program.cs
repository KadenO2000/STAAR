using System;

namespace STAAR
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            using (var game = new STAAR())
                game.Run();
        }
    }
}
