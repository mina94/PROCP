using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.ComponentModel;
using System.Threading;


namespace TrafficSimulator
{
    public enum LightColor{red,green}
  public class Light
    {
      public event EventHandler LightChanged;

      //Variables
      private bool lRed;                              // indicator for red light
      private bool lYellow;                           // indicator for yellow light
      private bool lGreen;                            // indicator for green light
      private Point Position;                         // X and Y

      
        /// <summary>
        /// Creates traffic light with Red light on, at specified location
        /// with default Green light time of 30 seconds.
        /// </summary>
        /// <param name="x">x coordinate of the traffic light</param>
        /// <param name="y">y coordinate of the traffic light</param>
        public Light(int x, int y)
        {
            // Check if  coordinates are within limit
            if (x < 0 || y < 0)
            { throw new ArgumentOutOfRangeException(); }
            else
            {
                Position = new Point(x, y);
                lRed = true;
                lGreen = false;
            }
        }

      /// <summary>
      /// Tells what light color is on
      /// </summary>
      /// <returns></returns>
        public int WhatIsOn()
        {
            
            int returnLight = -1;

            if (lGreen)
                returnLight = 0;
            else if ((lRed)&&(!lGreen))
                returnLight = 1;

          return  returnLight;
        }

        public Boolean IsGreen()
        {
            
            return lGreen;
        }

       
        public Point GetPosition()
        {
            return this.Position;
        }


        private void ChangeToGreen()
        {
            while (this.WhatIsOn() != -1)
	{
            //Light is Red
            if (this.WhatIsOn() == 1)
            {
                for (int i = 0; i < 1; i++)
                    Thread.Sleep(1000);
                //Turn off red 
                lRed = false;
                //Green Light is on
                lGreen = true;
                OnLightChanged(EventArgs.Empty);   
            }
        }
       
        }

       
        private void ChangeToRed()
        {
             while (this.WhatIsOn() != -1)
	{
            //Light is Green
            if (this.WhatIsOn() == 0)
            {
                //Forbid traffic
                lGreen = false;
                OnLightChanged(EventArgs.Empty);        //<<<<<<<<<<<<<<Event Fired
                for (int i = 0; i < 1; i++)
                    Thread.Sleep(1000);
                //Red light is on
                lRed = true;
            }
        }
        }

        public void ChangeLight()
        {
            if (this.IsGreen())
                this.ChangeToRed();
            else
                this.ChangeToGreen();
        }

        public void Draw(Graphics gr)
        {
            SolidBrush brushGreen = new SolidBrush(Color.LightGreen);
            SolidBrush brushRed = new SolidBrush(Color.Red);
            SolidBrush brushGray = new SolidBrush(Color.Gray);
            SolidBrush brushBlack = new SolidBrush(Color.Black);

            int status = this.WhatIsOn();

            switch (status)
            {
                case 0:
                    gr.FillRectangle(brushBlack, this.Position.X-1, this.Position.Y-1, 10, 24);
                    gr.FillEllipse(brushRed, this.Position.X, this.Position.Y, 7, 7);
                    gr.FillEllipse(brushGray, this.Position.X, this.Position.Y + 7, 7, 7);
                    gr.FillEllipse(brushGray, this.Position.X, this.Position.Y + 14, 7, 7);
                    break;
                case 1:
                    gr.FillRectangle(brushBlack, this.Position.X-1, this.Position.Y-1, 10, 24);
                    gr.FillEllipse(brushRed, this.Position.X, this.Position.Y, 7, 7);
            //      gr.FillEllipse(brushYellow, this.Position.X, this.Position.Y + 7, 7, 7);
                    gr.FillEllipse(brushGray, this.Position.X, this.Position.Y + 14, 7, 7);
                    break;
                case 2:
                    gr.FillRectangle(brushBlack, this.Position.X-1, this.Position.Y-1, 10, 24);
                    gr.FillEllipse(brushGray, this.Position.X, this.Position.Y, 7, 7);
           //       gr.FillEllipse(brushYellow, this.Position.X, this.Position.Y + 7, 7, 7);
                    gr.FillEllipse(brushGray, this.Position.X, this.Position.Y + 14, 7, 7);
                    break;
                case 3:
                    gr.FillRectangle(brushBlack, this.Position.X-1, this.Position.Y-1, 10, 24);
                    gr.FillEllipse(brushGray, this.Position.X, this.Position.Y, 7, 7);
                    gr.FillEllipse(brushGray, this.Position.X, this.Position.Y + 7, 7, 7);
                    gr.FillEllipse(brushGreen, this.Position.X, this.Position.Y + 14, 7, 7);
                    break;
                default:
                    gr.FillRectangle(brushBlack, this.Position.X-1, this.Position.Y-1, 10, 24);
                    gr.FillEllipse(brushGray, this.Position.X, this.Position.Y, 7, 7);
                    gr.FillEllipse(brushGray, this.Position.X, this.Position.Y + 7, 7, 7);
                    gr.FillEllipse(brushGray, this.Position.X, this.Position.Y + 14, 7, 7);
                    break;

            }
        }

        protected virtual void OnLightChanged(EventArgs e)
        {
            EventHandler handler = LightChanged;
            if (handler != null)
            {
                handler(this, e);
            }
        }
    }
}



     /*   public int Group { get; set; }
        public LightColor Color { get; set; }

        public Light(int group)
        {
            this.Group = group;
            this.Color = LightColor.red;
        }
      */
      
    

