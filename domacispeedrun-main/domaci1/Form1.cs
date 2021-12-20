using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace domaci1
{
    public partial class Form1 : Form
    {
        DataTable Knjiga = new DataTable();

        int red = 0;
        string cs = "Data source=LAPTOP-FGQM75PR\\SQLEXPRESS; Initial catalog=domaci1; Integrated security=true";

        private void osvezi(int x)
        {
            textBox1.Text = Knjiga.Rows[x]["ID"].ToString();
            textBox2.Text = Knjiga.Rows[x]["Naslov"].ToString();
            textBox3.Text = Knjiga.Rows[x]["Autor"].ToString();
            textBox4.Text = Knjiga.Rows[x]["BrojStrana"].ToString();
            textBox5.Text = Knjiga.Rows[x]["Povez"].ToString();

        }


        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            SqlConnection veza = new SqlConnection(cs);
            SqlDataAdapter adapter = new SqlDataAdapter("select * from Knjiga ", veza);
            adapter.Fill(Knjiga);

            osvezi(red);

            if (red == 0)
            {
                button2.Enabled = false;
            }
            if (red == Knjiga.Rows.Count - 1)
            {
                button3.Enabled = false;
            }
        }


        private void button3_Click(object sender, EventArgs e)
        {
            if (red < Knjiga.Rows.Count - 1)
            {
                red++;
                osvezi(red);
                button2.Enabled = true;
            }
            if (red == Knjiga.Rows.Count - 1)
            {
                button3.Enabled = false;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (red > 0)
            {
                red--;
                osvezi(red);
                button3.Enabled = true;
            }
            if (red == 0)
            {
                button2.Enabled = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            red = 0;
            osvezi(red);
            button2.Enabled = false;
            button3.Enabled = true;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            red = Knjiga.Rows.Count - 1;
            osvezi(red);
            button2.Enabled = true;
            button3.Enabled = false;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            SqlConnection veza = new SqlConnection(cs);
            SqlCommand naredba = new SqlCommand("DELETE FROM Knjiga WHERE ID=" + textBox1.Text, veza);
            veza.Open();
            naredba.ExecuteNonQuery();
            veza.Close();
            SqlDataAdapter adapter = new SqlDataAdapter("select * from Knjiga", veza);
            Knjiga.Clear();
            adapter.Fill(Knjiga);
            if (red == Knjiga.Rows.Count) red = red - 1;
            if (red == 0)
            {
                button2.Enabled = false;
            }
            if (Knjiga.Rows.Count > 1)
            {
                button3.Enabled = true;
            }
            if (red == Knjiga.Rows.Count - 1)
            {
                button3.Enabled = false;
            }

            osvezi(red);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            SqlConnection veza = new SqlConnection(cs);
            SqlCommand naredba = new SqlCommand("Update Knjiga Set Naslov= '" + textBox2.Text + "', Autor= '" + textBox3.Text + "' , BrojStrana= '" + textBox4.Text + "' , Povez= '" + textBox5.Text + "'  where ID= " + textBox1.Text, veza);
            veza.Open();
            naredba.ExecuteNonQuery();
            veza.Close();
            SqlDataAdapter adapter = new SqlDataAdapter("select * from Knjiga", veza);
            Knjiga.Clear();
            adapter.Fill(Knjiga);
            osvezi(red);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            SqlConnection veza = new SqlConnection(cs);
            SqlCommand naredba = new SqlCommand("insert into Knjiga (ID , Naslov, Autor, BrojStrana, Povez) values (" + textBox1.Text + ", '" + textBox2.Text + "' ,'" + textBox3.Text + "', '" + textBox4.Text + "' , '" + textBox5.Text + "' ) ", veza);
            veza.Open();
            naredba.ExecuteNonQuery();
            veza.Close();
            SqlDataAdapter adapter = new SqlDataAdapter("select * from Knjiga", veza);
            Knjiga.Clear();
            adapter.Fill(Knjiga);
            osvezi(red);
        }
    }
}
