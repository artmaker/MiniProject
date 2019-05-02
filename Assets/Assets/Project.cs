using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;

namespace Assets
{
    public partial class Project : Form
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ProjectA"].ToString());
        public Project()
        {
            InitializeComponent();
        }

        private void projectsview_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            AddProject Pro = new AddProject();
            if (e.ColumnIndex == 2)
            {
                //SqlConnection conn = new SqlConnection("Data Source=TALHAALI;Initial Catalog=ProjectA;User ID=sa;Password=talhaali");
                SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ProjectA"].ToString());

                conn.Open();
                string cm3 = string.Format("Select Id from project where Title='{0}'", projectview.Rows[e.RowIndex].Cells[0].Value.ToString());
                SqlCommand comm3 = new SqlCommand(cm3, conn);

                //string cm2 = string.Format("Update Project set Title='{0}', Description='{1}'", title.Text, description.Text);
                //SqlCommand comm2 = new SqlCommand(cm2, conn);
                //comm2.Parameters.AddWithValue("@Title", title.Text);
                //comm2.Parameters.AddWithValue("@Description", description.Text);

                int ide = (int)comm3.ExecuteScalar();
                //conn.Close();

                Pro.Change("Update", ide, projectview.Rows[e.RowIndex].Cells[0].Value.ToString(), projectview.Rows[e.RowIndex].Cells[1].Value.ToString());
                /* if (Pro.ShowDialog() == DialogResult.OK)
                 {
                     MessageBox.Show("Updated");
                     object obj = null;
                     EventArgs ev = null;
                     projectview.Rows.Clear();
                     this.Project_Load(obj, ev);
                 }*/
                Pro.Show();
            }
            if (e.ColumnIndex == 3)
            {
                var confirmResult = MessageBox.Show("Are you sure to delete this item ??",
                                     "Confirm Delete!!",
                                     MessageBoxButtons.YesNo);
                if (confirmResult == DialogResult.Yes)
                {
                    //SqlConnection conn = new SqlConnection("Data Source=TALHAALI;Initial Catalog=ProjectA;User ID=sa;Password=talhaali");
                    conn.Open();

                    String cm4 = string.Format("Delete from GroupProject where ProjectId=(Select Id from Project where Title='{0}');Delete from ProjectAdvisor where ProjectId=(Select Id from Project where Title='{0}');Delete from Project where Id=(Select Id from Project where Title='{0}');", projectview.Rows[e.RowIndex].Cells[0].Value.ToString());
                    SqlCommand comm4 = new SqlCommand(cm4, conn);
                    comm4.ExecuteNonQuery();
                    //SqlDataReader reader2 = comm4.ExecuteReader();
                    //int studentid = 0;
                    //while (reader2.Read())
                    //{
                    //    studentid = Convert.ToInt32(reader2["Id"]);
                    //}
                    conn.Close();
                    

                    MessageBox.Show("Deleted");

                    object sende = null;
                    EventArgs er = null;
                    projectview.Rows.Clear();
                    this.Project_Load(sende, er);


                }
                else
                {
                    MessageBox.Show("Not Deleted");
                }
            }
        }

        public void Project_Load(object sender, EventArgs e)
        {
            projectview.Rows.Clear();
            //SqlConnection conn = new SqlConnection("Data Source=TALHAALI;Initial Catalog=ProjectA;User ID=sa;Password=talhaali");
            //if(conn.State)
            conn.Open();
            string query = "Select Title,Description from Project";
            SqlCommand cmd = new SqlCommand(query, conn);
            //cmd.ExecuteNonQuery();
            //DataTable tbl = new DataTable();

            SqlDataReader reader = cmd.ExecuteReader();
            
            projectview.ColumnCount = 2;
            projectview.Columns[0].Name = "Title";
            projectview.Columns[1].Name = "Description";

            while (reader.Read())
            {
                ArrayList row = new ArrayList();
                row.Add(reader["Title"].ToString());
                row.Add(reader["Description"].ToString());

                projectview.Rows.Add(row.ToArray());


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
            projectview.Columns.Add(button);
            projectview.Columns.Add(button1);
            //dataview.CellClick += dataview_CellClick;




            conn.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AddProject pro = new AddProject();
            pro.Show();
            
            
            /*
            if (pro.ShowDialog() == DialogResult.OK)
            {
                MessageBox.Show("Project is Added");
                object obj = null;
                EventArgs ev = null;
                projectview.Rows.Clear();
                this.Project_Load(obj, ev);
            }*/
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ProjectAdvisor PR = new ProjectAdvisor();
            PR.Show();
        }
    }
}
