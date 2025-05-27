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

namespace alpdeneme
{
    public partial class Form1 : Form
    {
        string conStr = "Data Source=localhost;Initial Catalog=ArabaDB;Integrated Security=True;";
        SqlConnection con;
        public Form1()
        {
            con = new SqlConnection(conStr);
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        private void Listele()
        {
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Arabalar", con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("INSERT INTO Arabalar (Marka, Model, Yil, Motor, ResimYolu) VALUES (@Marka, @Model, @Yil, @Motor, @ResimYolu)", con);
            cmd.Parameters.AddWithValue("@Marka", txtMarka.Text);
            cmd.Parameters.AddWithValue("@Model", txtModel.Text);
            cmd.Parameters.AddWithValue("@Yil", int.Parse(txtYil.Text));
            cmd.Parameters.AddWithValue("@Motor", txtMotor.Text);
            cmd.Parameters.AddWithValue("@ResimYolu", pictureBox1.ImageLocation);

            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            Listele();
        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView1.SelectedCells[0].RowIndex;
            txtMarka.Text = dataGridView1.Rows[secilen].Cells["Marka"].Value.ToString();
            txtModel.Text = dataGridView1.Rows[secilen].Cells["Model"].Value.ToString();
            txtYil.Text = dataGridView1.Rows[secilen].Cells["Yil"].Value.ToString();
            txtMotor.Text = dataGridView1.Rows[secilen].Cells["Motor"].Value.ToString();
            pictureBox1.ImageLocation = dataGridView1.Rows[secilen].Cells["ResimYolu"].Value.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(dataGridView1.CurrentRow.Cells["Id"].Value);
            SqlCommand cmd = new SqlCommand("UPDATE Arabalar SET Marka=@Marka, Model=@Model, Yil=@Yil, Motor=@Motor, ResimYolu=@ResimYolu WHERE Id=@Id", con);
            cmd.Parameters.AddWithValue("@Id", id);
            cmd.Parameters.AddWithValue("@Marka", txtMarka.Text);
            cmd.Parameters.AddWithValue("@Model", txtModel.Text);
            cmd.Parameters.AddWithValue("@Yil", int.Parse(txtYil.Text));
            cmd.Parameters.AddWithValue("@Motor", txtMotor.Text);
            cmd.Parameters.AddWithValue("@ResimYolu", pictureBox1.ImageLocation);

            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            Listele();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(dataGridView1.CurrentRow.Cells["Id"].Value);
            SqlCommand cmd = new SqlCommand("DELETE FROM Arabalar WHERE Id=@Id", con);
            cmd.Parameters.AddWithValue("@Id", id);

            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            Listele();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Resim Dosyaları|*.jpg;*.jpeg;*.png;*.bmp";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.ImageLocation = ofd.FileName;
            }
        }
    }
}
