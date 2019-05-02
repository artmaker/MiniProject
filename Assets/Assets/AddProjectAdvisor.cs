using System;
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
    public partial class AddProjectAdvisor : Form
    {
        ProjectAdvisor obje = (ProjectAdvisor)Application.OpenForms["ProjectAdvisor"];

        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ProjectA"].ToString());
        public string Type { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }

        public string Role { get; set; }

        public DateTime AssignmentDate { get; set; }

        public AddProjectAdvisor()
        {
            InitializeComponent();
        }

        private void ProjectAdvisor_Load(object sender, EventArgs e)
        {
            conn.Open();
            string query = "Select Name=FirstName+' '+LastName from Advisor INNer Join Person on Person.Id=Advisor.Id";
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                comboBox2.Items.Add(reader["Name"]);
            }
            conn.Close();
            conn.Open();
            string query1 = "Select Name=Title from Project ";
            SqlCommand cmd1 = new SqlCommand(query1, conn);
            SqlDataReader reader1 = cmd1.ExecuteReader();
            while (reader1.Read())
            {
                comboBox1.Items.Add(reader1["Name"]);
            }
            conn.Close();
            conn.Open();
            string query2 = "Select Name=Value from Lookup where Category='ADVISOR_ROLE' ";
            SqlCommand cmd2 = new SqlCommand(query2, conn);
            SqlDataReader reader2 = cmd2.ExecuteReader();
            while (reader2.Read())
            {
                comboBox3.Items.Add(reader2["Name"]);
            }
            conn.Close();

            if (Type == "Update")
            {
                comboBox1.Text = Title;
                comboBox2.Text = Name;
                comboBox3.Text = Role;

            }
        }


        public void Change(string type,string nam,string title,string role)
        {
            Type = type;
            Name = nam;
            Title = title;
            Role = role;

        }
        public void Refresher()
        {
            this.Close();
            Object obj = null;
            EventArgs env = null;
            obje.ProjectAdvisor_Load(obj, env);
        }
        private void button1_Click(object sender, EventArgs e)
        {
            int pID;
            int aID;
            int lID;
            try {
                if (Type == "Update")
                {
                    if (comboBox1.Text == "" || comboBox2.Text == "" || comboBox3.Text == "")
                    {
                        throw new Exception("Empty values required");
                    }
                    conn.Open();
                    if (comboBox1.Text.ToString()!=Title || comboBox2.Text.ToString() != Name)
                    {
                        
                        String cm6 = string.Format("Select ProjectId from ProjectAdvisor where ProjectId=(Select Id from Project where Title='{0}') and AdvisorId=(Select Advisor.Id from Advisor INNER Join Person ON Person.Id=Advisor.Id where FirstName+' '+LastName='{1}')", comboBox1.Text.ToString(), comboBox2.Text.ToString());
                        SqlCommand comm6 = new SqlCommand(cm6, conn);

                        if (comm6.ExecuteScalar() != null)
                        {
                            conn.Close();
                            throw new Exception("Advisor already exist");
                        }
                    }
                    string query4 = string.Format("Update ProjectAdvisor set ProjectId=(Select Id from Project where Title='{0}'),AdvisorId=(Select Advisor.Id from Advisor INNER join Person On Person.Id=Advisor.Id where FirstName+' '+LastName='{1}'), AdvisorRole=(Select Lookup.Id from Lookup where Lookup.Value='{2}') where ProjectId=(Select Id from Project where Title='{3}') and AdvisorId=(Select Advisor.Id from Advisor INNER join Person On Person.Id=Advisor.Id where FirstName+' '+LastName='{4}')", comboBox1.Text.ToString(),comboBox2.Text.ToString(),comboBox3.Text.ToString(),Title,Name);
                    SqlCommand cmd4 = new SqlCommand(query4, conn);
                    cmd4.ExecuteNonQuery();
                    conn.Close();
                }
                if (Type != "Update")
                {
                    if(comboBox1.Text=="" || comboBox2.Text == "" || comboBox3.Text == "")
                    {
                        throw new Exception("Empty values required");
                    }

                    conn.Open();

                    String cm6 = string.Format("Select ProjectId from ProjectAdvisor where ProjectId=(Select Id from Project where Title='{0}') and AdvisorId=(Select Advisor.Id from Advisor INNER Join Person ON Person.Id=Advisor.Id where FirstName+' '+LastName='{1}')",comboBox1.Text.ToString(), comboBox2.Text.ToString());
                    SqlCommand comm6 = new SqlCommand(cm6, conn);

                    if (comm6.ExecuteScalar() != null)
                    {
                        conn.Close();
                        throw new Exception("Advisor already exist");
                    }

                    string query = string.Format("Select Id from Project where Title='{0}'", comboBox1.Text.ToString());
                    SqlCommand cmd = new SqlCommand(query, conn);
                    pID = (int)cmd.ExecuteScalar();

                    string query1 = string.Format("Select Advisor.Id from Advisor INNER join Person ON Person.Id=advisor.Id where FirstName+' '+LastName='{0}'", comboBox2.Text.ToString());
                    SqlCommand cmd1 = new SqlCommand(query1, conn);
                    aID = (int)cmd1.ExecuteScalar();

                    string query2 = string.Format("Select Id from Lookup where Value='{0}'", comboBox3.Text.ToString());
                    SqlCommand cmd2 = new SqlCommand(query2, conn);
                    lID = (int)cmd2.ExecuteScalar();

                    string query3 = string.Format("Insert Into ProjectAdvisor values({0},{1},{2},'{3}')", aID, pID, lID, DateTime.Now);
                    SqlCommand cmd3 = new SqlCommand(query3, conn);
                    cmd3.ExecuteNonQuery();
                    conn.Close();
                }
                //else
                //{
                //    conn.Open();
                //    string query = string.Format("Update ProjectAdvisor set ProjectId=(Select Id from Project whrer Title='{0}'),AdvisorId=(Select Advisor.Id from Advisor INNER join Person On Person.Id=Advisor.Id where FirstNmae+' '+LastName='{1}'), AdvisorRole=(Select Lookup.Id from Lookup INNER Join ProjectAdvisor On ProjectAdvisor.Id=Lookup.Id)");
                //    SqlCommand cmd = new SqlCommand(query, conn);
                //    cmd.ExecuteNonQuery();
                //    conn.Close();

                //}
                //this.Close();
                //obje.refresher();
                this.Refresher();
            }
            catch(Exception exc)
            {
                MessageBox.Show(exc.ToString());
            }
            




            ///
        }
    }
}
