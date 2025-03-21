using Raylib_cs;
using System;
using System.Numerics;

internal class TANKS
{
    public const int ruudunKorkeus = 800;
    public const int ruudunLeveys = 1000;

    static void Main(string[] args)
    {
        Raylib.InitWindow(ruudunLeveys, ruudunKorkeus, "TANKS!");
        Raylib.SetTargetFPS(60);

        while (!Raylib.WindowShouldClose())
        {
            Tank tankInstance = new Tank();
            tankInstance.IsKeyDown();
            Tank.TarkistaRajat();

            foreach (var ammus in Tank.p1Ammukset) ammus.Update();
            foreach (var ammus in Tank.p2Ammukset) ammus.Update();

            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.Black);

            Raylib.DrawRectanglePro(
            new Rectangle(Tank.p1Sijainti.X, Tank.p1Sijainti.Y, Tank.pelaajanKoko.X, Tank.pelaajanKoko.Y),
            new Vector2(Tank.pelaajanKoko.X / 2, Tank.pelaajanKoko.Y / 2),
            Tank.p1Kulma,
            Color.Green);

            Raylib.DrawRectanglePro(
                new Rectangle(Tank.p2Sijainti.X, Tank.p2Sijainti.Y, Tank.pelaajanKoko.X, Tank.pelaajanKoko.Y),
                new Vector2(Tank.pelaajanKoko.X / 2, Tank.pelaajanKoko.Y / 2),
                Tank.p2Kulma,
                Color.Red);

            foreach (var ammus in Tank.p1Ammukset) ammus.Draw();
            foreach (var ammus in Tank.p2Ammukset) ammus.Draw();

            Raylib.EndDrawing();
        }
        Raylib.CloseWindow();
    }
}