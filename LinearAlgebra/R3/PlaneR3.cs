namespace LinearAlgebra.R3
{
    public class PlaneR3
    {
        // NOTE: These constants DO NOT correspond to
        // the point constants in class Point. In
        // Point they are three points on the plane,
        // here R1 & R2 represent all sets of parallel 
        // planes, where R0 fixes a specific plane.
        // enumeration constants for vectors
        public const int R0 = 0;
        public const int R1 = 1;
        public const int R2 = 2;
        // enumeration constants for the EQ of a plane
        public const int A = 0;
        public const int B = 1;
        public const int C = 2;
        public const int D = 3;
        // private class members
        private PointR3 p0, p1, p2;
        // default constructor
        public PlaneR3()
        {
            p0 = new PointR3();
            p1 = new PointR3();
            p2 = new PointR3();
        }
        // point constructor
        public PlaneR3(PointR3 point0, PointR3 point1, PointR3 point2)
        {
            p0 = new PointR3(point0);
            p1 = new PointR3(point1);
            p2 = new PointR3(point2);
        }
        // vector / point constructor
        public PlaneR3(VectorR3 r1, VectorR3 r2, PointR3 p)
        {
            VectorR3 r0 = new VectorR3(p);
            p0 = p;
            p1 = r0.Add(r1).ToPoint();
            p2 = r0.Add(r2).ToPoint();
        }
        // get method for vector normal to the plane
        public VectorR3 GetNormal()
        {
            VectorR3 r1 = GetVector(R1);
            VectorR3 r2 = GetVector(R2);
            return r1.Cross(r2);
        }
        // get one of the three points that define the plane
        public PointR3 GetPoint(int point)
        {
            PointR3 toGet = new PointR3();
            switch (point)
            {
                case PointR3.P0:
                    toGet = p0;
                    break;
                case PointR3.P1:
                    toGet = p1;
                    break;
                case PointR3.P2:
                    toGet = p2;
                    break;
            }
            return toGet;
        }
        // set one of the three points that define the plane
        public void SetPoint(int point, PointR3 p)
        {
            switch (point)
            {
                case PointR3.P0:
                    p0 = p;
                    break;
                case PointR3.P1:
                    p1 = p;
                    break;
                case PointR3.P2:
                    p2 = p;
                    break;
            }
        }
        // get one of the three Vectors that define the plane
        public VectorR3 GetVector(int vector)
        {

            VectorR3 r = new VectorR3();
            switch (vector)
            {
                case R0:
                    r = new VectorR3(p0);
                    //System.out.println("r0 : " + r);
                    break;
                case R1:
                    r = new VectorR3(p0.Sub(p1));
                    //System.out.println("r1 : " + r);
                    break;
                case R2:
                    r = new VectorR3(p1.Sub(p2));
                    //System.out.println("r2 : " + r);
                    break;

            }
            return r;
        }
        // method to get the constants from the equation
        // f(x,y,z) = ax + by + bz + d, where
        // f(x,y,z) = 0 means (x,y,z) is on the plane.
        public double GetEQConstants(int constant)
        {
            double value = 0.0;
            switch (constant)
            {
                case A:
                    value = GetNormal().Get(CoordinatesR3.X);
                    break;
                case B:
                    value = GetNormal().Get(CoordinatesR3.Y);
                    break;
                case C:
                    value = GetNormal().Get(CoordinatesR3.Z);
                    break;
                case D:
                    value = CalculateConstantTermD();
                    break;
            }
            return value;
        }
        // function representing the function
        // f(x,y,z) = ax + by + cz + d
        public double F(PointR3 p)
        {
            double a, b, c, d, x, y, z;

            a = GetEQConstants(PlaneR3.A);
            b = GetEQConstants(PlaneR3.B);
            c = GetEQConstants(PlaneR3.C);
            d = GetEQConstants(PlaneR3.D);

            x = p.Get(CoordinatesR3.X);
            y = p.Get(CoordinatesR3.Y);
            z = p.Get(CoordinatesR3.Z);

            return a * x + b * y + c * z + d;
        }
        // private method to calculate constant d in f(x,y,z)
        private double CalculateConstantTermD()
        {
            VectorR3 r0 = new VectorR3(p0);
            return GetNormal().Neg().Dot(r0);
        }
    }

}
