using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html.simpleparser;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;

namespace Assets
{
    public partial class Reports : Form
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ProjectA"].ToString());

        public Reports()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();

            dataGridView1.ColumnCount = 4;
            dataGridView1.Columns[0].Name = "Project";
            dataGridView1.Columns[1].Name = "Advisor";
            dataGridView1.Columns[2].Name = "AdvisorDesignation";
            dataGridView1.Columns[3].Name = "AdvisorRole";
            conn.Open();

            //string query = string.Format("Select Title,Name=FirstName+' '+LastName,Role=AdvisorRole,Designation from Project INNER Join (ProjectAdvisor INNER Join (Advisor INNER Join Person On Person.Id=Advisor.Id) On Advisor.Id=ProjectAdvisor.AdvisorId) On Project.Id=ProjectAdvisor.ProjectId");
            //string query = "Select Title,Name=FirstName+' '+LastName,Role=AdvisorRole,Designationfrom Project INNER Join (ProjectAdvisor INNER Join (Advisor INNER Join Person On Person.Id = Advisor.Id) On Advisor.Id = ProjectAdvisor.AdvisorId) On Project.Id = ProjectAdvisor.ProjectId";
            Document doc = new Document();
            PdfPTable table = new PdfPTable(5);
            Paragraph parg = new Paragraph("Projects And Their Advisors\n"+Environment.NewLine);
            table.AddCell("NO");
            table.AddCell("Project Title");
            table.AddCell("Advisor Name");
            table.AddCell("Advisor Designation");
            table.AddCell("Advisor Role");
            try {
                PdfWriter.GetInstance(doc, new FileStream("Report1.pdf", FileMode.Create));
                doc.Open();
                string query = string.Format("Select Title=one.Title,Name=seo.Name,Designation=seo.Designation,Role=one.Role from (Select ProjectID ,Title,Name=FirstName+' '+LastName,Role=AdvisorRole,Designation=Lookup.Value from Lookup INNER Join(Project INNER Join(ProjectAdvisor INNER Join(Advisor INNER Join Person On Person.Id = Advisor.Id) On Advisor.Id = ProjectAdvisor.AdvisorId) On Project.Id = ProjectAdvisor.ProjectId) ON Lookup.Id = Advisor.Designation) as seo INNER join (Select ProjectId, Title, Name= FirstName + ' ' + LastName, Role = Lookup.Value, Designation from Lookup INNER Join(Project INNER Join(ProjectAdvisor INNER Join(Advisor INNER Join Person On Person.Id = Advisor.Id) On Advisor.Id = ProjectAdvisor.AdvisorId) On Project.Id = ProjectAdvisor.ProjectId) ON  Lookup.Id = ProjectAdvisor.AdvisorRole) as one ON seo.ProjectId = one.ProjectId");
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                int i = 1;
                while (reader.Read())
                {
                    ArrayList row = new ArrayList();
                    table.AddCell(i.ToString());
                    row.Add(reader["Title"].ToString());
                    table.AddCell(reader["Title"].ToString());
                    row.Add(reader["Name"].ToString());
                    table.AddCell(reader["Name"].ToString());

                    row.Add(reader["Designation"].ToString());
                    table.AddCell(reader["Designation"].ToString());
                    row.Add(reader["Role"].ToString());
                    table.AddCell(reader["Role"].ToString());
                    dataGridView1.Rows.Add(row.ToArray());
                    i = i + 1;

                }
                //conn.Close();


                doc.Add(parg);
                doc.Add(table);
                doc.Close();
                MessageBox.Show("Your File is Created Successfull in your Project folder");
            }
            catch(Exception exc)
            {
                MessageBox.Show("Can not access file because already open");
            }
            conn.Close();

            ////dataGridView1.Rows.Clear();
            //SqlDataAdapter da = new SqlDataAdapter("Select * from Project", conn);
            //DataSet ds = new DataSet();
            //da.Fill(ds);
            //dataGridView1.DataSource = ds;
            //conn.Close();
            //dataGridView1.DataBind();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();

