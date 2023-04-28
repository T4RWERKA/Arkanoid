using Classes;
using System.Windows.Forms;

namespace ClassesForms
{
    public partial class MainMenu : Form
    {
        private Game game;
        public MainMenu()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            Hide();
            game = new Game();
            game.InitGame();
            game.GameLoop();
            Show();
        }

        private async void btnLoad_Click(object sender, EventArgs e)
        {
            Hide();
            game = new Game();
            await game.Load();
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