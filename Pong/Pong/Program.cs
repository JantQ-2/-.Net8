using Raylib_cs;
using System.Numerics;

//Vihreä pelaaja pelaa W ja A näppämillä
//Punainen pelaaja pelaa Nuoli Ýlös ja Nuoli Alas Näppäimillä
namespace Pong
{
    internal class Program
    {
        //Pisteet
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
        private static int pallonNopeus = 200;
        static Vector2 pallonSuunta = new Vector2(1, -1); 
        static Vector2 pallonSijainti = new Vector2(screenWidth /2, screenHeight /2);

        static void Main(String[] args)
        {
            Raylib.SetTargetFPS(60);



            Raylib.InitWindow((int)screenWidth, (int)screenHeight, "Pong");

            

            while (!Raylib.WindowShouldClose())
            {

                float frameTime = Raylib.GetFrameTime();

                pallonSijainti += pallonSuunta * pallonNopeus * frameTime;

                //Player 1 liikkumis näppäimet, Player 1 on Vihreä
                if (Raylib.IsKeyDown(KeyboardKey.W)) p1Sijainti.Y -= mailanNopeus;
                if (Raylib.IsKeyDown(KeyboardKey.S)) p1Sijainti.Y += mailanNopeus;
                

                //Player 2 liikkumis näppäimet, Player 2 on Punainen
                if (Raylib.IsKeyDown(KeyboardKey.Up)) p2Sijainti.Y -= mailanNopeus; 
                if (Raylib.IsKeyDown(KeyboardKey.Down)) p2Sijainti.Y += mailanNopeus; 
             

                //Player 1 ulosmeno Esto
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

                //Player 1 mailan koko
                Rectangle p1Maila = new Rectangle(p1Sijainti.X - (mailanLeveys / 2), p1Sijainti.Y - (mailanKorkeus / 2), mailanLeveys, mailanKorkeus);
                Rectangle p2Maila = new Rectangle(p2Sijainti.X - (mailanLeveys / 2), p2Sijainti.Y - (mailanKorkeus / 2), mailanLeveys, mailanKorkeus);

                //Player 1 Piste Laskuri
                if (pallonSijainti.X + 7 >= 1000)
                {
                    pallonSijainti = new Vector2(screenWidth / 2, screenHeight / 2);
                    pallonSuunta.X = 1;
                }

                if (pallonSijainti.X + 10 >= 1000)
                {
                    p1Pisteet += 1;
                    Console.WriteLine("Player 1 on " + p1Pisteet + " Pistettä");
                }

                //Player 2 Piste Laskuri
                if (pallonSijainti.X - 7 <= 0)
                {
                    pallonSijainti = new Vector2(screenWidth / 2, screenHeight / 2);
                    pallonSuunta.X = -1;
                }

                if (pallonSijainti.X - 10 <= 0)
                {
                    p2Pisteet += 1;
                    Console.WriteLine("Player 2 on" + p2Pisteet + " Pistettä");
                }


                //Ei saa poistaa, Tarkistaa näytön ylä ja ala reunan osuman
                if (pallonSijainti.Y + 10 >= 600 || pallonSijainti.Y - 10 <= 0)
                {
                    pallonSuunta.Y *= -1;
                }

                if (Raylib.CheckCollisionCircleRec(pallonSijainti, 10, p1Maila) ||
                    Raylib.CheckCollisionCircleRec(pallonSijainti, 10, p2Maila))
                {
                    pallonSuunta.X *= -1; // Käännetään X-suunta törmäyksessä
                }


                Raylib.BeginDrawing();

                //Pallo
                Raylib.DrawCircle((int)pallonSijainti.X, (int)pallonSijainti.Y, 10, Color.White);

                //Player 1 Pisteet
                Raylib.DrawTextEx(Raylib.GetFontDefault(), $"{p1Pisteet}", p1Pistesijainti, 40, 2, Color.Green);

                //Player 2 Pisteet
                Raylib.DrawTextEx(Raylib.GetFontDefault(), $"{p2Pisteet}", p2Pistesijainti, 40, 2, Color.Red);

                //Player 1
                Raylib.DrawRectangleRec(p1Maila, Color.Green);

                //Player 2
                Raylib.DrawRectangleRec(p2Maila, Color.Red);
                

                Raylib.ClearBackground(Color.Black);

                Raylib.EndDrawing();

            }
            Raylib.CloseWindow();
        }
    }
}
