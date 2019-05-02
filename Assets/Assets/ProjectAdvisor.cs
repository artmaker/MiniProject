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
    public partial class ProjectAdvisor : Form
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ProjectA"].ToString());
        public ProjectAdvisor()
        {
            InitializeComponent();
        }

        public void ProjectAdvisor_Load(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            conn.Open();
            string query1 = string.Format("Select Name=FirstName+' '+LastName, Title,Role=Lookup.Value, ASSDate=ProjectAdvisor.AssignmentDate from Lookup INNer join (Project INNER Join (ProjectAdvisor INNER join (Advisor INNER join Person ON Person.Id=Advisor.Id) ON ProjectAdvisor.AdvisorId=Advisor.Id) ON Project.Id=ProjectAdvisor.ProjectId) ON ProjectAdvisor.AdvisorRole=Lookup.Id");
            SqlCommand cmd1 = new SqlCommand(query1, conn);
            //aID = (int)cmd1.ExecuteScalar();
            SqlDataReader reader = cmd1.ExecuteReader();
            while (reader.Read())
            {
                ArrayList row = new ArrayList();
                row.Add(reader["Name"].ToString());
                row.Add(reader["Title"].ToString());
                row.Add(reader["Role"].ToString());
                row.Add(reader["ASSDate"].ToString());
                //row.Add(reader["DateOfBirth"].ToString());
                //row.Add(reader["Gender"]);



                //row.Add(reader["RegistrationNo"]);
                dataGridView1.Rows.Add(row.ToArray());


            }
            conn.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            AddProjectAdvisor ADP = new AddProjectAdvisor();
            ADP.Show();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            AddProjectAdvisor form5 = new AddProjectAdvisor();

            //when edit button clicked
            if (e.ColumnIndex == 4)
            {
                form5.Change("Update",dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString(),
                    dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString(),
                    dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString()
                    );
                form5.Show();
            }
            if (e.ColumnIndex == 5)
            {
                var confirmResult = MessageBox.Show("Are you sure to delete this item ??",
                                     "Confirm Delete!!",
                                     MessageBoxButtons.YesNo);
                if (confirmResult == DialogResult.Yes)
                {
                    //SqlConnection conn = new SqlConnection("Data Source=TALHAALI;Initial Catalog=ProjectA;User ID=sa;Password=talhaali");
                    //conn.Open();

                    //String cm4 = string.Format("Select Id from Person where Email='{0}'", dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString());
                    //SqlCommand comm4 = new SqlCommand(cm4, conn);
                    //SqlDataReader reader2 = comm4.ExecuteReader();
                    //int studentid = 0;
                    //while (reader2.Read())
                    //{
                    //    studentid = Convert.ToInt32(reader2["Id"]);
                    //}
                    //conn.Close();
                    conn.Open();

                    String cm1 = string.Format("Delete From ProjectAdvisor where ProjectId=ANY(Select Id from Project where Title='{0}') and AdvisorId=ANY(Select Advisor.Id from Advisor INNER join Person On Advisor.Id=Person.Id where FirstName+' '+LastName='{1}' )",dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString(), dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());
                    SqlCommand comm1 = new SqlCommand(cm1, conn);
                    comm1.ExecuteNonQuery();

                    //String cm = string.Format("Delete From Person where Id='{0}'", studentid);
                    //SqlCommand comm = new SqlCommand(cm, conn);
                    //var row = comm.ExecuteNonQuery();
                    conn.Close();

                    MessageBox.Show("Deleted");
                    object sende = null;
                    EventArgs er = null;
                    dataGridView1.Rows.Clear();
                    this.ProjectAdvisor_Load(sende, er);


                }
                else
                {
                    MessageBox.Show("Not Deleted");
                }
            }
        }
    }
}
