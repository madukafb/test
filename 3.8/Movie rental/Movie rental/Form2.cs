using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Oracle.DataAccess.Client;
using System.Windows.Forms.DataVisualization.Charting;
using System.Globalization;


namespace Movie_rental
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void btn_load_Click(object sender, EventArgs e)
        {
            int maxCount = 0;
            String maxMovie = "";


            String fromDate = dtp_From.Value.ToShortDateString();
            String toDate = dtp_To.Value.ToShortDateString();

            int reportType = 2;
            String s = "Most Popular Genre ";
            if (rbn_customer.Checked)
            {
                reportType = 0;
                s = "The Best Customer ";
            }
            else if (rbn_movie.Checked)
            {
                reportType = 1;
                s = "Most Popular Movie ";
            }


            DBconnection db = new DBconnection();
            OracleDataReader reader = db.DetailsForChart(fromDate, toDate, reportType);


            while (reader.Read())
            {
                if (maxCount < Convert.ToInt32(reader[1]))
                {
                    maxCount = Convert.ToInt32(reader[1]);
                    maxMovie = reader[0].ToString();
                }

                this.chart1.Series["Number of times rented"].Points.AddXY(reader[0], reader[1]);

            }

            lbl_summary.Text = s + " for the Period : " + maxMovie;
            lbl_summary.Show();

            btn_load.Enabled = false; 
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            lbl_summary.Hide();
            dtp_From.MaxDate = DateTime.Now;
            dtp_To.MaxDate = DateTime.Now;
        }

        private void rbn_customer_CheckedChanged(object sender, EventArgs e)
        {
            btn_load.Enabled = true;
            this.chart1.Series["Number of times rented"].Points.Clear();
        }

        private void rbn_movie_CheckedChanged(object sender, EventArgs e)
        {
            btn_load.Enabled = true;
            this.chart1.Series["Number of times rented"].Points.Clear();
        }

        private void rbn_genre_CheckedChanged(object sender, EventArgs e)
        {
            btn_load.Enabled = true;
            this.chart1.Series["Number of times rented"].Points.Clear();
        }

        private void dtp_From_ValueChanged(object sender, EventArgs e)
        {
            btn_load.Enabled = true;
        }

        private void dtp_To_ValueChanged(object sender, EventArgs e)
        {
            btn_load.Enabled = true;

        }



    }
}
