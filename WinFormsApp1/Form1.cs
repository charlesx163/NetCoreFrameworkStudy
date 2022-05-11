using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var mc1 = new MyClass() { Name = "mc1" };
            var mc2 = new MyClass() { Name = "mc2" };
            mc1 = null;
        }
    }

    internal class MyClass
    {
        ~ MyClass()
        {
        }

        public string Name { get; set; }
    }
}
