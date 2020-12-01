using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;
/// <summary>
/// Coded by: Kim Dave Torres
/// Start-Date: November 27, 2020
/// End-Date: November 30, 2020
/// Subject: DBMS
/// </summary>
namespace Torres_RegistrationActivity
{
    public partial class Registration : Form
    {
        public Registration()
        {
            InitializeComponent();
            DisplayUserData();
        }
 

        //SQL CONNECTION STRING --START
        string connection_string = "Data Source=DESKTOP-DBIK9BF;Initial Catalog=UserRegistration;Integrated Security=True";
        //--END


        string img_location = "";
        int userid;


        //BROWSE PHOTO --START
        private void browseBtn_Click(object sender, EventArgs e)
        {
            OpenFileDialog file = new OpenFileDialog();
            file.Filter = "All files(*.*)|*.*|png files(*.png)|*.png|jpg files(*.jpg)|*.jpg";
            if (file.ShowDialog() == DialogResult.OK)
            {
                img_location = file.FileName.ToString();
                pictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
                pictureBox.ImageLocation = img_location;
            }
        }
        //--END


        //ADD NEW USER --START
        private void save_Click(object sender, EventArgs e)
        {
            if (pictureBox.Image == null)
            {
                MessageBox.Show("Please provide your image.", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                browseBtn.Select();
            }
            else if (fname.Text == "")
            {
                MessageBox.Show("Please provide your firstname.", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                fname.Select();
            }
            else if (lname.Text == "")
            {
                MessageBox.Show("Please provide your lastname.", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                lname.Select();
            }
            else if (emailAddress.Text == "")
            {
                MessageBox.Show("Please provide your email.", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                emailAddress.Select();
            }
            else if (uname.Text == "")
            {
                MessageBox.Show("Please provide your username.", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                uname.Select();
            }
            else if (pwd.Text == "")
            {
                MessageBox.Show("Please provide your password.", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                pwd.Select();
            }
            else
            {
                SqlConnection sqlcnn;
                sqlcnn = new SqlConnection(connection_string);
                sqlcnn.Open();

                //convert image to binary
                MemoryStream mem_stream = new MemoryStream();
                pictureBox.Image.Save(mem_stream, ImageFormat.Jpeg);

                byte[] photo_arr = new byte[mem_stream.Length];

                mem_stream.Position = 0;
                mem_stream.Read(photo_arr, 0, photo_arr.Length);
                //end

                SqlCommand sqlcmd = new SqlCommand("sp_insert_user", sqlcnn);
                sqlcmd.CommandType = CommandType.StoredProcedure;
                sqlcmd.Parameters.AddWithValue("@UserID", 0);
                sqlcmd.Parameters.AddWithValue("@ProfileImage", photo_arr);
                sqlcmd.Parameters.AddWithValue("@Firstname", fname.Text);
                sqlcmd.Parameters.AddWithValue("@Lastname", lname.Text);
                sqlcmd.Parameters.AddWithValue("@email", emailAddress.Text);
                sqlcmd.Parameters.AddWithValue("@Username", uname.Text);
                sqlcmd.Parameters.AddWithValue("@Password", pwd.Text);

                int num = sqlcmd.ExecuteNonQuery();
                if (num > 0)
                {
                    MessageBox.Show("Saved Sucessfully.", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearInputField();
                    DisplayUserData();
                }
                else
                {
                    MessageBox.Show("Error.", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    ClearInputField();
                }
                sqlcnn.Close();
            }
        }
        //--END


        //UPDATE USER --START
        private void button3_Click(object sender, EventArgs e)
        {
            SqlConnection sqlcnn;
            sqlcnn = new SqlConnection(connection_string);
            sqlcnn.Open();

            //convert image to binary
            MemoryStream mem_stream = new MemoryStream();
            pictureBox.Image.Save(mem_stream, ImageFormat.Jpeg);

            byte[] photo_arr = new byte[mem_stream.Length];

            mem_stream.Position = 0;
            mem_stream.Read(photo_arr, 0, photo_arr.Length);
            //end

            SqlCommand sqlcmd = new SqlCommand("sp_update_user", sqlcnn);
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.Parameters.AddWithValue("@UserID", userid);
            sqlcmd.Parameters.AddWithValue("@ProfileImage", photo_arr);
            sqlcmd.Parameters.AddWithValue("@Firstname", fname.Text);
            sqlcmd.Parameters.AddWithValue("@Lastname", lname.Text);
            sqlcmd.Parameters.AddWithValue("@email", emailAddress.Text);
            sqlcmd.Parameters.AddWithValue("@Username", uname.Text);
            sqlcmd.Parameters.AddWithValue("@Password", pwd.Text);

            int num = sqlcmd.ExecuteNonQuery();
            if (num > 0)
            {
                MessageBox.Show("Update successfully.", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearInputField();
                DisplayUserData();
            }
            else
            {
                MessageBox.Show("Error.", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ClearInputField();
            }
            sqlcnn.Close();
        }
        //END


        //DELETE USER --START
        private void delete_Click(object sender, EventArgs e)
        {
            SqlConnection sqlcnn;
            sqlcnn = new SqlConnection(connection_string);
            sqlcnn.Open();

            //convert image to binary
            MemoryStream mem_stream = new MemoryStream();
            pictureBox.Image.Save(mem_stream, ImageFormat.Jpeg);

            byte[] photo_arr = new byte[mem_stream.Length];

            mem_stream.Position = 0;
            mem_stream.Read(photo_arr, 0, photo_arr.Length);
            //end

            SqlCommand sqlcmd = new SqlCommand("sp_delete_user", sqlcnn);
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.Parameters.AddWithValue("@UserID", userid);


            int num = sqlcmd.ExecuteNonQuery();
            if (num > 0)
            {
                MessageBox.Show("Deleted successfully.", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearInputField();
                DisplayUserData();
            }
            else
            {
                MessageBox.Show("Error.", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ClearInputField();
            }
            sqlcnn.Close();
        }
        //END


        //SEARCH --START
        private void search_TextChanged(object sender, EventArgs e)
        {
            SqlConnection sqlcnn;
            sqlcnn = new SqlConnection(connection_string);
            sqlcnn.Open();

            DataTable dt = new DataTable();
            SqlCommand sqlcmd = new SqlCommand("sp_search_users", sqlcnn);
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.Parameters.AddWithValue("@keyword", search.Text);
            SqlDataAdapter sda = new SqlDataAdapter(sqlcmd);
            sda.Fill(dt);
            UserDataGrid.DataSource = dt;
            if (UserDataGrid.Rows.Count < 1)
            {
                MessageBox.Show("No data found.", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        //--END


        //DISPLAY USER DATA --START
        public void DisplayUserData()
        {
            SqlConnection sqlcnn;
            sqlcnn = new SqlConnection(connection_string);
            sqlcnn.Open();

            DataTable dt = new DataTable();
            SqlCommand sqlcmd = new SqlCommand("sp_users_display", sqlcnn);
            sqlcmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter sda = new SqlDataAdapter(sqlcmd);
            sda.Fill(dt);
            UserDataGrid.DataSource = dt;

            for (int i = 0; i < UserDataGrid.Columns.Count; i++)
            {
                if (UserDataGrid.Columns[i] is DataGridViewImageColumn)
                {
                    ((DataGridViewImageColumn)UserDataGrid.Columns[i]).ImageLayout = DataGridViewImageCellLayout.Stretch;
                    break;
                }
            }
            for (int i = UserDataGrid.Rows.Count - 1; i > -1; i--)
            {
                DataGridViewRow row = UserDataGrid.Rows[i];
                if (!row.IsNewRow && row.Cells[0].Value == null)
                {
                    UserDataGrid.Rows.RemoveAt(i);
                }
            }
            sqlcnn.Close();
        }
        //--END


        //DATA GRID CLICK --START
        private void UserDataGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            userid = int.Parse(UserDataGrid.CurrentRow.Cells[0].Value.ToString());
            
            byte[] photo_arr = (byte[])UserDataGrid.CurrentRow.Cells[1].Value;
            MemoryStream mem_stream = new MemoryStream(photo_arr);
            pictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            pictureBox.Image = Image.FromStream(mem_stream);
            
            fname.Text = UserDataGrid.CurrentRow.Cells[2].Value.ToString();
            lname.Text = UserDataGrid.CurrentRow.Cells[3].Value.ToString();
            emailAddress.Text = UserDataGrid.CurrentRow.Cells[4].Value.ToString();
            uname.Text = UserDataGrid.CurrentRow.Cells[5].Value.ToString();
            pwd.Text = UserDataGrid.CurrentRow.Cells[6].Value.ToString();
        }
        //--END


        //CLEAR INPUT FIELDS --START
        public void ClearInputField()
        {
            pictureBox.Image = null;
            fname.Text = string.Empty;
            lname.Text = string.Empty;
            emailAddress.Text = string.Empty;
            uname.Text = string.Empty;
            pwd.Text = string.Empty;
        }
        //--END





        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
        private void label5_Click(object sender, EventArgs e)
        {

        }
        private void groupBoxFunctionality_Enter(object sender, EventArgs e)
        {


        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           
        }
        private void Registration_Load(object sender, EventArgs e)
        {

        }
        private void label1_Click(object sender, EventArgs e)
        {

        }
        private void label2_Click(object sender, EventArgs e)
        {

        }
        private void label3_Click(object sender, EventArgs e)
        {

        }
        private void label4_Click(object sender, EventArgs e)
        {

        }
        private void label5_Click_1(object sender, EventArgs e)
        {

        }
    }
}
