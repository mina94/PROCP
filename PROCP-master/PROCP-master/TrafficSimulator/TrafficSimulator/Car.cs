using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace TrafficSimulator
{
    public class Car
    {
        /// <summary>
        /// stores the basic info needed for creat a car(point)    maybe not needed 
        /// </summary>
        //
        
        private static Random rng = new Random();
        private int x;
        private int y;
        private int dirX;
        private int dirY;
        public int size = 7;
        private SolidBrush brush;
        private Lane Path { set; get; }//The lane it is currently a part of
        public bool ReachedEnd { get; set; }
        private int MoveCounter { set; get; }
        private Color Color { set; get; }

        public Car(int X, int Y, int directionX, int directionY)
        {
            this.x = X;
            this.y = Y;
            this.dirX = directionX;
            this.dirY = directionY;
            brush = new SolidBrush(Color.Red);
        }

        public int getX()
        {
            return this.x;
        }

        public int setX(int posX)
        {
            return this.x = posX;
        }

        public int getY()
        {
            return this.y;
        }

        public int setY(int posY)
        {
            return this.y = posY;
        }

        public void DrawCar(Graphics gr)
        {
            gr.FillEllipse(brush, this.x, this.y, 7, 7);
        }

        /// <summary>
        /// Generates different colors for cars to be more easily visible 
        /// </summary>
        private void GenerateColor()
        {
            List<Color> colorList = new List<Color>();
            colorList.Add(Color.Brown);
            colorList.Add(Color.DeepPink);
            colorList.Add(Color.Orange);
            colorList.Add(Color.LightBlue);
            colorList.Add(Color.DarkBlue);
            colorList.Add(Color.DarkGray);
            colorList.Add(Color.Black);
            colorList.Add(Color.Olive);

            this.Color = colorList[rng.Next(0, colorList.Count)];
        }


    
    }
}
