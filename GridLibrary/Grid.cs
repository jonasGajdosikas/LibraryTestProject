using System;
using coordLibrary;
using System.Drawing;
using System.Drawing.Imaging;
using System.Collections.Generic;

namespace GridLibrary
{
    public class Grid
    {
        public int[,] tiles;
        public int width;
        public int height;
        public Random rand;
        
        public Grid(int _width, int _height)
        {
            width = _width;
            height = _height;
            tiles = new int[width, height];
            rand = new Random();
        }
        public int this[int x, int y]
        {
            get { return tiles[x, y]; }
            set { tiles[x, y] = value; }
        }
        public int this[Coord point]
        {
            get { return tiles[point.X, point.Y]; }
            set { tiles[point.X, point.Y] = value; }
        }
        public bool InMap(Coord coord)
        {
            return (coord >= new Coord(0, 0) && coord < new Coord(width, height));
        }
        public bool InMap(int X, int Y)
        {
            return (X >= 0 && X < width && Y >= 0 && Y < height);
        }
        public Bitmap Graphics(bool bordered)
        {
            Bitmap result = new Bitmap(width + (bordered ? 2 : 0), height + (bordered ? 2 : 0));
            int[] bitmapData = new int[result.Height * result.Width];
            Dictionary<int, Color> colorDict = new Dictionary<int, Color>
            {
                { 0, Color.White },
                { 1, Color.Black }
            };
            for (int x = 0; x < result.Width; x++)
            {
                for (int y = 0; y < result.Height; y++)
                {
                    Color c;
                    if (bordered)
                    {
                        if (x == 0 || y == 0 || x == result.Width - 1 || y == result.Height - 1)
                        {
                            c = Color.Black;
                        }
                        else
                        {
                            int val = this[x - 1, y - 1];
                            if (!colorDict.ContainsKey(val))
                            {
                                int R = rand.Next(256);
                                int G = rand.Next(256);
                                int B = rand.Next(256);
                                colorDict.Add(val, Color.FromArgb(R, G, B));
                            }
                            c = colorDict[val];
                        }

                    }
                    else
                    {
                        int val = this[x, y];
                        if (!colorDict.ContainsKey(val))
                        {
                            int R = rand.Next(256);
                            int G = rand.Next(256);
                            int B = rand.Next(256);
                            colorDict.Add(val, Color.FromArgb(R, G, B));
                        }
                        c = colorDict[val];
                    }
                    result.SetPixel(x, y, c);
                    bitmapData[x + y * result.Width] = unchecked((int)0xff000000 | (c.R << 16) | (c.G << 8) | c.B);
                }
            }

            var bits = result.LockBits(new Rectangle(0, 0, result.Width, result.Height), ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);
            System.Runtime.InteropServices.Marshal.Copy(bitmapData, 0, bits.Scan0, bitmapData.Length);
            result.UnlockBits(bits);
            return result;
        }
    }
}
