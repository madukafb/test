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
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
        }

        private void btn_load_Click(object sender, EventArgs e)
        {

            String mGenre = cmb_mGenre.SelectedItem.ToString();

            DBconnection db = new DBconnection();
            OracleDataReader reader = db.DetailsForChart(mGenre, 1);

            while (reader.Read())
            {
                chart1.Series[reader[0].ToString()].Points.AddXY(DateTimeFormatInfo.CurrentInfo.GetAbbreviatedMonthName(Convert.ToInt32(reader[2])), reader[1]);

            }
        }
    }
}
