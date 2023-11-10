namespace GFS_Spiel
{
    public partial class SelectScreen : Form
    {
        public SelectScreen()
        {
            InitializeComponent();

            // Ändert das Design erstmal damit der ChangeDesignBT
            // das gegenteilige Design hat um ihn hervorzuheben
            Theme.ChangeMode();
            ChangeDesignBT_Click(null, null);
            //---------------------------------------------------
        }

        /// <summary>
        /// Wenn der JoinBT geklickt wird, dann wird ein <see cref="LobbyScreen"/> als <c>nicht Host</c> erstellt und angezeigt
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void JoinBT_Click(object sender, EventArgs e)
        {
            LobbyScreen ls = new LobbyScreen(false);
            Hide();
            ls.ShowDialog();
            Show();
        }
        /// <summary>
        /// Wenn der JoinBT geklickt wird, dann wird ein <see cref="LobbyScreen"/> als <c>Host</c> erstellt und angezeigt
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HostBT_Click(object sender, EventArgs e)
        {
            LobbyScreen ls = new LobbyScreen(true);
            Hide();
            ls.ShowDialog();
            Show();
        }

        /// <summary>
        /// Ändert das Design des aktuellen Forms
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChangeDesignBT_Click(object? sender, EventArgs? e)
        {
            // Speichert die Farben des Buttons im aktuellen Design
            Color btBackColor = Theme.GetThemeColor(ChangeDesignBT.BackColor);
            Color btForeColor = Theme.GetThemeColor(ChangeDesignBT.ForeColor);
            // Wechselt das Design
            Theme.ChangeMode();
            Theme.RefreshTheme(this);
            ChangeDesignBT.Text = Theme.IsDarkMode() ? "Light Mode" : "Dark Mode";
            // Setzt die Farben des Buttons auf die Farben des alten Designs um das andere Design zu zeigen
            ChangeDesignBT.BackColor = btBackColor;
            ChangeDesignBT.ForeColor = btForeColor;
        }
    }
}
