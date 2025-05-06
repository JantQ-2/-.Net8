using Raylib_cs;
using System.Numerics;

namespace Asteroids
{
    internal class Program
    {
        Player player;
        public static Vector2 screenSize = new Vector2(1500, 950);

        static void Main(string[] args)
        {

            Program game = new Program();
            game.Init();
            game.GameLoop();
            Raylib.CloseWindow();
        }

        void Init()
        {
            Raylib.InitWindow((int)screenSize.X, (int)screenSize.Y, "Asteroids");
            Raylib.SetTargetFPS(60);

            player = new Player();

            if (Raylib.IsKeyDown(KeyboardKey.Escape))
            {
                Raylib.CloseWindow();
            }
        }

        void GameLoop()
        {
            while (!Raylib.WindowShouldClose())
            {
                Raylib.BeginDrawing();
                Raylib.ClearBackground(Color.Black);

                player.DrawPlayer();

                Raylib.EndDrawing();
            }
        }
    }
}
