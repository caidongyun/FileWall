using System;
using System.Threading;
using System.Windows.Forms;
using VitaliiPianykh.FileWall.Client;
using VitaliiPianykh.FileWall.Shared;


namespace RemotingClient
{
    public partial class FormClient : Form
    {
        public FormClient()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Thread.Sleep(1000);

        }


        static void serviceInterface_AccessRequested(object sender, CoreAccessRequestedEventArgs e)
        {
            e.Allow = true;
            MessageBox.Show("AccessRequested", "Client");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var serviceInterface = (new ServiceInterfaceManager()).GetMarshalledInteface();

            var rules = serviceInterface.GetRuleset();

            AccessRequestedWrapper wrapper = new AccessRequestedWrapper(serviceInterface);
            wrapper.AccessRequested += serviceInterface_AccessRequested;
            button1.Enabled = false;
        }
    }
}
