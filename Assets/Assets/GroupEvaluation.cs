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
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Assets
{
    public partial class GroupEvaluation : Form
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ProjectA"].ToString());
        public int Id { get; set; }
        public GroupEvaluation()
        {
            InitializeComponent();
        }

        private void GroupEvaluation_Load(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            comboBox1.Items.Clear();
            conn.Open();
            string query = string.Format("Select Name,TotalMarks,ObtainedMarks,EvaluationDate from Evaluation INNER join GroupEvaluation ON Evaluation.Id=GroupEvaluation.EvaluationId where GroupId={0}",Id); 
                //(Student INNER join (GroupStudent Inner join Lookup ON Lookup.Id=GroupStudent.Status) ON Student.Id=GroupStudent.StudentId)  On Student.Id=Person.Id";
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataReader reader = cmd.ExecuteReader();
            List<string> list = new List<string>();
            while (reader.Read())
            {
                //comboBox1.Items.Add(reader["RegistrationNo"]);
                ArrayList row = new ArrayList();
                row.Add(reader["Name"].ToString());
                row.Add(reader["TotalMarks"].ToString());
                row.Add(reader["ObtainedMarks"].ToString());
                list.Add(reader["Name"].ToString());
                row.Add(reader["EvaluationDate"].ToString());

                dataGridView1.Rows.Add(row.ToArray());
            }
            conn.Close();


            conn.Open();
            string query1 = string.Format("Select Name from Evaluation ");
            //(Student INNER join (GroupStudent Inner join Lookup ON Lookup.Id=GroupStudent.Status) ON Student.Id=GroupStudent.StudentId)  On Student.Id=Person.Id";
            SqlCommand cmd1 = new SqlCommand(query1, conn);
            SqlDataReader reader1 = cmd1.ExecuteReader();
            //List<string> list = new List<string>();
            while (reader1.Read())
            {
                
                if (!list.Contains(reader1["Name"].ToString()))
                {

                    //list.Add(reader["Name"].ToString());
                    comboBox1.Items.Add(reader1["Name"].ToString());
                }
                
            }
            conn.Close();
        }
        public bool validator(Regex str, string txt)
        {
            return str.IsMatch(txt);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                Regex marksregx = new Regex(@"^[0-9]{1,3}$");
                if (comboBox1.Text.ToString() == "")
                {
                    throw new Exception("Select Evalaution from list");
                }
                conn.Open();
                string query1 = string.Format("Select TotalMarks from Evaluation where Name='{0}'", comboBox1.Text.ToString()); 
                    //INNER join Group Evaluation ON Evalution.Id=GroupEvaluation.EvaluationId where Evaluation='{0}'",);
                //(Student INNER join (GroupStudent Inner join Lookup ON Lookup.Id=GroupStudent.Status) ON Student.Id=GroupStudent.StudentId)  On Student.Id=Person.Id";
                SqlCommand cmd1 = new SqlCommand(query1, conn);
                int marks = (int)cmd1.ExecuteScalar();
                //SqlDataReader reader1 = cmd1.ExecuteReader();
                conn.Close();
                if (!(validator(marksregx,textBox1.Text.ToString())) )
                {
                    throw new Exception("Marks should be in numbers");
                }
                else if(int.Parse(textBox1.Text.ToString()) > marks)
                {
                    throw new Exception("Obtained Marks should be less than Total marks");
                }

                conn.Open();
                string query2 = string.Format("Insert Into GroupEvaluation values({0},(Select Id from Evaluation where Name='{1}'),{2},'{3}')", Id, comboBox1.Text.ToString(), int.Parse(textBox1.Text.ToString()), DateTime.Now);
                SqlCommand cmd2 = new SqlCommand(query2, conn);
                cmd2.ExecuteNonQuery();
                conn.Close();
                object ob = null;
                EventArgs er = null;
                this.GroupEvaluation_Load(ob,er);
            }
            catch(Exception exc)
            {
                MessageBox.Show(exc.ToString());
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 5)
            {
                var confirmResult = MessageBox.Show("Are you sure to delete this item ??",
                                     "Confirm Delete!!",
                                     MessageBoxButtons.YesNo);
                if (confirmResult == DialogResult.Yes)
                {
                    conn.Open();
                    string query = string.Format("Delete from GroupEvaluation where GroupId={0} and EvaluationId=(Select Id from Evaluation where Name='{1}')", Id, dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.ExecuteNonQuery();
                    conn.Close();



                    object ob = null;
                    EventArgs er = null;
                    this.GroupEvaluation_Load(ob, er);

                }
            }
            if (e.ColumnIndex == 4)
            {
                MessageBox.Show("Delete this one and Evaluate Again");
            }
        }
    }
}