            dataGridView1.ColumnCount = 5;
            dataGridView1.Columns[0].Name = "Project";
            dataGridView1.Columns[1].Name = "Evaluation";
            dataGridView1.Columns[2].Name = "TotalMarks";
            dataGridView1.Columns[3].Name = "ObtainedMarks";
            dataGridView1.Columns[4].Name = "TotalWeightage";
            conn.Open();
            //string query = string.Format("Select Title,Name=FirstName+' '+LastName,Role=AdvisorRole,Designation from Project INNER Join (ProjectAdvisor INNER Join (Advisor INNER Join Person On Person.Id=Advisor.Id) On Advisor.Id=ProjectAdvisor.AdvisorId) On Project.Id=ProjectAdvisor.ProjectId");
            //string query = "Select Title,Name=FirstName+' '+LastName,Role=AdvisorRole,Designationfrom Project INNER Join (ProjectAdvisor INNER Join (Advisor INNER Join Person On Person.Id = Advisor.Id) On Advisor.Id = ProjectAdvisor.AdvisorId) On Project.Id = ProjectAdvisor.ProjectId";
            Document doc = new Document();
            PdfPTable table = new PdfPTable(6);
            Paragraph parg = new Paragraph("Projects And Their Evaluations\n" + Environment.NewLine);
            table.AddCell("NO");
            table.AddCell("Project Title");
            table.AddCell("Evaluation Name");
            table.AddCell("TotalMarks");
            table.AddCell("ObtainedMarks");
            table.AddCell("TotalWeightage");
            try
            {
                PdfWriter.GetInstance(doc, new FileStream("Report2.pdf", FileMode.Create));
                doc.Open();
                string query = string.Format("Select Project=Project.Title,Evaluation=Evaluation.Name,TotalMarks,ObtainedMarks,TotalWeightage from Project INNER JOIN (GroupProject INNER JOIN (GroupEvaluation INNER JOIN Evaluation ON GroupEvaluation.EvaluationId=Evaluation.Id) ON GroupEvaluation.GroupId=GroupProject.GroupId) ON Project.Id=GroupProject.ProjectId");
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                int i = 1;
                while (reader.Read())
                {
                    ArrayList row = new ArrayList();
                    table.AddCell(i.ToString());
                    row.Add(reader["Project"].ToString());
                    table.AddCell(reader["Project"].ToString());
                    row.Add(reader["Evaluation"].ToString());
                    table.AddCell(reader["Evaluation"].ToString());

                    row.Add(reader["TotalMarks"].ToString());
                    table.AddCell(reader["TotalMarks"].ToString());
                    row.Add(reader["ObtainedMarks"].ToString());
                    table.AddCell(reader["ObtainedMarks"].ToString());
                    row.Add(reader["TotalWeightage"].ToString());
                    table.AddCell(reader["TotalWeightage"].ToString());

                    dataGridView1.Rows.Add(row.ToArray());
                    i++;

                }
                


                doc.Add(parg);
                doc.Add(table);
                doc.Close();
                MessageBox.Show("Your File is Created Successfull in your Project folder");
            }
            catch (Exception exc)
            {
                MessageBox.Show("Can not access file because already open");
            }
            conn.Close();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();

