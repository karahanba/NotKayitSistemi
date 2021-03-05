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
    public partial class StudentDetail : Form
    {
        public StudentDetail()
        {
            InitializeComponent();
        }

        public string number;

        SqlConnection myconnection = new SqlConnection(@"Data Source=LAPTOP-8H4E7KRP;Initial Catalog=DbNotKayit;Integrated Security=True");
        //Data Source=LAPTOP-8H4E7KRP;Initial Catalog=DbNotKayit;Integrated Security=True

        private void StudentDetail_Load(object sender, EventArgs e)
        {
            lblNumber.Text = number;

            myconnection.Open();
            SqlCommand command1 = new SqlCommand("select * from StudentCourse where Student_Number = @p1", myconnection);
            command1.Parameters.AddWithValue("@p1", number);
            SqlDataReader dr = command1.ExecuteReader();
            while (dr.Read())
            {
                lblNameSurname.Text = dr[2].ToString() + " " + dr[3].ToString();
                lblExam1.Text = dr[4].ToString();
                lblExam2.Text = dr[5].ToString();
                lblExam3.Text = dr[6].ToString();
                lblAverage.Text = dr[7].ToString();
                lblStatus.Text = dr[8].ToString();
            }
            myconnection.Close();

            if (lblStatus.Text == "True")
            {
                lblStatus.Text = "Passed / Geçti";
            }
            else if (lblStatus.Text == "False")
            {
                lblStatus.Text = "Failed / Kaldı";
            }
        }
    }
}
