using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace kargotakip
{
    public partial class Login : MetroFramework.Forms.MetroForm
    {
        OleDbConnection conn = new OleDbConnection("Provider = Microsoft.Jet.OLEDB.4.0; Data Source = \"C:\\Users\\aynur balcı\\Source\\Repos\\kargotakip\\kargotakip\\Kargo.mdb\"");
        OleDbCommand cmd;
        OleDbDataAdapter adapter;
        DataTable dt;
        string imgLocation = "";
        public Login()
        {
            InitializeComponent();
        }

        private void Login_Load(object sender, EventArgs e)
        {
            // TODO: Bu kod satırı 'kargoDataSet7.kisiler' tablosuna veri yükler. Bunu gerektiği şekilde taşıyabilir, veya kaldırabilirsiniz.
            this.kisilerTableAdapter.Fill(this.kargoDataSet7.kisiler);

        }

        private void metroButton2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(metroTextBox3.Text))
            {
                MessageBox.Show("Please Enter your name.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                metroTextBox3.Focus();
                return;
            }
            try
            {
                KargoDataSet7TableAdapters.kisilerTableAdapter user = new KargoDataSet7TableAdapters.kisilerTableAdapter();
                KargoDataSet7.kisilerDataTable dt = user.NAME(metroTextBox3.Text, metroTextBox4.Text);
                if (dt.Rows.Count > 0)
                {
                    CARGO ms = new CARGO();
                    ms.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Username or Password is incorrect !");
                }


            }
            catch (Exception)
            {

                throw;
            }
        }

        private void metroTextBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
                metroTextBox3.Focus();
        }

        private void metroTextBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
                metroButton2.PerformClick();
        }

        private void metroButton2_Click_1(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(metroTextBox3.Text))
            {
                MessageBox.Show("Please Enter your name.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                metroTextBox3.Focus();
                return;
            }
            try
            {
                KargoDataSet7TableAdapters.kisilerTableAdapter user = new KargoDataSet7TableAdapters.kisilerTableAdapter();
                KargoDataSet7.kisilerDataTable dt = user.NAME(metroTextBox3.Text, metroTextBox4.Text);
                if (dt.Rows.Count > 0)
                {
                    CARGO ms = new CARGO();
                    ms.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Username or Password is incorrect !");
                }


            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
