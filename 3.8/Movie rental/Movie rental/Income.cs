using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Oracle.DataAccess.Client;
using Oracle.DataAccess;
using System.Collections;
using System.IO;
using System.Reflection;
using iTextSharp.text.pdf;
using iTextSharp.text;

namespace Movie_rental
{
    public partial class Income : Form
    {
        DateTime from, to;
        DBconnection db1;
        MovieRental mr1;
        public Income()
        {
            InitializeComponent();
        }
        //date time picker max and min values
        private void Income_Load(object sender, EventArgs e)
        {
            dateTimePicker_from.MaxDate = DateTime.Now.AddDays(-1);
            dateTimePicker_to.MaxDate = DateTime.Now;
        }

        //Generate report and fill datagrid
        private void btn_submit_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.AppStarting;
            from = dateTimePicker_from.Value.Date;
            to = dateTimePicker_to.Value.Date;
            db1 = new DBconnection();
            DataTable dt = new DataTable();
            dt = db1.displayIncome(from, to);
            dataGridView1.DataSource = dt;
            this.Height = 500; this.Width = 550; this.Cursor = Cursors.Default;
            incomeCal();
        }

        private void incomeCal()
        {
            decimal Total = 0;

            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                Total += Convert.ToDecimal(dataGridView1.Rows[i].Cells["FARE"].Value);
            }
            label5.Text = "Rs. " + Total.ToString();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            mr1 = new MovieRental();
            this.Hide();
            mr1.Show();
        }

        private void pictureBox1_MouseHover(object sender, EventArgs e)
        {
            System.Windows.Forms.ToolTip ToolTip1 = new System.Windows.Forms.ToolTip();
            ToolTip1.SetToolTip(this.pictureBox1, "Back");
        }

        private void btn_Export_Click(object sender, EventArgs e)
        {
            if (dataGridView1.RowCount != -1)
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
                    string folderPath = "C:\\PDFs\\Income\\";
                    if (!Directory.Exists(folderPath))
                    {
                        Directory.CreateDirectory(folderPath);
                    }
                    using (FileStream stream = new FileStream(folderPath + "Income Report.pdf", FileMode.Create))
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
                catch (Exception)
                {
                    MessageBox.Show("Exporting Failed", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("No data to be Exported", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
