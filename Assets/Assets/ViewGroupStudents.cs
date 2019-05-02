using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Assets
{
    public partial class ViewGroupStudents : Form
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ProjectA"].ToString());
        public int Id { get; set; }

        public ViewGroupStudents()
        {
            InitializeComponent();
        }

        private void ViewGroupStudents_Load(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            comboBox1.Items.Clear();
            conn.Open();
            string query = String.Format("Select RegistrationNo,FirstName,LastName,Status=Lookup.Value from Person INNER join (Student INNER join (GroupStudent Inner join Lookup ON Lookup.Id=GroupStudent.Status) ON Student.Id=GroupStudent.StudentId)  On Student.Id=Person.Id where GroupId={0}",Id);
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataReader reader = cmd.ExecuteReader();
            List<string> list = new List<string>();
            while (reader.Read())
            {
                //comboBox1.Items.Add(reader["RegistrationNo"]);
                ArrayList row = new ArrayList();
                row.Add(reader["FirstName"].ToString());
                row.Add(reader["LastName"].ToString());
                row.Add(reader["RegistrationNo"].ToString());
                list.Add(reader["RegistrationNo"].ToString());
                row.Add(reader["Status"].ToString());

                dataGridView1.Rows.Add(row.ToArray());
            }
            conn.Close();

            conn.Open();
            //string query3 = "Select RegistrationNo from Student";
            string query3 = "Select RegistrationNo from Student Except Select RegistrationNo from Student INNER join GroupStudent ON Student.Id = GroupStudent.StudentId";
            SqlCommand cmd3 = new SqlCommand(query3, conn);
            SqlDataReader reader3 = cmd3.ExecuteReader();
            while (reader3.Read())
            {
                if (!list.Contains(reader3["RegistrationNo"]))
                {
                    comboBox1.Items.Add(reader3["RegistrationNo"]);
                }
                
            }
            conn.Close();


            //conn.Open();
            //string query1 = "Select Value from Lookup where Category='Status'";
            //SqlCommand cmd1 = new SqlCommand(query1, conn);
            //SqlDataReader reader1 = cmd1.ExecuteReader();
            //while (reader1.Read())
            //{
            //    comboBox2.Items.Add(reader1["Value"]);
                
            //}
            //conn.Close();


        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            if (comboBox1.Text.ToString() != "" )
            {
                conn.Open();
                string query = string.Format("Insert into GroupStudent values({0},(select Id from Student where RegistrationNo='{1}'),(Select Id from Lookup where Value='{2}'),'{3}')",Id,comboBox1.Text.ToString(),"Active",DateTime.Now);
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.ExecuteNonQuery();
                conn.Close();
                object ob = null;
                EventArgs er = null;
                this.ViewGroupStudents_Load(ob,er);

            }
            else
            {
                MessageBox.Show("Select any Student");
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.ColumnIndex == 4)
            {
                conn.Open();
                //string query = string.Format("Delete from GroupStudent where GroupId={0} and StudentId=(Select Id from Student where RegistrationNo='{1}')", Id, dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString());
                string query;
                string msg;
                if (dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString() == "Active")
                {
                    msg = "InActive";
                   query = string.Format("Update GroupStudent set Status=(Select Id from Lookup where Value='InActive') where GroupId={0} and StudentId=(Select Id from Student where RegistrationNo='{1}')",Id,dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString());
                }
                else
                {
                    msg = "Active";
                    query = string.Format("Update GroupStudent set Status=(Select Id from Lookup where Value='Active') where GroupId={0} and StudentId=(Select Id from Student where RegistrationNo='{1}')", Id, dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString());

                }
                var confirmResult = MessageBox.Show("Are you sure to Change Status to"+" " +msg,
                                     msg,
                                     MessageBoxButtons.YesNo);
                if (confirmResult == DialogResult.Yes)
                {
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    object ob = null;
                    EventArgs er = null;
                    this.ViewGroupStudents_Load(ob, er);
                }
                conn.Close();

            }
            if (e.ColumnIndex == 5)
            {
                var confirmResult = MessageBox.Show("Are you sure to delete this item ??",
                                     "Confirm Delete!!",
                                     MessageBoxButtons.YesNo);
                if (confirmResult == DialogResult.Yes)
                {
                    conn.Open();
                    string query = string.Format("Delete from GroupStudent where GroupId={0} and StudentId=(Select Id from Student where RegistrationNo='{1}')", Id, dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString());
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.ExecuteNonQuery();
                    conn.Close();



                    object ob = null;
                    EventArgs er = null;
                    this.ViewGroupStudents_Load(ob, er);

                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MessageBox.Show("If You want to change group for any student then Delete that student from existing group and add into new Group");
        }
    }
}
