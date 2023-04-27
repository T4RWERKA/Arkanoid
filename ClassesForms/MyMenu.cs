using Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClassesForms
{
    public partial class MyMenu : Form
    {
        public event EventHandler ExitEvent;
        public event EventHandler ContinueEvent;
        public event EventHandler SaveEvent;
        public MyMenu()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Close();
            ExitEvent?.Invoke(this, EventArgs.Empty);
        }

        private void btnContinue_Click(object sender, EventArgs e)
        {
            Close();
            ContinueEvent?.Invoke(this, EventArgs.Empty);
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveEvent?.Invoke(this, EventArgs.Empty);
        }
    }
}
