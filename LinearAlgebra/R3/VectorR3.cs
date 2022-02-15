using System;
using System.Collections.Generic;

namespace LinearAlgebra.R3
{
    public class VectorR3 : PointR3
    {
        // public readonly vectors 
        public static readonly VectorR3 ZERO_VECTOR = new VectorR3(PointR3.ORIGIN);
        public static readonly VectorR3 E1 = new VectorR3(PointR3.E1_POINT);
        public static readonly VectorR3 E2 = new VectorR3(PointR3.E2_POINT);
        public static readonly VectorR3 E3 = new VectorR3(PointR3.E3_POINT);
        // private constants
        private const int X = CoordinatesR3.X;
        private const int Y = CoordinatesR3.Y;
        private const int Z = CoordinatesR3.Z;
        private const int N = CoordinatesR3.DIMENSIONS;
        // default constructor
        public VectorR3() : base() { }
        // coordinate constructor
        public VectorR3(double xp, double yp, double zp) : base(xp, yp, zp) { }
        // point constructor
        public VectorR3(PointR3 p) : base(p) { }
        // 2 point constructor
        public VectorR3(PointR3 head, PointR3 tail) : base(head.Sub(tail)) { }
        // copy constructor
        public VectorR3(VectorR3 v) : base(v.Get(X), v.Get(Y), v.Get(Z)) { }
        // method to see if two vectors are equal
        public bool Equals(VectorR3 v)
        {
            for (int component = 0; component < N; component++)
            {
                double diff = Math.Abs(Get(component) - v.Get(component));
                if (diff < Constants.ZERO)
                    return true;
            }
            return true;
        }
        // method to add two vectors
        public VectorR3 Add(VectorR3 v)
        {
            PointR3 vectorSum = new PointR3(this);
            return new VectorR3(vectorSum.Add(v.ToPoint()));
        }
        // method for multiplying vector by -1
        public new VectorR3 Neg()
        {
            PointR3 toNegate = new PointR3(this);
            return new VectorR3(toNegate.Neg());
        }
        // method to subtract two vectors
        public VectorR3 Sub(VectorR3 v)
        {
            PointR3 vectorDiff = new PointR3(this);
            return new VectorR3(vectorDiff.Sub(v.ToPoint()));
        }
        // vector dot product
        public double Dot(VectorR3 v)
        {
            return Get(X) * v.Get(X) + Get(Y) * v.Get(Y) + Get(Z) * v.Get(Z);
        }
        // multiply a vector by a scalar
        new public VectorR3 Sx(double s)
        {
            return new VectorR3(s * Get(X), s * Get(Y), s * Get(Z));
        }
        // vector cross product
        public VectorR3 Cross(VectorR3 v)
        {
            double iHat = Get(Y) * v.Get(Z) - Get(Z) * v.Get(Y);
            double jHat = Get(Z) * v.Get(X) - Get(X) * v.Get(Z);
            double kHat = Get(X) * v.Get(Y) - Get(Y) * v.Get(X);

            return new VectorR3(iHat, jHat, kHat);
        }
        // method for checking if vector is zero vector
        public bool IsZero()
        {
            bool xIsZero = Math.Abs(Get(X)) < Constants.ZERO;
            bool yIsZero = Math.Abs(Get(Y)) < Constants.ZERO;
            bool zIsZero = Math.Abs(Get(Z)) < Constants.ZERO;

            return (xIsZero && yIsZero && zIsZero);
        }
        // for checking if two vectors are parallel
        public bool IsParallelTo(VectorR3 v)
        {
            return Cross(v).IsZero();
        }
        // for checking if two vectors are perpendicular
        public bool IsPerpendicularTo(VectorR3 v)
        {
            return Math.Abs(Dot(v)) < Constants.ZERO;
        }
        // method to check if two parallel vectors are in the same direction\
        public bool IsInSameDirection(VectorR3 v)
        {
            if (!IsParallelTo(v))
                throw new Exception("Vectors aren't parallel!");

            VectorR3 thisUnit = v.Unit();
            VectorR3 thatUnit = v.Unit();

            return thisUnit.Equals(thatUnit);
        }
        // get method for magnitude
        public double Mag()
        {
            return Math.Sqrt(MagSquared());
        }
        // method for squaring the magnitude
        public double MagSquared()
        {
            return Dot(this);
        }
        // returns the normalized vector
        public VectorR3 Unit()
        {
            return Sx(1 / Mag());
        }
        // convert a vector to a point so avoid down-casting
        public PointR3 ToPoint()
        {
            return new PointR3(Get(X), Get(Y), Get(Z));
        }
        // public set method given a point
        public void Set(PointR3 p)
        {
            Set(X, p.Get(X));
            Set(Y, p.Get(Y));
            Set(Z, p.Get(Z));
        }
        // public set method given a point
        public void Set(VectorR3 v)
        {
            Set(X, v.Get(X));
            Set(Y, v.Get(Y));
            Set(Z, v.Get(Z));
        }
        // toString method for printing in tests
        public override String ToString()
        {
            return " <" + String.Format("%,.2f", Get(X)) + ", " + String.Format("%,.2f", Get(Y)) + ", " + String.Format("%,.2f", Get(Z)) + "> ";
        }

        public class VectorR3EqualityComparer : IEqualityComparer<VectorR3>
        {
            bool IEqualityComparer<VectorR3>.Equals(VectorR3 v1, VectorR3 v2)
            {
                return v1.Equals(v2);
            }

            int IEqualityComparer<VectorR3>.GetHashCode(VectorR3 obj)
            {
                throw new NotImplementedException();
            }
        }
    }
}
