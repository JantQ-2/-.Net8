using Raylib_cs;
using System.Numerics;
using System.Security.Cryptography.X509Certificates;

namespace DVD
{
    internal class Program
    {
       
        static void Main(String[] args)
        {
            Vector2 A = new Vector2(800 / 2, 0);
            Vector2 B = new Vector2(0, 800 / 2);
            Vector2 C = new Vector2(800, 600);

            Raylib.InitWindow(800, 800, "DVD");

            while (Raylib.WindowShouldClose() == false)
            {
                Raylib.BeginDrawing();
                Raylib.ClearBackground(Color.Black);

                Raylib.DrawLineV(A, B, Color.Green);
                Raylib.DrawLineV(B, C, Color.Yellow);
                Raylib.DrawLineV(C, A, Color.SkyBlue);
                Raylib.EndDrawing();
            }

            Raylib.CloseWindow();
        }
    }
}
