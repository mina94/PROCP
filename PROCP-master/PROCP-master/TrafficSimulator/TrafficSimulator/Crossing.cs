using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrafficSimulator
{
    [Serializable]
    public class Crossing
    {
        public Image image { get; set; }
        public int Size { get; set; }
        public Point StartPoint { get; set; }

        public Point[] Neighbor; //may not needed 

        public List<Point> Entrance;

        public List<Lane> Lanes;
        public Crossing(Image image, int size)
        {
            this.image = image;
            this.Size = size;
            this.StartPoint = new Point(0, 0);
            Neighbor = new Point[4];

            Entrance = new List<Point>();
            Lanes = new List<Lane>();
            AddLanes();
            //this.image = (Image)new Bitmap(new Bitmap(this.Image_Filename.Split('.')[0] + ".png"), this.image.Size);
        }
        public Crossing(int x, int y)
        {

            Entrance = new List<Point>();
            Entrance.Add(new Point(x, y));
            Lanes = new List<Lane>();
        }
        public void AddLanes()
        {
            Lanes.Add(new Lane(70, 0, 1));
            Lanes[0].AddCars(5);
        }
        
    }
}
