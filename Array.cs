using System;

namespace Lab3
{
    public class Array
    {
        private int[,] _matrix;

        public Array(int n, int m)
        {
            _matrix = new int[n, m];
        }

        public Array(int n)
        {
            _matrix = new int[n, n];
        }

        public Array()
        {
            _matrix = new int[3, 3];
        }

        public void FillFromKeyboard(int[,] inputData)
        {
            int n = _matrix.GetLength(0);
            int m = _matrix.GetLength(1);

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    _matrix[i, j] = inputData[i, j];
                }
            }
        }

        public void FillBySideDiagonalCondition()
        {
            int n = _matrix.GetLength(0);
            var rand = new Random();

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (i + j < n - 1)
                    {
                        _matrix[i, j] = rand.Next(-65, 121);
                    }
                    else
                    {
                        double val = rand.NextDouble() * 
                        (10.75 - (-3.5)) + (-3.5);
                        _matrix[i, j] = (int)Math.Floor(val);
                    }
                }
            }
        }

        public void FillThirdArrayVariant4()
        {
            int n = _matrix.GetLength(0);

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    _matrix[i, j] = 0;
                }
            }

            int currentVal = 1;

            for (int d = 0; d < n; d++)
            {
                if (d % 2 == 0)
                {
                    int row = 0;
                    int col = n - 1 - d;

                    while (row <= d)
                    {
                        _matrix[row, col] = currentVal++;
                        row++;
                        col++;
                    }
                }
                else
                {
                    int row = d;
                    int col = n - 1;

                    while (row >= 0)
                    {
                        _matrix[row, col] = currentVal++;
                        row--;
                        col--;
                    }
                }
            }
        }

        public int GetBankWithMaxDebt()
        {
            int m = _matrix.GetLength(0);
            int maxBankIndex = 0;
            int maxSum = -1;

            for (int i = 0; i < m; i++)
            {
                int currentBankDebt = 0;

                for (int j = 0; j < m; j++)
                {
                    currentBankDebt += _matrix[i, j];
                }

                if (currentBankDebt > maxSum)
                {
                    maxSum = currentBankDebt;
                    maxBankIndex = i;
                }
            }

            return maxBankIndex;
        }

        public static Array operator *(int scalar, Array matrix)
        {
            int n = matrix._matrix.GetLength(0);
            int m = matrix._matrix.GetLength(1);
            Array result = new Array(n, m);

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    result._matrix[i, j] = scalar * 
                    matrix._matrix[i, j];
                }
            }
            return result;
        }

        public static Array operator *(Array a, Array b)
        {
            int n1 = a._matrix.GetLength(0);
            int m1 = a._matrix.GetLength(1);
            int n2 = b._matrix.GetLength(0);
            int m2 = b._matrix.GetLength(1);

            if (m1 != n2)
                throw new ArgumentException("Число столбцов " +
                "1-ой матрицы должно " +
                "быть равно числу строк 2-ой матрицы!");

            Array result = new Array(n1, m2);

            for (int i = 0; i < n1; i++)
            {
                for (int j = 0; j < m2; j++)
                {
                    int sum = 0;
                    for (int k = 0; k < m1; k++)
                    {
                        sum += a._matrix[i, k] * b._matrix[k, j];
                    }
                    result._matrix[i, j] = sum;
                }
            }
            return result;
        }

        public static Array operator -(Array a, Array b)
        {
            int n = a._matrix.GetLength(0);
            int m = a._matrix.GetLength(1);

            if (n != b._matrix.GetLength(0) || 
                m != b._matrix.GetLength(1))
                throw new ArgumentException("Размерности матриц " +
                "должны совпадать для вычитания!");

            Array result = new Array(n, m);

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    result._matrix[i, j] = a._matrix[i, j] - 
                        b._matrix[i, j];
                }
            }
            return result;
        }

        public Array Transpose()
        {
            int n = _matrix.GetLength(0);
            int m = _matrix.GetLength(1);
            Array result = new Array(m, n); 

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    result._matrix[j, i] = _matrix[i, j];
                }
            }
            return result;
        }

        public override string ToString()
        {
            int n = _matrix.GetLength(0);
            int m = _matrix.GetLength(1);

            string result = "";

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    result += $"{_matrix[i, j],6}";
                }
                result += "\n";
            }

            return result;
        }
    }
}
    