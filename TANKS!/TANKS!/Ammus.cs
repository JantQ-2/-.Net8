using Raylib_cs;
using System.Numerics;

public class Ammus
{
    public Vector2 sijainti;
    public Vector2 suunta;
    public float nopeus = 500f;
    public Tank omistaja;

    public Ammus(Vector2 position, Vector2 direction, Tank omistaja)
    {
        sijainti = position;
        suunta = direction;
        this.omistaja = omistaja;
    }

    public void drawAmmus()
    {
        sijainti += nopeus * suunta * Raylib.GetFrameTime();
    }
}
