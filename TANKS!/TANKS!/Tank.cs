using Raylib_cs;
using System.Numerics;


public class Tank
{

    public double lastShootTime = 0;
    public double shootInterval = 0.4;
    
    int tankWidth = 40;
    int tankHeight = 40;
    int cannonWidth = 15;
    int cannonHeight = 15;

    public Vector2 position = new Vector2();
    public Vector2 direction = new Vector2(0, 1);
    
    int speed = 2;
    public Color color;
    public KeyboardKey Up, Down, Left, Right, Space;
    public bool isAlive = true;

    public Tank(Vector2 startPos, KeyboardKey upKey, KeyboardKey downKey, KeyboardKey leftKey, KeyboardKey rightKey, KeyboardKey shootKey, Color color)
    {
        position = startPos;
        Up = upKey;
        Down = downKey;
        Left = leftKey;
        Right = rightKey;
        Space = shootKey;
        this.color = color;
        isAlive = true;
    }

    public void DrawTank(Rectangle wall1, Rectangle wall2)
    {
        if (!isAlive) return;

        Raylib.DrawRectangle((int)position.X - (tankWidth / 2), (int)position.Y - tankHeight / 2, tankWidth, tankHeight, color);

        if (Raylib.IsKeyDown(Up) && !CheckCollision(position.X, position.Y - speed, wall1, wall2))
        {
            position.Y -= speed;
            direction.Y = -1;
            direction.X = 0;
        }
        if (Raylib.IsKeyDown(Down) && !CheckCollision(position.X, position.Y + speed, wall1, wall2))
        {
            position.Y += speed;
            direction.Y = 1;
            direction.X = 0;
        }
        if (Raylib.IsKeyDown(Left) && !CheckCollision(position.X - speed, position.Y, wall1, wall2))
        {
            position.X -= speed;
            direction.Y = 0;
            direction.X = -1;
        }
        if (Raylib.IsKeyDown(Right) && !CheckCollision(position.X + speed, position.Y, wall1, wall2))
        {
            position.X += speed;
            direction.Y = 0;
            direction.X = 1;
        }

        Raylib.DrawRectangle((int)((position.X + direction.X * tankWidth / 2) - cannonWidth / 2), 
                             (int)((position.Y + direction.Y * tankHeight / 2) - (cannonHeight / 2)), 
                             cannonWidth, cannonHeight, Color.Gray);

        if (position.Y >= TANKS.ruudunKorkeus - (tankHeight / 2)) position.Y = TANKS.ruudunKorkeus - (tankHeight / 2);
        if (position.Y <= 0 + (tankHeight / 2)) position.Y = 0 + (tankHeight / 2);
        if (position.X >= TANKS.ruudunLeveys - (tankWidth / 2)) position.X = TANKS.ruudunLeveys - (tankWidth / 2);
        if (position.X <= 0 + (tankWidth / 2)) position.X = 0 + (tankWidth / 2);
    }

    private bool CheckCollision(float x, float y, Rectangle wall1, Rectangle wall2)
    {
        Rectangle tankRect = new Rectangle(x - tankWidth / 2, y - tankHeight / 2, tankWidth, tankHeight);
        
        return Raylib.CheckCollisionRecs(tankRect, wall1) || Raylib.CheckCollisionRecs(tankRect, wall2);
    }
    public bool shoot() {
        bool canShoot = Raylib.GetTime() - lastShootTime >= shootInterval;
        bool keyDown = (Raylib.IsKeyDown(Space));
        if (keyDown && canShoot)
        { 
            lastShootTime = Raylib.GetTime();
            return canShoot;
        }
        else return false;
    }
    public Rectangle GetTankRectangle()
    {
        return new Rectangle(position.X - 20, position.Y - 20, 40, 40); 
    }
}

public class Wall
{
    public Wall() { }
}