using Raylib_cs;
using System.Numerics;
using System.Security.Cryptography.X509Certificates;
using static System.Net.Mime.MediaTypeNames;

namespace DVD
{
    internal class Program
    {
       
        static void Main(String[] args)
        {
            Vector2 Position = new Vector2(100, 100);
            Vector2 suunta = new Vector2(1, -1);
            
            float nopeus = 50f;
            string teksti = "DVD";
            
            Raylib.InitWindow(800, 800, "DVD");

            // Hae ikkunan leveys ja korkeus
            int width = Raylib.GetScreenWidth();
            int height = Raylib.GetScreenHeight();

            
            int fontSize = 40;
            Vector2 textSize = Raylib.MeasureTextEx(Raylib.GetFontDefault(), teksti, fontSize, 1);

            while (!Raylib.WindowShouldClose())
            {

                float frameTime = Raylib.GetFrameTime();

                Position += suunta * nopeus * frameTime;

                if (Position.X + textSize.X >= width || Position.X <= 0)
                {
                    suunta = new Vector2(suunta.X * -1, suunta.Y);
                    Position.X = Math.Clamp(Position.X, 0, width - textSize.X); 
                }

                // Törmäystarkistus ylhäällä ja alhaalla
                if (Position.Y + textSize.Y >= height || Position.Y <= 0)
                {
                    suunta = new Vector2(suunta.X, suunta.Y * -1); 
                    Position.Y = Math.Clamp(Position.Y, 0, height - textSize.Y); 
                }

                Raylib.BeginDrawing();

                Raylib.ClearBackground(Color.Black);

                Raylib.DrawText(teksti, (int)Position.X, (int)Position.Y, fontSize, Color.Yellow);

                Raylib.EndDrawing();
            }

            Raylib.CloseWindow();
        }
    }
}
