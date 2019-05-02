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
    public partial class AddAdvisor : Form
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ProjectA"].ToString());
        Advisor obje = (Advisor)Application.OpenForms["Advisor"];
        public int Id { get; set; }
        public string Type { get; set; }
        public string FirstName { get; set; }

        public string LastName { get; set; }
        public string Contact { get; set; }
        public string Email { get; set; }
        public Nullable<DateTime> DateOfBirth { get; set; }
        public string Gender { get; set; }

        public string Designation{ get; set; }
        public Nullable<int> Salary { get; set; }
        public AddAdvisor()
        {
            InitializeComponent();
        }
        

      public void Refresher()
        {
            this.Close();
            Object obj = null;
            EventArgs env = null;
            obje.Advisor_Load(obj, env);
        }

        public bool validator(Regex str, string txt)
        {
            return str.IsMatch(txt);
        }

        //Not Useable
       //private void Add_Click(object sender, EventArgs e)
       // {
       //     FirstName = firstname.Text;
       //     LastName = lastname.Text;
       //     Contact = contact.Text;
       //     string emailee = Email;
       //     Email = email.Text;
       //     DateOfBirth = Convert.ToDateTime(dateofbirth.Text);
       //     string gen = GenderList.Text;
       //     //Gender = 0;
       //     //if (gen == "male")
       //     //{
       //     //    Gender = 1;

       //     //}
       //     //else
       //     //{
       //     //    Gender = 2;
       //     //}
            
       //     //Designation = 0;

       //     //int Gender = 0;
       //     //RegistrationNo = registraionnumber.Text;
       //     try
       //     {
       //         Regex first = new Regex(@"^[a-zA-Z\s]{1,20}$");
       //         Regex last = new Regex(@"^[a-zA-Z\s]{1,20}$");
       //         Regex contac = new Regex(@"^\d{4}-\d{7}$");
       //         Regex mail = new Regex(@"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z");
       //         //bool isEmail = Regex.IsMatch(emailString, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase);
       //         //Regex Reg = new Regex(@"^[0-9]{4}[a-zA-Z]{2,5}[0-9]{1,3}$");
                
       //         if (!validator(first, FirstName))
       //         {
       //             throw new Exception("FirstName only contain letters");
       //         }
       //         if (!validator(last, LastName))
       //         {
       //             throw new Exception("LastName only contain letters");
       //         }
       //         if (!validator(contac, Contact))
       //         {
       //             throw new Exception("Conatct only contain numbers like 0300-1111111");
       //         }
       //         if (!validator(mail, Email))
       //         {
       //             throw new Exception("Email should be like ali123@gmail.com");
       //         }
       //         Salary = Convert.ToInt32(salary.Text);
       //         SqlConnection conn = new SqlConnection("Data Source=TALHAALI;Initial Catalog=ProjectA;User ID=sa;Password=talhaali");
       //         if (Type != "Update")
       //         {
       //             String cm6 = string.Format("Select Id from Advisor INNER Join Person ON Advisor.Id=Person.Id where Email='{0}'", email.Text.ToString());
       //             SqlCommand comm6 = new SqlCommand(cm6, conn);

       //             if (comm6.ExecuteScalar() != null)
       //             {
       //                 throw new Exception("Email already exist");
       //             }

                    
       //             //SqlConnection conn = new SqlConnection("Data Source=TALHAALI;Initial Catalog=ProjectA;User ID=sa;Password=talhaali");
       //             conn.Open();
       //             //String cm = string.Format("Select Id from Lookup where Category='Gender' and Value='{0}'", 
       //             //    gen.ToString());
       //             //SqlCommand comm = new SqlCommand(cm, conn);
       //             //SqlDataReader reader = comm.ExecuteReader();
       //             //while (reader.Read())
       //             //{
       //             //    Gender = Convert.ToInt32(reader["Id"]);
       //             //}
       //             //conn.Close();
       //             //conn.Open();
       //             string cm2 = string.Format("Insert into Person values(@FirstName,@lastName,@Contact,@Email,@DateOfBirth,(Select Id from Lookup where Category='Gender' and Value='{0}'))", GenderList.ToString());
       //             SqlCommand comm2 = new SqlCommand(cm2, conn);
       //             comm2.Parameters.AddWithValue("@FirstName", FirstName);
       //             comm2.Parameters.AddWithValue("@LastName", LastName);
       //             comm2.Parameters.AddWithValue("@Contact", Contact);
       //             comm2.Parameters.AddWithValue("@Email", Email);
       //             comm2.Parameters.AddWithValue("@DateOfBirth", DateOfBirth);
       //             //comm2.Parameters.AddWithValue("@Gender", Gender);
       //             int row = comm2.ExecuteNonQuery();
       //             conn.Close();
       //             if (row > 0)
       //             {
       //                 //String cm4 = string.Format("Select Id from Person where Email='{0}'", Email);
       //                 //SqlCommand comm4 = new SqlCommand(cm4, conn);
       //                 //SqlDataReader reader2 = comm4.ExecuteReader();
       //                 //int studentid = 0;
       //                 //while (reader2.Read())
       //                 //{
       //                 //    studentid = Convert.ToInt32(reader2["Id"]);
       //                 //}
       //                 //conn.Close();
       //                 conn.Open();
       //                 string cm3 = string.Format("Insert into Advisor values((Select Id from Person where Email='{0}'),@Designation,@Salary)", Email);
       //                 SqlCommand comm5 = new SqlCommand(cm3, conn);
       //                 comm5.Parameters.AddWithValue("@Designation", Designation);
       //                 comm5.Parameters.AddWithValue("@Salary", Salary);
       //                 comm5.ExecuteNonQuery();
       //                 this.Refresher();
       //                 conn.Close();

       //             }
       //         }
       //         else
       //         {


       //             conn.Open();
       //             String cm2 = string.Format("Select Id from Lookup where Category='Gender' and Value='{0}'", GenderList.Text);
       //             SqlCommand comm2 = new SqlCommand(cm2, conn);

       //             int Gend = (int)comm2.ExecuteScalar();

       //             String cm = string.Format("Update Person set FirstName='{0}', LastName='{1}',Contact='{2}',Email='{3}',DateOfBirth='{4}',Gender={5} where Id=(Select Id from Person where Email='{6}')", FirstName, LastName, Contact, Email, Convert.ToDateTime(DateOfBirth), Gend, emailee);
       //             SqlCommand comm = new SqlCommand(cm, conn);
       //             var row = comm.ExecuteNonQuery();
       //             string cm1 = string.Format("Update Advisor set Salary='{0}',Designation=(Select Id from Lookup where Value='{1}') where Id=(Select Id from Person where Email='{2}')", Salary, designation.Text.ToString(), Email);
       //             SqlCommand comm1 = new SqlCommand(cm1, conn);
       //             comm1.ExecuteNonQuery();

       //             this.Refresher();
       //             conn.Close();
       //         }
       //         //string cm1 = string.Format("Update Student set RegistrationNo='{0}' where Id={1}", RegistrationNo, ID);
       //         //SqlCommand comm1 = new SqlCommand(cm1, conn);
       //         //comm1.ExecuteNonQuery();
       //         //conn.Close();
       //     }
       //     catch (Exception exc)
       //     {
       //         string line1 = exc.ToString().Split(new[] { '\r', '\n' }).FirstOrDefault();
       //         //button1.DialogResult = DialogResult.Cancel;
       //         MessageBox.Show(line1);
       //     }

       // }

        

        public void Change(string type, int id, string first, string last,string conatc,string mail,DateTime? birth,string gende, string designate, int? pay)
        {
            Type = type;
            Id = id;
            FirstName = first;
            LastName = last;
            Contact = conatc;
            Email = mail;
            Designation = designate;
            Salary = pay;
            Gender = gende;
            DateOfBirth = birth;

        }

        private void Add_Click_1(object sender, EventArgs e)
        {
            
            ////FirstName = firstname.Text;
            //LastName = lastname.Text;
            //Contact = contact.Text;
            string emailee = Email;
            
            //Email = email.Text;
            //DateOfBirth = Convert.ToDateTime(dateofbirth.Text);
            //string gen = GenderList.Text;
            
            

            
            try
            {
                
                Regex first = new Regex(@"^[a-zA-Z\s]{1,20}$");
                Regex last = new Regex(@"^[a-zA-Z\s]{0,20}$");
                //Regex contac = new Regex(@"^\d{4}-\d{7}$");
                Regex contac = new Regex(@"^[0-9]{0,20}$");
                Regex mail = new Regex(@"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z");
                //bool isEmail = Regex.IsMatch(emailString, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase);
                //Regex Reg = new Regex(@"^[0-9]{4}[a-zA-Z]{2,5}[0-9]{1,3}$");
                Regex pay = new Regex(@"^[0-9]{0,8}$");
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
                    throw new Exception("Conatct only contain numbers like 0300-1111111");
                }
                if (!validator(mail, email.Text.ToString()))
                {
                    throw new Exception("Email should be like ali123@gmail.com");
                }
                if (designation.Text.ToString() == "")
                {
                    throw new Exception("Select Designation from list");
                }
                if (!validator(pay, salary.Text))
                {
                    throw new Exception("salary should be number and less than 99999999");
                }
                //Salary = Convert.ToInt32(salary.Text);
                if (Type != "Update")
                {

                    
                    //SqlConnection conn = new SqlConnection("Data Source=TALHAALI;Initial Catalog=ProjectA;User ID=sa;Password=talhaali");
                    conn.Open();

                    String cm6 = string.Format("Select Advisor.Id from Advisor INNER Join Person ON Advisor.Id=Person.Id where Email='{0}'", email.Text.ToString());
                    SqlCommand comm6 = new SqlCommand(cm6, conn);

                    if (comm6.ExecuteScalar() != null)
                    {
                        throw new Exception("Email already exist");
                    }
                    int income = 0;
                    if (!String.IsNullOrEmpty(salary.Text as String))
                    {
                        income = Convert.ToInt32(salary.Text);
                    }
                    string empty = GenderList.Text;
                    bool check = String.IsNullOrEmpty(empty as String);
                    if(check != true)
                    {
                        string cm2 = string.Format("Insert into Person values(@FirstName,@lastName,@Contact,@Email,@DateOfBirth,(Select Id from Lookup where Category='Gender' and Value='{0}'))", GenderList.Text.ToString());
                        SqlCommand comm2 = new SqlCommand(cm2, conn);
                        comm2.Parameters.AddWithValue("@FirstName", firstname.Text);
                        comm2.Parameters.AddWithValue("@LastName", lastname.Text);
                        comm2.Parameters.AddWithValue("@Contact", contact.Text);
                        comm2.Parameters.AddWithValue("@Email", email.Text);
                        comm2.Parameters.AddWithValue("@DateOfBirth", Convert.ToDateTime(dateofbirth.Text));
                        //comm2.Parameters.AddWithValue("@Gender", Gender);
                        int row = comm2.ExecuteNonQuery();
                        conn.Close();
                        if (row > 0)
                        {

                            conn.Open();
                            string cm3 = string.Format("Insert into Advisor values((Select Id from Person where Email='{0}'),(Select Id from Lookup where Category='DESIGNATION' and Value='{1}'),@Salary)", email.Text, designation.Text.ToString());
                            SqlCommand comm5 = new SqlCommand(cm3, conn);
                            //comm5.Parameters.AddWithValue("@Designation", Designation);
                            comm5.Parameters.AddWithValue("@Salary", income);
                            comm5.ExecuteNonQuery();
                            this.Refresher();
                            conn.Close();

                        }
                    }
                    //if (check != true)
                    //{
                    //    int j = 0;
                    //    string cm2 = string.Format("Insert into Person values(@FirstName,@lastName,@Contact,@Email,@DateOfBirth,(Select Id from Lookup where Category='Gender' and Value='{0}'))", GenderList.Text.ToString());
                    //    SqlCommand comm2 = new SqlCommand(cm2, conn);
                    //    comm2.Parameters.AddWithValue("@FirstName", firstname.Text);
                    //    comm2.Parameters.AddWithValue("@LastName", lastname.Text);
                    //    comm2.Parameters.AddWithValue("@Contact", contact.Text);
                    //    comm2.Parameters.AddWithValue("@Email", email.Text);
                    //    comm2.Parameters.AddWithValue("@DateOfBirth", Convert.ToDateTime(dateofbirth.Text));
                    //    //comm2.Parameters.AddWithValue("@Gender", Gender);
                    //    int row = comm2.ExecuteNonQuery();
                    //    conn.Close();
                    //    if (row > 0)
                    //    {

                    //        conn.Open();
                    //        string cm3 = string.Format("Insert into Advisor values((Select Id from Person where Email='{0}'),(Select Id from Lookup where Category='DESIGNATION' and Value='{1}'),@Salary)", email.Text, designation.Text.ToString());
                    //        SqlCommand comm5 = new SqlCommand(cm3, conn);
                    //        //comm5.Parameters.AddWithValue("@Designation", Designation);
                    //        comm5.Parameters.AddWithValue("@Salary", income);
                    //        comm5.ExecuteNonQuery();
                    //        this.Refresher();
                    //        conn.Close();

                    //    }
                    //}
                    else
                    {
                        string cm2 = string.Format("Insert into Person values(@FirstName,@lastName,@Contact,@Email,@DateOfBirth,@Gender)");
                        SqlCommand comm2 = new SqlCommand(cm2, conn);
                        comm2.Parameters.AddWithValue("@FirstName", firstname.Text);
                        comm2.Parameters.AddWithValue("@LastName", lastname.Text);
                        comm2.Parameters.AddWithValue("@Contact", contact.Text);
                        comm2.Parameters.AddWithValue("@Email", email.Text);
                        comm2.Parameters.AddWithValue("@DateOfBirth", Convert.ToDateTime(dateofbirth.Text));
                        comm2.Parameters.AddWithValue("@Gender", DBNull.Value);
                        int row = comm2.ExecuteNonQuery();
                        conn.Close();
                        if (row > 0)
                        {

                            conn.Open();
                            string cm3 = string.Format("Insert into Advisor values((Select Id from Person where Email='{0}'),(Select Id from Lookup where Category='DESIGNATION' and Value='{1}'),@Salary)", email.Text, designation.Text.ToString());
                            SqlCommand comm5 = new SqlCommand(cm3, conn);
                            //comm5.Parameters.AddWithValue("@Designation", Designation);
                            comm5.Parameters.AddWithValue("@Salary", income);
                            comm5.ExecuteNonQuery();
                            this.Refresher();
                            conn.Close();

                        }
                    }
                }
                else
                {
                    //SqlConnection conn = new SqlConnection("Data Source=TALHAALI;Initial Catalog=ProjectA;User ID=sa;Password=talhaali");
                    //SqlConnection conn = new SqlConnection("Data Source=TALHAALI;Initial Catalog=ProjectA;User ID=sa;Password=talhaali");
                    conn.Open();

                    String cm8 = string.Format("Select Id from Person where Email='{0}' and Id=ANY(Select Id from Person where Id != (Select Id from Person where Email='{1}'))", email.Text.ToString(), emailee);
                    SqlCommand comm8 = new SqlCommand(cm8, conn);
                    //SqlDataReader reader12 = comm6.ExecuteReader();
                    if (comm8.ExecuteScalar() != null)
                    {
                        throw new Exception("Email already exist");
                    }

                    int income = 0;
                    if(!String.IsNullOrEmpty(salary.Text as String))
                    {
                        income = Convert.ToInt32(salary.Text);
                    }

                     if (!String.IsNullOrEmpty(GenderList.Text as String))
                    {
                        String cm = string.Format("Update Person set FirstName='{0}', LastName='{1}',Contact='{2}',Email='{3}',DateOfBirth='{4}',Gender=(Select Id from Lookup where Value='{5}') where Id=(Select Id from Person where Email='{6}')", firstname.Text.ToString(), lastname.Text.ToString(), contact.Text.ToString(), email.Text.ToString(), Convert.ToDateTime(dateofbirth.Text.ToString()), GenderList.Text.ToString(), Email);
                        SqlCommand comm = new SqlCommand(cm, conn);
                        var row = comm.ExecuteNonQuery();
                        String cm5 = string.Format("Select Id from Lookup where Value='{0}'", designation.Text.ToString());
                        SqlCommand comm5 = new SqlCommand(cm5, conn);
                        int ide = (int)comm5.ExecuteScalar();
                        string cm1 = string.Format("Update Advisor set Salary={0}, Designation={1} where Id=(Select Id from Person where Email='{2}')", income, ide, email.Text.ToString());
                        SqlCommand comm1 = new SqlCommand(cm1, conn);
                        comm1.ExecuteNonQuery();
                    }
                    else
                    {
                        String cm = string.Format("Update Person set FirstName='{0}', LastName='{1}',Contact='{2}',Email='{3}',DateOfBirth='{4}',Gender=@Gender where Id=(Select Id from Person where Email='{5}')", firstname.Text.ToString(), lastname.Text.ToString(), contact.Text.ToString(), email.Text.ToString(), Convert.ToDateTime(dateofbirth.Text.ToString()),Email);
                        SqlCommand comm = new SqlCommand(cm, conn);
                        comm.Parameters.AddWithValue("@Gender", DBNull.Value);
                        var row = comm.ExecuteNonQuery();
                        String cm5 = string.Format("Select Id from Lookup where Value='{0}'", designation.Text.ToString());
                        SqlCommand comm5 = new SqlCommand(cm5, conn);
                        int ide = (int)comm5.ExecuteScalar();
                        string cm1 = string.Format("Update Advisor set Salary={0}, Designation={1} where Id=(Select Id from Person where Email='{2}')", income, ide, email.Text.ToString());
                        SqlCommand comm1 = new SqlCommand(cm1, conn);
                        comm1.ExecuteNonQuery();
                    }

                    this.Refresher();
                    conn.Close();
                }
                
            }
            catch (Exception exc)
            {
                string line1 = exc.ToString().Split(new[] { '\r', '\n' }).FirstOrDefault();
                //button1.DialogResult = DialogResult.Cancel;
                MessageBox.Show(exc.ToString());
            }

        }

        private void AddAdvisor_Load_1(object sender, EventArgs e)
        {
            
            if (Type == "Update")
            {
                //label1.Text = "Update Project";
                firstname.Text = FirstName;
                lastname.Text = LastName;
                contact.Text = Contact;
                email.Text = Email;
                dateofbirth.Text = Convert.ToString(DateOfBirth);
                GenderList.Text = Gender;
                designation.Text = Designation;
                //SqlConnection conn = new SqlConnection("Data Source=TALHAALI;Initial Catalog=ProjectA;User ID=sa;Password=talhaali");
                //conn.Open();
                //String cm3 = string.Format("Select Value from Lookup where Id={0}", Gender);

                //SqlCommand comm3 = new SqlCommand(cm3, conn);
                //GenderList.Text = comm3.ExecuteScalar().ToString();

                //String cm4 = string.Format("Select Value from Lookup where Id={0}", Designation);
                // SqlCommand comm4 = new SqlCommand(cm4, conn);
                //designation.Text = comm4.ExecuteScalar().ToString();
                //conn.Close();
                //designation.Text = Designation;
                salary.Text = Salary.ToString();

            }
        
    }
    }
    
}
