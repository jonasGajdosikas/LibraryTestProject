using System;
using GridLibrary;
using coordLibrary;
using System.Drawing;
using System.Collections.Generic;

namespace TestApp
{
    class Program
    {
        static void Main()
        {
            int width = 256;
            int height = 256;
            Tree grid = new Tree(width, height);

            grid.Graphics(false).Save($"output.png");
        }
    }

    public class Tree : Grid
    {
        public List<Coord> ActiveCoords;
        public Tree(int _width, int _height) : base(_width, _height) { }

        public void PlaceSeeds(List<Coord> seeds)
        {
            foreach (Coord seed in seeds) this[seed] = 1;
            ActiveCoords.AddRange(seeds);
        }
    }
}
