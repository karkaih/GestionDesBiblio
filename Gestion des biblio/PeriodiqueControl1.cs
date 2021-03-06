using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Gestion_des_biblio
{
    public partial class PeriodiqueControl1 : UserControl
    {
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-ANFSKNC;Initial Catalog=GestionDesBibliotheuqes;Integrated Security=True");
        int id;
        public PeriodiqueControl1()
        {
            InitializeComponent();
         //   LoadAllRecords();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Equals("") || textBox2.Text.Equals("") || textBox3.Text.Equals(""))
            {
                MessageBox.Show("Remplis les champs");
            }
            else
            {
                con.Open();
                SqlCommand com = new SqlCommand("insert into dbo.Periodique ( Nom , Numero ,Periodicite , Stock , D_Ajout ) values ( '" + textBox1.Text + "', '" + textBox2.Text + "', '" + textBox3.Text + "', '" + numericUpDown1.Value + "', '" + DateTime.Parse(dateTimePicker1.Text) + "' )", con);
                com.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("secuussefully saved");
                textBox1.Clear();
                textBox2.Clear();
                textBox3.Clear();
                numericUpDown1.Value = 1;
                dateTimePicker1.Value = DateTime.Now;
                LoadAllRecords();
            }

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {


                DataGridViewRow row = dataGridView1.SelectedRows[0];
                id = int.Parse(row.Cells[0].Value.ToString());
                textBox1.Text = row.Cells[1].Value.ToString();
                textBox2.Text = row.Cells[2].Value.ToString();
                textBox3.Text = row.Cells[3].Value.ToString();
                numericUpDown1.Value = int.Parse(row.Cells[4].Value.ToString());
                dateTimePicker1.Text = row.Cells[5].Value.ToString();

                button3.Enabled = true;

            }
            else
            {  //optional    
                MessageBox.Show("Please select a row");
            }
        }
        void LoadAllRecords()
        {
            SqlCommand com = new SqlCommand("select * from  Periodique ", con);
            SqlDataAdapter da = new SqlDataAdapter(com);

            DataTable dt = new DataTable();
            da.Fill(dt);

            dataGridView1.DataSource = dt;

        }
        private void PeriodiqueControl1_Load(object sender, EventArgs e)
        {
            LoadAllRecords();
         //   Timer timer = new Timer();
           // timer.Interval = (1 * 1000); // 10 secs
            //timer.Tick += new EventHandler(timer_Tick);
            //timer.Start();
        }
        private void timer_Tick(object sender, EventArgs e)
        {
           // LoadAllRecords();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                if (MessageBox.Show("Are you confirm to delete ? ", "Delete", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    // DataGridViewSelectedRowCollection row = dataGridView1.SelectedRows;
                    // taking the index of the selected rows and removing/
                    con.Open();
                    DataGridViewRow row = dataGridView1.SelectedRows[0];
                    // textBox1.Text = row.Cells[1].Value.ToString();
                    //textBox2.Text = row.Cells[2].Value.ToString();
                    SqlCommand com = new SqlCommand("delete from dbo.Periodique where Id = '" + row.Cells[0].Value.ToString() + "'", con);
                    com.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("Successfully deleted");
                    LoadAllRecords();
                    //dataGridView1.Rows.RemoveAt(dataGridView1.SelectedRows[0].Index);
                }
            }
            else
            {  //optional    
                MessageBox.Show("Please select a row");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Equals("") || textBox2.Text.Equals(""))
            {
                MessageBox.Show("Remplis les champs");
            }
            else
            {
                con.Open();
                SqlCommand com = new SqlCommand("update dbo.Periodique set  Nom =  '" + textBox1.Text + "' , Numero = '" + textBox2.Text + "' , Periodicite =  '" + textBox3.Text + "' , Stock = '" + numericUpDown1.Value + "' , D_Ajout = '" + DateTime.Parse(dateTimePicker1.Text) + "'where Id = ' " + id + "'", con);
                com.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("secuussefully Modified");
                textBox1.Clear();
                textBox2.Clear();
                textBox3.Clear();
                numericUpDown1.Value = 1;
                dateTimePicker1.Value = DateTime.Now;
                LoadAllRecords();

                button3.Enabled = false;
            }
        }
        int Id;
        string Titre;
        string Category;
        private void button4_Click(object sender, EventArgs e)
        {

            if (dataGridView1.SelectedRows.Count > 0)
            {
                DataGridViewRow row = dataGridView1.SelectedRows[0];
                Id = int.Parse(row.Cells[0].Value.ToString());
                Titre = row.Cells[1].Value.ToString();
                Category = "Periodique";
                EmpruntForm emprunt = new EmpruntForm(Id, Titre, Category);
                if (Id.Equals(0) || Titre.Equals("") || Category.Equals(""))
                {
                    MessageBox.Show("Please select a row");

                }
                else
                {
                    emprunt.Show();
                }


            }
            else
            {  //optional    
                MessageBox.Show("Please select a row");
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            LoadAllRecords();
        }
    }
}
