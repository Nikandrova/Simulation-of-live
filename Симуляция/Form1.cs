using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Симуляция
{
    public partial class Form1 : Form

    {
        
        private Graphics graphics;
        private int resolution;
        private GameEngine gameEngine;
        // private bool[,] field;
        //    private int rows;
        //  private int cols;
        public Form1()
        {
            InitializeComponent();
        }
        private void StartGame()
        {
            if (timer1.Enabled)
                return;


            nudResolution.Enabled = false;
            nudDensity.Enabled = false;
            resolution = (int)nudResolution.Value;

            gameEngine = new GameEngine
                (
                    rows: pictureBox1.Height / resolution,
                    cols: pictureBox1.Width / resolution,
                   density: (int)(nudDensity.Minimum) + (int)nudDensity.Maximum - (int)nudDensity.Value
  
                );


            Text = $"Генерация{gameEngine.CurrentGeneration}";

            // field = new bool[cols, rows];

            // Random random = new Random();
            //  for (int x = 0; x < cols; x++)
            //  {
            //for (int y = 0; y < rows; y++)
              //  {
              //      field[x, y] = random.Next((int)nudDensity.Value) == 0;
               // }
          //  }
            pictureBox1.Image = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            graphics = Graphics.FromImage(pictureBox1.Image);
            timer1.Start();
            //graphics.FillRectangle(Brushes.DarkMagenta, 0, 0, resolution, resolution);
        }
        private void DrawNextGeneration()
        {
            graphics.Clear(Color.Black);

            var field = gameEngine.GetCurrentGenertion();

            for (int x = 0; x < field.GetLength(0); x++)
            {
                for (int y = 0; y < field.GetLength(1); y++)
                {
                    if (field[x, y])
                    {
                        graphics.FillRectangle(Brushes.DarkMagenta, x * resolution, y * resolution, resolution - 1, resolution - 1);
                    }
                }
            }
            pictureBox1.Refresh();
            Text = $"Генерация{gameEngine.CurrentGeneration}";
            gameEngine.NextGeneration();
        }
        private void splitContainer1_Panel2_Paint(object sender, PaintEventArgs e)
        {

        }
        private void splitContainer1_SplitterMoved(object sender, SplitterEventArgs e)
        {

        }
        private void label1_Click(object sender, EventArgs e)
        {

        }
        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {

        }
        private void label2_Click(object sender, EventArgs e)
        {

        }
        private void StopGame()
        {
            if (!timer1.Enabled)
                return;
            timer1.Stop();
            nudResolution.Enabled = true;
            nudDensity.Enabled = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            DrawNextGeneration();
        }

        private void bStart_Click(object sender, EventArgs e)
        {
            StartGame();
        }

        private void bStop_Click(object sender, EventArgs e)
        {
            StopGame();
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (!timer1.Enabled)
                return;
            if (e.Button == MouseButtons.Left)
            {
                var x = e.Location.X / resolution;
                var y = e.Location.Y / resolution;
                gameEngine.AddCell(x, y);

            }
            if (e.Button == MouseButtons.Right)
            {
                var x = e.Location.X / resolution;
                var y = e.Location.Y / resolution;
                gameEngine.RemoveCell(x, y);
            }

        }


        private void nudResolution_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}
