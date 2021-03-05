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

namespace Not_Kayit_Sistemi
{
    public partial class TutorDetail : Form
    {
        public TutorDetail()
        {
            InitializeComponent();
        }

        SqlConnection myconnection = new SqlConnection(@"Data Source=LAPTOP-8H4E7KRP;Initial Catalog=DbNotKayit;Integrated Security=True");

        private void TutorDetail_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'dbNotKayitDataSet.StudentCourse' table. You can move, or remove it, as needed.
            this.studentCourseTableAdapter.Fill(this.dbNotKayitDataSet.StudentCourse);

            //myconnection.Open();
            //SqlCommand mycommand2 = new SqlCommand("SELECT COUNT(*) from StudentCourse where SuccessStatus=1", myconnection);

            ////mycommand2.Parameters.AddWithValue("@P1", txtExam1.Text);
            ////mycommand2.Parameters.AddWithValue("@P2", txtExam2.Text);
            ////mycommand2.Parameters.AddWithValue("@P3", txtExam3.Text);
            ////mycommand2.Parameters.AddWithValue("@P4", decimal.Parse(lblAverage.Text));
            ////mycommand2.Parameters.AddWithValue("@P5", status);
            ////mycommand2.Parameters.AddWithValue("@P6", mskNumber.Text);

            ////mycommand2.ExecuteNonQuery();
            //SqlDataReader reader = mycommand2.ExecuteReader();


            //lblPassed.Text = reader.GetString(0);
            //reader.Close();
            //myconnection.Close();


            //LblGecens.Text = dbNotKayitDataSet.TBLDERS.Count(x => x.DURUM == true).ToString();

            //LblKalans.Text = dbNotKayitDataSet.TBLDERS.Count(x => x.DURUM == false).ToString();

            //LblOrt.Text = dbNotKayitDataSet.TBLDERS.Sum(y => y.ORTALAMA / (Convert.ToInt32(LblGecens.Text) + Convert.ToInt32(LblKalans.Text))).ToString()

            lblPassed.Text = dbNotKayitDataSet.StudentCourse.Count(x => x.SuccessStatus == true).ToString();
            lblFailed.Text = dbNotKayitDataSet.StudentCourse.Count(x => x.SuccessStatus == false).ToString();

            //lblAverage.Text = dbNotKayitDataSet.StudentCourse.Sum(y => y.AveragePoint / (Convert.ToInt32(lblPassed.Text) + Convert.ToInt32(lblFailed.Text))).ToString();
            // Virgülden sonra 2 basamak göstermek için aşağıdaki gibi yaptım
            lblAverage.Text = dbNotKayitDataSet.StudentCourse.Sum(y => Math.Round(y.AveragePoint / (Convert.ToInt32(lblPassed.Text) + Convert.ToInt32(lblFailed.Text)),2)).ToString();


            //Math.Round(d, 2)
            //ToString();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            myconnection.Open();
            SqlCommand mycommand = new SqlCommand("INSERT INTO StudentCourse (Student_Number, Student_Name, Student_Surname) VALUES (@P1, @P2, @P3)", myconnection);
            mycommand.Parameters.AddWithValue("@P1", mskNumber.Text);
            mycommand.Parameters.AddWithValue("@P2", txtName.Text);
            mycommand.Parameters.AddWithValue("@P3", txtSurname.Text);
            mycommand.ExecuteNonQuery();
            myconnection.Close();
            MessageBox.Show("Student is added into the system. / Öğrenci sisteme eklendi.");

            this.studentCourseTableAdapter.Fill(this.dbNotKayitDataSet.StudentCourse);
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int selection = dataGridView1.SelectedCells[0].RowIndex;

            mskNumber.Text = dataGridView1.Rows[selection].Cells[1].Value.ToString();
            txtName.Text = dataGridView1.Rows[selection].Cells[2].Value.ToString();
            txtSurname.Text = dataGridView1.Rows[selection].Cells[3].Value.ToString();
            txtExam1.Text = dataGridView1.Rows[selection].Cells[4].Value.ToString();
            txtExam2.Text = dataGridView1.Rows[selection].Cells[5].Value.ToString();
            txtExam3.Text = dataGridView1.Rows[selection].Cells[6].Value.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            double average, ex1, ex2, ex3;
            string status;
            ex1 = Convert.ToDouble(txtExam1.Text);
            ex2 = Convert.ToDouble(txtExam2.Text);
            ex3 = Convert.ToDouble(txtExam3.Text);

            average = (ex1 + ex2 + ex3) / 3;
            lblAverage.Text = average.ToString();

            if (average >=50)
            {
                status = "True";
            }
            else
            {
                status = "False";
            }

            myconnection.Open();
            SqlCommand mycommand2 = new SqlCommand("UPDATE StudentCourse SET Student_FirstExam = @P1, Student_SecondExam = @P2, Student_ThirdExam = @P3, AveragePoint =@P4, SuccessStatus = @P5 WHERE Student_Number = @P6", myconnection);

            mycommand2.Parameters.AddWithValue("@P1", txtExam1.Text);
            mycommand2.Parameters.AddWithValue("@P2", txtExam2.Text);
            mycommand2.Parameters.AddWithValue("@P3", txtExam3.Text);
            mycommand2.Parameters.AddWithValue("@P4", decimal.Parse(lblAverage.Text));
            mycommand2.Parameters.AddWithValue("@P5", status);
            mycommand2.Parameters.AddWithValue("@P6", mskNumber.Text);

            mycommand2.ExecuteNonQuery();
            myconnection.Close();
            MessageBox.Show("Student grades are updated. / Öğrenci Notları Güncellendi.");
            this.studentCourseTableAdapter.Fill(this.dbNotKayitDataSet.StudentCourse);
        }
    }
}
