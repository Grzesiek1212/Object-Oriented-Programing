using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab05
{
    public struct Vector3
    {
        private float X;
        private float Y;
        private float Z;

        public Vector3(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }
        public override string ToString()
        {
            return $"Point({X}, {Y}, {Z})";
        }

        public static Vector3 Add(Vector3 v1, Vector3 v2)
        {
            return new Vector3(v1.X+v2.X,v1.Y+v2.Y,v1.Z+v2.Z);
        }

        public static implicit operator Vector3(Figure v)
        {
            throw new NotImplementedException();
        }
    }
}
