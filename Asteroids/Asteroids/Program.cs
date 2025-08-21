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
        int currentWave = 1;
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
            Asteroid.Init();

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

                UpdateAsteroids();
                DrawAsteroids();

                HandleBulletAsteroidCollisions();

                if (CountActiveAsteroids() == 0)
                {
                    StartNextWave();
                }


                Raylib.EndDrawing();
            }

            dataHandler.SaveShipData();
        }

        public static void SpawnAsteroid(int index)
        {
            float x = (float)Asteroid.rng.NextDouble() * Program.screenSize.X;
            float y = (float)Asteroid.rng.NextDouble() * Program.screenSize.Y;
            SpawnAsteroid(index, 2, new Vector2(x, y));
        }

        public static void SpawnAsteroid(int index, int size, Vector2 pos)
        {
            float scale = size == 2 ? 1.6f : size == 1 ? 1.0f : 0.6f;

            float speedBase = size == 2 ? 1.6f : size == 1 ? 1f : 0.3f;

            float angle = (float)(Asteroid.rng.NextDouble() * MathF.PI * 2f);
            Vector2 vel = new Vector2(MathF.Cos(angle), MathF.Sin(angle)) * (speedBase + (float)Asteroid.rng.NextDouble() * 1.5f);

            float rotSpd = (float)(Asteroid.rng.NextDouble() * 4.0 - 2.0);

            Asteroid.asteroids[index] = new Asteroid
            {
                Position = pos,
                Velocity = vel,
                RotationDeg = (float)(Asteroid.rng.NextDouble() * 360f),
                RotationSpeedDeg = rotSpd,
                Scale = scale,
                Size = size,
                Active = true
            };
        }

        void UpdateAsteroids()
        {
            for (int i = 0; i < Asteroid.asteroids.Length; i++)
            {
                if (!Asteroid.asteroids[i].Active) continue;
                Asteroid.asteroids[i].Update(screenSize);

            }
        }

        void DrawAsteroids()
        {
            for (int i = 0; i < Asteroid.asteroids.Length; i++)
                if (Asteroid.asteroids[i].Active)
                    Asteroid.asteroids[i].Draw(Asteroid.asteroidTexture);
        }
        const float BulletRadius = 3f;

        float GetAsteroidRadius(in Asteroid a)
        {
            float baseRadius = Asteroid.asteroidTexture.Width * a.Scale * 0.5f;
            return baseRadius * 0.45f;
        }

        void SplitAsteroidAt(int index, Vector2 hitDir)
        {
            var a = Asteroid.asteroids[index];
            Asteroid.asteroids[index].Active = false;

            if (a.Size <= 0) return;

            int children = a.Size == 2 ? 2 : 1;
            int childSize = a.Size - 1;

            for (int i = 0; i < children; i++)
            {
                int slot = FindFreeAsteroidSlot();
                if (slot == -1) break;

                float baseAngle = MathF.Atan2(hitDir.Y, hitDir.X);
                float spread = ((float)Asteroid.rng.NextDouble() - 0.5f) * 1.4f;
                float ang = baseAngle + spread;

                float speed = a.Velocity.Length() + 2.5f + (float)Asteroid.rng.NextDouble() * 1.5f;
                Vector2 vel = new Vector2(MathF.Cos(ang), MathF.Sin(ang)) * speed;

                SpawnAsteroid(slot, childSize, a.Position);
                Asteroid.asteroids[slot].Velocity = vel;
            }
        }

        void HandleBulletAsteroidCollisions()
        {
            var bs = player.GetBullets();

            for (int b = 0; b < bs.Length; b++)
            {
                if (!bs[b].Active) continue;

                for (int i = 0; i < Asteroid.asteroids.Length; i++)
                {
                    ref var a = ref Asteroid.asteroids[i];
                    if (!a.Active) continue;

                    float r = GetAsteroidRadius(a) + BulletRadius;
                    Vector2 d = bs[b].Position - a.Position;
                    if (Vector2.Dot(d, d) <= r * r)
                    {
                        bs[b].Active = false;

                        Vector2 hitDir = d;
                        if (hitDir.LengthSquared() < 0.0001f) hitDir = new Vector2(1, 0);
                        else hitDir = Vector2.Normalize(hitDir);

                        SplitAsteroidAt(i, hitDir);
                        break;
                    }
                }
            }
        }
        int CountActiveAsteroids()
        {
            int n = 0;
            for (int i = 0; i < Asteroid.asteroids.Length; i++)
                if (Asteroid.asteroids[i].Active) n++;
            return n;
        }

        int FindFreeAsteroidSlot()
        {
            for (int i = 0; i < Asteroid.asteroids.Length; i++)
                if (!Asteroid.asteroids[i].Active) return i;
            return -1;
        }

        void StartNextWave()
        {
            currentWave++;

            int toSpawn = Math.Min(2 + currentWave, Asteroid.asteroids.Length);

            int spawned = 0;
            while (spawned < toSpawn)
            {
                int slot = FindFreeAsteroidSlot();
                if (slot == -1) break; 

                float x = (float)Asteroid.rng.NextDouble() * Program.screenSize.X;
                float y = (float)Asteroid.rng.NextDouble() * Program.screenSize.Y;
                Vector2 pos = new Vector2(x, y);

                SpawnAsteroid(slot, 2, pos);
                spawned++;
            }
        }

    }
}
