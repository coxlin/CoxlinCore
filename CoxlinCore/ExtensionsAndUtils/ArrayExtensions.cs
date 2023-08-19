/**********************************************************                                                                                
CoxlinCore - Copyright (c) 2023 Lindsay Cox / MIT License                                                                                                         
**********************************************************/
using System;

namespace CoxlinCore
{
    public static class ArrayExtensions
    {
        private static readonly Random _random = new Random();
        
        public static T PickRandom<T>(this T[] array)
        {
            if (array == null)
            {
                throw new ArgumentNullException(nameof(array));
            }

            switch (array.Length)
            {
                case 0:
                    throw new InvalidOperationException("Array is empty.");
                case 1:
                    return array[0];
                default:
                {
                    int randomIndex = _random.Next(0, array.Length);
                    return array[randomIndex];
                }
            }
        }
        
        public static T Find<T>(this T[] array, Predicate<T> predicate)
        {
            return Array.Find(array, predicate);
        }
        
        public static T First<T>(this T[] array)
        {
            return array[0];
        }

        public static T Last<T>(this T[] array)
        {
            return array[^1];
        }
        
        public static bool IndexIsInRange<T>(this T[] array, int index)
        {
            return index >= 0 && index < array.Length;
        }
        
        /// <summary>
        /// This is alright for performance,there is a faster way though using unsafe code
        /// and pointers. However, for best compatibility and use cases, this does it the old
        /// fashioned way
        /// </summary>
        /// <param name="array"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T[] Flatten<T>(this T[,] array)
        {
            var rows = array.GetLength(0);
            var columns = array.GetLength(1);

            var result = new T[rows * columns];

            int index = 0;

            for (int i = 0; i < rows; ++i)
            {
                for (int j = 0; j < columns; ++j)
                {
                    result[index++] = array[i, j];
                }
            }

            return result;
        }
        
        public static T[,] To2D<T>(this T[] flatArray, int width)
        {
            int numRows = (flatArray.Length + width - 1) / width;
            var result = new T[numRows, width];
            int rowIndex, colIndex;

            for (int i = 0; i < flatArray.Length; ++i)
            {
                rowIndex = i / width;
                colIndex = i % width;
                result[rowIndex, colIndex] = flatArray[i];
            }

            return result;
        }
    }
}