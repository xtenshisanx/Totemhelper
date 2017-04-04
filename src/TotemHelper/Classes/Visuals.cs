using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SharpDX;

namespace TotemHelper.Classes
{
    class Visuals
    {
        public static List<Vector2> CirclePoints(Vector3 Center, float Radius, Int32 points)
        {
            List<Vector2> result = new List<Vector2>();
            var slice = (2 * Math.PI) / points;
            for (int i = 0; i < points; i++)
            {
                var angle = slice * i;
                var x = (decimal)Center.X + Decimal.Multiply((decimal)Radius, (decimal)Math.Cos(angle));
                var y = (decimal)Center.Y + Decimal.Multiply((decimal)Radius, (decimal)Math.Sin(angle));
                var Y = Decimal.Multiply((decimal)(Center.Y + Radius), (decimal)Math.Sin(angle));
                result.Add(new Vector2((float)x, (float)y));
            }
            return result;
        }
    }
}
