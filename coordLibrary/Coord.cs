using System;

namespace coordLibrary
{
    
    public class Coord
    {
        public int X;
        public int Y;
        public Coord(int _x, int _y)
        {
            X = _x;
            Y = _y;
        }
        public Coord(float radius, float angle)
        {
            X = (int)Math.Floor(radius * Math.Cos(angle));
            Y = (int)Math.Floor(radius * Math.Sin(angle));
        }
        public Coord()
        {
            X = 0;
            Y = 0;
        }
        public Coord[] Neighbors()
        {
            return new Coord[]
            {
                new Coord(X - 1, Y),
                new Coord(X, Y - 1),
                new Coord(X + 1, Y),
                new Coord(X, Y + 1)
            };
        }
        public static int DotProduct(Coord first, Coord second)
        {
            return first.Dot(second);
        }
        public static int CrossProduct(Coord first, Coord second)
        {
            return first.Cross(second);
        }
        public int Distance(Coord other)
        {
            return Math.Abs(X - other.X) + Math.Abs(Y - other.Y);
        }
        public float Dist(Coord other, int pow = 2, int size = 0)
        {
            Coord diff = this - other;
            if (size == 0) return (float)(Math.Pow(Math.Abs(diff.X), pow) + Math.Pow(Math.Abs(diff.Y), pow));
            float dist = size * 2;
            for (int x = -size; x <= size; x += size)
            {
                for (int y = -size; y <= size; y += size)
                {
                    dist = Math.Min(dist, (float)(Math.Pow(Math.Abs(diff.X - x), pow) + Math.Pow(Math.Abs(diff.Y - y), pow)));
                }
            }
            return dist;
        }
        public float Dist(int x, int y, int pow = 2, int size = 0)
        {
            return Dist(new Coord(x, y), pow, size);
        }
        public Coord Interpolate(Coord other, int percentage)
        {
            return (percentage * this + (100 - percentage) * other) / 100;
        }
        public static Coord Lerp(Coord first, Coord second, int percentage)
        {
            return first.Interpolate(second, percentage);
        }
        public static Coord operator +(Coord left, Coord right)
        {
            return new Coord
            {
                X = left.X + right.X,
                Y = left.Y + right.Y
            };
        }
        public static Coord operator -(Coord left, Coord right)
        {
            return new Coord
            {
                X = left.X - right.X,
                Y = left.Y - right.Y
            };
        }
        public static Coord operator *(Coord left, int right)
        {
            return right * left;
        }
        public static Coord operator *(int left, Coord right)
        {
            return new Coord
            {
                X = left * right.X,
                Y = left * right.Y
            };
        }
        public static Coord operator /(Coord left, int right)
        {
            return new Coord
            {
                X = left.X / right,
                Y = left.Y / right
            };
        }
        public static bool operator >(Coord left, Coord right)
        {
            return (left.X > right.X && left.Y > right.Y);
        }
        public static bool operator <(Coord left, Coord right)
        {
            return (left.X < right.X && left.Y < right.Y);
        }
        public static bool operator >=(Coord left, Coord right)
        {
            return (left.X >= right.X && left.Y >= right.Y);
        }
        public static bool operator <=(Coord left, Coord right)
        {
            return (left.X <= right.X && left.Y <= right.Y);
        }
        /*
         * Code for equality based on stored values from
         * https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/statements-expressions-operators/how-to-define-value-equality-for-a-type
         * Class example. The code there was adapted only with a few changes in variable name
         * 
         */
        public override bool Equals(object obj) => this.Equals(obj as Coord);
        public bool Equals(Coord p)
        {
            if (p is null)
            {
                return false;
            }

            // Optimization for a common success case.
            if (Object.ReferenceEquals(this, p))
            {
                return true;
            }

            // If run-time types are not exactly the same, return false.
            if (this.GetType() != p.GetType())
            {
                return false;
            }

            // Return true if the fields match.
            // Note that the base class is not invoked because it is
            // System.Object, which defines Equals as reference equality.
            return (X == p.X) && (Y == p.Y);
        }
        public override int GetHashCode() => (X, Y).GetHashCode();
        public static bool operator ==(Coord left, Coord right)
        {
            if (left is null)
            {
                if (right is null)
                {
                    return true;
                }

                // Only the left side is null.
                return false;
            }
            // Equals handles case of null on right side.
            return left.Equals(right);
        }
        public static bool operator !=(Coord left, Coord right) => !(left == right);
        
        public int Dot(Coord other)
        {
            return this.X * other.X + this.Y * other.Y;
        }
        public int Cross(Coord other)
        {
            return this.X * other.Y - other.X * this.Y;
        }
    }
}
