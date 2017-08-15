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
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        private void btn_load_Click(object sender, EventArgs e)
        {
           
            String cusnic = txt_cusNic.Text;

            DBconnection db = new DBconnection();
            OracleDataReader reader = db.DetailsForChart(cusnic, 0);

            while (reader.Read())
            {
                chart1.Series[reader[1].ToString()].Points.AddXY(DateTimeFormatInfo.CurrentInfo.GetAbbreviatedMonthName(Convert.ToInt32(reader[3])), reader[2]);

            }
        }
    }
}
