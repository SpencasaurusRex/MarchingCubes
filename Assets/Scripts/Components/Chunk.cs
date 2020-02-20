namespace Components
{
    public class Chunk
    {
        public const int Bits = 4;
        public const int Size = 1 << Bits;
        public const int Volume = Size * Size * Size;

        public bool Loaded;
        public int[] Data;
        public ChunkCoord Coord;

        public Chunk(ChunkCoord coord)
        {
            Loaded = false;
            Data = new int[Volume];
            Coord = coord;
        }
    }
}
