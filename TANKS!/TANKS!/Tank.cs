using Raylib_cs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;


public class Tank
{
    public static Vector2 p1Sijainti = new Vector2(TANKS.ruudunLeveys / 10, TANKS.ruudunKorkeus / 2);
    public static Vector2 p2Sijainti = new Vector2(TANKS.ruudunLeveys - TANKS.ruudunLeveys / 10, TANKS.ruudunKorkeus / 2);
    public static float nopeus = 3f;
    public static float kaannosNopeus = 3f;
    public static Vector2 pelaajanKoko = new Vector2(50, 50);
    public static float p1Kulma = 0f;
    public static float p2Kulma = 180f;

    public static List<Ammus> p1Ammukset = new List<Ammus>();
    public static List<Ammus> p2Ammukset = new List<Ammus>();

    public void IsKeyDown()
    {
        // Pelaaja 1 liike
        if (Raylib.IsKeyDown(KeyboardKey.A)) p1Kulma -= kaannosNopeus;
        if (Raylib.IsKeyDown(KeyboardKey.D)) p1Kulma += kaannosNopeus;
        if (Raylib.IsKeyDown(KeyboardKey.W)) p1Sijainti += GetLiikeVector(p1Kulma);
        if (Raylib.IsKeyDown(KeyboardKey.S)) p1Sijainti -= GetLiikeVector(p1Kulma);

        // Pelaaja 1 ammus
        if (Raylib.IsKeyPressed(KeyboardKey.Space))
        {
            Vector2 ammusSuunta = GetLiikeVector(p1Kulma);
            p1Ammukset.Add(new Ammus(p1Sijainti, ammusSuunta));
        }

        // Pelaaja 2 liike
        if (Raylib.IsKeyDown(KeyboardKey.Left)) p2Kulma -= kaannosNopeus;
        if (Raylib.IsKeyDown(KeyboardKey.Right)) p2Kulma += kaannosNopeus;
        if (Raylib.IsKeyDown(KeyboardKey.Up)) p2Sijainti += GetLiikeVector(p2Kulma);
        if (Raylib.IsKeyDown(KeyboardKey.Down)) p2Sijainti -= GetLiikeVector(p2Kulma);

        // Pelaaja 2 ammus
        if (Raylib.IsKeyPressed(KeyboardKey.Enter))
        {
            Vector2 ammusSuunta = GetLiikeVector(p2Kulma);
            p2Ammukset.Add(new Ammus(p2Sijainti, ammusSuunta));
        }
    }

    public static Vector2 GetLiikeVector(float kulma)
    {
        return new Vector2(MathF.Sin(kulma * MathF.PI / 180), -MathF.Cos(kulma * MathF.PI / 180)) * nopeus;
    }

    public static void TarkistaRajat()
    {
        p1Sijainti = Vector2.Clamp(p1Sijainti, Vector2.Zero, new Vector2(TANKS.ruudunLeveys, TANKS.ruudunKorkeus));
        p2Sijainti = Vector2.Clamp(p2Sijainti, Vector2.Zero, new Vector2(TANKS.ruudunLeveys, TANKS.ruudunKorkeus));
    }
}