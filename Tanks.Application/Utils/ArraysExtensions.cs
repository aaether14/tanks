namespace Tanks.Application.Utils;

public static class ArrayExtensions
{
    public static int[][] ToJaggedArray(this int[,] array)
    {
        int rows = array.GetLength(0);
        int columns = array.GetLength(1);
        int[][] jaggedArray = new int[rows][];

        for (int i = 0; i < rows; i++)
        {
            jaggedArray[i] = new int[columns];

            for (int j = 0; j < columns; j++)
            {
                jaggedArray[i][j] = array[i, j];
            }
        }

        return jaggedArray;
    }
}