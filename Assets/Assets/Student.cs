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
using System.Data.Sql;
using System.Collections;
using System.Configuration;

namespace Assets
{
    public partial class Student : Form
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ProjectA"].ToString());
        
        public Student()
        {
            InitializeComponent();
            
            
        }

        public void Form1_Load(object sender, EventArgs e)
        {

            // TODO: This line of code loads data into the 'assetsDataSet.IT_Assets' table. You can move, or remove it, as needed.
            //this.iT_AssetsTableAdapter.Fill(this.assetsDataSet.IT_Assets);
            dataview.Rows.Clear();
            //SqlConnection conn = new SqlConnection("Data Source=TALHAALI;Initial Catalog=ProjectA;User ID=sa;Password=talhaali");
            conn.Open();
            //string query = "Select FirstName,LastName,Contact,Email,DateOfBirth,Gender,RegistrationNo from Person INNER Join Student ON Student.Id=Person.Id";
            //string query = "Select FirstName,LastName,Contact,Email,DateOfBirth,Gender=Lookup.Value,RegistrationNo from Student INNER Join (Person INNER Join Lookup ON Lookup.Id=Person.Gender) ON Student.Id=Person.Id";
            string query = "Select FirstName,LastName,Contact,Email,DateOfBirth,Gender=Lookup.Value,RegistrationNo from Student INNER Join (Person Right Join Lookup ON Lookup.Id=Person.Gender ) ON Student.Id=Person.Id Union Select FirstName,LastName,Contact,Email,DateOfBirth,Gender=CAST(NULL AS VARCHAR(MAX)),RegistrationNo from Student  INNer join Person ON Person.Id=Student.Id where Gender is null";
            SqlCommand cmd = new SqlCommand(query, conn);
            //cmd.ExecuteNonQuery();
            //DataTable tbl = new DataTable();

            SqlDataReader reader = cmd.ExecuteReader();
            List<Person> list = new List<Person>();
            dataview.ColumnCount = 7;
            dataview.Columns[0].Name = "FisrtName";
            dataview.Columns[1].Name = "LastName";
            dataview.Columns[2].Name = "Contact";
            dataview.Columns[2].Width = 80;
            dataview.Columns[3].Name = "Email";
            dataview.Columns[3].Width = 80;
            dataview.Columns[4].Name = "DateOfBirth";
            dataview.Columns[5].Name = "Gender";
            dataview.Columns[5].Width = 50;
            dataview.Columns[6].Name = "RegistraionNo";
            

            while (reader.Read())
            {
                ArrayList row = new ArrayList();
                row.Add(reader["FirstName"].ToString());
                row.Add(reader["LAstName"].ToString());
                row.Add(reader["Contact"].ToString());
                row.Add(reader["Email"].ToString());
                row.Add(reader["DateOfBirth"].ToString());
                row.Add(reader["Gender"]);
                
                
                
                row.Add(reader["RegistrationNo"]);
                dataview.Rows.Add(row.ToArray());


            }
            //read.Fill(tbl);


            //dataview.DataSource = tbl;
            DataGridViewButtonColumn button = new DataGridViewButtonColumn();
            button.UseColumnTextForButtonValue = true;
            button.Name = "Update";
            button.Text = "Update";
            button.HeaderText = "Update";
            DataGridViewButtonColumn button1 = new DataGridViewButtonColumn();
            button1.UseColumnTextForButtonValue = true;
            button1.Name = "Delete";
            button1.Text = "Delete";
            button1.HeaderText = "Delete";

            //dataview.Columns.Insert(0, button);
            dataview.Columns.Add(button);
            dataview.Columns.Add(button1);
            //dataview.CellClick += dataview_CellClick;




            conn.Close();
        }

        private void dataview_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            UpdateStudent form5 = new UpdateStudent();

            //when edit button clicked
            if (e.ColumnIndex == 7)
            {
                MessageBox.Show(e.RowIndex.ToString());
                DateTime? date;
                if (String.IsNullOrEmpty(dataview.Rows[e.RowIndex].Cells[4].Value as String))
                {
                    MessageBox.Show("Empty");
                    date = null;

                }
                else
                {
                    date = Convert.ToDateTime(dataview.Rows[e.RowIndex].Cells[4].Value.ToString());
                }

                form5.change((int)e.RowIndex+1,dataview.Rows[e.RowIndex].Cells[0].Value.ToString(),
                    dataview.Rows[e.RowIndex].Cells[1].Value.ToString(), 
                    dataview.Rows[e.RowIndex].Cells[2].Value.ToString(), 
                    dataview.Rows[e.RowIndex].Cells[3].Value.ToString(),

                    date,
                    dataview.Rows[e.RowIndex].Cells[5].Value.ToString(),dataview.Rows[e.RowIndex].Cells[6].Value.ToString());
                /*if (form5.ShowDialog() == DialogResult.OK)
                {
                   dataview.Rows[e.RowIndex].Cells[0].Value = form5.FirstName;
                   dataview.Rows[e.RowIndex].Cells[1].Value = form5.LastName;
                   dataview.Rows[e.RowIndex].Cells[2].Value = form5.Contact;
                   dataview.Rows[e.RowIndex].Cells[3].Value = form5.Email;
                    dataview.Rows[e.RowIndex].Cells[4].Value = form5.DateOfBirth;
                    dataview.Rows[e.RowIndex].Cells[5].Value = form5.Gender;
                    dataview.Rows[e.RowIndex].Cells[6].Value = form5.RegistrationNo;
                        




                }*/
                form5.Show();
            }
            if (e.ColumnIndex == 8)
            {
                var confirmResult = MessageBox.Show("Are you sure to delete this item ??",
                                     "Confirm Delete!!",
                                     MessageBoxButtons.YesNo);
                if (confirmResult == DialogResult.Yes)
                {
                    SqlConnection conn = new SqlConnection("Data Source=TALHAALI;Initial Catalog=ProjectA;User ID=sa;Password=talhaali");
                    conn.Open();

                    String cm4 = string.Format("Select Id from Person where Email='{0}'", dataview.Rows[e.RowIndex].Cells[3].Value.ToString());
                    SqlCommand comm4 = new SqlCommand(cm4, conn);
                    SqlDataReader reader2 = comm4.ExecuteReader();
                    int studentid = 0;
                    while (reader2.Read())
                    {
                        studentid = Convert.ToInt32(reader2["Id"]);
                    }
                    conn.Close();
                    conn.Open();

                    //String cm1 = string.Format("Delete From Student where Id={0};Delete From Person where Id={0};Delete From GroupStudent where StudentId={0}", studentid);
                    String cm1 = string.Format("Delete From GroupStudent where StudentId={0};Delete From Student where Id={0};Delete From Person where Id={0}", studentid);
                    SqlCommand comm1 = new SqlCommand(cm1, conn);
                    comm1.ExecuteNonQuery();

                    //String cm = string.Format("Delete From Person where Id='{0}'", studentid);
                    //SqlCommand comm = new SqlCommand(cm, conn);
                    //var row = comm.ExecuteNonQuery();
                    conn.Close();
                    
                    MessageBox.Show("Deleted");
                    object sende = null;
                    EventArgs er = null;
                    dataview.Rows.Clear();
                    this.Form1_Load(sende, er);


                }
                else
                {
                    MessageBox.Show("Not Deleted");
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AddStudent Student = new AddStudent();
            Student.Show();
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Project pro = new Project();
            pro.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Advisor adv = new Advisor();
            adv.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Evaluation Ev = new Evaluation();
            Ev.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Groups grp = new Groups();
            grp.Show();
        }
    }
}