            dataGridView1.ColumnCount = 5;
            dataGridView1.Columns[0].Name = "GroupID";
            dataGridView1.Columns[1].Name = "StudentName";
            dataGridView1.Columns[2].Name = "Email";
            dataGridView1.Columns[3].Name = "Status";
            dataGridView1.Columns[4].Name = "RegistrationNo";
            conn.Open();
            //string query = string.Format("Select Title,Name=FirstName+' '+LastName,Role=AdvisorRole,Designation from Project INNER Join (ProjectAdvisor INNER Join (Advisor INNER Join Person On Person.Id=Advisor.Id) On Advisor.Id=ProjectAdvisor.AdvisorId) On Project.Id=ProjectAdvisor.ProjectId");
            //string query = "Select Title,Name=FirstName+' '+LastName,Role=AdvisorRole,Designationfrom Project INNER Join (ProjectAdvisor INNER Join (Advisor INNER Join Person On Person.Id = Advisor.Id) On Advisor.Id = ProjectAdvisor.AdvisorId) On Project.Id = ProjectAdvisor.ProjectId";
            Document doc = new Document();
            PdfPTable table = new PdfPTable(6);
            Paragraph parg = new Paragraph("Student Groups And Thrier Status\n" + Environment.NewLine);
            table.AddCell("NO");
            table.AddCell("Group ID");
            table.AddCell("Student Name");
            table.AddCell("Email");
            table.AddCell("Status");
            table.AddCell("RegistrationNo");
            try
            {
                PdfWriter.GetInstance(doc, new FileStream("Report3.pdf", FileMode.Create));
                doc.Open();
                string query = string.Format("Select GroupId=GroupStudent.GroupId,StudentName=FirstName+' '+LastName,Email,Status=Lookup.Value,RegistrationNo from Person INNER Join (Student Inner Join(GroupStudent INNER Join Lookup ON Lookup.Id = GroupStudent.Status) ON GroupStudent.StudentId = Student.Id) ON Student.Id = Person.Id");
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                int i = 1;
                while (reader.Read())
                {
                    ArrayList row = new ArrayList();
                    table.AddCell(i.ToString());
                    row.Add(reader["GroupId"].ToString());
                    table.AddCell(reader["GroupId"].ToString());
                    row.Add(reader["StudentName"].ToString());
                    table.AddCell(reader["StudentName"].ToString());

                    row.Add(reader["Email"].ToString());
                    table.AddCell(reader["Email"].ToString());
                    row.Add(reader["Status"].ToString());
                    table.AddCell(reader["Status"].ToString());
                    row.Add(reader["RegistrationNo"].ToString());
                    table.AddCell(reader["RegistrationNo"].ToString());

                    dataGridView1.Rows.Add(row.ToArray());
                    i++;
                }
                


                doc.Add(parg);
                doc.Add(table);
                doc.Close();
                MessageBox.Show("Your File is Created Successfull in your Project folder");
            }
            catch (Exception exc)
            {
                MessageBox.Show("Can not access file because already open");
            }
            conn.Close();
        }

        //private void button4_Click(object sender, EventArgs e)
        //{
        //    conn.Open();
        //    Document doc = new Document();
        //    PdfPTable table = new PdfPTable(6);
        //    Paragraph parg = new Paragraph("Student Groups And Thrier Status\n" + Environment.NewLine);
        //    table.AddCell("NO");
        //    table.AddCell("Student ID");
        //    table.AddCell("Student Name");
        //    table.AddCell("Group Id");
        //    PdfWriter.GetInstance(doc, new FileStream("Report4.pdf", FileMode.Create));
        //    doc.Open();
        //    //string query = string.Format("Select GroupId=GroupStudent.GroupId,StudentName=FirstName+' '+LastName,Email,Status=Lookup.Value,RegistrationNo from Person INNER Join (Student Inner Join(GroupStudent INNER Join Lookup ON Lookup.Id = GroupStudent.Status) ON GroupStudent.StudentId = Student.Id) ON Student.Id = Person.Id");
        //    string query = string.Format("Execute Problem4 '{0}'",textBox1.Text.ToString());
        //    SqlCommand cmd = new SqlCommand(query, conn);
        //    SqlDataReader reader = cmd.ExecuteReader();
        //    int i = 1;
        //    while (reader.Read())
        //    {
        //        ArrayList row = new ArrayList();
        //        table.AddCell(i.ToString());
        //        //row.Add(reader["studentid"].ToString());
        //        table.AddCell(reader["studentid"].ToString());
        //        //row.Add(reader["StudentName"].ToString());
        //        table.AddCell(reader["StudentName"].ToString());

        //        //row.Add(reader["Email"].ToString());
        //        table.AddCell(reader["GroupId"].ToString());
        //        //row.Add(reader["Status"].ToString());
        //        //table.AddCell(reader["Status"].ToString());
        //        //row.Add(reader["RegistrationNo"].ToString());
        //        //table.AddCell(reader["RegistrationNo"].ToString());

        //        //dataGridView1.Rows.Add(row.ToArray());
        //        i++;
        //    }



        //    doc.Add(parg);
        //    doc.Add(table);
        //    doc.Close();
        //    conn.Close();
        //    MessageBox.Show("Your File is Created Successfull in your Project folder");

        //    //table.AddCell("Status");
        //    //table.AddCell("RegistrationNo");
        //}

        private void Reports_Load(object sender, EventArgs e)
        {
            
        }
    }
}
