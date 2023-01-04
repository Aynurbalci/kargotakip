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
using System.IO;

namespace kargotakip
{
    public partial class CARGO : MetroFramework.Forms.MetroForm
    {
        public CARGO()
        {
            InitializeComponent();
        }

        OleDbConnection conn = new OleDbConnection("Provider = Microsoft.Jet.OLEDB.4.0; Data Source = \"C:\\Users\\aynur balcı\\Source\\Repos\\kargotakip\\kargotakip\\Kargo.mdb\"");
        OleDbCommand cmd;
        OleDbDataAdapter adapter;
        DataTable dt;
        string imgLocation = "";


        public void insert(string fileName, byte[] img)
        {
            try
            {



                conn.Open();
                cmd = new OleDbCommand("insert into [kargolar] ([FULLNAME], [NUMBER], [SENDER_ADDRESS], [RECEIVE_ADDRESS], [SITUATION], [DESI], [PRICE], [DATE], [PICTURE], [GENDER]) values (@Name,@phone,@sender_address,@receiveradress,@situation,@desi,@price,@date,@picture,@gender)", conn);

                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@Name", metroTextBoxname.Text);
                cmd.Parameters.AddWithValue("@phone", PHONE.Text);
                cmd.Parameters.AddWithValue("@sender_address", SENDAD.Text);
                cmd.Parameters.AddWithValue("@receive_address,", RECAD.Text);
                cmd.Parameters.AddWithValue("@situation", SITUATION.SelectedItem.ToString());
                cmd.Parameters.AddWithValue("@desi", Convert.ToInt32(DESI.Text));
                cmd.Parameters.AddWithValue("@price", Convert.ToInt32(PRICE.Text));
                cmd.Parameters.AddWithValue("@date", metroDateTime1.Value);
                cmd.Parameters.AddWithValue("@picture", img);
                cmd.Parameters.AddWithValue("@gender", GENDER.SelectedItem.ToString());
                cmd.ExecuteNonQuery();
                conn.Close();
                list();
                MessageBox.Show("Added");

            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }

        }
        public void update(string fileName, byte[] img)
        {
            try
            {
              
                string update = "UPDATE [kargolar] SET [FULLNAME]=@Name, [NUMBER]=@phone, [SENDER_ADDRESS]=@sender_address, [RECEIVE_ADDRESS]=@receive_address, [SITUATION]=@situation, [DESI]=@desi, [PRICE]=@price, [DATE]=@date, [PICTURE]=@picture, [GENDER]=@gender WHERE [ID]=@id";
                cmd = new OleDbCommand(update, conn);

                cmd.Parameters.AddWithValue("@Name", metroTextBoxname.Text);
                cmd.Parameters.AddWithValue("@phone", PHONE.Text);
                cmd.Parameters.AddWithValue("@sender_address", SENDAD.Text);
                cmd.Parameters.AddWithValue("@receive_address,", RECAD.Text);
                cmd.Parameters.AddWithValue("@situation", SITUATION.SelectedItem.ToString());
                cmd.Parameters.AddWithValue("@desi", Convert.ToInt32(DESI.Text));
                cmd.Parameters.AddWithValue("@price", Convert.ToInt32(PRICE.Text));
                cmd.Parameters.AddWithValue("@date", metroDateTime1.Value);
                cmd.Parameters.AddWithValue("@picture", img);
                cmd.Parameters.AddWithValue("@gender", GENDER.SelectedItem.ToString());
                cmd.Parameters.AddWithValue("@id", metroTextBox1.Text);
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
                list();
                MessageBox.Show("Updated");

            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }

        }
        void list()
        {
            conn.Open();
            cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from kargolar";
            cmd.ExecuteNonQuery();
            dt = new DataTable();
            adapter = new OleDbDataAdapter(cmd);
            adapter.Fill(dt);
            dataGridView1.DataSource = dt;
            conn.Close();
        }
        byte[] ConvertImageToByte(Image img)
        {
            if (img != null)
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    img.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                    return ms.ToArray();
                }
            }
            return null;


        }
        public Image ConvertByteArrayToImage(byte[] data)
        {
            using (MemoryStream ms = new MemoryStream(data))
            {
                return Image.FromStream(ms);
            }
        }
        private void Form4_Load(object sender, EventArgs e)
        {
            // TODO: Bu kod satırı 'kargoDataSet7.kargolar' tablosuna veri yükler. Bunu gerektiği şekilde taşıyabilir, veya kaldırabilirsiniz.
            this.kargolarTableAdapter.Fill(this.kargoDataSet7.kargolar);
            list();
        }

        private void SAVE_Click(object sender, EventArgs e)
        {
            try
            {
                insert(imgLocation, ConvertImageToByte(pictureBox1.Image));





            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BROWSE_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.Filter = "JPEG Dosyası |*.jpeg| JPEG Dosyası|*.jpg";
                ofd.FilterIndex = 2;
                ofd.RestoreDirectory = true;
                ofd.CheckFileExists = false;
                ofd.Title = "JPEG Dosyası Seçiniz..";
                ofd.Multiselect = false;

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    string DosyaYolu = ofd.FileName;
                    // string DosyaAdi = ofd.SafeFileName;
                    pictureBox1.ImageLocation = DosyaYolu;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void DELETE_Click(object sender, EventArgs e)
        {
            string s = "DELETE FROM kargolar WHERE ID=@id";
            cmd = new OleDbCommand(s, conn);
            cmd.Parameters.AddWithValue("@id", dataGridView1.CurrentRow.Cells[0].Value);
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
            MessageBox.Show("Deleted");
        }

        private void metroButton1_Click(object sender, EventArgs e)
        {
            update(imgLocation, ConvertImageToByte(pictureBox1.Image));
           
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {

     
                    metroTextBoxname.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                    PHONE.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
                    SENDAD.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
                    RECAD.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
                    SITUATION.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
                    DESI.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();
                    PRICE.Text = dataGridView1.CurrentRow.Cells[7].Value.ToString();
                    metroDateTime1.Text = dataGridView1.CurrentRow.Cells[8].Value.ToString();
                    pictureBox1.Text = dataGridView1.CurrentRow.Cells[9].Value.ToString();
                    GENDER.Text = dataGridView1.CurrentRow.Cells[10].Value.ToString();
                   metroTextBox1.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
    

        }

        private void metroButton4_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void metroButton2_Click(object sender, EventArgs e)
        {
            list();
            MessageBox.Show("Refreshed");
        }
        KargoDataSet7TableAdapters.kargolarTableAdapter kar = new KargoDataSet7TableAdapters.kargolarTableAdapter();

        private void metroButton3_Click(object sender, EventArgs e)
        {
            Form3 f3 = new Form3();
            f3.DELIVERED = Convert.ToInt32(kar.SIT("DELIVERED"));
            f3.UNDELIVERED = Convert.ToInt32(kar.SIT("UNDELIVERED"));
            f3.ShowDialog();
        }

        private void dataGridView1_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void metroTextBox5_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                if (string.IsNullOrEmpty(metroTextBox5.Text))

                {
                    this.kargolarTableAdapter.Fill(this.kargoDataSet7.kargolar);
                    kargolarBindingSource.DataSource = this.kargoDataSet7.kargolar;
                    //dataGridView1.DataSource = kargolarBindingSource;
                }
                else
                {

                    var query = from o in this.kargoDataSet7.kargolar
                                where o.FULLNAME.Contains(metroTextBoxname.Text) || o.NUMBER.Contains(PHONE.Text) || o.RECEIVE_ADDRESS.Contains(RECAD.Text) || o.SENDER_ADDRESS.Contains(SENDAD.Text)
                                select o;
                    kargolarBindingSource.DataSource = query.ToList();
                   // dataGridView1.DataSource = query.ToList();

                }

            }
        }

        private void metroTextBox5_TextChanged(object sender, EventArgs e)
        {
            if (conn.State == ConnectionState.Closed)
                conn.Open();
            cmd = new OleDbCommand("SELECT [FULLNAME], [NUMBER], [SENDER_ADDRESS], [RECEIVE_ADDRESS], [SITUATION], [DESI], [PRICE], [DATE], [PICTURE], [GENDER] FROM [kargolar] WHERE [FULLNAME] LIKE '%" + metroTextBox5.Text + "%'", conn);
            OleDbDataAdapter adapter = new OleDbDataAdapter();
            adapter.SelectCommand = cmd;
            dt.Clear();
            adapter.Fill(dt);
            dataGridView1.DataSource = dt;
            conn.Close();
        }

        private void metroButton5_Click(object sender, EventArgs e)
        {
            Login login = new Login();
            this.Hide();
            login.ShowDialog();

        }

        private void metroTextBoxname_Click(object sender, EventArgs e)
        {
            metroTextBoxname.BackColor = Color.Yellow;
        }
    }
}
