using Newtonsoft.Json;
using Raylib_cs;
using System;
using System.Numerics;
using TimeOutStuff;

namespace Asteroids
{
    internal class Program
    {
        Player player;
        int currentWave = 1;
        public static bool isGameOver = false;
        public static Vector2 screenSize = new Vector2(1800, 950);
        static void Main(string[] args)
        {

            //LIbrary, Halusin lisätä JavaScriptistä Tutun SetTimeout en tiiä käytänkö
            Timings.SetTimeout(3, () =>
            {
                Console.WriteLine(screenSize);
            });
            //Library, Halusin myös lisätä itselleni Tick() joka toteuttaa itsensä joka 0.1s (100ms)
            Timings.Tick(() =>
            {
                Console.WriteLine(DateTime.Now);
            });

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
            Asteroid.Init();

            if (Raylib.IsKeyDown(KeyboardKey.Escape))
            {
                Raylib.CloseWindow();
            }
        }

        void GameLoop()
        {

            ShipData currentShip = GameData.LoadShipData();
            GameData.SaveShipData();


            while (!Raylib.WindowShouldClose())
            {
                Raylib.BeginDrawing();
                Raylib.ClearBackground(Color.Black);

                if (!isGameOver)
                {
                    player.DrawPlayer();
                    player.SideSwapper();

                    Asteroid.UpdateAsteroids();
                    Asteroid.DrawAsteroids();

                    Asteroid.HandleBulletAsteroidCollisions();

                    if (Asteroid.IsPlayerColliding(player))
                        isGameOver = true;

                    if (Asteroid.CountActiveAsteroids() == 0)
                        Timings.SetTimeout(3, () =>
                        {
                            StartNextWave();
                        });
                }
                
                Raylib.EndDrawing();
            }


            GameData.SaveShipData();
        }
        public void StartNextWave()
        {
            currentWave++;

            int toSpawn = Math.Min(2 + currentWave, Asteroid.asteroids.Length);

            int spawned = 0;
            while (spawned < toSpawn)
            {
                int slot = Asteroid.FindFreeAsteroidSlot();
                if (slot == -1) break;

                float x = (float)Asteroid.rng.NextDouble() * Program.screenSize.X;
                float y = (float)Asteroid.rng.NextDouble() * Program.screenSize.Y;
                Vector2 pos = new Vector2(x, y);

                Asteroid.SpawnAsteroid(slot, 2, pos);
                spawned++;
            }
        }

    }
}
