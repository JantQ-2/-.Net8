using Raylib_cs;
using System;
using System.Numerics;

internal class TANKS
{
    public static int ruudunKorkeus = 800;
    public static int ruudunLeveys = 1000;

    public static Vector2 p1sijainti = new Vector2(TANKS.ruudunLeveys / 10, TANKS.ruudunKorkeus / 2);
    public static Vector2 p2sijainti = new Vector2(TANKS.ruudunLeveys - (TANKS.ruudunLeveys / 10), TANKS.ruudunKorkeus / 2);

    public static Rectangle wall1 = new Rectangle(ruudunLeveys / 5, ruudunKorkeus / 4, 50, 300);
    public static Rectangle wall2 = new Rectangle(ruudunLeveys - (ruudunLeveys / 5), ruudunKorkeus / 4, 50, 300);

    static void Main(string[] args)
    {
        Raylib.InitWindow(ruudunLeveys, ruudunKorkeus, "TANKS!");
        Raylib.SetTargetFPS(60);

        Tank tank1 = new Tank(p1sijainti, KeyboardKey.W, KeyboardKey.S, KeyboardKey.A, KeyboardKey.D, KeyboardKey.Space, Color.Red);
        Tank tank2 = new Tank(p2sijainti, KeyboardKey.Up, KeyboardKey.Down, KeyboardKey.Left, KeyboardKey.Right, KeyboardKey.Enter, Color.Green);

        List<Ammus> Ammukset = new List<Ammus>();

        bool gameOver = false;
        double gameOverTime = -1;

        while (!Raylib.WindowShouldClose())
        {
            if (!gameOver)
            {
                if (tank1.isAlive && tank1.shoot())
                    Ammukset.Add(new Ammus(tank1.position, tank1.direction, tank1));

                if (tank2.isAlive && tank2.shoot())
                    Ammukset.Add(new Ammus(tank2.position, tank2.direction, tank2));
            }

            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.Black);

            Raylib.DrawRectangleRec(wall1, Color.White);
            Raylib.DrawRectangleRec(wall2, Color.White);

            if (tank1.isAlive) tank1.DrawTank(wall1, wall2);
            if (tank2.isAlive) tank2.DrawTank(wall1, wall2);

            for (int i = Ammukset.Count - 1; i >= 0; i--)
            {
                Ammus ammus = Ammukset[i];
                ammus.drawAmmus();
                Raylib.DrawCircleV(ammus.sijainti, 10, Color.Yellow);

                if (Raylib.CheckCollisionCircleRec(ammus.sijainti, 10, wall1) ||
                    Raylib.CheckCollisionCircleRec(ammus.sijainti, 10, wall2))
                {
                    Ammukset.RemoveAt(i);
                }

                if (tank1.isAlive && Raylib.CheckCollisionCircleRec(ammus.sijainti, 10, tank1.GetTankRectangle()) && ammus.omistaja != tank1)
                {
                    tank1.isAlive = false;
                    Ammukset.RemoveAt(i);
                    gameOver = true;
                    gameOverTime = Raylib.GetTime();
                }

                if (tank2.isAlive && Raylib.CheckCollisionCircleRec(ammus.sijainti, 10, tank2.GetTankRectangle()) && ammus.omistaja != tank2)
                {
                    tank2.isAlive = false;
                    Ammukset.RemoveAt(i);
                    gameOver = true;
                    gameOverTime = Raylib.GetTime();
                }
            }

            if (gameOver)
            {
                if (!tank1.isAlive && tank2.isAlive)
                    Raylib.DrawText("GREEN TANK WINS!", ruudunLeveys / 2 - 150, ruudunKorkeus / 2, 40, Color.Green);
                else if (!tank2.isAlive && tank1.isAlive)
                    Raylib.DrawText("RED TANK WINS!", ruudunLeveys / 2 - 150, ruudunKorkeus / 2, 40, Color.Red);
                else if (!tank1.isAlive && !tank2.isAlive)
                    Raylib.DrawText("BOTH TANKS DESTROYED!", ruudunLeveys / 2 - 200, ruudunKorkeus / 2, 40, Color.LightGray);

                if (Raylib.GetTime() - gameOverTime >= 3)
                {
                    tank1 = new Tank(p1sijainti, KeyboardKey.W, KeyboardKey.S, KeyboardKey.A, KeyboardKey.D, KeyboardKey.Space, Color.Red);
                    tank2 = new Tank(p2sijainti, KeyboardKey.Up, KeyboardKey.Down, KeyboardKey.Left, KeyboardKey.Right, KeyboardKey.Enter, Color.Green);
                    Ammukset.Clear();
                    gameOver = false;
                    
                }

            }

            Raylib.EndDrawing();
        }

        Raylib.CloseWindow();
    }

}
