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
    public partial class Advisor : Form
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ProjectA"].ToString());
        public Advisor()
        {
            InitializeComponent();
        }

        public void Advisor_Load(object sender, EventArgs e)
        {
            Advisorsview.Rows.Clear();
            //SqlConnection conn = new SqlConnection("Data Source=TALHAALI;Initial Catalog=ProjectA;User ID=sa;Password=talhaali");
            conn.Open();
            //string query = "Select FirstName,LastName,Contact,Email,DateOfBirth,Gender,Designation,Salary from Advisor INNER Join Person ON Advisor.Id=Person.Id";
            //string query = "Select FirstName,LastName,Contact,Email,DateOfBirth,Gender=Lookup.Value,Designation,Salary from Advisor INNER Join (Person INNER Join Lookup ON Lookup.Id=Person.Gender) ON Advisor.Id=Person.Id";

            //string query = string.Format("Select Id=seo.Id,FirstName=seo.FirstName,LastName=seo.LastName,Contact=seo.Contact,Email=seo.Email,DateOfBirth=seo.DateOfBirth,Gender=one.Gender,Designation=seo.DValue,Salary=seo.Salary from(Select Id = Person.Id, FirstName = Person.FirstName, LastName = Person.LastName, Contact = Person.Contact, Email = Person.Email, DateOfBirth = Person.DateOfBirth, Gender = Person.Gender, Designation = Advisor.Designation, DValue = Lookup.Value, Salary = Advisor.Salary from Person INNER Join(Advisor INNER Join Lookup ON Lookup.Id = Advisor.Designation) ON Advisor.Id = Person.Id) as seo INNer join (Select Id = Person.Id, FirstName = Person.FirstName, LastName = Person.LastName, Contact = Person.Contact, Email = Person.Email, DateOfBirth = Person.DateOfBirth, Gender = Lookup.Value, Designation = Advisor.Designation, DValue = Advisor.Designation, Salary = Advisor.Salary from Advisor INNER Join(Person INNER Join Lookup ON Lookup.Id = Person.Gender) ON Person.Id = Advisor.Id) as one On one.Id = seo.id");
            string query = "Select Id=seo.Id,FirstName=seo.FirstName,LastName=seo.LastName,Contact=seo.Contact,Email=seo.Email,DateOfBirth=seo.DateOfBirth, Gender=one.Gender,Designation=seo.DValue,Salary=seo.Salary from(Select Id = Person.Id, FirstName = Person.FirstName, LastName = Person.LastName, Contact = Person.Contact, Email = Person.Email, DateOfBirth = Person.DateOfBirth, Gender = Person.Gender, Designation = Advisor.Designation, DValue = Lookup.Value, Salary = Advisor.Salary from Person INNER Join (Advisor INNER Join Lookup ON Lookup.Id = Advisor.Designation) ON Advisor.Id = Person.Id) as seo  INNer join (Select Id = Person.Id, FirstName = Person.FirstName, LastName = Person.LastName, Contact = Person.Contact, Email = Person.Email, DateOfBirth = Person.DateOfBirth, Gender = Lookup.Value, Designation = Advisor.Designation,DValue = Advisor.Designation, Salary = Advisor.Salary from Advisor INNER Join(Person INNER Join Lookup ON Lookup.Id = Person.Gender) ON Person.Id = Advisor.Id) as one On one.Id = seo.id Union Select Id = Person.Id, FirstName = Person.FirstName,LastName = Person.LastName, Contact = Person.Contact, Email = Person.Email, DateOfBirth = Person.DateOfBirth,Gender = CAST(NULL AS VARCHAR(MAX)), Designation = Lookup.Value,Salary = Advisor.Salary from Person Inner join (Advisor INNER join Lookup On Lookup.Id = Advisor.Designation) On Advisor.Id = Person.Id where Gender is null";
            SqlCommand cmd = new SqlCommand(query, conn);
            //cmd.ExecuteNonQuery();
            //DataTable tbl = new DataTable();

            SqlDataReader reader = cmd.ExecuteReader();

            //Advisorsview.ColumnCount = 2;
            //Advisorsview.Columns[0].Name = "Title";
            //Advisorsview.Columns[1].Name = "Description";

            while (reader.Read())
            {
                ArrayList row = new ArrayList();
                row.Add(reader["FirstName"].ToString());
                row.Add(reader["LastName"].ToString());
                row.Add(reader["Contact"].ToString());
                row.Add(reader["Email"].ToString());
                row.Add(reader["Gender"].ToString());
                row.Add(reader["DateOfBirth"].ToString());
                row.Add(reader["Designation"].ToString());
                row.Add(reader["Salary"].ToString());

                Advisorsview.Rows.Add(row.ToArray());


            }
            //read.Fill(tbl);


            ////dataview.DataSource = tbl;

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
            //Advisorsview.Columns.Add(button);
            //Advisorsview.Columns.Add(button1);
            ////dataview.CellClick += dataview_CellClick;




            conn.Close();
        }

        private void AddAdvisor_Click(object sender, EventArgs e)
        {
            AddAdvisor advisor = new AddAdvisor();
            advisor.Show();
        }

        private void Advisorsview_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            AddAdvisor adv = new AddAdvisor();
            

            if (e.ColumnIndex == 8)
            {
                DateTime? date;
                int? pay;
                if (String.IsNullOrEmpty(Advisorsview.Rows[e.RowIndex].Cells[5].Value as String))
                {
                    //MessageBox.Show("Empty");
                    date = null;

                }
                else
                {
                    date = Convert.ToDateTime(Advisorsview.Rows[e.RowIndex].Cells[5].Value.ToString());
                }
                string gender = Advisorsview.Rows[e.RowIndex].Cells[4].Value.ToString();
                string desig = Advisorsview.Rows[e.RowIndex].Cells[6].Value.ToString();
                if (String.IsNullOrEmpty(Advisorsview.Rows[e.RowIndex].Cells[7].Value as String))
                {
                    MessageBox.Show("salary empty");
                    pay = null;
                }
                else
                {
                    pay = Convert.ToInt32(Advisorsview.Rows[e.RowIndex].Cells[7].Value);
                    

                }
                adv.Change("Update", (int)e.RowIndex,
                    Advisorsview.Rows[e.RowIndex].Cells[0].Value.ToString()
                    , Advisorsview.Rows[e.RowIndex].Cells[1].Value.ToString(),
                    Advisorsview.Rows[e.RowIndex].Cells[2].Value.ToString(),
                    Advisorsview.Rows[e.RowIndex].Cells[3].Value.ToString(),
                    date,
                    gender,
                    desig,
                    pay);
                adv.Show();
            }
            if (e.ColumnIndex == 9)
            {
                var confirmResult = MessageBox.Show("Are you sure to delete this item ??",
                                     "Confirm Delete!!",
                                     MessageBoxButtons.YesNo);
                if (confirmResult == DialogResult.Yes)
                {
                    //SqlConnection conn = new SqlConnection("Data Source=TALHAALI;Initial Catalog=ProjectA;User ID=sa;Password=talhaali");
                    conn.Open();

                    //String cm4 = string.Format("Select Id from Person where Email='{0}'", Advisorsview.Rows[e.RowIndex].Cells[3].Value.ToString());
                    //SqlCommand comm4 = new SqlCommand(cm4, conn);
                    //SqlDataReader reader2 = comm4.ExecuteReader();
                    //int studentid = 0;
                    //while (reader2.Read())
                    //{
                    //    studentid = Convert.ToInt32(reader2["Id"]);
                    //}
                    //conn.Close();
                    //conn.Open();

                    String cm1 = string.Format("Delete From ProjectAdvisor where AdvisorId=ANY(Select Id from Person where Email='{0}');Delete From Advisor where Id=ANY(Select Id from Person where Email='{0}');Delete From Person where Id=ANY(Select Id from Person where Email='{0}')", Advisorsview.Rows[e.RowIndex].Cells[3].Value.ToString());
                    SqlCommand comm1 = new SqlCommand(cm1, conn);
                    comm1.ExecuteNonQuery();

                    //String cm = string.Format("Delete From Person where Id=ANY(Select Id from Person where Email='{0}')", Advisorsview.Rows[e.RowIndex].Cells[3].Value.ToString());
                    //SqlCommand comm = new SqlCommand(cm, conn);
                    //var row = comm.ExecuteNonQuery();
                    conn.Close();

                    MessageBox.Show("Deleted");
                    object sende = null;
                    EventArgs er = null;
                    Advisorsview.Rows.Clear();
                    this.Advisor_Load(sende, er);


                }
                else
                {
                    MessageBox.Show("Not Deleted");
                }
            }
        }   
    }
}
