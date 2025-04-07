using Raylib_cs;
using System.Numerics;

namespace Asteroids
{
    internal class Program
    {
        Texture2D playerShipTexture;

        static void Main(string[] args)
        {
            Program game = new Program();
            game.Init();
        }

        void Init()
        {
            Raylib.InitWindow(800, 800, "Asteroids");
            Raylib.SetTargetFPS(60);

            playerShipTexture = Raylib.LoadTexture("images/more_images/PNG/playerShip1_red.png");


            GameLoop();

            Raylib.CloseWindow();
        }

        void GameLoop()
        {
            while (!Raylib.WindowShouldClose())
            {
                Raylib.BeginDrawing();
                Raylib.ClearBackground(Color.Black);

                Raylib.DrawTexture(playerShipTexture, 10, 10, Color.Red);


                Raylib.EndDrawing();
            }
        }
    }
}
