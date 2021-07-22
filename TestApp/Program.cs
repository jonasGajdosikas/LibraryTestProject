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
            int width = 255;
            int height = 255;
            Tree grid = new Tree(width, height);
            grid.ActivateTiles(new List<Coord>{ grid.RandomCoord()});
            grid.Propagate();
            grid.Graphics(false).Save($"output.png");
        }
    }

    public class Tree : Grid
    {
        public List<Coord> ActiveCoords;
        public Tree(int _width, int _height) : base(_width, _height) { }

        public void ActivateTiles(List<Coord> seeds)
        {
            foreach (Coord seed in seeds) this[seed] = 1;
            ActiveCoords = seeds;
        }

        public void Propagate()
        {
            while (ActiveCoords.Count > 0){
                List<Coord> newCoords = new List<Coord>();
                foreach (Coord Active in ActiveCoords)
                {
                    foreach (Coord Neighbor in Active.Neighbors())
                    {
                        if (!this.InMap(Neighbor)) continue;
                        int count = 0;
                        foreach (Coord second in Neighbor.Neighbors()) 
                        {
                            if (!this.InMap(second)) continue;
                            if (this[second] != 0) count++; 
                        }
                           
                        if (count == 1) newCoords.Add(Neighbor);
                    }
                }
                ActivateTiles(newCoords);
            }
        }
    }
}
