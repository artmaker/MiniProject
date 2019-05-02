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
    public partial class Evaluation : Form
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ProjectA"].ToString());
        public Evaluation()
        {
            InitializeComponent();
        }

        public void Evaluation_Load(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            //dataGridView1.Columns.Clear();
            //dataGridView1.Refresh();
            //SqlConnection conn = new SqlConnection("Data Source=TALHAALI;Initial Catalog=ProjectA;User ID=sa;Password=talhaali");
            conn.Open();
            string query = "Select Name,TotalMarks,TotalWeightage from Evaluation";
            SqlCommand cmd = new SqlCommand(query, conn);
            

            SqlDataReader reader = cmd.ExecuteReader();


            int i = 0;
            while (reader.Read())
            {
                //MessageBox.Show(i.ToString());
                ArrayList row = new ArrayList();
                row.Add(reader["Name"].ToString());
                row.Add(reader["TotalMarks"].ToString());
                row.Add(reader["TotalWeightage"].ToString());

                dataGridView1.Rows.Add(row.ToArray());
                ++i;



            }
            //dataGridView1.Rows.RemoveAt(i-1);
            
            //DataGridViewButtonColumn button = new DataGridViewButtonColumn();
            //button.UseColumnTextForButtonValue = true;
            //button.Name = "Update";
            //button.Text = "Update";
            //button.HeaderText = "Update";
            //DataGridViewButtonColumn button1 = new DataGridViewButtonColumn();
            //button1.UseColumnTextForButtonValue = true;
            //button1.Name = "Delete";
            //button1.Text = "Delete";
            //button1.HeaderText = "Delete";

            
            //dataGridView1.Columns.Add(button);
            //dataGridView1.Columns.Add(button1);
            //dataview.CellClick += dataview_CellClick;
            conn.Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            AddEvaluation Pro = new AddEvaluation();
            if (e.ColumnIndex == 3)
            {
                //SqlConnection conn = new SqlConnection("Data Source=TALHAALI;Initial Catalog=ProjectA;User ID=sa;Password=talhaali");
                //conn.Open();
                //string cm3 = string.Format("Select Id from project where Title='{0}'", projectview.Rows[e.RowIndex].Cells[0].Value.ToString());
                //SqlCommand comm3 = new SqlCommand(cm3, conn);

                

                //int ide = (int)comm3.ExecuteScalar();
                
                Pro.Change("Update",dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString(),
                    (int)Convert.ToDouble(dataGridView1.Rows[e.RowIndex].Cells[1].Value), (float)Convert.ToDouble(dataGridView1.Rows[e.RowIndex].Cells[2].Value));
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
            if (e.ColumnIndex == 4)
            {
                var confirmResult = MessageBox.Show("Are you sure to delete this item ??",
                                     "Confirm Delete!!",
                                     MessageBoxButtons.YesNo);
                if (confirmResult == DialogResult.Yes)
                {
                    //SqlConnection conn = new SqlConnection("Data Source=TALHAALI;Initial Catalog=ProjectA;User ID=sa;Password=talhaali");
                    conn.Open();

                    String cm4 = string.Format("Delete from GroupEvaluation where EvaluationId=(Select Id from Evaluation where Name='{0}');Delete from Evaluation where Name='{0}' and TotalMarks={1} and TotalWeightage={2};", dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString(), Convert.ToDouble(dataGridView1.Rows[e.RowIndex].Cells[1].Value),dataGridView1.Rows[e.RowIndex].Cells[2].Value);
                    SqlCommand comm4 = new SqlCommand(cm4, conn);
                    comm4.ExecuteNonQuery();
                    //SqlDataReader reader2 = comm4.ExecuteReader();
                    //int studentid = 0;
                    //while (reader2.Read())
                    //{
                    //    studentid = Convert.ToInt32(reader2["Id"]);
                    //}
                    conn.Close();
                    /*
                    conn.Open();

                    String cm1 = string.Format("Delete From Student where Id='{0}'", studentid);
                    SqlCommand comm1 = new SqlCommand(cm1, conn);
                    comm1.ExecuteNonQuery();

                    String cm = string.Format("Delete From Person where Id='{0}'", studentid);
                    SqlCommand comm = new SqlCommand(cm, conn);
                    var row = comm.ExecuteNonQuery();
                    conn.Close();
                    */

                    MessageBox.Show("Deleted");

                    object sende = null;
                    EventArgs er = null;
                    dataGridView1.Rows.Clear();
                    this.Evaluation_Load(sende, er);


                }
                else
                {
                    MessageBox.Show("Not Deleted");
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AddEvaluation Add = new AddEvaluation();
            Add.Show();
        }
    }
}
