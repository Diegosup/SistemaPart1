using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SistemaParticulas1
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Windows.Forms;
        public partial class Form1 : Form
        {
            private Bitmap bmp;
            private Graphics g;
            private int count = 0;
            private int n = 30;//numero de pelotas
            private List<Pelota> Pelotas = new List<Pelota>();
            private Random random = new Random();

            public Form1()
            {
                InitializeComponent();
                bmp = new Bitmap(pictureBox1.Width, pictureBox1.Height);
                g = Graphics.FromImage(bmp);
                pictureBox1.Image = bmp;
                for (int i = 0; i < n; i++)
                {
                List<Color> colores = new List<Color>() { Color.Red, Color.Blue, Color.Green, Color.Yellow, Color.Orange };
                Color nuevoColor = colores[random.Next(0, 5)];
                int rn1 = random.Next(10, 40);
                int ballPosy = i * (rn1 * 40); // Ajustar la posición vertical de la pelota según su índice en la lista
                int ballPosx = random.Next(0, 700);
                double rebote = random.NextDouble() * 2; // Asignar un valor de rebote aleatorio
                int moveStepX = 0;
                int moveStepY = (random.Next(2, 10) * 1) + 1; // Asignar un movimiento hacia abajo aleatorio
                Pelota newPelota = new Pelota(new Size(rn1, rn1), new Point(ballPosx, ballPosy));
                newPelota.Rebote = rebote; // Asignar el valor de rebote a la nueva pelota
                newPelota.MoveStepX = moveStepX; // Asignar el movimiento en X a la nueva pelota
                newPelota.MoveStepY = moveStepY; // Asignar el movimiento en Y a la nueva pelota
                newPelota.life = true;
                newPelota.colorP = nuevoColor;
                Pelotas.Add(newPelota);
            }
            }

            private void GenerateBalls()
            {
                List<Color> colores = new List<Color>() { Color.Red, Color.Blue, Color.Green, Color.Yellow, Color.Orange };
                Color nuevoColor = colores[random.Next(0, 5)];
                int rn1 = random.Next(10, 40);
                int ballPosy = rn1* (rn1 * 40); // Ajustar la posición vertical de la pelota según su índice en la lista
                int ballPosx = random.Next(0, 700);
                double rebote = random.NextDouble() * 2; // Asignar un valor de rebote aleatorio
                                                         //int moveStepX = (random.Next(2) == 0 ? -1 : 1) * (int)(random.NextDouble() * 5) + 1; // Asignar un movimiento en X aleatorio
                int moveStepX = 0;
                int moveStepY = (random.Next(2, 10) * 1) + 1; // Asignar un movimiento hacia abajo aleatorio
                                                              //int moveStepY = (random.Next(2) == 0 ? -1 : 1) * (int)(random.NextDouble() * 5) + 1; // Asignar un movimiento en Y aleatorio
                Pelota newPelota = new Pelota(new Size(rn1, rn1), new Point(ballPosx, ballPosy));
                newPelota.Rebote = rebote; // Asignar el valor de rebote a la nueva pelota
                newPelota.MoveStepX = moveStepX; // Asignar el movimiento en X a la nueva pelota
                newPelota.MoveStepY = moveStepY; // Asignar el movimiento en Y a la nueva pelota
                newPelota.life = true;
                newPelota.colorP = nuevoColor;
                Pelotas.Add(newPelota);
            }
            

            private void MoveBalls()
            {
                foreach (Pelota pelota in Pelotas)
                {
                    int ballPosx = pelota.Location.X;
                    int ballPosy = pelota.Location.Y;

                    ballPosx += pelota.MoveStepX;
                    if (ballPosx < 0)
                    {
                        // La pelota ha alcanzado el extremo izquierdo, por lo que reaparece en la posición del extremo derecho
                        ballPosx = pictureBox1.ClientSize.Width - pelota.Size.Width;
                    }
                    else if (ballPosx + pelota.Size.Width > pictureBox1.ClientSize.Width)
                    {
                        // La pelota ha alcanzado el extremo derecho, por lo que reaparece en la posición del extremo izquierdo
                        ballPosx = 0;
                    }

                    ballPosy += pelota.MoveStepY;
                    if (ballPosy < 0)
                    {
                        // La pelota ha alcanzado el extremo superior, por lo que reaparece en la posición del extremo inferior
                        ballPosy = pictureBox1.ClientSize.Height - pelota.Size.Height;
                    }
                    else if (ballPosy + pelota.Size.Height > pictureBox1.ClientSize.Height)
                    {
                        // La pelota ha alcanzado el extremo inferior, por lo que reaparece en la posición del extremo superior
                        ballPosy = 0;
                    }

                    pelota.Location = new Point(ballPosx, ballPosy);
                }
            }



            private void Form1_Load(object sender, EventArgs e)
            {

            }

        private void timer1_Tick_1(object sender, EventArgs e)
        {
            count++;
            int limit = random.Next(60, 80);
            g.Clear(Color.Black);
            //
            for (int i = 0; i < Pelotas.Count; i++)
            {
                Pelota pelota = Pelotas[i];
                if (count > limit)
                {
                    pelota.life = false;
                    for (int j = 0; j < Pelotas.Count; j++)
                    {

                        if (!pelota.life)
                        {
                            Pelotas.Remove(pelota);
                        }
                    }
                    GenerateBalls();
                    count = 0;
                }
                if (pelota.life)
                {
                    g.FillEllipse(new SolidBrush(pelota.colorP), pelota.Location.X, pelota.Location.Y, pelota.Size.Width, pelota.Size.Height);
                    g.DrawEllipse(Pens.Black, pelota.Location.X, pelota.Location.Y, pelota.Size.Width, pelota.Size.Height);
                    pelota.Update();
                }
            }
            
            MoveBalls();
            pictureBox1.Invalidate();
        }
    }
}
