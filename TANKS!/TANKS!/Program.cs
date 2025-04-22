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

        Tank tank1 = new Tank(p2sijainti, KeyboardKey.Up, KeyboardKey.Down, KeyboardKey.Left, KeyboardKey.Right, KeyboardKey.Enter, Color.Green);
        Tank tank2 = new Tank(p1sijainti, KeyboardKey.W, KeyboardKey.S, KeyboardKey.A, KeyboardKey.D, KeyboardKey.Space, Color.Red);

        List<Ammus> Ammukset = new List<Ammus>();

        while (!Raylib.WindowShouldClose())
        {
            if (tank1.shoot())
            {
                Ammukset.Add(new Ammus(tank1.position, tank1.direction));
            }
            if (tank2.shoot())
            {
                Ammukset.Add(new Ammus(tank2.position, tank2.direction));
            }

            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.Black);

            Raylib.DrawRectangleRec(wall1, Color.White);
            Raylib.DrawRectangleRec(wall2, Color.White);

            tank1.DrawTank(wall1, wall2);
            tank2.DrawTank(wall1, wall2);

            foreach (Ammus a in Ammukset)
            {
                Raylib.DrawCircleV(a.sijainti, 10, Color.Yellow);
                a.drawAmmus();
            }

            Raylib.EndDrawing();
        }

        Raylib.CloseWindow();
    }
}
