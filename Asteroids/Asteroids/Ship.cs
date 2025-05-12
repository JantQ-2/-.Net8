using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Asteroids
{
    public class Ship
    {
        public string Name { get; set; }
        public int Health { get; set; }
        public bool Status { get; set; }
        public Position Position { get; set; }
        public Rotation Rotation { get; set; }
    }

    public class Position
    {
        public int X { get; set; }
        public int Y { get; set; }
    }

    public class Rotation
    {
        public float playerRotation { get; set; }
    }
}
