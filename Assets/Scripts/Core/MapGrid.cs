using System;

namespace Core
{
    public class MapGrid<T> where T : class
    {
        public const int Width = 100;
        public const int Height = 100;
        public const int Capacity =  Width * Height;
        
        private readonly T[] _items = new T[Capacity];

        public T this[int x, int y]
        {
            get
            {
                if (x >= Width || y >= Height || x < 0 || y < 0)
                {
                    return null;
                }
                return _items[x + y * Width];
            }
            set
            {
                if (x >= Width || y >= Height || x < 0 || y < 0)
                {
                    throw new ArgumentOutOfRangeException();
                }
                _items[x + y * Width] = value;
            }
        }

        public void Clear()
        {
            for (var i = 0; i < _items.Length; ++i)
            {
                _items[i] = null;
            }
        }

        public bool InBounds(int x, int y)
        {
            return x < Width && y < Height && x >= 0 && y >= 0;
        }
    }
}