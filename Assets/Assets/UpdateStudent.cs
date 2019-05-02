using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Configuration;

namespace Assets
{
    

    public partial class UpdateStudent : Form
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ProjectA"].ToString());
        Student obje = (Student)Application.OpenForms["Student"];
        public int ID { get; set; }
        public string FirstName { get; set; }

        public string LastName { get; set; }
        public string Contact { get; set; }
        public string Email { get; set; }
        public Nullable<DateTime> DateOfBirth { get; set; }

        
        public string Gender { get; set; }

        public string RegistrationNo { get; set; }
        
        public UpdateStudent()
        {
            InitializeComponent();
        }
        public void Refresher()
        {
            //Student objee = (Student)Application.OpenForms["Student"];
            this.Close();
            Object obj = null;
            EventArgs env = null;
            //obje.Form1_Load(obj, env);
            obje.Form1_Load(obj, env);
        }

        public bool validator(Regex str, string txt)
        {
            return str.IsMatch(txt);
        }
        public void change(int id,string First,string Last,string Cont, string mail,DateTime? date,string gend,string RegNO)
        {
            
            ID = id;
            FirstName = First;
            firstname.Text = First;

            LastName = Last;
            lastname.Text = Last;

            Contact = Cont;
            contact.Text = Cont;

            Email = mail;
            email.Text = mail;

            DateOfBirth = date;
            dateofbirth.Text= Convert.ToString(date);

            Gender = gend;
            genderr.Text = Convert.ToString(gend);

            

            RegistrationNo = RegNO;
            registraionnumber.Text = RegNO;
            



        }

