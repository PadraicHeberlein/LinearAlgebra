using System;

namespace LinearAlgebra.R3
{
    public class MatrixR3
    {
        // constants and common matrices
        public const int n = CoordinatesR3.DIMENSIONS;
        public static MatrixR3 ZERO = new MatrixR3();
        public static MatrixR3 I = new MatrixR3(VectorR3.E1, VectorR3.E2, VectorR3.E3, false);
        // private members
        private double[,] e;
        // default constructor
        public MatrixR3()
        {
            e = new double[n, n];
        }
        // row / column constructor
        public MatrixR3(VectorR3 v0, VectorR3 v1, VectorR3 v2, bool givenRows)
        {
            e = new double[n, n];

            if (givenRows)
            {
                for (int col = 0; col < n; col++)
                {
                    e[0, col] = v0.Get(col);
                    e[1, col] = v1.Get(col);
                    e[2, col] = v2.Get(col);
                }
            }
            else
            {
                for (int row = 0; row < n; row++)
                {
                    e[row, 0] = v0.Get(row);
                    e[row, 1] = v1.Get(row);
                    e[row, 2] = v2.Get(row);
                }
            }
        }
        // copy constructor
        public MatrixR3(MatrixR3 m)
        {
            e = new double[n, n];
            e = m.e;
        }
        // method to check if two matrices are equal
        public bool Equals(MatrixR3 m)
        {
            for (int row = 0; row < n; row++)
            {
                for (int col = 0; col < n; col++)
                {
                    double diff = Math.Abs(e[row, col] - m.e[row, col]);
                    if (diff > Constants.ZERO)
                        return false;
                }
            }
            return true;
        }
        // method to add two matrices
        public MatrixR3 Add(MatrixR3 m)
        {
            MatrixR3 sum = new MatrixR3();

            for (int row = 0; row < n; row++)
            {
                for (int col = 0; col < n; col++)
                    sum.e[row, col] = e[row, col] + m.e[row, col];
            }

            return sum;
        }
        // method to subtract two matrices
        public MatrixR3 Sub(MatrixR3 m)
        {
            return new MatrixR3(Add(m.Sx(-1)));
        }
        // method to multiply two matrices
        public MatrixR3 Xm(MatrixR3 m)
        {
            MatrixR3 product = new MatrixR3();
            VectorR3[] rows = GetRows();
            VectorR3[] otherCols = m.GetCols();

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                    product.e[i, j] = rows[i].Dot(otherCols[j]);
            }
            return product;
        }
        // method to multiply a vector by a matrix
        public VectorR3 Xv(VectorR3 v)
        {
            VectorR3 product = new VectorR3();
            VectorR3[] rows = GetRows();

            for (int i = 0; i < n; i++)
                product.Set(i, rows[i].Dot(v));

            return product;
        }
        // multiply a scalar by a matrix
        public MatrixR3 Sx(double s)
        {
            MatrixR3 product = new MatrixR3();
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                    product.e[i, j] = s * e[i, j];
            }
            return product;
        }
        // method for calculating the determinant
        public double Det()
        {
            double first = e[0, 0] * (e[1, 1] * e[2, 2] - e[1, 2] * e[2, 1]);
            double second = e[0, 1] * (e[1, 0] * e[2, 2] - e[1, 2] * e[2, 0]);
            double third = e[0, 2] * (e[1, 0] * e[2, 1] - e[1, 1] * e[2, 0]);

            return first - second + third;
        }
        // method for calculating the inverse
        public MatrixR3 Inv()
        {
            MatrixR3 theInverse = new MatrixR3();
            double det = Det();

            theInverse.e[0, 0] = e[1, 1] * e[2, 2] - e[1, 2] * e[2, 1];
            theInverse.e[0, 1] = -(e[1, 0] * e[2, 2] - e[1, 2] * e[2, 0]);
            theInverse.e[0, 2] = e[1, 0] * e[2, 1] - e[1, 1] * e[2, 0];

            theInverse.e[1, 0] = -(e[0, 1] * e[2, 2] - e[0, 2] * e[2, 1]);
            theInverse.e[1, 1] = e[0, 0] * e[2, 2] - e[0, 2] * e[2, 0];
            theInverse.e[1, 2] = -(e[0, 0] * e[2, 1] - e[0, 1] * e[2, 0]);

            theInverse.e[2, 0] = e[0, 1] * e[1, 2] - e[0, 2] * e[1, 1];
            theInverse.e[2, 1] = -(e[0, 0] * e[1, 2] - e[0, 2] * e[1, 0]);
            theInverse.e[2, 2] = e[0, 0] * e[1, 1] - e[0, 1] * e[1, 0];

            theInverse = theInverse.Sx(1 / det);
            return theInverse.T();
        }
        // method for calculating the transpose
        public MatrixR3 T()
        {
            VectorR3[] rows = GetRows();
            return new MatrixR3(rows[0], rows[1], rows[2], false);
        }
        // get method for getting a single element
        public double Get(int row, int col)
        {
            return e[row, col];
        }
        // get method for getting a row
        public VectorR3 GetRow(int row)
        {
            VectorR3 theRow = new VectorR3();
            for (int col = 0; col < n; col++)
                theRow.Set(col, e[row, col]);
            return theRow;
        }
        // get method for all the rows in th matrix
        public VectorR3[] GetRows()
        {
            VectorR3[] rows = new VectorR3[n];
            for (int row = 0; row < n; row++)
            {
                VectorR3 theRow = GetRow(row);
                rows[row] = theRow;
            }
            return rows;
        }
        // get method for getting a column
        public VectorR3 GetCol(int col)
        {
            VectorR3 theCol = new VectorR3();
            for (int row = 0; row < n; row++)
                theCol.Set(row, e[row, col]);
            return theCol;
        }
        // get method for all the columns in the matrix
        public VectorR3[] GetCols()
        {
            VectorR3[] cols = new VectorR3[n];
            for (int col = 0; col < n; col++)
            {
                VectorR3 theCol = GetCol(col);
                cols[col] = theCol;
            }
            return cols;
        }
        // set method for a single element value
        public void Set(double value, int row, int col)
        {
            e[row, col] = value;
        }
        // set method for a single row
        public void SetRow(int rowNum, VectorR3 theRow)
        {
            e[rowNum, 0] = theRow.Get(0);
            e[rowNum, 1] = theRow.Get(1);
            e[rowNum, 2] = theRow.Get(3);
        }
        // set method for a single column
        public void SetCol(int colNum, VectorR3 theCol)
        {
            e[0, colNum] = theCol.Get(0);
            e[1, colNum] = theCol.Get(1);
            e[2, colNum] = theCol.Get(2);
        }
        // static methods that returns a rotation 
        // matrix around each axis by phi radians
        public static MatrixR3 Rx(double phi)
        {
            MatrixR3 rx = new MatrixR3();

            rx.SetCol(0, new VectorR3(1, 0, 0));
            rx.SetCol(1, new VectorR3(0, Math.Cos(phi), Math.Sin(phi)));
            rx.SetCol(2, new VectorR3(0, -1 * Math.Sin(phi), Math.Cos(phi)));

            return rx;
        }
        public static MatrixR3 Ry(double phi)
        {
            MatrixR3 rx = new MatrixR3();

            rx.SetCol(0, new VectorR3(1, 0, 0));
            rx.SetCol(1, new VectorR3(0, Math.Cos(phi), Math.Sin(phi)));
            rx.SetCol(2, new VectorR3(0, -1 * Math.Sin(phi), Math.Cos(phi)));

            return rx;
        }
        public static MatrixR3 Rz(double phi)
        {
            MatrixR3 ry = new MatrixR3();

            ry.SetCol(0, new VectorR3(Math.Cos(phi), 0, -1 * Math.Sin(phi)));
            ry.SetCol(1, new VectorR3(0, 1, 0));
            ry.SetCol(2, new VectorR3(Math.Sin(phi), 0, Math.Cos(phi)));

            return ry;
        }
        public override String ToString()
        {
            return " | " + String.Format("%,.2f", e[0, 0]) + "  " + String.Format("%,.2f", e[0, 1]) + "  " + String.Format("%,.2f", e[0, 2]) + " |\n" +
                   " | " + String.Format("%,.2f", e[1, 0]) + "  " + String.Format("%,.2f", e[1, 1]) + "  " + String.Format("%,.2f", e[1, 2]) + " |\n" +
                   " | " + String.Format("%,.2f", e[2, 0]) + "  " + String.Format("%,.2f", e[2, 1]) + "  " + String.Format("%,.2f", e[2, 2]) + " |\n";
        }
    }
}
