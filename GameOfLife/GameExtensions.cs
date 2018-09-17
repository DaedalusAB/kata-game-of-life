using System.Collections.Generic;
using System.Linq;

namespace GameOfLife
{
    public static class GameExtensions
    {
        private static readonly (int x, int y)[] CoordinateDeltas =
        {
            (-1, -1), (0, -1), (1, -1), (-1, 0), (1, 0), (-1, 1), (0, 1), (1, 1)
        };

        public static IEnumerable<(int x, int y)> CoordinatesOfAllNeighbors(this Game game, (int x, int y) coordinate)
        {
            return CoordinateDeltas
                .Select(d => (coordinate.x + d.x, coordinate.y + d.y))
                .Where(c => c.Item1 >= 0 && c.Item1 < game.Size && c.Item2 >= 0 && c.Item2 < game.Size);
        }
    }
}
