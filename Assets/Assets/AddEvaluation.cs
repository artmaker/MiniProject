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
    public partial class AddEvaluation : Form
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ProjectA"].ToString());
        Evaluation obje = (Evaluation)Application.OpenForms["Evaluation"];
        private string Type { get; set; }
        private string Title { get; set; }
        private int TotalMarks { get; set; }
        private float TotalWeightage { get; set; }
        public AddEvaluation()
        {
            InitializeComponent();
        }
        public void Refresher()
        {
            this.Close();
            Object obj = null;
            EventArgs env = null;
            obje.Evaluation_Load(obj, env);
        }

        public bool validator(Regex str, string txt)
        {
            return str.IsMatch(txt);
        }
        private void Save_Click(object sender, EventArgs e)
        {
            try
            {
                Regex titleregx = new Regex(@"^[a-zA-Z0-9]{1,200}$");
                Regex marksregx = new Regex(@"^[0-9]{1,3}$");
                Regex weightageregx = new Regex(@"^[0-9]{1,3}$");


                if (!validator(titleregx, name.Text.ToString()))
                {

                    throw new Exception("Title can only contain letters");

                }
                if (!validator(marksregx, marks.Text.ToString()) || (float)Convert.ToDouble(marks.Text)>100)
                {

                    throw new Exception("Marks should be 0-100 and only contain digits");

                }
                if (!validator(weightageregx, weightage.Text.ToString()) || (float)Convert.ToDouble(weightage.Text) > 100)
                {

                    throw new Exception("Weightage should be 0-100 and only contain digits");

                }
                if (Type != "Update")
                {
                    //SqlConnection conn = new SqlConnection("Data Source=TALHAALI;Initial Catalog=ProjectA;User ID=sa;Password=talhaali");
                    conn.Open();

                    String cm6 = string.Format("Select Id from Evaluation where Name='{0}'", name.Text.ToString());
                    SqlCommand comm6 = new SqlCommand(cm6, conn);

                    if (comm6.ExecuteScalar() != null)
                    {
                        throw new Exception("Name already exist");
                    }

                    string cm2 = string.Format("Insert into Evaluation values(@Name,@TotalMarks,@TotalWeightage)");
                    SqlCommand comm2 = new SqlCommand(cm2, conn);
                    comm2.Parameters.AddWithValue("@Name", name.Text);
                    comm2.Parameters.AddWithValue("@TotalMarks", (float)Convert.ToDouble(marks.Text));
                    comm2.Parameters.AddWithValue("@TotalWeightage", (float)Convert.ToDouble(weightage.Text));


                    int row = comm2.ExecuteNonQuery();
                    this.Refresher();
                    conn.Close();
                }
                else
                {
                    //SqlConnection conn = new SqlConnection("Data Source=TALHAALI;Initial Catalog=ProjectA;User ID=sa;Password=talhaali");
                    conn.Open();

                    String cm6 = string.Format("Select Id from Evaluation where Name='{0}' and Id=ANY(Select Id from Evaluation where Id != (Select Id from Evaluation where Name='{1}'))", name.Text.ToString(), Title);
                    SqlCommand comm6 = new SqlCommand(cm6, conn);
                    //SqlDataReader reader12 = comm6.ExecuteReader();
                    if (comm6.ExecuteScalar() != null)
                    {
                        throw new Exception("Name already exist");
                    }


                    string cm2 = string.Format("Update Evaluation set Name='{0}', TotalMarks={1},TotalWeightage={2} where Name='{3}' and TotalMarks={4} and TotalWeightage={5} ", 
                        name.Text, (float)Convert.ToDouble(marks.Text), (float)Convert.ToDouble(weightage.Text),Title,TotalMarks,TotalWeightage);
                    SqlCommand comm2 = new SqlCommand(cm2, conn);
                    //comm2.Parameters.AddWithValue("@Title", title.Text);
                    //comm2.Parameters.AddWithValue("@Description", description.Text);

                    int row = comm2.ExecuteNonQuery();
                    this.Refresher();
                    conn.Close();
                }
            }
            catch (Exception exc)
            {
                string line1 = exc.ToString().Split(new[] { '\r', '\n' }).FirstOrDefault();
                //button1.DialogResult = DialogResult.Cancel;
                MessageBox.Show(line1);
            }

        }
        public void Change(string type,string title,int totalmarks, float weightage)
        {
            Type = type;
            Title = title;
            TotalMarks = totalmarks;
            TotalWeightage = weightage;

        }

        private void AddEvaluation_Load(object sender, EventArgs e)
        {
            if (Type == "Update")
            {
                name.Text = Title;
                marks.Text = Convert.ToString(TotalMarks);
                weightage.Text = Convert.ToString(TotalWeightage);
            }

        }
    }
}
