using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;

namespace Assets
{
    public partial class AddStudent : Form
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ProjectA"].ToString());
        Student obje = (Student)Application.OpenForms["Student"];
        public int ID { get; set; }
        public string FirstName { get; set; }

        public string LastName { get; set; }
        public string Contact { get; set; }
        public string Email { get; set; }
        public Nullable<DateTime> DateOfBirth { get; set; }
        public Nullable<int> Gender { get; set; }

        public string RegistrationNo { get; set; }
        public AddStudent()
        {
            InitializeComponent();
        }
        public void Refresher()
        {
            this.Close();
            Object obj = null;
            EventArgs env = null;
            obje.Form1_Load(obj, env);
        }

        public bool validator(Regex str, string txt)
        {
            return str.IsMatch(txt);
        }
        private void Add_Click(object sender, EventArgs e)
        {
            FirstName = firstname.Text;
            LastName = lastname.Text;
            Contact = contact.Text;
            //string emailee = Email;
            Email = email.Text;
            DateOfBirth = Convert.ToDateTime(dateofbirth.Text);
            string gen = GenderList.Text;
            //int Gender=0;
            RegistrationNo = registraionnumber.Text;
            try {




                Regex first = new Regex(@"^[a-zA-Z\s]{1,20}$");
                Regex last = new Regex(@"^[a-zA-Z\s]{0,20}$");
                //Regex contac = new Regex(@"^\d{4}-\d{7}$");
                Regex contac = new Regex(@"^[0-9]{0,20}$");
                Regex mail = new Regex(@"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z");
                //bool isEmail = Regex.IsMatch(emailString, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase);
                Regex Reg = new Regex(@"^[0-9]{4}[a-zA-Z]{2,5}[0-9]{1,3}$");
                if (!validator(first, FirstName))
                {
                    throw new Exception("FirstName only contain letters");
                }
                if (!validator(last, LastName))
                {
                    throw new Exception("LastName only contain letters");
                }
                if (!validator(contac, Contact))
                {
                    throw new Exception("Conatct only contain numbers ");
                }
                if (!validator(mail, Email))
                {
                    throw new Exception("Email should be like ali123@gmail.com");
                }
                if (!validator(Reg, RegistrationNo))
                {
                    throw new Exception("RegistrationNo should be like 2016ce5");
                }
                //SqlConnection conn = new SqlConnection("Data Source=TALHAALI;Initial Catalog=ProjectA;User ID=sa;Password=talhaali");
                conn.Open();


                String cm6 = string.Format("Select Id from Person where Email='{0}'", email.Text.ToString());
                SqlCommand comm6 = new SqlCommand(cm6, conn);
                
                if (comm6.ExecuteScalar()!=null)
                {
                    throw new Exception("Email already exist");
                }

                String cm7 = string.Format("Select Id from Student where RegistrationNo='{0}'", registraionnumber.Text.ToString());
                SqlCommand comm7 = new SqlCommand(cm7, conn);

                if (comm7.ExecuteScalar() != null)
                {
                    throw new Exception("RegistrationNo already exist");
                }
                conn.Close();
                //int? drt = null;
                conn.Open();
                if (!String.IsNullOrEmpty(gen as String))
                {
                    String cm = string.Format("Select Id from Lookup where Category='Gender' and Value='{0}'", gen.ToString());
                    SqlCommand comm = new SqlCommand(cm, conn);
                    SqlDataReader reader = comm.ExecuteReader();
                    while (reader.Read())
                    {
                        Gender = Convert.ToInt32(reader["Id"]);
                    }
                }
                else
                {
                    //DBNull genee = null;
                    Gender=null;
                }
                conn.Close();
                conn.Open();
                string cm2 = string.Format("Insert into Person values(@FirstName,@lastName,@Contact,@Email,@DateOfBirth,@Gender)");

                SqlCommand comm2 = new SqlCommand(cm2, conn);
                comm2.Parameters.AddWithValue("@FirstName", FirstName);
                comm2.Parameters.AddWithValue("@LastName", LastName);
                comm2.Parameters.AddWithValue("@Contact", Contact);
                comm2.Parameters.AddWithValue("@Email", Email);
                comm2.Parameters.AddWithValue("@DateOfBirth", DateOfBirth);
                if (Gender == null)
                {
                    comm2.Parameters.AddWithValue("@Gender", DBNull.Value);
                }
                else
                {
                    comm2.Parameters.AddWithValue("@Gender", Gender);

                }

                //comm2.Parameters.AddWithValue("@Gender", Gender);
                int row = comm2.ExecuteNonQuery();
                if (row > 0)
                {
                    String cm4 = string.Format("Select Id from Person where Email='{0}'", Email);
                    SqlCommand comm4 = new SqlCommand(cm4, conn);
                    SqlDataReader reader2 = comm4.ExecuteReader();
                    int studentid = 0;
                    while (reader2.Read())
                    {
                        studentid = Convert.ToInt32(reader2["Id"]);
                    }
                    conn.Close();
                    conn.Open();
                    string cm3 = string.Format("Insert into Student values(@Id,@RegistrationNo)");
                    SqlCommand comm5 = new SqlCommand(cm3, conn);
                    comm5.Parameters.AddWithValue("@Id", studentid);
                    comm5.Parameters.AddWithValue("@RegistrationNo", RegistrationNo);
                    comm5.ExecuteNonQuery();
                    this.Refresher();
                    conn.Close();

                }

                //string cm1 = string.Format("Update Student set RegistrationNo='{0}' where Id={1}", RegistrationNo, ID);
                //SqlCommand comm1 = new SqlCommand(cm1, conn);
                //comm1.ExecuteNonQuery();
                //conn.Close();
            }
            catch (Exception exc)
            {
                string line1 = exc.ToString().Split(new[] { '\r', '\n' }).FirstOrDefault();
                //button1.DialogResult = DialogResult.Cancel;
                MessageBox.Show(line1);
            }
        }
    }
}
