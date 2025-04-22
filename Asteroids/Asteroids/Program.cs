using Raylib_cs;
using System.Numerics;

namespace Asteroids
{

    internal class Program
    {
        Vector2 playerPosition = new Vector2(400, 400);
        int playerRotation = 0;
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
                if (Raylib.IsKeyDown(KeyboardKey.A))
                {
                    playerRotation += 10;
                }

                Raylib.BeginDrawing();
                Raylib.ClearBackground(Color.Black);

                Raylib.DrawTexturePro(playerShipTexture, playerPosition, , playerRotation, Color.Red);


                Raylib.EndDrawing();
            }
        }
    }
}
