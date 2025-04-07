using Raylib_cs;
using System;
using System.Numerics;

internal class TANKS
{
    public const int ruudunKorkeus = 800;
    public const int ruudunLeveys = 1000;

    public static Vector2 p1sijainti = new Vector2(ruudunLeveys / 10, ruudunKorkeus / 2);
    public static Vector2 p2sijainti = new Vector2(ruudunLeveys - (ruudunLeveys / 10), ruudunKorkeus / 2);

    static void Main(string[] args)
    {
        Raylib.InitWindow(ruudunLeveys, ruudunKorkeus, "TANKS!");
        Raylib.SetTargetFPS(60);

        while (!Raylib.WindowShouldClose())
        {
            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.Black);


            Console.WriteLine(p1sijainti);
            Console.WriteLine(p2sijainti);
           
            Raylib.EndDrawing();
        }
        Raylib.CloseWindow();
    }
}