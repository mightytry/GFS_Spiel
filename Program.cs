using StunTools;
using System.CodeDom;
using System.Diagnostics;
using System.Windows.Forms;

namespace GFS_Spiel
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            Application.EnableVisualStyles();
            ApplicationConfiguration.Initialize();
            Application.Run(new SelectScreen());
        }
        
   
    }

    /// <summary>
    /// Klasse die, die Farben für die jeweiligen Elemente des Programms enthält.
    /// </summary>
    public static class Theme 
    {
        /// <summary>
        /// Die Farben für den DarkMode
        /// Anfang ist von: https://www.color-hex.com/color-palette/98179
        /// </summary>
        static Color[] DarkModeColors =
        {
            //------------Farbe-----------    ----------Beschreibung-----------   -----Systemfarbe-----
            Color.FromArgb(255, 255, 255),  // Text                             Control
            Color.FromArgb(62, 62, 66),     // Hintergund 4                     ControlDark
            Color.FromArgb(45, 45, 48),     // Hintergund 3                     ControlDarkDark
            Color.FromArgb(37, 37, 38),     // Hintergund 2                     ControlLight
            Color.FromArgb(30, 30, 30),     // Hintergund                       ControlLightLight
            Color.FromArgb(0, 122, 204),    // Kontur                           ControlText     
            Color.FromArgb(22, 22, 50),     // Zu viele Schiffe                 Desktop
            Color.FromArgb(21, 55, 18),     // Richtige Anzahl Schiffe          GrayText
            Color.FromArgb(128, 30, 0),     // Zu wenig Schiffe                 Highlight
            Color.FromArgb(128, 128, 128),  // Schiff                           HighlightText
            Color.FromArgb(128, 0, 0),      // Versenkt                         HotTrack
        };
        /// <summary>
        /// Die Farben für den DarkMode
        /// Anfang ist von: https://www.color-hex.com/color-palette/106748
        /// </summary>
        static Color[] LightModeColors =
        {
            //------------Farbe-----------    ----------Beschreibung-----------   -----Systemfarbe-----
            Color.FromArgb(0, 0, 0),        // Text                             Control
            Color.FromArgb(147,148,165),    // Hintergund 4                     ControlDark
            Color.FromArgb(210,211,219),    // Hintergund 3                     ControlDarkDark
            Color.FromArgb(228,229,241),    // Hintergund 2                     ControlLight
            Color.FromArgb(250,250,250),    // Hintergund                       ControlLightLight
            Color.FromArgb(72,75,106),      // Kontur                           Desktop
            Color.FromArgb(173, 216, 230),  // Zu viele Schiffe                 GrayText
            Color.FromArgb(144, 238, 144),  // Richtige Anzahl Schiffe          Highlight
            Color.FromArgb(255, 182, 193),  // Zu wenig Schiffe                 HighlightText
            Color.FromArgb(128, 128, 128),  // Schiff                           HotTrack
            Color.FromArgb(255, 127, 80),   // Versenkt                         InactiveCaption
        };                                    
        static bool DarkMode = true;

        /// <summary>
        /// Wechselt zwischen DarkMode und LightMode
        /// </summary>
        public static void ChangeMode()
        {
            DarkMode = !DarkMode;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>true falls das Theme gerade Dunkel ist.</returns>
        public static bool IsDarkMode()
        {
            return DarkMode;
        }

        /// <summary>
        /// Gibt die jeweilige Theme Farbe für die aktuelle Farbe zurück
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static Color GetThemeColor(Color type)
        {
            // Es wird überprüft ob die Farbe eine Systemfarbe ist (also im Fromdesigner gesetzte) Farbe ist
            if (type.IsNamedColor)
            {
                // Wenn sie eine Systemfarbe ist, dann wird durch den Wert der Farbe in dem KnownColor Enum die Farbe zurückgegeben
                if (((int)type.ToKnownColor()- 5) >= LightModeColors.Length)
                    return type;
                return DarkMode ? DarkModeColors[(int)type.ToKnownColor() - 5] : LightModeColors[(int)type.ToKnownColor() - 5];
            }
            else
            {
                // Wenn nicht, dann wird der Index der Farbe in dem alten Colorarray gesucht und die jeweilige Farbe, des Indexes im neuen zurückgegeben
                int index = DarkMode ? Array.IndexOf(LightModeColors, type) : Array.IndexOf(DarkModeColors, type);
                if (index >= LightModeColors.Length || index < 0)
                    return type;
                return DarkMode ? DarkModeColors[index] : LightModeColors[index];
            }
        }
        /// <summary>
        /// Aktualisiert das Theme für das aktuelle Form/Control
        /// </summary>
        /// <param name="control"></param>
        public static void RefreshTheme(Control control)
        {
            // Für jedes Control wird die Funktion erneut aufgerufen
            foreach (Control c in control.Controls)
            {
                RefreshTheme(c);
            }
            // Wenn das Control ein DataGridView ist, dann werden die Farben der einzelnen Elemente gesetzt
            if (control.GetType() == typeof(DataGridView))
            {
                DataGridView dg = (DataGridView)control;
                dg.DefaultCellStyle.BackColor = GetThemeColor(dg.DefaultCellStyle.BackColor);
                dg.DefaultCellStyle.ForeColor = GetThemeColor(dg.DefaultCellStyle.ForeColor);
                // Für jede Zelle wird die Hintergrundfarbe und die Vordergrundfarbe gesetzt
                foreach (DataGridViewRow i in dg.Rows)
                {
                    foreach (DataGridViewCell c in i.Cells)
                    {
                        c.Style.ForeColor = GetThemeColor(c.Style.ForeColor);
                        c.Style.BackColor = GetThemeColor(c.Style.BackColor);
                    }
                }

                dg.ColumnHeadersDefaultCellStyle.BackColor = GetThemeColor(dg.ColumnHeadersDefaultCellStyle.BackColor);
                dg.ColumnHeadersDefaultCellStyle.ForeColor = GetThemeColor(dg.ColumnHeadersDefaultCellStyle.ForeColor);
                dg.GridColor = GetThemeColor(dg.GridColor);
                dg.BackgroundColor = GetThemeColor(dg.BackgroundColor);

                // Für jede Spalte wird die Hintergrundfarbe und die Vordergrundfarbe vom Header gesetzt
                foreach (DataGridViewColumn i in dg.Columns)
                {
                    i.HeaderCell.Style.BackColor = GetThemeColor(i.HeaderCell.Style.BackColor);
                    i.HeaderCell.Style.ForeColor = GetThemeColor(i.HeaderCell.Style.ForeColor);
                }
            }
            // Wenn das Control ein Button ist, dann wird die Randfarbe des Buttons gesetzt
            else if (control.GetType() == typeof(Button))
            {
                ((Button)control).FlatAppearance.BorderColor = GetThemeColor(((Button)control).FlatAppearance.BorderColor);
            }
            // Von jedem Control wird die Hintergrundfarbe und die Vordergrundfarbe gesetzt
            control.BackColor = GetThemeColor(control.BackColor);
            control.ForeColor = GetThemeColor(control.ForeColor);
        }
    }

}