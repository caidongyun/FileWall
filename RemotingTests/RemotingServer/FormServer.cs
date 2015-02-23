using System;
using System.Windows.Forms;
using VitaliiPianykh.FileWall.Shared;


namespace Server
{
    public partial class FormServer : Form
    {
        private ServiceInterface Interface;
        public FormServer()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Interface = ServiceInterface.Marshal(new Ruleset());
        }


        private void button1_Click(object sender, EventArgs e)
        {
            var ea = new CoreAccessRequestedEventArgs("TestOperation", "TestPath", "TestProcess");
            ea.Allow = false;

            Interface.OnAccessRequested(ea);

            if (ea.Allow == false)
                MessageBox.Show("Allow is false!");
        }
    }
}
