using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AnimatorCreator
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Animator.rows = int.Parse(Rows.Text);
            Animator.cols = int.Parse(Cols.Text);

            Animator.createFile(FileName.Text);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Animator.animations.Add(new Animation(AnimationName.Text, int.Parse(StartFrame.Text), int.Parse(EndFrame.Text)));
            Animations.Items.Add("Name: " + AnimationName.Text + " StartFrame: " + StartFrame.Text + " EndFrame: " + EndFrame.Text);
        }
    }
}
