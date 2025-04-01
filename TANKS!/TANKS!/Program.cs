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
           

            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.Black);

           
            Raylib.EndDrawing();
        }
        Raylib.CloseWindow();
    }
}