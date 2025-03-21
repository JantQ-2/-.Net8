using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Raylib_cs;
using System.Numerics;
public class Ammus
    {
        public Vector2 sijainti;
        public Vector2 suunta;
        public float nopeus = 5f;

        public Ammus(Vector2 sijainti, Vector2 suunta)
        {
            this.sijainti = sijainti;
            this.suunta = suunta;
        }

        public void Update()
        {
            sijainti += suunta * nopeus;
        }

        public void Draw()
        {
            Raylib.DrawCircle((int)sijainti.X, (int)sijainti.Y, 5, Color.Yellow);
        }
    }
