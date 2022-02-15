using System;

namespace LinearAlgebra.R3
{
    public class LineR3
    {
        private VectorR3 originVector, dirVector;

        public LineR3()
        {
            originVector = new VectorR3();
            dirVector = new VectorR3();
        }

        public LineR3(VectorR3 on, VectorR3 dir)
        {
            originVector = on;
            dirVector = dir;
        }

        public LineR3(PointR3 on, PointR3 dir)
        {
            originVector = new VectorR3(on);
            dirVector = new VectorR3(dir);
        }

        public LineR3(PointR3 on, VectorR3 dir)
        {
            originVector = new VectorR3(on);
            dirVector = dir;
        }

        public bool Intersects(LineR3 v)
        {
            VectorR3 bridge = new VectorR3(originVector.Sub(v.originVector));
            VectorR3 n1 = dirVector.Cross(bridge);
            VectorR3 n2 = v.dirVector.Cross(bridge);

            return n1.IsParallelTo(n2);
        }

        public bool Intersects(PlaneR3 pl)
        {
            return !dirVector.IsPerpendicularTo(pl.GetNormal());
        }

        public bool IsParallelTo(LineR3 od)
        {
            return dirVector.IsParallelTo(od.dirVector);
        }

        public bool IsPerpendicularTo(LineR3 od)
        {
            return Math.Abs(dirVector.Dot(od.dirVector)) < Constants.ZERO;
        }

        public PointR3 FindIntersectionWith(PlaneR3 pl)
        {
            if (!Intersects(pl))
                throw new Exception("Line does not intersect plane!");

            VectorR3 n = pl.GetNormal();
            double d = pl.GetEQConstants(PlaneR3.D);
            double dProduct1 = n.Dot(dirVector);
            double dProduct2 = n.Dot(originVector);
            double t = -(dProduct2 + d) / dProduct1;

            return new PointR3(originVector.Add(dirVector.Sx(t)));
        }

        public PointR3 FindIntersectionWith(LineR3 od)
        {
            if (!Intersects(od))
                throw new Exception("Lines do not intersect!");

            VectorR3 d = dirVector.Cross(od.dirVector);
            double dotProduct = d.Dot(d);
            VectorR3 o = od.originVector.Sub(originVector).Cross(od.dirVector);
            double t = o.Dot(d) / dotProduct;

            return new PointR3(originVector.Add(dirVector.Sx(t)));
        }

        public VectorR3 GetDirection()
        {
            return dirVector;
        }

        public PointR3 GetOrigin()
        {
            return originVector.ToPoint();
        }

        public void SetDirection(VectorR3 dir)
        {
            dirVector.Set(dir);
        }

        public void SetOnPoint(PointR3 onNow)
        {
            originVector.Set(onNow.ToVector());
        }

        public void SetOnPoint(VectorR3 onNow)
        {
            originVector.Set(onNow);
        }
    }
}
