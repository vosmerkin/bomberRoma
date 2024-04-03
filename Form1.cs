using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp2
{
    public partial class Form1 : Form
    {

        int[,] campo;
        
        public Form1()
        {
            InitializeComponent();
            int mine = 0;
            

        }

        private void righe_TextChanged(object sender, EventArgs e)
        {

        }

        private void colonne_TextChanged(object sender, EventArgs e)
        {

        }

        private void inizio_Click(object sender, EventArgs e)
        {
            int row = int.Parse(righe.Text);
            int column = int.Parse(colonne.Text);
            campo = new int[row, column];
            Random rnd = new Random();
            for (int r = 0; r < row; r++)
            {
                for (int c = 0; c < column; c++)
                {
                    campo[r, c] = 0;
                }
            }


            dataGridView1.Columns.Clear();
            dataGridView1.Rows.Clear();

            for (int i = 0; i < column; i++)
            {
                dataGridView1.Columns.Add("Column" + i, "Column " + i);
                dataGridView1.Columns[i].Width = 20;

            }
            for (int i = 0; i < row; i++)
            {
                dataGridView1.Rows.Add();
                dataGridView1.Rows[i].Height = 20;
            }


            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < column; j++)
                {
                    DataGridViewImageCell imageCell = new DataGridViewImageCell();
                    string imagePath = System.IO.Path.Combine(Application.StartupPath, "sfondo.jpeg");
                    Image originalImage = Image.FromFile(imagePath);
                    int newWidth = 20;
                    int newHeight = 20;
                    Image resizedImage = new Bitmap(originalImage, newWidth, newHeight);


                    imageCell.Value = resizedImage;
                    imageCell.ToolTipText = imagePath;



                    dataGridView1.Rows[i].Cells[j] = imageCell;
                }
            }
        }

        private void FirstClick(DataGridViewCellMouseEventArgs e, int mine)
        {
            DataGridViewCell clickedCell = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex];
            do
            {
                int colonna, riga;
                Random random = new Random();
                colonna = random.Next(campo.GetLength(0), campo.GetLength(1));
                riga = random.Next(campo.GetLength(0), campo.GetLength(1));
                if (campo[riga, colonna] != campo[e.RowIndex, e.ColumnIndex])
                {
                    if (campo[riga, colonna] != campo[e.RowIndex - 1, e.ColumnIndex] || campo[riga, colonna] != campo[e.RowIndex + 1, e.ColumnIndex] || campo[riga, colonna] != campo[e.RowIndex - 1, e.ColumnIndex - 1] || campo[riga, colonna] != campo[e.RowIndex - 1, e.ColumnIndex + 1] || campo[riga, colonna] != campo[e.RowIndex, e.ColumnIndex + 1] || campo[riga, colonna] != campo[e.RowIndex, e.ColumnIndex - 1] || campo[riga, colonna] != campo[e.RowIndex + 1, e.ColumnIndex - 1] || campo[riga, colonna] != campo[e.RowIndex + 1, e.ColumnIndex + 1])
                    {
                        campo[riga, colonna] = 1;
                        mine++;
                    }
                }
            }
            while (mine != (campo.GetLength(0) * campo.GetLength(1)) / 5);
        }
        
        private void mouseleft(DataGridViewCellMouseEventArgs e)
        {
            DataGridViewCell clickedCell = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex];
            Image cellImage = (Image)clickedCell.Value;
            string imgin = clickedCell.ToolTipText;
            string imagePath = "";
            if (imgin == "bnd")
            {
                imagePath = System.IO.Path.Combine(Application.StartupPath, "sfondo.jpeg");
                Image newImage = Image.FromFile(imagePath);
                int newWidth = 20;
                int newHeight = 20;
                Image resizedImage = new Bitmap(newImage, newWidth, newHeight);
                clickedCell.Value = resizedImage;
                clickedCell.ToolTipText = "sbnd";
            }
            else if (imgin == "sbnd")
            {
                FirstClick(e, 0);
                clickedCell = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex];
                imagePath = System.IO.Path.Combine(Application.StartupPath, "sfondo2.jpeg");
                Image newImage = Image.FromFile(imagePath);
                int newWidth = 20;
                int newHeight = 20;
                Image resizedImage = new Bitmap(newImage, newWidth, newHeight);
                clickedCell.Value = resizedImage;
                clickedCell.ToolTipText = "sf";
            }
        }
        private void mouseright(DataGridViewCellMouseEventArgs e)
        {
            DataGridViewCell clickedCell = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex];
            Image cellImage = (Image)clickedCell.Value;
            

            string imagePath = "";




            imagePath = System.IO.Path.Combine(Application.StartupPath, "bandiera.png");
            Image newImage = Image.FromFile(imagePath);
            int newWidth = 20;
            int newHeight = 20;
            Image resizedImage = new Bitmap(newImage, newWidth, newHeight);
            clickedCell.Value = resizedImage;
            clickedCell.ToolTipText = "bnd";

            
            
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            
            if (e.Button == MouseButtons.Right && e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                mouseright(e);              
            }
            if (e.Button == MouseButtons.Left && e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                mouseleft(e);
            }
        }
    }
}

