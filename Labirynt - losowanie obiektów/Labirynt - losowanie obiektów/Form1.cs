using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;


namespace Labirynt___losowanie_obiektów
{
	public partial class Form1 : Form
	{

		int[,] labiryntTab;

		public Form1()
		{
			InitializeComponent();
		}

		private void groupBox4_Paint(object sender, PaintEventArgs e)
		{
			
			if (labiryntTab != null)
			{
				int rectSize = 11, spacing = 10;
				SolidBrush brush = new SolidBrush(Color.Red);
				Pen pen = new Pen(Color.DarkBlue);
				rectSize = (groupBox4.Size.Width-40) / labiryntTab.GetLength(0);
				spacing = rectSize-1;
				

				for (int i = 0; i < labiryntTab.GetLength(1); i++)
				{
					for (int j = 0; j < labiryntTab.GetLength(0); j++)
					{
						Rectangle kwadrat = new Rectangle(j * rectSize + 20, i * rectSize + 20, spacing - 1, spacing - 1);
						e.Graphics.DrawRectangle(pen, kwadrat);
						if (labiryntTab[j, i] == 10)
						{
							Rectangle kolo = new Rectangle(j * rectSize + 20 + rectSize/10, i * rectSize + 20 + rectSize/10, spacing-rectSize/5, spacing-rectSize/5);
							e.Graphics.FillEllipse(brush, kolo);
							
						  
						}
		
					}
				}
			}
		}

		private void button1_Click(object sender, EventArgs e)
		{
			labiryntTab = new int[(int)numericUpDown1.Value,(int)numericUpDown2.Value];
			richTextBox1.Clear();
			int i = 1;
			
			foreach (var item in LosujObiekty())
			{
				richTextBox1.Text += "Obiekt " + i + " = " + item.ToString() + "\n";
				i++;
			}
			groupBox4.Refresh();
		}

		List<Point> LosujObiekty()
		{
			Random rnd = new Random();
			Point obiekt = new Point();
			List<Point> listaObiektow = new List<Point>();
			bool zaBlisko=false;
			bool zaDlugo=false;
			int licznik = 0;

			while(zaDlugo==false)
			{
	
				licznik = 0;
				do
				{
					licznik++;
					if (licznik > 500) 
					{
						zaDlugo=true;
						break;
					}
					
					obiekt.X = rnd.Next(labiryntTab.GetLength(0));
					obiekt.Y = rnd.Next(labiryntTab.GetLength(1));
					Debug.WriteLine(licznik);
					zaBlisko = false;

					for (int i = 0; i < labiryntTab.GetLength(1); i++)
					{
						for (int j = 0; j < labiryntTab.GetLength(0); j++)
						{
							if ((Math.Pow((j - obiekt.X), 2) + Math.Pow((i - obiekt.Y), 2) < Math.Pow((int)numericUpDown3.Value, 2)) && labiryntTab[j, i] == 10)
							{
								zaBlisko = true;
								break;
							}	
						}
					}
				} while (zaBlisko == true);

				if(zaDlugo==false)
				{
				listaObiektow.Add(obiekt);
				labiryntTab[obiekt.X, obiekt.Y] = 10;
				}
			}
			return listaObiektow;
		}
	}
}