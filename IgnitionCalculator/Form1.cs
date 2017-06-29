using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace IgnitionCalculator
{
    public partial class Form1 : Form
    {
        private List<DataItem> DataSet = new List<DataItem>();
        private List<DataItem> ResultSet = new List<DataItem>();

        public Form1()
        {
            InitializeComponent();
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.Cancel)
                return;

            filenamestrip.Text = openFileDialog1.FileName;

            ReadFile(openFileDialog1.FileName);
            Calculate();
        }

        private void ReadFile(string filename)
        {
            using (StreamReader sr = new StreamReader(filename))
            {
                DataSet.Clear();

                sr.ReadLine(); // read header
                while (!sr.EndOfStream)
                {
                    string[] data = sr.ReadLine().Split(';');

                    DataItem item = new DataItem();

                    try
                    {
                        item.Rpm = double.Parse(data[3].Replace("\"", "").Replace(",", "."));
                        item.Angle = double.Parse(data[4].Replace("\"", "").Replace(",", "."));
                        item.Sensor = double.Parse(data[5].Replace("\"", "").Replace(",", "."));
                    }
                    catch
                    {
                        break;
                    }

                    DataSet.Add(item);
                }
            }
        }

        private void Calculate()
        {
            int[] layersconfig = new int[33];
            layersconfig[32] = 1;

            for(int i = 0; i < 32; i++)
            {
                layersconfig[i] = 12;
            }

            NeuralNetwork.NeuralNetwork net = new NeuralNetwork.NeuralNetwork(2, layersconfig);

            float[][] X = new float[DataSet.Count][];
            float[][] Y = new float[DataSet.Count][];
            
            // prepare data

        }
    }
}
