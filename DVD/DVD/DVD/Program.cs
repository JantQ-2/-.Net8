using Raylib_cs;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using static System.Net.Mime.MediaTypeNames;

namespace DVD
{
    internal class Program
    {
        
        
        static Color GetRandomColor()
        {
            Random random = new Random();
            return new Color(
                random.Next(256), 
                random.Next(256), 
                random.Next(256), 
                255
            );
        }


        static void Main(String[] args)
        {

            Vector2 Position = new Vector2(400, 400);
            Vector2 suunta = new Vector2(1, -1);


            bool looppi = true;
            float nopeus = 100;
            string teksti = "DVD";

            
            Raylib.InitWindow(800, 800, "DVD");

            // Hae ikkunan leveys ja korkeus
            int width = Raylib.GetScreenWidth();
            int height = Raylib.GetScreenHeight();

            Color textColor = Color.Yellow;
            

            int fontSize = 40;
            Vector2 textSize = Raylib.MeasureTextEx(Raylib.GetFontDefault(), teksti, fontSize, 2);

                while (!Raylib.WindowShouldClose())
                {



                    float frameTime = Raylib.GetFrameTime();


                    Position += suunta * nopeus * frameTime;

                    if (Position.X + textSize.X >= width || Position.X <= 0)
                    {
                        suunta = new Vector2(suunta.X * -1, suunta.Y);
                        Position.X = Math.Clamp(Position.X, 0, width - textSize.X);

                        textColor = GetRandomColor();

                        nopeus = Raylib.GetRandomValue(100, 500);

                        Console.WriteLine($"Nopeus on {nopeus}");
                    }

                    // Törmäystarkistus ylhäällä ja alhaalla
                    if (Position.Y + textSize.Y >= height || Position.Y <= 0)
                    {
                        suunta = new Vector2(suunta.X, suunta.Y * -1);
                        Position.Y = Math.Clamp(Position.Y, 0, height - textSize.Y);

                        textColor = GetRandomColor();
                        nopeus = Raylib.GetRandomValue(100, 500);

                        Console.WriteLine($"Nopeus on {nopeus}");
                    }

                    Raylib.BeginDrawing();

                    Raylib.ClearBackground(Color.Black);

                    Raylib.DrawTextEx(Raylib.GetFontDefault(), teksti, Position, fontSize, 2, textColor);

                    Raylib.EndDrawing();

                }
            Raylib.CloseWindow();

        }
    }
}
