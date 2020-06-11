using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;
namespace student_Database_management
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        // Local Veriable Initialization

        OleDbConnection con; OleDbDataAdapter adpt;
        bool b;int a, a1, a2, a3, a4, a5;string deletekey;

       //connection method

        public void connection()
        {
            con = new OleDbConnection("PROVIDER=MICROSOFT.JET.OLEDB.4.0; DATA SOURCE=DATABASE1.MDB");
            con.Open(); 
        }
        
        //show data in datagridview

        public void showgrid()
        {
            adpt = new OleDbDataAdapter("select * from info", con);
            DataSet ds = new DataSet();
            adpt.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
        }

        //Clear controls method

        public void clear(Control[] con)
        {
            foreach(Control c in con)
            {
                c.Text = "";
            }
        }

        //check data is numeric or not

        public void isnumeric(Control[] co)
        {
           foreach(Control c in co)
            {
                if(c.Text=="")
                {
                    a = 0;
                }
                else
                b = int.TryParse(c.Text, out a);
            }
        }
        
        //show data in listbox method

        public void showlist(string v1,string v2,string v3,string v4, string v5,string v6,string v7, string v8, string v9, string v10)
        {
            listBox1.Items.Clear();
            listBox1.Items.Add("Student result acroading system");
            listBox1.Items.Add("\n"); listBox1.Items.Add("\n"); listBox1.Items.Add("\n");
            listBox1.Items.Add("================================");
            listBox1.Items.Add("Student ID  :     '"+v1+"'");
            listBox1.Items.Add("Student Name:     '" + v2 + "'");
            listBox1.Items.Add("Module 1    :     '" + v3+"'");
            listBox1.Items.Add("Module 2    :     '"+ v4+"'");
            listBox1.Items.Add("Module 3    :     '"+ v5+"'");
            listBox1.Items.Add("Module 4    :     '" +v6+ "'");
            listBox1.Items.Add("Module 5    :     '" + v7 + "'");
            listBox1.Items.Add("\n"); listBox1.Items.Add("\n"); listBox1.Items.Add("\n");
            listBox1.Items.Add("\n================================");
            listBox1.Items.Add("\n");
            listBox1.Items.Add("Total       :     '" + v8 + "'");
            listBox1.Items.Add("Average     :     '" + v9 + "'");
            listBox1.Items.Add("Grade       :     '" + v10 + "'");
        }

        //generate student id automatically

        public int studentid()
        {
            int n = 0;
            adpt = new OleDbDataAdapter("SELECT top 1 [ID] FROM INFO order by [ID] desc", con);
            DataTable ds = new DataTable();
            adpt.Fill(ds);
            foreach (DataRow dr in ds.Rows)
            {
                n = Convert.ToInt16(dr["id"]);
                n++;
            }
            return n;
        }

        //search method

        public void search(string s)
        {
            if (textBox2.Text != "")
            {
                adpt = new OleDbDataAdapter("select * from info where course ='" + s + "'", con);
                DataSet ds = new DataSet();
                adpt.Fill(ds);
                dataGridView1.DataSource = ds.Tables[0];
            }
            else
            {
                adpt = new OleDbDataAdapter("select * from info", con);
                DataSet ds = new DataSet();
                adpt.Fill(ds);
                dataGridView1.DataSource = ds.Tables[0];
            }
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            connection();
            showgrid();
            comboBox1.SelectedIndex = 1;
            panel2.Top = (groupBox2.Height / 2) - (panel2.Height / 2);
            panel2.Left = (groupBox2.Width / 2) - (panel2.Width / 2);
            panel3.Left = (groupBox1.Width / 2) - (panel3.Width / 2);
            panel1.Left = (this.Width / 2) - (panel1.Width / 2);
            textBox1.Text = Convert.ToString(studentid());
        }

        //Insert Button method starts here

        private void button1_Click(object sender, EventArgs e)
        {
            string name = textBox3.Text + " " + textBox4.Text;
            OleDbCommand cmd = new OleDbCommand("insert into info (id,Course,Student_name,m1,m2,m3,m4,m5,Total,Average,Grade) values ('"+Convert.ToInt16( textBox1.Text)+"','" + comboBox1.Text+ "','" + name + "','" +(textBox14.Text) + "','" + ( textBox13.Text )+ "','" +(textBox12.Text) + "','" + ( textBox11.Text) + "','" + ( textBox10.Text) + "','" + (textBox5.Text) + "','" + (textBox6.Text) + "','" + (textBox7.Text )+ "')", con);
            cmd.ExecuteNonQuery();
            showlist(textBox1.Text, name, textBox14.Text, textBox13.Text, textBox12.Text, textBox11.Text, textBox10.Text, textBox5.Text, textBox6.Text, textBox7.Text);
            clear(new Control[] {textBox1,textBox3, textBox4, textBox7, textBox10, textBox11, textBox12, textBox13, textBox14, textBox1 });
            comboBox1.SelectedIndex = 1;
            textBox1.Text = Convert.ToString(studentid());
            showgrid();
        }

        //click on datagridview cells to show data in listbox

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex>=0)
            {
                DataGridViewRow row = new DataGridViewRow();
                row =dataGridView1.Rows[e.RowIndex];
                deletekey = Convert.ToString(row.Cells["id"].Value);
                showlist(Convert.ToString(row.Cells["id"].Value), Convert.ToString(row.Cells["Student_name"].Value), Convert.ToString(row.Cells["m1"].Value), Convert.ToString(row.Cells["m2"].Value), Convert.ToString(row.Cells["m3"].Value), Convert.ToString(row.Cells["m4"].Value), Convert.ToString(row.Cells["m5"].Value), Convert.ToString(row.Cells["Total"].Value) ,Convert.ToString(row.Cells["Average"].Value), Convert.ToString(row.Cells["Grade"].Value));
            }
        }

        //Clear button function starts here

        private void button3_Click(object sender, EventArgs e)
        {
            clear(new Control[] { textBox3, textBox4, textBox7, textBox10, textBox11, textBox12, textBox13, textBox14, textBox1 });
            comboBox1.SelectedIndex = 1;
            textBox1.Text = Convert.ToString(studentid());
        }

        //Delete Button methos starts here

        private void button4_Click(object sender, EventArgs e)
        {
            OleDbCommand cmd = new OleDbCommand("DELETE FROM INFO WHERE id=(?)", con);
            cmd.Parameters.AddWithValue("?", deletekey);
            cmd.ExecuteNonQuery();
            showgrid();
        }

        /******************* Auto calculate methods starts here *******************/
        private void textBox14_TextChanged(object sender, EventArgs e)
        {
            isnumeric(new Control[] { textBox14 });
            if (b == true)
            {
                a1 = a;
                textBox5.Text = Convert.ToString(a1+a2+a3+a4+a5);
            }
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            textBox6.Text = Convert.ToString(Convert.ToInt16(textBox5.Text) / 5);
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            search(textBox2.Text);
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            int avg = Convert.ToInt16(textBox6.Text);
            if (avg >= 80 && avg <= 100)
                textBox7.Text = "E";
            else if (avg >= 60 && avg <= 79)
                textBox7.Text = "A";
            else if (avg >= 40 && avg <= 59)
                textBox7.Text = "B";
            else if (avg >= 25 && avg <= 39)
                textBox7.Text = "C";
            else
                textBox7.Text = "Fail";
        }
        private void textBox13_TextChanged(object sender, EventArgs e)
        {
            isnumeric(new Control[] { textBox13 });
            if (b == true)
            {
                a2 = a;
                textBox5.Text = Convert.ToString(a1 + a2 + a3 + a4 + a5);
            }
        }
        private void textBox12_TextChanged(object sender, EventArgs e)
        {
            isnumeric(new Control[] { textBox12 });
            if (b == true)
            {
                a3 = a;
                textBox5.Text = Convert.ToString(a1 + a2 + a3 + a4 + a5);
            }
        }
        private void textBox11_TextChanged(object sender, EventArgs e)
        {
            isnumeric(new Control[] { textBox11 });
            if (b == true)
            {
                a4 = a;
                textBox5.Text = Convert.ToString(a1 + a2 + a3 + a4 + a5);
            }
        }

        private void textBox10_TextChanged(object sender, EventArgs e)
        {
            isnumeric(new Control[] { textBox10 });
            if (b == true)
            {
                a5 = a;
                textBox5.Text = Convert.ToString(a1 + a2 + a3 + a4 + a5);
            }
        }
        /******************* Auto calculate methods Ends here *******************/

            //Exit Button function
        private void button7_Click(object sender, EventArgs e)
        {
            Close();
        }




    }
}
