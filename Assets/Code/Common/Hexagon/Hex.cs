using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bestagon.Hexagon
{
    /// <summary>
    /// Representation of an Axial Hex Coordinate (implicit Cube coordinate)
    /// </summary>
    /// 
    public static class SideExtensions
    {
        public static float Degrees(this Hex.Side side)
        {
            return (int)side * 60.0f;
        }
        public static Hex Offset(this Hex.Side side)
        {
            return Hex.Offsets[(int)side];
        }

        public static (float min, float max) PointRadians(this Hex.Side side)
        {
            switch (side)
            {
                default:
                case Hex.Side.Right:
                    return (330f * Mathf.Deg2Rad, 30f * Mathf.Deg2Rad);
                case Hex.Side.UpRight:
                    return (30f * Mathf.Deg2Rad, 90f * Mathf.Deg2Rad);
                case Hex.Side.UpLeft:
                    return (90f * Mathf.Deg2Rad, 150f * Mathf.Deg2Rad);
                case Hex.Side.Left:
                    return (150f * Mathf.Deg2Rad, 210f * Mathf.Deg2Rad);
                case Hex.Side.DownLeft:
                    return (210f * Mathf.Deg2Rad, 270f * Mathf.Deg2Rad);
                case Hex.Side.DownRight:
                    return (270f * Mathf.Deg2Rad, 330f * Mathf.Deg2Rad);
            }
        }

        public static float Radians(this Hex.Side side)
        {
            switch (side)
            {
                default:
                case Hex.Side.Right:
                    return 0f;
                case Hex.Side.UpRight:
                    return 60f * Mathf.Deg2Rad;
                case Hex.Side.UpLeft:
                    return 120f * Mathf.Deg2Rad;
                case Hex.Side.Left:
                    return 180f * Mathf.Deg2Rad;
                case Hex.Side.DownLeft:
                    return 240f * Mathf.Deg2Rad;
                case Hex.Side.DownRight:
                    return 300f * Mathf.Deg2Rad;
            }
        }

        public static (float clockwise, float counterclockwise) SidePointRadians(this Hex.Side side)
        {
            return (side.Radians() + (30f * Mathf.Deg2Rad), side.Radians() - (30f * Mathf.Deg2Rad));
        }

        public static Hex.Side Inverse(this Hex.Side side)
        {
            switch (side)
            {
                default:
                case Hex.Side.Right:
                    return Hex.Side.Left;
                case Hex.Side.UpRight:
                    return Hex.Side.DownLeft;
                case Hex.Side.UpLeft:
                    return Hex.Side.DownRight;
                case Hex.Side.Left:
                    return Hex.Side.Right;
                case Hex.Side.DownLeft:
                    return Hex.Side.UpRight;
                case Hex.Side.DownRight:
                    return Hex.Side.UpLeft;
            }
        }

        public static bool Adjacent(this Hex.Side side, Hex.Side other, out bool clockwise)
        {
            switch (side)
            {
                default:
                case Hex.Side.Right:
                    {
                        clockwise = other == Hex.Side.DownRight;
                        return other == Hex.Side.DownRight || other == Hex.Side.UpRight;
                    }
                case Hex.Side.UpRight:
                    {
                        clockwise = other == Hex.Side.Right;
                        return other == Hex.Side.Right || other == Hex.Side.UpLeft;
                    }
                case Hex.Side.UpLeft:
                    {
                        clockwise = other == Hex.Side.UpRight;
                        return other == Hex.Side.UpRight || other == Hex.Side.Left;
                    }
                case Hex.Side.Left:
                    {
                        clockwise = other == Hex.Side.UpLeft;
                        return other == Hex.Side.UpLeft || other == Hex.Side.DownLeft;
                    }
                case Hex.Side.DownLeft:
                    {
                        clockwise = other == Hex.Side.Left;
                        return other == Hex.Side.Left || other == Hex.Side.DownRight;
                    }
                case Hex.Side.DownRight:
                    {
                        clockwise = other == Hex.Side.DownLeft;
                        return other == Hex.Side.DownLeft || other == Hex.Side.Right;
                    }
            }
        }

        private static Hex.Side[] right_clockwise = new Hex.Side[]  { Hex.Side.DownRight, Hex.Side.DownLeft, Hex.Side.Left, Hex.Side.UpLeft, Hex.Side.UpRight };
        private static Hex.Side[] UpRight_clockwise = new Hex.Side[]{ Hex.Side.Right, Hex.Side.DownRight, Hex.Side.DownLeft, Hex.Side.Left, Hex.Side.UpLeft };
        private static Hex.Side[] UpLeft_clockwise = new Hex.Side[] { Hex.Side.UpRight, Hex.Side.Right, Hex.Side.DownRight, Hex.Side.DownLeft, Hex.Side.Left };
        private static Hex.Side[] Left_clockwise = new Hex.Side[]   { Hex.Side.UpLeft, Hex.Side.UpRight, Hex.Side.Right, Hex.Side.DownRight, Hex.Side.DownLeft };
        private static Hex.Side[] DownLeft_clockwise = new Hex.Side[] { Hex.Side.Left, Hex.Side.UpLeft, Hex.Side.UpRight, Hex.Side.Right, Hex.Side.DownRight };
        private static Hex.Side[] DownRight_clockwise = new Hex.Side[] { Hex.Side.DownLeft, Hex.Side.Left, Hex.Side.UpLeft, Hex.Side.UpRight, Hex.Side.Right};

        public static Hex.Side[] RotationalExpansion(this Hex.Side side, int dist, bool clockwise)
        {
            if (dist <= 0)
                return new Hex.Side[] { };

            Hex.Side[] rotationalSet;
            switch (side)
            {
                case Hex.Side.Right:
                    rotationalSet = right_clockwise;
                    break;
                case Hex.Side.UpRight:
                    rotationalSet = UpRight_clockwise;
                    break;
                case Hex.Side.UpLeft:
                    rotationalSet = UpLeft_clockwise;
                    break;
                case Hex.Side.Left:
                    rotationalSet = Left_clockwise;
                    break;
                case Hex.Side.DownLeft:
                    rotationalSet = DownLeft_clockwise;
                    break;
                case Hex.Side.DownRight:
                    rotationalSet = DownRight_clockwise;
                    break;
                default:
                    rotationalSet = null;
                    break;
            }
            if (rotationalSet == null)
            {
                return null;
            }

            Hex.Side[] returns = new Hex.Side[dist > 6 ? 6 : dist];
            for (int i = 0; i < returns.Length; i++)
            {
                returns[i] = rotationalSet[clockwise ? i : (rotationalSet.Length - i - 1)];
            }
            return returns;
        }

        /// <summary>
        /// Returns the two sides that indicate a clockwise rotation relative to this side
        /// </summary>
        /// <param name="side"></param>
        /// <returns></returns>
        public static Hex.Side[] ClockwiseIndications(this Hex.Side side)
        {
            switch (side)
            {
                default:
                case Hex.Side.Right:
                    return new Hex.Side[] { Hex.Side.DownRight, Hex.Side.DownLeft };
                case Hex.Side.UpRight:
                    return new Hex.Side[] { Hex.Side.Right, Hex.Side.DownRight };
                case Hex.Side.UpLeft:
                    return new Hex.Side[] { Hex.Side.UpRight, Hex.Side.Right };
                case Hex.Side.Left:
                    return new Hex.Side[] { Hex.Side.UpLeft, Hex.Side.UpRight };
                case Hex.Side.DownLeft:
                    return new Hex.Side[] { Hex.Side.Left, Hex.Side.UpLeft };
                case Hex.Side.DownRight:
                    return new Hex.Side[] { Hex.Side.DownLeft, Hex.Side.Left };
            }
        }
    }


    [System.Serializable]
    public struct Hex
    {
        #region Static
        #region Definitions
        public enum Side
        {
            Right = 0,
            UpRight = 1,
            UpLeft = 2,
            Left = 3,
            DownLeft = 4,
            DownRight = 5
        }
        
        public static readonly Hex[] Offsets = {
            new Hex(+1, 0),
            new Hex(0, +1),
            new Hex(-1, +1),
            new Hex(-1, 0),
            new Hex(0, -1),
            new Hex(+1, -1),
        };

        public IEnumerable<Hex> Neighbors()
        {
            for (int i = 0; i < Offsets.Length; i++)
            {
                yield return this + Offsets[i];
            }
        }
        #endregion

        #region Operators
        public static Hex operator +(Hex a, Hex b) => new Hex(a.q + b.q, a.r + b.r);
        public static Hex operator -(Hex a, Hex b) => new Hex(a.q - b.q, a.r - b.r);
        public static Hex operator *(Hex h, float m) => Hex.Round(h.q * m, h.r * m);
        public static Hex operator *(float m, Hex h) => h * m;
        public static Hex operator *(int m, Hex h) => h * m;
        public static Hex operator *(Hex h, int m) => new Hex(h.q * m, h.r * m);
        public static Hex operator /(Hex h, int m) => new Hex(h.q / m, h.r / m);

        public static bool operator ==(Hex a, Hex b) => Hex.Equals(a, b);
        public static bool operator !=(Hex a, Hex b) => !Hex.Equals(a, b);

        #endregion
        #endregion

        #region Constructors
        public Hex(int q /*x*/, int r/*y*/)
        {
            this.m_q = q;
            this.m_r = r;
        }

        public Hex(Vector2Int v) : this(v.x, v.y) { }
        public Hex(Vector3Int v) : this(v.x, v.z) { }
        #endregion

        #region Members
#pragma warning disable IDE1006 // Naming Styles
        [SerializeField] private int m_q;
        [SerializeField] private int m_r;
        public int q { get { return m_q; } }
        public int r { get { return m_r; } }
        public int s { get { return -m_q - m_r; } }

        public int magnitude { get {
                return Mathf.Max(Mathf.Abs(q), Mathf.Abs(r), Mathf.Abs(s));
            }
        }
#pragma warning restore IDE1006 // Naming Styles
        #endregion


        public Vector3 UnityPosition()
        {
            return TileRenderer.GetCenterPosOfTile(TileRenderer.Channel.Debug, ToTilemap());
        }

        public Vector3 SideToWorld(Hex.Side side)
        {
            Vector3 basePos = UnityPosition();
            float r = Hex.SQRT3DIV2 / 2f;
            float theta = side.Degrees() * Mathf.Deg2Rad;

            return basePos + new Vector3(Mathf.Cos(theta) * r, Mathf.Sin(theta) * r);
        }


        public void DebugDrawCircle(float innerCirleSize, Color c)
        {
            Vector3 center = UnityPosition();
            int step = 16;
            float angle = 360f / (float)step;

            Vector3 CirclePoint(Hex h, float angleDegrees)
            {
                return h.UnityPosition() + new Vector3(Mathf.Cos(angleDegrees * Mathf.Deg2Rad) * innerCirleSize, Mathf.Sin(angleDegrees * Mathf.Deg2Rad) * innerCirleSize);
            }


            for (int i = 0; i <= step; i++)
                Debug.DrawLine(CirclePoint(this, i * angle), CirclePoint(this, (i + 1) * angle), c);

        }


        #region Overrides
        public override bool Equals(object obj)
        {
            if (obj.GetType() == typeof(Hex))
                return q == ((Hex)obj).q && r == ((Hex)obj).r;
            else if (obj.GetType() == typeof(Vector2Int))
                return q == ((Vector2Int)obj).x && r == ((Vector2Int)obj).y;
            else if (obj.GetType() == typeof(Vector3Int))
                return q == ((Vector3Int)obj).x && r == ((Vector3Int)obj).z;
            else
                return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            //Assume that the Hex positions will never be above +/- 32,000 in either axis
            return (q << 16) | (r & 0xFFFF);
        }

        public override string ToString()
        {
            return string.Format("Hex({0}, {1})", q, r);
        }

        public Vector2Int ToTilemap()
        {
            var col = q + (r - (r & 1)) / 2;
            var row = r;
            return new Vector2Int(col, row);
        }

        public static Hex FromTilemap(Vector3Int cell)
        {
            var row = cell.y;
            var col = cell.x;
            var q = col - ((row - (row & 1)) / 2);
            var r = row;
            return new Hex(q, r);
        }
        #endregion

        #region Static
        #region CONSTS
        public const float SQRT3 = 1.7320508075688772935274463415058723669428052538103806280558069794f;
        public const float SQRT3DIV2 = 0.8660254037844386467637231707529361834714026269051903140279034897f;
        public const float SQRT3DIV3 = 0.5773502691896257645091487805019574556476017512701268760186023264f;

        public static Hex zero { get { return new Hex(0, 0); } }
        #endregion

        /// <summary>
        /// Creates a ring of hexagons a distance from the center, with the specified radius
        /// </summary>
        /// <param name="center"></param>
        /// <param name="radius"></param>
        /// <returns></returns>
        public static List<Hex> Ring(Hex center, int radius)
        {
            List<Hex> results = new List<Hex>();
            Ring_RefList(ref results, center, radius);
            return results;
        }

        /// <summary>
        /// Creates a filled area of hex space (shaped like a large hexagon)
        /// </summary>
        /// <param name="center"></param>
        /// <param name="radius"></param>
        /// <returns></returns>
        public static List<Hex> Area(Hex center, int radius)
        {
            List<Hex> results = new List<Hex>();
            for (int i = 0; i <= radius; i++)
            {
                Ring_RefList(ref results, center, i);
            }
            return results;
        }

        /// <summary>
        /// Calculates a ring of hexes and places them in the reference data structure
        /// </summary>
        /// <param name="results"></param>
        /// <param name="center"></param>
        /// <param name="radius"></param>
        private static void Ring_RefList(ref List<Hex> results, Hex center, int radius)
        {
            if (radius == 0)
            {
                results.Add(center);
                return;
            }
            Hex cube = center + (Hex.Offsets[4] * radius);
            for (int i = 0; i < 6; i++)
            {
                for (int r = 0; r < radius; r++)
                {
                    results.Add(cube);
                    cube += Hex.Offsets[i];
                }
            }
        }

        /// <summary>
        /// Calcualtes the distance between two Hex in cube space
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static int Distance(Hex a, Hex b)
        {
            return (Mathf.Abs(a.q - b.q) + Mathf.Abs(a.r - b.r) + Mathf.Abs(a.s - b.s)) / 2;
        }

        /// <summary>
        /// Rounds a floating point hex (in Hex space, NOT Unity Space) to the closest hex. Does not handle pixel perfect mapping
        /// </summary>
        /// <param name="q"></param>
        /// <param name="r"></param>
        /// <returns></returns>
        public static Hex Round(float q, float r)
        {
            float s = -q - r;
            int rx = Mathf.RoundToInt(q), ry = Mathf.RoundToInt(r), rz = Mathf.RoundToInt(s);
            float xDiff = Mathf.Abs((float)rx - q), yDiff = Mathf.Abs((float)ry - r), zDiff = Mathf.Abs((float)rz - s);

            if (xDiff > yDiff && xDiff > zDiff)
                rx = -ry - rz;
            else if (yDiff > zDiff)
                ry = -rx - rz;
            else
                _ = -rx - ry; //Case of RZ, left here for completeness

            return new Hex(rx, ry);
        }

        public static (Hex, Hex.Side) GetSideUID(Hex referenceHex, Hex.Side side)
        {
            switch (side)
            {
                case Hex.Side.Right:
                case Hex.Side.UpRight:
                case Hex.Side.UpLeft:
                default:
                    return (referenceHex, side);
                case Hex.Side.Left:
                case Hex.Side.DownLeft:
                case Hex.Side.DownRight:
                    return (referenceHex + side.Offset(), side.Inverse());
            }
        }

        public static bool InLine(Hex a, Hex b)
        {
            //They are in line of 
            Hex offset = a - b;
            return offset.q == 0 || offset.r == 0 || offset.s == 0;
        }

        public static bool GetRelativeSide(Hex center, Hex relative, out Hex.Side side, out bool adjacent)
        {
            side = Side.Right;
            adjacent = false;

            if (center.Equals(relative) || InLine(center, relative) == false)
            {
                if (InLine(center, relative) == false)
                    Debug.LogError($"Inline failure {center} -> {relative}");
                return false;
            }

            adjacent = Hex.Distance(center, relative) == 1;
            Hex normalizedRelative = center + ((relative - center) / Hex.Distance(center, relative));

            for (int i = 0; i < Offsets.Length; i++)
            {
                if (normalizedRelative.Equals(center + Offsets[i]))
                {
                    side = (Side)i;
                    return true;
                }
            }
            Debug.LogError("Special relative failure");
            return false;
        }

        public static bool RoundToSide(Hex center, Vector3 world, out Hex.Side side, Hex.Side? stickside = null, float angleTolderance = 60f)
        {
            Vector3 perspective = center.UnityPosition();
            Vector3 offset = world - perspective;
            float theta = Mathf.Atan2(offset.y, offset.x);
            theta *= Mathf.Rad2Deg;

            while (theta < 0.0f)
                theta += 360.0f;

            //Default set
            side = stickside ?? Hex.Side.Right;

            float r = Mathf.Sqrt((offset.x * offset.x) + (offset.y * offset.y));
            bool distanceFromCenter = (r < 1f / 8f);

            for (int i = 0; i <= 6; i++)
            {
                float baseAngle = i * 60f;
                if (Mathf.Abs(baseAngle - theta) < angleTolderance / 2f)
                {
                    if (i == 6)
                        side = Side.Right;
                    else
                        side = (Side)i;
                    return distanceFromCenter;
                }
            }

            return distanceFromCenter;

        }

        public static Hex Rotate(Hex about, Hex hex, bool clockwise = true)
        {
            Hex relative = hex - about;
            return about + (clockwise ? new Hex(-relative.s, -relative.q) : new Hex(-relative.r, -relative.s));
        }

        public static List<Hex> OrderedRing(Hex center, int dist, bool clockwise = true)
        {
            Side[] order = clockwise ?
                new Side[] { Side.DownRight, Side.DownLeft, Side.Left, Side.UpLeft, Side.UpRight, Side.Right } :
                new Side[] { Side.Left, Side.DownLeft, Side.DownRight, Side.Right, Side.UpRight, Side.UpLeft };
            List<Hex> ring = new List<Hex>();
            Hex ringPosition = center + Hex.Side.UpRight.Offset() * dist;
            ring.Add(ringPosition);
            for (int s = 0; s < order.Length; s++)
            {
                for (int k = 0; k < dist; k++)
                {
                    ringPosition += order[s].Offset();
                    if (ring.Contains(ringPosition) == false)
                        ring.Add(ringPosition);
                    //else
                    //    Debug.LogError("Duplicate ring add");
                }
            }

            return ring;
        }

        /// <summary>
        /// Returns the list of hexes traversed for a rotation
        /// </summary>
        /// <param name="relative"></param>
        /// <param name="clockwise"></param>
        /// <returns></returns>
        public static List<Hex> RotationSweep(Hex about, Hex hex, bool clockwise = true)
        {
            if (Hex.zero.Equals(hex - about))
                return new List<Hex>() { hex };

            List<Hex> result = new List<Hex>();

            Hex rotation = Rotate(about, hex, clockwise);

            List<Hex> ring = OrderedRing(about, Hex.Distance(about, hex), clockwise);
            int startPosition = -1;
            for (int s = 0; s < ring.Count; s++)
            {
                if (ring[s].Equals(hex))
                {
                    startPosition = s;
                    break;
                }
            }
            if (startPosition < 0)
            {
                Debug.LogError("Wrong ring");
                return new List<Hex>() { hex };
            }

            for (int i = 0; i < ring.Count; i++)
            {
                int index = startPosition + i;
                if (index >= ring.Count) index -= ring.Count;
                result.Add(ring[index]);
                if (ring[index].Equals(rotation))
                {
                    break;
                }
            }

            return result;
        }

        #endregion
    }
}