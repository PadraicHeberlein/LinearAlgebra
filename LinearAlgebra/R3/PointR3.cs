using System;
using System.Collections.Generic;

namespace LinearAlgebra.R3
{
    public class PointR3
    {
        // public readonly common points
        public static readonly PointR3 ORIGIN = new PointR3(0.0, 0.0, 0.0);
        public static readonly PointR3 E1_POINT = new PointR3(1.0, 0.0, 0.0);
        public static readonly PointR3 E2_POINT = new PointR3(0.0, 1.0, 0.0);
        public static readonly PointR3 E3_POINT = new PointR3(0.0, 0.0, 1.0);
        // enumeration constants for each point that defines 
        // a plane or vector
        public const int P0 = 0;
        public const int P1 = 1;
        public const int P2 = 2;
        // private class members
        private double x, y, z;
        // default constructor
        public PointR3()
        {
            x = 0.0;
            y = 0.0;
            z = 0.0;
        }
        // coordinate constructor
        public PointR3(double xp, double yp, double zp)
        {
            x = xp;
            y = yp;
            z = zp;
        }
        // copy constructor
        public PointR3(PointR3 p)
        {
            x = p.x;
            y = p.y;
            z = p.z;
        }
        // vector constructor
        public PointR3(VectorR3 v)
        {
            x = v.Get(CoordinatesR3.X);
            y = v.Get(CoordinatesR3.Y);
            z = v.Get(CoordinatesR3.Z);
        }
        // method for down-casting to a vector
        public VectorR3 ToVector()
        {
            return new VectorR3(x, y, z);
        }
        // multiply point by -1
        public PointR3 Neg()
        {
            return new PointR3(-x, -y, -z);
        }
        // add two points
        public PointR3 Add(PointR3 p)
        {
            return new PointR3(x + p.x, y + p.y, z + p.z);
        }
        // subtract two points
        public PointR3 Sub(PointR3 p)
        {
            return Add(p.Neg());
        }
        // multiply point by a scalar
        public PointR3 Sx(double scalar)
        {
            return new PointR3(scalar * x, scalar * y, scalar * z);
        }
        // get method based on enumeration constants
        public double Get(int coordinate)
        {
            double toReturn = 0;
            switch (coordinate)
            {
                case CoordinatesR3.X:
                    toReturn = x;
                    break;
                case CoordinatesR3.Y:
                    toReturn = y;
                    break;
                case CoordinatesR3.Z:
                    toReturn = z;
                    break;
            }
            return toReturn;
        }
        // set method based on final constants for index
        public void Set(int coordinate, double coordinateValue)
        {
            switch (coordinate)
            {
                case CoordinatesR3.X:
                    x = coordinateValue;
                    break;
                case CoordinatesR3.Y:
                    y = coordinateValue;
                    break;
                case CoordinatesR3.Z:
                    z = coordinateValue;
                    break;
            }
        }
        // to check whether a point is on the given plane
        public bool IsOnPlane(PlaneR3 pl)
        {
            return Math.Abs(pl.F(this)) < Constants.ZERO;
        }
        // method to determine if the given point is on 
        // the same side of the given plane as this point
        public bool IsOnSameSideOfPlane(PlaneR3 pl, PointR3 p)
        {
            // f(x,y,x) = ax + by + cz + d == 0 when 
            // point (x,y,z) is on the plane, < 0 when 
            // on one side and > 0 when on the other.
            if (IsOnPlane(pl) || p.IsOnPlane(pl))
                return false;
            double thisSide = pl.F(this);
            double thatSide = pl.F(p);
            // when this side and that side have the same 
            // sign, i.e. they're on the same side, then
            // there product is always positive
            return thisSide * thatSide > 0;
        }
        // toString method for printing in tests
        public override String ToString()
        {
            return " (" + x + ", " + y + ", " + z + ") ";
        }

        public class PointR3EqualityComparer : IEqualityComparer<PointR3>
        {
            bool IEqualityComparer<PointR3>.Equals(PointR3 p1, PointR3 p2)
            {
                bool xDiff = Math.Abs(p1.x - p2.x) < Constants.ZERO;
                bool yDiff = Math.Abs(p1.y - p2.y) < Constants.ZERO;
                bool zDiff = Math.Abs(p1.z - p2.z) < Constants.ZERO;

                return xDiff && yDiff && zDiff;
            }

            int IEqualityComparer<PointR3>.GetHashCode(PointR3 obj)
            {
                throw new NotImplementedException();
            }
        }
    }
}
