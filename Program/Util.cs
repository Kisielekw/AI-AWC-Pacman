using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Pacman
{
    internal static class Util
    {
        public static Vector2 Rotate(this Vector2 pVector, float pAngle)
        {
            float x = -MathF.Sin(pAngle) * pVector.Y + MathF.Cos(pAngle) * pVector.X;
            float y = MathF.Sin(pAngle) * pVector.X + MathF.Cos(pAngle) * pVector.Y;
            return new Vector2(x, y);
        }
    }
}
