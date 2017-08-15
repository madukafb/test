using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Oracle.DataAccess.Client;
using System.IO;
using System.Reflection;
using iTextSharp.text.pdf;
using iTextSharp.text;


namespace Movie_rental
{
    public partial class Form1 : Form
    {
        int row1;
        public Form1()
        {
            InitializeComponent();
        }

        private void btn_Load_Click(object sender, EventArgs e)
        {


            string genreType = "";
            int value1 = 0;

            if (rbn_allMovies.Checked)
            {
                value1 = 1;
                lbl_title.Text = "ALL MOVIE DETAILS";
                lbl_title.Show();

            }
            else if (rbn_byGenre.Checked)
            {
                value1 = 2;
                genreType = cmb_type.SelectedItem.ToString();
                lbl_title.Text = genreType.ToUpper() + " MOVIE DETAILS";
                lbl_title.Show();
            }
            else if (rbn_allCustomers.Checked)
            {
                value1 = 0;
                lbl_title.Text = " UNAVAILABLE MOVIES";
                lbl_title.Show();
            }

            DBconnection db = new DBconnection();
            OracleDataReader reader=db.searchDetails(value1,genreType);     

            
            DataTable dt = new DataTable();
            dt.Load(reader);
            dataGridView1.DataSource = dt;
            row1 = dataGridView1.RowCount;
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            cmb_type.Enabled = false;
            dataGridView1.Enabled = false;
            lbl_title.Hide();
        }

        private void rbn_byGenre_CheckedChanged(object sender, EventArgs e)
        {
            cmb_type.Enabled = true;
        }

        private void rbn_allMovies_CheckedChanged(object sender, EventArgs e)
        {
            cmb_type.Enabled = false;
            cmb_type.SelectedIndex = -1;

        }

        private void rbn_allCustomers_CheckedChanged(object sender, EventArgs e)
        {
            cmb_type.Enabled = false;
            cmb_type.SelectedIndex = -1;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (row1 != -1)
            {
                try
                {
                    //Creating iTextSharp Table from the DataTable data
                    PdfPTable pdfTable = new PdfPTable(dataGridView1.ColumnCount);
                    pdfTable.DefaultCell.Padding = 3;
                    pdfTable.WidthPercentage = 30;
                    pdfTable.HorizontalAlignment = Element.ALIGN_LEFT;
                    pdfTable.DefaultCell.BorderWidth = 1;

                    //Adding Header row
                    foreach (DataGridViewColumn column in dataGridView1.Columns)
                    {
                        PdfPCell cell = new PdfPCell(new Phrase(column.HeaderText));
                        //cell.BackgroundColor = new iTextSharp.text.Color(240, 240, 240);
                        pdfTable.AddCell(cell);
                    }

                    //Adding DataRow

                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        foreach (DataGridViewCell cell in row.Cells)
                        {
                            pdfTable.AddCell(cell.Value.ToString());
                        }
                    }


                    //Exporting to PDF
                    string folderPath = "C:\\PDFs\\Movie\\";
                    if (!Directory.Exists(folderPath))
                    {
                        Directory.CreateDirectory(folderPath);
                    }
                    using (FileStream stream = new FileStream(folderPath + "DataGridViewExport.pdf", FileMode.Create))
                    {
                        Document pdfDoc = new Document(PageSize.A2, 10f, 10f, 10f, 0f);
                        PdfWriter.GetInstance(pdfDoc, stream);
                        pdfDoc.Open();
                        pdfDoc.Add(pdfTable);
                        pdfDoc.Close();
                        stream.Close();
                        MessageBox.Show("Exported Successfully to " + folderPath, "Alert", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception i)
                {
                    MessageBox.Show(i.Message, "Alert", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("No data to be exported", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }
        
    }
}
