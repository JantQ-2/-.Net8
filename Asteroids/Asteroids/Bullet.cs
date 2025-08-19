using System.Numerics;

public struct Bullet
{
    public Vector2 Position;
    public Vector2 Velocity;
    public int LifeFrames;
    public bool Active;

    public void Update(Vector2 screenSize)
    {
        if (!Active) return;

        Position += Velocity;

        if (Position.X > screenSize.X) Position.X = 0;
        else if (Position.X < 0) Position.X = screenSize.X;

        if (Position.Y > screenSize.Y) Position.Y = 0;
        else if (Position.Y < 0) Position.Y = screenSize.Y;

        LifeFrames--;
        if (LifeFrames <= 0) Active = false;
    }
}
