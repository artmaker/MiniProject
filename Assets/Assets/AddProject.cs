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
    public partial class AddProject : Form
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ProjectA"].ToString());
        Project obje = (Project)Application.OpenForms["Project"];

        public string Type { get; set; }
        public int Id { get; set; }
        
        public string Title { get; set; }
        public string Description { get; set; }

        public AddProject()
        {
            InitializeComponent();
        }

        public void Refresher()
        {
            this.Close();
            Object obj = null;
            EventArgs env = null;
            obje.Project_Load(obj, env);
        }

        public bool validator(Regex str,string txt)
        {
            return str.IsMatch(txt);
        }
        private void button1_Click(object sender, EventArgs e)
        {
            try {
                Regex regForTitle = new Regex(@"^[a-zA-Z\s]*$");
                

                if (!validator(regForTitle,title.Text.ToString()))
                {
                    
                    throw new Exception("Title can only contain letters");
                    
                }
                if (Type != "Update")
                {
                    //SqlConnection conn = new SqlConnection("Data Source=TALHAALI;Initial Catalog=ProjectA;User ID=sa;Password=talhaali");
                    conn.Open();

                    String cm6 = string.Format("Select Id from Project where Title='{0}'", title.Text.ToString());
                    SqlCommand comm6 = new SqlCommand(cm6, conn);

                    if (comm6.ExecuteScalar() != null)
                    {
                        throw new Exception("Title already exist");
                    }


                    string cm2 = string.Format("Insert into Project values(@Description,@Title)");
                    SqlCommand comm2 = new SqlCommand(cm2, conn);
                    comm2.Parameters.AddWithValue("@Title", title.Text);
                    comm2.Parameters.AddWithValue("@Description", description.Text);

                    int row = comm2.ExecuteNonQuery();
                    
                    conn.Close();
                    this.Refresher();
                }
                else
                {
                    //SqlConnection conn = new SqlConnection("Data Source=TALHAALI;Initial Catalog=ProjectA;User ID=sa;Password=talhaali");
                    conn.Open();


                    String cm6 = string.Format("Select Id from Project where Title='{0}' and Id=ANY(Select Id from Project where Id != (Select Id from Project where Title='{1}'))", title.Text.ToString(), Title);
                    SqlCommand comm6 = new SqlCommand(cm6, conn);
                    //SqlDataReader reader12 = comm6.ExecuteReader();
                    if (comm6.ExecuteScalar() != null)
                    {
                        throw new Exception("Title already exist");
                    }

                    string cm2 = string.Format("Update Project set Title='{0}', Description='{1}' where Id={2}", title.Text, description.Text, Id);
                    SqlCommand comm2 = new SqlCommand(cm2, conn);
                    //comm2.Parameters.AddWithValue("@Title", title.Text);
                    //comm2.Parameters.AddWithValue("@Description", description.Text);

                    int row = comm2.ExecuteNonQuery();
                    
                    conn.Close();
                    this.Refresher();
                }
            }
            catch(Exception exc)
            {
                string line1 = exc.ToString().Split(new[] { '\r', '\n' }).FirstOrDefault();
                //button1.DialogResult = DialogResult.Cancel;
                MessageBox.Show(exc.ToString());
            }

        }

        private void AddProject_Load(object sender, EventArgs e)
        {
            if (Type == "Update")
            {
                label1.Text = "Update Project";
                title.Text = Title;
                description.Text = Description;
            }
        }

        public void Change(string type,int id,string title,string descr)
        {
            Type = type;
            Id = id;
            Title = title;
            Description = descr;

        }
    }
}
