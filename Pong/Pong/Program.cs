using Raylib_cs;
using System.Numerics;
using System.Security.Cryptography.X509Certificates;


namespace Pong
{
    internal class Program
    {

        //Pisteet
        private static string p1Pistelaskuri = $"{p1Pisteet}";
        private static string p2Pistelaskuri = $"{p2Pisteet}";
        private static float p1Pisteet = 0;
        private static float p2Pisteet = 0;
        static Vector2 p1Pistesijainti = new Vector2(400, 50);
        static Vector2 p2Pistesijainti = new Vector2(600, 50);

        //Näytön koko
        private static float screenWidth = 1000;
        private static float screenHeight = 600;

        //Pelaajan sijainnit
        static Vector2 p1Sijainti = new Vector2(1000/20, 600/2);
        static Vector2 p2Sijainti = new Vector2(1000 - (1000/20), 600/2);
                 
        //Mailan ominaisuudet
        private static float mailanNopeus = 5f;
        private static int mailanLeveys = 10;
        private static int mailanKorkeus = 50;

        //Pallon ominaisuudet
        private static int pallonNopeus = 20;
        static Vector2 pallonSuunta = new Vector2(1, -1); 
        static Vector2 pallonAlkupiste = new Vector2(screenWidth /2, screenHeight /2);

        static void Main(String[] args)
        {
            Raylib.SetTargetFPS(60);

            float frameTime = Raylib.GetFrameTime();

            Raylib.InitWindow((int)screenWidth, (int)screenHeight, "Pong");

            while (!Raylib.WindowShouldClose())
            {
                //Player 1 liikkumis näppäimet, Player 1 on Vihreä
                if (Raylib.IsKeyDown(KeyboardKey.W))
                {
                    p1Sijainti.Y -= mailanNopeus;
                }
                if (Raylib.IsKeyDown(KeyboardKey.S))
                {
                    p1Sijainti.Y += mailanNopeus;
                }

                //Player 2 liikkumis näppäimet, Player 2 on Punainen
                if (Raylib.IsKeyDown(KeyboardKey.Up))
                {
                    p2Sijainti.Y -= mailanNopeus;
                }
                if (Raylib.IsKeyDown(KeyboardKey.Down))
                {
                    p2Sijainti.Y += mailanNopeus;
                }

                //Player 1 ulsomeno Esto
                if (p1Sijainti.Y >= (600 - (mailanKorkeus / 2)))
                {
                    p1Sijainti.Y = (600 - (mailanKorkeus / 2));
                }
                if (p1Sijainti.Y <= (0 + (mailanKorkeus / 2)))
                {
                    p1Sijainti.Y = (0 + (mailanKorkeus / 2));
                }

                //Player 2 ulosmeno Esto
                if (p2Sijainti.Y >= (600 - (mailanKorkeus / 2)))
                {    
                    p2Sijainti.Y = (600 - (mailanKorkeus / 2));
                }    
                if (p2Sijainti.Y <= (0 + (mailanKorkeus / 2)))
                {    
                    p2Sijainti.Y = (0 + (mailanKorkeus / 2));
                }

                Raylib.BeginDrawing();

                //Pallo
                Raylib.DrawCircle((int)pallonAlkupiste.X, (int)pallonAlkupiste.Y, 10, Color.White);

                //Player 1 Pisteet
                Raylib.DrawTextEx(Raylib.GetFontDefault(), p1Pistelaskuri, p1Pistesijainti, 40, 2, Color.Green);

                //Player 2 Pisteet
                Raylib.DrawTextEx(Raylib.GetFontDefault(), p2Pistelaskuri, p2Pistesijainti, 40, 2, Color.Red);

                //Player 1
                Raylib.DrawRectangle((int)p1Sijainti.X - (mailanLeveys / 2), (int)p1Sijainti.Y - (mailanKorkeus / 2), mailanLeveys, mailanKorkeus, Color.Green);

                //Player 2
                Raylib.DrawRectangle((int)p2Sijainti.X - (mailanLeveys / 2), (int)p2Sijainti.Y - (mailanKorkeus / 2), mailanLeveys, mailanKorkeus, Color.Red);

                Raylib.ClearBackground(Color.Black);

                Raylib.EndDrawing();

            }
            Raylib.CloseWindow();
        }
    }
}
