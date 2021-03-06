﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TrafficSimulator
{
    [Serializable]
    public class WorkspaceDesign
    {
        private string savedFile;
        public string Grid { get; set; }
        public string Name { get; set; }
        public DateTime Time { get; set; }
        public List<Crossing> allcreatedcrossings;

        public WorkspaceDesign(string grid, string name, DateTime time)
        {
            this.Grid = grid;
            this.Name = name;
            this.Time = time;
            this.allcreatedcrossings = new List<Crossing>();
        }

        /// <summary>
        /// load a simulation deasign 
        /// </summary>
        /// <param name="steam"></param>
        public void Load(Stream steam)
        {
            IFormatter formatter = new BinaryFormatter();
            WorkspaceDesign deserialized = (WorkspaceDesign)formatter.Deserialize(steam);

            this.Name = deserialized.Name;
            this.Grid = deserialized.Grid;
            this.Time = deserialized.Time;
            this.allcreatedcrossings = deserialized.allcreatedcrossings;
        }

        /// <summary>
        /// save the design  
        /// </summary>
        public bool Save(Controller controler)
        {
            Stream saveStream = null;
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.FileName = savedFile;
            if ((saveStream = saveFileDialog.OpenFile()) != null)
            {
                IFormatter formater = new BinaryFormatter();
                formater.Serialize(saveStream, controler);
                saveStream.Close();
                return true;
            }
            else
                return false;
        }


        /// <summary>
        /// save as a design 
        /// </summary>
        /// <param name="stream"></param>
        public bool SaveAs(Controller controler)
        {
            SaveFileDialog dialog = new SaveFileDialog();

            dialog.FileName = "Simulation1";
            dialog.Filter = "SimulatorExtension files (*.simex)|*.simex";
            dialog.FilterIndex = 1;
            dialog.RestoreDirectory = true;

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                FileStream fs = null;
                BinaryFormatter bf = null;

                fs = new FileStream(dialog.FileName, FileMode.Create, FileAccess.Write);
                bf = new BinaryFormatter();
                this.savedFile = dialog.FileName;
                bf.Serialize(fs, controler);
                fs.Close();
                return true;

            }
            return false;

        }


        public bool CheckIfIsValidToSetUpSimulator() 
        {
            this.allcreatedcrossings.Sort();
            //bool check = true;
            if (allcreatedcrossings.Count == 1)
            {
                return true;
            }
            else
            {
                for (int i = 0; i < allcreatedcrossings.Count -1; i++)
                {
                    if (!(
                        ((allcreatedcrossings[i].StartPoint.Y - allcreatedcrossings[i].Size) == allcreatedcrossings[i + 1].StartPoint.Y)
                        &&
                        (allcreatedcrossings[i].StartPoint.X == allcreatedcrossings[i + 1].StartPoint.X)

                        ||
                        ((allcreatedcrossings[i].StartPoint.X + allcreatedcrossings[i].Size) == allcreatedcrossings[i + 1].StartPoint.X)
                        &&
                        (allcreatedcrossings[i].StartPoint.Y == allcreatedcrossings[i + 1].StartPoint.Y)

                        ||
                        ((allcreatedcrossings[i].StartPoint.X - allcreatedcrossings[i].Size) == allcreatedcrossings[i + 1].StartPoint.X)
                        &&
                        (allcreatedcrossings[i].StartPoint.Y == allcreatedcrossings[i + 1].StartPoint.Y)

                        ||
                        ((allcreatedcrossings[i].StartPoint.Y + allcreatedcrossings[i].Size) == allcreatedcrossings[i + 1].StartPoint.Y)
                        &&
                        (allcreatedcrossings[i].StartPoint.X == allcreatedcrossings[i + 1].StartPoint.X)
                      ))
                    {
                        return false;
                    }
                }
            }
            return true;
        }


        public void SetUpLanes(List<Crossing> All, out List<LaneType1> T1,out List<LaneType2> T2,out List<EmptyLane> Empty)
        {
            All.Sort();

            T1 = new List<LaneType1>();
            T2 = new List<LaneType2>();
            Empty = new List<EmptyLane>();

            foreach (var item in All)
            {
                if (item.CType == 1)
                {
                    T1.Add(new LaneType1(new Point(item.StartPoint.X + ((Int32)0.4*(item.Size)),item.StartPoint.Y), 
                        new Point(item.StartPoint.X + ((Int32)0.4*item.Size),item.StartPoint.Y + ((Int32)(1/3)*item.Size))));
                    T2.Add(new LaneType2(new Point(item.StartPoint.X + ((Int32)0.5 * (item.Size)), item.StartPoint.Y),
                        new Point(item.StartPoint.X + ((Int32)0.5 * item.Size), item.StartPoint.Y + ((Int32)(1 / 3) * item.Size))));
                    Empty.Add(new EmptyLane(new Point(item.StartPoint.X + ((Int32)0.6 * (item.Size)), item.StartPoint.Y + ((Int32)(1 / 3) * item.Size)),
                       new Point(item.StartPoint.X + ((Int32)0.6 * item.Size), item.StartPoint.Y)));

                    T1.Add(new LaneType1(new Point(item.StartPoint.X + ((Int32)0.4 * (item.Size)), item.StartPoint.Y + ((Int32)0.4 * item.Size)),
                        new Point(item.StartPoint.X + ((Int32)(2/3) * item.Size), item.StartPoint.Y + ((Int32)0.4 * item.Size))));
                    T2.Add(new LaneType2(new Point(item.StartPoint.X + item.Size , item.StartPoint.Y + ((Int32)0.5 * item.Size)),
                        new Point(item.StartPoint.X + ((Int32)(2/3) * item.Size), item.StartPoint.Y + ((Int32)0.5 * item.Size))));
                    Empty.Add(new EmptyLane(new Point(item.StartPoint.X + ((Int32)(2/3) * (item.Size)), item.StartPoint.Y + ((Int32)(0.6) * item.Size)),
                       new Point(item.StartPoint.X +item.Size, item.StartPoint.Y + ((Int32)0.6 * item.Size))));

                    T1.Add(new LaneType1(new Point(item.StartPoint.X + ((Int32)0.4 * (item.Size)), item.StartPoint.Y + item.Size),
                       new Point(item.StartPoint.X + ((Int32)0.6 * item.Size), item.StartPoint.Y + ((Int32)(2/3) * item.Size))));
                    T2.Add(new LaneType2(new Point(item.StartPoint.X + ((Int32)0.5 * item.Size), item.StartPoint.Y + item.Size),
                        new Point(item.StartPoint.X + ((Int32)(1/ 2) * item.Size), item.StartPoint.Y + ((Int32)(2/3) * item.Size))));
                    Empty.Add(new EmptyLane(new Point(item.StartPoint.X + ((Int32)0.4 * (item.Size)), item.StartPoint.Y + item.Size),
                       new Point(item.StartPoint.X + ((Int32)0.4* (item.Size)), item.StartPoint.Y + ((Int32)(2 / 3) * item.Size))));

                    T1.Add(new LaneType1(new Point(item.StartPoint.X, item.StartPoint.Y + ((Int32)0.6 * item.Size)),
                      new Point(item.StartPoint.X + ((Int32)(1/3) * item.Size), item.StartPoint.Y + ((Int32)0.6 * item.Size))));
                    T2.Add(new LaneType2(new Point(item.StartPoint.X , item.StartPoint.Y + ((Int32)0.5 * item.Size)),
                        new Point(item.StartPoint.X + ((Int32)(1 / 3) * item.Size), item.StartPoint.Y + ((Int32)0.5 * item.Size))));
                    Empty.Add(new EmptyLane(new Point(item.StartPoint.X + ((Int32)(1 / 3) * (item.Size)), item.StartPoint.Y + ((Int32)0.4 * item.Size)),
                       new Point(item.StartPoint.X, item.StartPoint.Y + ((Int32)0.4 * item.Size))));

                }
                else if(item.CType == 2)
                {

                }
            }
        }


    }
}
