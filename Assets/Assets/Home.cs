using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Assets
{
    public partial class Home : Form
    {
        public Home()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Student stu = new Student();
            stu.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Project prj = new Project();
            prj.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Advisor Adv = new Advisor();
            Adv.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Evaluation Evr = new Evaluation();
            Evr.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Groups grp = new Groups();
            grp.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Reports rpt = new Reports();
            rpt.Show();
        }
    }
}
