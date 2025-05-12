using Newtonsoft.Json;
using Raylib_cs;
using System;
using System.Numerics;
using Newtonsoft.Json;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Asteroids
{
    internal class Program
    {
        Player player;
        public static Vector2 screenSize = new Vector2(1800, 950);
        static void Main(string[] args)
        {

            Program game = new Program();
            game.Init();
            
            game.GameLoop();
            Raylib.CloseWindow();
        }

        void Init()
        {
            Player.Init();
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
            var dataHandler = new GameData();
            ShipData currentShip = dataHandler.LoadShipData();
            dataHandler.SaveShipData();

            while (!Raylib.WindowShouldClose())
            {
                Raylib.BeginDrawing();
                Raylib.ClearBackground(Color.Black);

                player.DrawPlayer();
                player.SideSwapper();

                Raylib.EndDrawing();
            }

            dataHandler.SaveShipData();
        }
    }
}
