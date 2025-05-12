using Asteroids;
using Raylib_cs;
using System.Numerics;

public class Player
{
    Vector2 playerPosition = new Vector2(Program.screenSize.X / 2, Program.screenSize.Y / 2);
    Vector2 velocity = new Vector2(0, 0);
    float playerRotation = 0;
    Texture2D playerShipTexture;

    float acceleration = 0.1f;
    float rotationSpeed = 3;
    float maxSpeed = 20;
    public Player()
    {
        playerShipTexture = Raylib.LoadTexture("images/more_images/PNG/playerShip1_red.png");
    }

    public void DrawPlayer()
    {
        KeyDown();

        Rectangle sourceRec = new Rectangle(0, 0, playerShipTexture.Width, playerShipTexture.Height);

        float scale = 1.5f;
        Rectangle destRec = new Rectangle(playerPosition.X, playerPosition.Y, playerShipTexture.Width * scale, playerShipTexture.Height * scale);

        Vector2 origin = new Vector2(playerShipTexture.Width * scale / 2, playerShipTexture.Height * scale / 2);

        Raylib.DrawTexturePro(playerShipTexture, sourceRec, destRec, origin, playerRotation, Color.White);
    }

    public void KeyDown()
    {
        if (Raylib.IsKeyDown(KeyboardKey.A))
        {
            playerRotation -= rotationSpeed;
        }
        if (Raylib.IsKeyDown(KeyboardKey.D))
        {
            playerRotation += rotationSpeed;
        }

        if (Raylib.IsKeyDown(KeyboardKey.W))
        {
            float radians = MathF.PI / 180f * (playerRotation - 90);
            Vector2 direction = new Vector2(MathF.Cos(radians), MathF.Sin(radians));

            velocity += direction * acceleration;
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
}
