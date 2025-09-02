using Asteroids;
using Raylib_cs;
using System.Numerics;

public class Player
{
    public static Vector2 playerPosition;
    Vector2 velocity = new Vector2(0, 0);
    public static float playerRotation = 0;
    public static Texture2D playerShipTexture;


    public float Scale;
    float acceleration = 0.1f;
    float rotationSpeed = 3;
    float maxSpeed = 20;
    int playerHealth = 5;

    const int MaxBullets = 8;
    Bullet[] bullets = new Bullet[MaxBullets];
    int fireCooldownFrames = 15;   // 15f per Frame
    const int fireDelayFrames = 7; // ~9 shots/sec at 60 FPS
    const float bulletSpeedPerFrame = 15f; // 15 pixels per frame at 60 FPS
    const int bulletLifeFrames = 120;       // 2s at 60 FPS 
    public static Player Instance { get; private set; }

    public Bullet[] GetBullets() => bullets;


    // In Player.cs constructor
    public Player()
    {
        Instance = this;
        playerShipTexture = Raylib.LoadTexture("images/more_images/PNG/playerShip1_red.png");
        Scale = 1.5f;                
        LoadPlayerData();
    }


    public static void Init()
    {
        var savedData = GameData.LoadShipData();
        if (savedData != null)
        {
            playerPosition = new Vector2(savedData.Position.X, savedData.Position.Y);
            playerRotation = savedData.Rotation.PlayerRotation;
        }
        else
        {
            playerPosition = new Vector2(Program.screenSize.X / 2, Program.screenSize.Y / 2);
            playerRotation = 0;
        }
    }

    public void LoadPlayerData()
    {
        var savedData = GameData.LoadShipData();
        if (savedData != null)
        {
            playerPosition = new Vector2(savedData.Position.X, savedData.Position.Y);
            playerRotation = savedData.Rotation.PlayerRotation;
        }
    }

    public void DrawPlayer()
    {
        KeyDown();

        // Bullet
        for (int i = 0; i < bullets.Length; i++)
            if (bullets[i].Active)
                bullets[i].Update(Program.screenSize);

        Rectangle sourceRec = new Rectangle(0, 0, playerShipTexture.Width, playerShipTexture.Height);

        float scale = 1.5f;
        Rectangle destRec = new Rectangle(playerPosition.X, playerPosition.Y, playerShipTexture.Width * scale, playerShipTexture.Height * scale);

        Vector2 origin = new Vector2(playerShipTexture.Width * scale / 2, playerShipTexture.Height * scale / 2);

        Raylib.DrawTexturePro(playerShipTexture, sourceRec, destRec, origin, playerRotation, Color.White);

        // Draw Bullet
        for (int i = 0; i < bullets.Length; i++)
        {
            if (!bullets[i].Active) continue;
            Raylib.DrawCircleV(bullets[i].Position, 3f, Color.White);
        }
    }


    public void KeyDown()
    {
        // Turn
        if (Raylib.IsKeyDown(KeyboardKey.A))
        {
            playerRotation -= rotationSpeed;
        }
        if (Raylib.IsKeyDown(KeyboardKey.D))
        {
            playerRotation += rotationSpeed;
        }

        // Forward depending on angle
        if (Raylib.IsKeyDown(KeyboardKey.W))
        {
            float radians = MathF.PI / 180f * (playerRotation - 90);
            Vector2 direction = new Vector2(MathF.Cos(radians), MathF.Sin(radians));

            velocity += direction * acceleration;
        }

        // Shoot
        if (fireCooldownFrames > 0) fireCooldownFrames--;
        if (Raylib.IsKeyDown(KeyboardKey.Space) && fireCooldownFrames <= 0)
        {
            FireBullet();
            fireCooldownFrames = fireDelayFrames;
        }



        playerPosition += velocity;

        if (velocity.Length() > maxSpeed)
        {
            velocity = Vector2.Normalize(velocity) * maxSpeed;
        }
    }
    public void SideSwapper()
    {
        if (playerPosition.X > Program.screenSize.X)
        {
            playerPosition.X = 0;
            
        }
        else if (playerPosition.X < 0)
        {
            playerPosition.X = Program.screenSize.X;
        }

        if (playerPosition.Y > Program.screenSize.Y)
        {
            playerPosition.Y = 0;
        }
        else if (playerPosition.Y < 0)
        {
            playerPosition.Y = Program.screenSize.Y;
        }

    }
    void FireBullet()
    {
        float rad = (playerRotation - 90f) * (MathF.PI / 180f);
        Vector2 dir = new Vector2(MathF.Cos(rad), MathF.Sin(rad));

       
        float scale = 1.5f;
        float halfHeight = (playerShipTexture.Height * scale) * 0.5f;
        Vector2 spawn = playerPosition + dir * halfHeight;

      
        for (int i = 0; i < bullets.Length; i++)
        {
            if (bullets[i].Active) continue;

            bullets[i] = new Bullet
            {
                Position = spawn,
                Velocity = dir * bulletSpeedPerFrame + (velocity * 0.25f),
                LifeFrames = bulletLifeFrames,
                Active = true
            };
            break; 
        }
    }

    void GetPlayerWidth()
    {

    }

    public static float GetPlayerRadius(Player p)
    {
        if (p == null) return 0f;
        float baseRadius = playerShipTexture.Width * p.Scale * 0.5f;
        return baseRadius * 0.45f;
    }
}
