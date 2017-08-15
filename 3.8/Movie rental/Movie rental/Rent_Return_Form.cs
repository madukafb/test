using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Movie_rental
{
    public partial class Rent_Return_Form : Form
    {
        customerSearch cs1;
        MovieRental m1,mm;
        public int movno1 = 0, movno2 = 0, movno3 = 0;
        public static int movno = 0;
        string mov1,mov2;
        string customerName;
        
        public Rent_Return_Form(string nicInput,string name)
        {
            customerName = name;
            InitializeComponent();
            txt_ReCusNIC.Text = nicInput;
            txt_RuCusNIC.Text = nicInput;
            txt_ReCusNIC.Focus();
        }

        private void Rent_Load(object sender, EventArgs e)
        {
            
            dataGridView1.Visible = false;
            txt_ReReturnDate.Text = System.DateTime.Today.AddDays(7).ToShortDateString();

        }

        //Search movies according to the selected genre
        private void com_ReGen1_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmd_ReMov1.Enabled = true;
            cmd_ReMov1.Items.Clear();
            List<string> list = new List<string>();
            DBconnection db = new DBconnection();
            list.AddRange(db.searchByGenre(com_ReGen1.SelectedItem.ToString()));
            cmd_ReMov1.Items.AddRange(list.ToArray());
        }

        //Search movies according to the selected genre
        private void com_ReGen2_SelectedIndexChanged(object sender, EventArgs e)
        {
            //cmd_ReMov2.Enabled = true;
            cmd_ReMov2.Items.Clear();
            List<string> list1 = new List<string>();
            DBconnection db1 = new DBconnection();
            list1.AddRange(db1.searchByGenre(com_ReGen2.SelectedItem.ToString()));
            foreach (string s in list1)
            {
                if (s==mov1)
                {
                    list1.Remove(s);
                    break;
                }

            }
            cmd_ReMov2.Items.AddRange(list1.ToArray());
        }

        //Search movies according to the selected genre
        private void com_ReGen3_SelectedIndexChanged(object sender, EventArgs e)
        {
            //cmd_ReMov3.Enabled = true;
            cmd_ReMov3.Items.Clear();
            List<string> list2 = new List<string>();
            DBconnection db2 = new DBconnection();
            list2.AddRange(db2.searchByGenre(com_ReGen3.SelectedItem.ToString()));
            foreach (string s in list2.ToList())
            {
                if (s == mov1)
                {
                    list2.Remove(s);
                    continue;
                }
                if (s == mov2)
                {
                    list2.Remove(s);
                    continue;
                }
            }
            cmd_ReMov3.Items.AddRange(list2.ToArray());
        }

        //Handles the Renting Function
        private void btn_ReRent_Click(object sender, EventArgs e)
        {
           string m1="m1", m2="m2", m3="m3", g1="g1", g2="g2", g3="g3";
            ReceiptForm f1;
            int val = 0;
            if (com_ReGen1.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a genre from genre list", "Select Genre", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (cmd_ReMov1.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a movie from movie list", "Select Movie", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (cmd_ReMov1.SelectedIndex != -1)
                {
                    val = 1;
                    m1 = this.cmd_ReMov1.GetItemText(this.cmd_ReMov1.SelectedItem);
                    g1 = this.com_ReGen1.GetItemText(this.com_ReGen1.SelectedItem);
                    Rent r = new Rent();
                    r.MovieNo = movno1;
                    r.Nic = txt_ReCusNIC.Text.ToString();
                    r.Status = 'n';
                    int ret = r.addRent();
                    if (ret == 0)
                    {
                        MessageBox.Show("Something went wrong", "Warning");
                    }                

                }

                if (cmd_ReMov2.SelectedIndex != -1)
                {
                    val = 2;
                    m1 = this.cmd_ReMov1.GetItemText(this.cmd_ReMov1.SelectedItem);
                    m2 = this.cmd_ReMov2.GetItemText(this.cmd_ReMov2.SelectedItem);
                    g1 = this.com_ReGen1.GetItemText(this.com_ReGen1.SelectedItem);
                    g2 = this.com_ReGen2.GetItemText(this.com_ReGen2.SelectedItem);
                    Rent r1 = new Rent();
                    r1.MovieNo = movno2;
                    r1.Nic = txt_ReCusNIC.Text.ToString();
                    r1.Status = 'n';
                    //MessageBox.Show("Done");
                    int ret = r1.addRent();
                    if (ret == 0)
                    {
                        MessageBox.Show("Something went wrong", "Warning");
                    }
                }

                if (cmd_ReMov3.SelectedIndex != -1)
                {
                    val = 3;
                    Rent r2 = new Rent();
                    r2.MovieNo = movno3;
                    r2.Nic = txt_ReCusNIC.Text.ToString();
                    r2.Status = 'n';
                    //MessageBox.Show("Done");
                    int ret = r2.addRent();
                    if (ret == 0)
                    {
                        MessageBox.Show("Something went wrong", "Warning");
                    }
                    m1 = this.cmd_ReMov1.GetItemText(this.cmd_ReMov1.SelectedItem);
                    m2 = this.cmd_ReMov2.GetItemText(this.cmd_ReMov2.SelectedItem);
                    m3 = this.cmd_ReMov3.GetItemText(this.cmd_ReMov3.SelectedItem);
                    g1 = this.com_ReGen1.GetItemText(this.com_ReGen1.SelectedItem);
                    g2 = this.com_ReGen2.GetItemText(this.com_ReGen2.SelectedItem);
                    g3 = this.com_ReGen3.GetItemText(this.com_ReGen3.SelectedItem);
                        
                }
                
            }
            this.Close();
            mm = new MovieRental();
            mm.Show();
            DialogResult dr1 = MessageBox.Show("Do you want to print a receipt", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (dr1 == DialogResult.Yes)
            {

                switch (val)
                {
                    case 1:
                        f1 = new ReceiptForm(customerName, txt_ReCusNIC.Text, DateTime.Now.ToString("mm/dd/yyyy"), txt_ReReturnDate.Text, m1, g1);
                        f1.Show(); break;
                    case 2: f1 = new ReceiptForm(customerName, txt_ReCusNIC.Text, DateTime.Now.ToString("mm/dd/yyyy"), txt_ReReturnDate.Text, m1, g1, m2, g2);
                        f1.Show(); break;
                    case 3: f1 = new ReceiptForm(customerName, txt_ReCusNIC.Text, DateTime.Now.ToString("mm/dd/yyyy"), txt_ReReturnDate.Text, m1, g1, m2, g2, m3, g3);
                        f1.Show(); break;
                    case 0: MessageBox.Show("Select atleast a 1 movie", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); break;

                }
            }
            else
                MessageBox.Show("You have sucessfully rented "+val+" movies","Message");
            
            
            
        }

        private void cmd_ReMov1_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmd_ReMov2.Enabled = true;
            DBconnection db = new DBconnection();
            movno1 = db.takemovid(cmd_ReMov1.SelectedItem.ToString());
            mov1 = cmd_ReMov1.SelectedItem.ToString();
            //MessageBox.Show(movno1.ToString());
        }

        private void cmd_ReMov2_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmd_ReMov3.Enabled = true;
            DBconnection db = new DBconnection();
            movno2 = db.takemovid(cmd_ReMov2.SelectedItem.ToString());
            mov2=cmd_ReMov2.SelectedItem.ToString();
            //MessageBox.Show(movno2.ToString());
        }

        private void cmd_ReMov3_SelectedIndexChanged(object sender, EventArgs e)
        {
            DBconnection db = new DBconnection();
            movno3 = db.takemovid(cmd_ReMov3.SelectedItem.ToString());
            //MessageBox.Show(movno3.ToString());
        }

        //Calculate bill values and fines 
        // when returning when a rented movie is selected from datagrid
        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                var item = dataGridView2.Rows[e.RowIndex].Cells[0].Value;
                txt_RuMovieName.Text = item.ToString();
                DBconnection db1 = new DBconnection();
                Movie m = db1.searchMovie(txt_RuMovieName.Text);
                movno = m.Movie_Id;
                txt_RuMovieRent.Text = m.Price.ToString();
                Rent r1 = new Rent();
                int fineval = r1.getfines(m.Movie_Id, txt_RuCusNIC.Text);
                txt_fines.Text = fineval.ToString();
                txt_RuTotal.Text = ((m.Price) + fineval).ToString();
            }
            catch (Exception)
            {
                MessageBox.Show("Click on a movie name","Select Movie",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }

        //Handles the return function
        private void btn_Return_Click(object sender, EventArgs e)
        {
            if (txt_RuMovieName.Text != "")
            {
                try
                {
                    Rent r = new Rent();
                    r.Nic = txt_RuCusNIC.Text;
                    r.MovieNo = movno;
                    r.BillVal = Int32.Parse(txt_RuTotal.Text);
                    int ret = r.addReturn();
                    if (ret == 0)
                    {
                        MessageBox.Show("Something went wrong", "Warning");
                    }
                    else
                    {
                        MessageBox.Show("Movie Renturned Succesfully", "Returned");
                    }
                    Customer c = new Customer();
                    dataGridView2.DataSource = (c.searchCustomer(txt_RuCusNIC.Text));
                    txt_RuMovieName.Text = "";
                    txt_RuMovieRent.Text = "";
                    txt_RuTotal.Text = "";
                    txt_fines.Text = "";
                }
                catch (Exception)
                {
                    MessageBox.Show("Click on a movie name", "Select Movie", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("No rented movies for this customer", "No Movie", MessageBoxButtons.OK, MessageBoxIcon.Error);
                mm = new MovieRental();
                this.Hide();
                mm.Show();
            }

        }

        //Load already burrowed movies to data grid
        private void txt_ReCusNIC_Enter(object sender, EventArgs e)
        {
            dataGridView1.Visible = true;
            Customer c = new Customer();
            dataGridView1.DataSource = (c.searchCustomer(txt_ReCusNIC.Text));
            int rowCount = dataGridView1.RowCount;
            //MessageBox.Show(rowCount.ToString());
            switch (rowCount)
            {
                case 1:
                    MessageBox.Show("Customer can burrow 2 movies");
                    com_ReGen1.Enabled = true;
                    com_ReGen2.Enabled = true;
                    //cmd_ReMov1.Enabled = true;
                    //cmd_ReMov2.Enabled = true;
                    break;
                case 2:
                    MessageBox.Show("Customer can burrow 1 movies");
                    com_ReGen1.Enabled = true;
                    //cmd_ReMov1.Enabled = true;
                    break;
                case 3:
                    MessageBox.Show("Customer has already burrowed 3 movies.");
                    //tab_Returning.SelectedIndex = 1;
                    break;
                default:
                    MessageBox.Show("Customer can burrow 3 movies");
                    com_ReGen1.Enabled = true;
                    com_ReGen2.Enabled = true;
                    com_ReGen3.Enabled = true;
                    //cmd_ReMov1.Enabled = true;
                    //cmd_ReMov2.Enabled = true;
                    //cmd_ReMov3.Enabled = true;
                    break;
            }

        }
       
        

        private void pictureBox1_Click(object sender, EventArgs e)
        {
             m1 = new MovieRental();
            m1.Show();
            this.Hide();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
             m1 = new MovieRental();
            m1.Show();
            this.Hide();
        }

        //Load already burrowed movies to data grid
        private void txt_RuCusNIC_Enter(object sender, EventArgs e)
        {
            Customer c = new Customer();
            dataGridView2.DataSource = (c.searchCustomer(txt_RuCusNIC.Text));
        }

        

        
    }
}
