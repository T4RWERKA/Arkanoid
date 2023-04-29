using Classes;
using System.Windows.Forms;

namespace ClassesForms
{
    public partial class MainMenu : Form
    {
        private Game? game;
        public MainMenu()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
        }

        private async void btnStart_Click(object sender, EventArgs e)
        {
            Hide();
            game = new Game();
            game.InitGame();
            await game.Save("begin_state");
            game.GameLoop();
            Show();
        }

        private async void btnLoad_Click(object sender, EventArgs e)
        {
            Hide();
            game = new Game();
            await game.Load("save");
            game.InitGame();
            game.GameLoop();
            Show();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void MainMenu_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                Application.Exit();
            }
        }
    }
}