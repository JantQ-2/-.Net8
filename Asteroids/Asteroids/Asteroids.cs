using Asteroids;
using Raylib_cs;
using System.Numerics;

public struct Asteroid
{
    public Vector2 Position;
    public Vector2 Velocity;          
    public float RotationDeg;         
    public float RotationSpeedDeg;    
    public float Scale;               
    public bool Active;


    public static Texture2D asteroidTexture;
    const int MaxAsteroids = 12;
    public static Asteroid[] asteroids = new Asteroid[MaxAsteroids];
    public static Random rng = new Random();

    public int Size; // 2 = big, 1 = mid, 0 = smallt


    public void Update(Vector2 screenSize)
    {
        if (!Active) return;

        Position += Velocity;
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

        for (int i = 0; i < 6; i++) Program.SpawnAsteroid(i);
    }


}
