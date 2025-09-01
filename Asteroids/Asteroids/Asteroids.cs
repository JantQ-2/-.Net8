using Asteroids;
using Raylib_cs;
using System.Numerics;

public class Asteroid
{
    public Vector2 Position;
    public Vector2 Velocity;
    public float RotationDeg;
    public float RotationSpeedDeg;
    public float Scale;
    public bool Active;
    public static float speedScale = 0.5f;

    public static Texture2D asteroidTexture;
    const int MaxAsteroids = 12;
    public static Asteroid[] asteroids = new Asteroid[MaxAsteroids];
    public static Random rng = new Random();

    public int Size; // 2 = big, 1 = mid, 0 = small

    public void Update(Vector2 screenSize)
    {
        if (!Active) return;

        Position += Velocity * speedScale;
        RotationDeg += RotationSpeedDeg;

        if (Position.X > screenSize.X) Position.X = 0;
        else if (Position.X < 0) Position.X = screenSize.X;

        if (Position.Y > screenSize.Y) Position.Y = 0;
        else if (Position.Y < 0) Position.Y = screenSize.Y;
    }

    public void Draw(Texture2D tex)
    {
        if (!Active) return;

        Rectangle src = new Rectangle(0, 0, tex.Width, tex.Height);
        Rectangle dst = new Rectangle(Position.X, Position.Y, tex.Width * Scale, tex.Height * Scale);
        Vector2 origin = new Vector2(dst.Width * 0.5f, dst.Height * 0.5f);

        Raylib.DrawTexturePro(tex, src, dst, origin, RotationDeg, Color.White);
    }


    public static void Init()
    {
        asteroidTexture = Raylib.LoadTexture("images/more_images/PNG/Meteors/meteorGrey_big1.png");

        for (int i = 0; i < asteroids.Length; i++)
            asteroids[i] = new Asteroid { Active = false, Scale = 1f, Size = 2 };

        for (int i = 0; i < 6 && i < asteroids.Length; i++)
            SpawnAsteroid(i);
    }

    public static void SpawnAsteroid(int index)
    {
        float x = (float)rng.NextDouble() * Program.screenSize.X;
        float y = (float)rng.NextDouble() * Program.screenSize.Y;
        SpawnAsteroid(index, 2, new Vector2(x, y));
    }

    public static void SpawnAsteroid(int index, int size, Vector2 pos)
    {
        if (index < 0 || index >= asteroids.Length) return;

        float scale = size == 2 ? 1.6f : size == 1 ? 1.0f : 0.6f;
        float speedBase = size == 2 ? 1.6f : size == 1 ? 1.0f : 0.3f;

        float angle = (float)(rng.NextDouble() * MathF.PI * 2f);
        Vector2 vel = new Vector2(MathF.Cos(angle), MathF.Sin(angle)) * (speedBase + (float)rng.NextDouble() * 1.5f);
        float rotSpd = (float)(rng.NextDouble() * 4.0 - 2.0);

        var a = asteroids[index];
        a.Position = pos;
        a.Velocity = vel;
        a.RotationDeg = (float)(rng.NextDouble() * 360f);
        a.RotationSpeedDeg = rotSpd;
        a.Scale = scale;
        a.Size = size;
        a.Active = true;
    }

    public static void UpdateAsteroids()
    {
        for (int i = 0; i < asteroids.Length; i++)
        {
            var a = asteroids[i];
            if (a == null || !a.Active) continue;
            a.Update(Program.screenSize);
        }
    }

    public static void DrawAsteroids()
    {
        if (asteroidTexture.Width == 0) return; 
        for (int i = 0; i < asteroids.Length; i++)
        {
            var a = asteroids[i];
            if (a == null || !a.Active) continue;
            a.Draw(asteroidTexture);
        }
    }

    const float BulletRadius = 3f;

    public static float GetAsteroidRadius(Asteroid a)
    {
        if (a == null) return 0f;
        float baseRadius = asteroidTexture.Width * a.Scale * 0.5f;
        return baseRadius * 0.45f;
    }

    public static void SplitAsteroidAt(int index, Vector2 hitDir)
    {
        if (index < 0 || index >= asteroids.Length) return;

        var a = asteroids[index];
        if (a == null || !a.Active) return;

        a.Active = false;

        if (a.Size <= 0) return;

        int children = a.Size == 2 ? 2 : 1;
        int childSize = a.Size - 1;

        for (int i = 0; i < children; i++)
        {
            int slot = FindFreeAsteroidSlot();
            if (slot == -1) break;

            float baseAngle = MathF.Atan2(hitDir.Y, hitDir.X);
            float spread = ((float)rng.NextDouble() - 0.5f) * 1.4f;
            float ang = baseAngle + spread;

            float speed = a.Velocity.Length() + 2.5f + (float)rng.NextDouble() * 1.5f;
            Vector2 vel = new Vector2(MathF.Cos(ang), MathF.Sin(ang)) * speed;

            SpawnAsteroid(slot, childSize, a.Position);
            asteroids[slot].Velocity = vel;
        }
    }

    public static void HandleBulletAsteroidCollisions()
    {
        var bs = Player.Instance?.GetBullets();
        if (bs == null) return;

        for (int b = 0; b < bs.Length; b++)
        {
            if (!bs[b].Active) continue;

            for (int i = 0; i < asteroids.Length; i++)
            {
                var a = asteroids[i];
                if (a == null || !a.Active) continue;

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

    public static int CountActiveAsteroids()
    {
        int n = 0;
        for (int i = 0; i < asteroids.Length; i++)
        {
            var a = asteroids[i];
            if (a != null && a.Active) n++;
        }
        return n;
    }

    public static int FindFreeAsteroidSlot()
    {
        for (int i = 0; i < asteroids.Length; i++)
        {
            var a = asteroids[i];
            if (a == null || !a.Active) return i;
        }
        return -1;
    }
    public static bool IsPlayerColliding(Player p)
    {
        if (p == null) return false;

        float pr = Player.GetPlayerRadius(p);        
        float pr2 = pr * pr;

        for (int i = 0; i < asteroids.Length; i++)
        {
            var a = asteroids[i];
            if (a == null || !a.Active) continue;

            float ar = GetAsteroidRadius(a);         
            float rr = pr + ar;

            Vector2 d = Player.playerPosition - a.Position;
            if (Vector2.Dot(d, d) <= rr * rr)        
                return true;
        }
        return false;
    }

}