        private void button1_Click(object sender, EventArgs e)
        {
            //FirstName = firstname.Text;
            //LastName = lastname.Text;
            //Contact = contact.Text;
            //string mailee = Email;
            //string regeister = RegistrationNo;
            //Email = email.Text;
            //DateOfBirth = Convert.ToDateTime(dateofbirth.Text);
            //RegistrationNo = registraionnumber.Text;
           try {
                Regex first = new Regex(@"^[a-zA-Z\s]{1,20}$");
                Regex last = new Regex(@"^[a-zA-Z\s]{0,20}$");
                //Regex contac = new Regex(@"^\d{4}-\d{7}$");
                Regex contac = new Regex(@"^[0-9]{0,20}$");
                Regex mail = new Regex(@"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z");
                //bool isEmail = Regex.IsMatch(emailString, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase);
                Regex Reg = new Regex(@"^[0-9]{4}[a-zA-Z]{2,5}[0-9]{1,3}$");
                if (!validator(first, firstname.Text.ToString()))
                {
                    throw new Exception("FirstName only contain letters");
                }
                if (!validator(last, lastname.Text.ToString()))
                {
                    throw new Exception("LastName only contain letters");
                }
                if (!validator(contac, contact.Text.ToString()))
                {
                    throw new Exception("Conatct only contain numbers [0-20]");
                }
                if (!validator(mail, email.Text.ToString()))
                {
                    throw new Exception("Email should be like ali123@gmail.com");
                }
                if (!validator(Reg, registraionnumber.Text.ToString()))
                {
                    throw new Exception("RegistrationNo should be like 2016ce5");
                }


                //RegistrationNo = registraionnumber.Text;
                //SqlConnection conn = new SqlConnection("Data Source=TALHAALI;Initial Catalog=ProjectA;User ID=sa;Password=talhaali");
                //SqlConnection conn = new SqlConnection("Data Source=TALHAALI;Initial Catalog=ProjectA;User ID=sa;Password=talhaali");
                conn.Open();

                String cm6 = string.Format("Select Id from Person where Email='{0}' and Id=ANY(Select Id from Person where Id != (Select Id from Person where Email='{1}'))", email.Text.ToString(),Email);
                SqlCommand comm6 = new SqlCommand(cm6, conn);
                //SqlDataReader reader12 = comm6.ExecuteReader();
                if (comm6.ExecuteScalar() != null)
                {
                    throw new Exception("Email already exist");
                }

                String cm7 = string.Format("Select Id from Student where RegistrationNo='{0}' and Id=ANY(Select Id from Student where Id != (Select Id from Student where RegistrationNo='{1}'))", registraionnumber.Text.ToString(),RegistrationNo);
                SqlCommand comm7 = new SqlCommand(cm7, conn);

                if (comm7.ExecuteScalar() != null)
                {
                    throw new Exception("RegistrationNo already exist");
                }

                int Gend;
                if (!String.IsNullOrEmpty(genderr.Text as String))
                {
                    String cm2 = string.Format("Select Id from Lookup where Category='Gender' and Value='{0}'", genderr.Text);
                    SqlCommand comm2 = new SqlCommand(cm2, conn);

                    Gend = (int)comm2.ExecuteScalar();

                    String cm = string.Format("Update Person set FirstName='{0}', LastName='{1}',Contact='{2}',Email='{3}',DateOfBirth='{4}',Gender={5} where Id=(Select Id from Person where Email='{6}')", firstname.Text.ToString(), lastname.Text.ToString(), contact.Text.ToString(), email.Text.ToString(), Convert.ToDateTime(dateofbirth.Text.ToString()), Gend, Email);
                    SqlCommand comm = new SqlCommand(cm, conn);
                    var row = comm.ExecuteNonQuery();
                    string cm1 = string.Format("Update Student set RegistrationNo='{0}' where Id=(Select Id from Person where Email='{1}')", registraionnumber.Text.ToString(), email.Text.ToString());
                    SqlCommand comm1 = new SqlCommand(cm1, conn);
                    comm1.ExecuteNonQuery();

                    //conn.Close();
                    //conn.Open();
                }
                else
                {
                    String cm = string.Format("Update Person set FirstName='{0}', LastName='{1}',Contact='{2}',Email='{3}',DateOfBirth='{4}',Gender=@Gender where Id=(Select Id from Person where Email='{5}')", firstname.Text.ToString(), lastname.Text.ToString(), contact.Text.ToString(), email.Text.ToString(), Convert.ToDateTime(dateofbirth.Text.ToString()),Email);
                    SqlCommand comm = new SqlCommand(cm, conn);
                    comm.Parameters.AddWithValue("@Gender", DBNull.Value);
                    var row = comm.ExecuteNonQuery();
                    string cm1 = string.Format("Update Student set RegistrationNo='{0}' where Id=(Select Id from Person where Email='{1}')", registraionnumber.Text.ToString(), email.Text.ToString());
                    SqlCommand comm1 = new SqlCommand(cm1, conn);
                    comm1.ExecuteNonQuery();
                }
                //String cm = string.Format("Update Person set FirstName='{0}', LastName='{1}',Contact='{2}',Email='{3}',DateOfBirth='{4}',Gender={5} where Id=(Select Id from Person where Email='{6}')", firstname.Text.ToString(), lastname.Text.ToString(), contact.Text.ToString(),email.Text.ToString(), Convert.ToDateTime(dateofbirth.Text.ToString()), Gend, Email);
                //SqlCommand comm = new SqlCommand(cm, conn);
                //var row = comm.ExecuteNonQuery();
                //string cm1 = string.Format("Update Student set RegistrationNo='{0}' where Id=(Select Id from Person where Email='{1}')", registraionnumber.Text.ToString(), email.Text.ToString());
                //SqlCommand comm1 = new SqlCommand(cm1, conn);
                //comm1.ExecuteNonQuery();
                conn.Close();
                this.Refresher();
                
            }
            catch(Exception exc)
            {
                string line1 = exc.ToString().Split(new[] { '\r', '\n' }).FirstOrDefault();
                //button1.DialogResult = DialogResult.Cancel;
                MessageBox.Show(exc.ToString());
            }

        }
    }
}
