using Raylib_cs;
using System.Numerics;

namespace Artillery
{
    internal class Program
    {

        public static int ruudunLeveys = 1000;
        public static int ruudunKorkeus = 750;
        public static int muunnin = 30;

        public static Vector2 maaX = new Vector2(0, muunnin);
        public static Vector2 maaY = new Vector2(0, 0);

        static void Main(string[] args)
        {
            Raylib.InitAudioDevice();
            Sound sound = Raylib.LoadSound("Bullet.mp3");

            Raylib.InitWindow(ruudunLeveys, ruudunKorkeus, "Artillery");
            Raylib.SetTargetFPS(120);
            int random1 = Raylib.GetRandomValue(10, 100);


            while (!Raylib.WindowShouldClose())
            {

                if (Raylib.IsKeyPressed(KeyboardKey.Space))
                {
                    Raylib.PlaySound(sound);
                }
                Raylib.BeginDrawing();
                Raylib.ClearBackground(Color.Black);
                Raylib.DrawRectangle((int)maaX.X, ruudunKorkeus-random1, muunnin, random1, Color.Green);
                Raylib.EndDrawing();
            }
            Raylib.CloseWindow();
        }
    }
}
