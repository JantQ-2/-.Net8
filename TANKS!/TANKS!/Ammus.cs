using Raylib_cs;
using System.Numerics;
public class Ammus
{
    public Vector2 sijainti;
    public Vector2 suunta;
    public float nopeus = 500f;
    public Ammus(Vector2 position, Vector2 direciton)
    {
        sijainti = position;
        suunta = direciton;
    }

    public void drawAmmus()
    {
        sijainti += nopeus * suunta * Raylib.GetFrameTime();
    }
}
