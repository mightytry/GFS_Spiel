namespace GFS_Spiel
{
    partial class LobbyScreen
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            DataGridViewCellStyle dataGridViewCellStyle5 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle6 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle7 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle8 = new DataGridViewCellStyle();
            ShipsInfoGrid = new DataGridView();
            PlayerIMG = new PictureBox();
            PlayerNameLB = new Label();
            OponentIMG = new PictureBox();
            OponentNameLB = new Label();
            label1 = new Label();
            CopyBT = new Button();
            CodeInputTB = new TextBox();
            ConnectBT = new Button();
            StartBT = new Button();
            CodeLB = new Label();
            StatusLB = new Label();
            label3 = new Label();
            PortLB = new Label();
            label2 = new Label();
            ChangeDesignBT = new Button();
            ((System.ComponentModel.ISupportInitialize)ShipsInfoGrid).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PlayerIMG).BeginInit();
            ((System.ComponentModel.ISupportInitialize)OponentIMG).BeginInit();
            SuspendLayout();
            // 
            // ShipsInfoGrid
            // 
            ShipsInfoGrid.AllowUserToAddRows = false;
            ShipsInfoGrid.AllowUserToDeleteRows = false;
            ShipsInfoGrid.AllowUserToResizeColumns = false;
            ShipsInfoGrid.AllowUserToResizeRows = false;
            ShipsInfoGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            ShipsInfoGrid.BackgroundColor = SystemColors.ControlLightLight;
            ShipsInfoGrid.BorderStyle = BorderStyle.None;
            ShipsInfoGrid.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle5.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = SystemColors.ControlDarkDark;
            dataGridViewCellStyle5.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle5.ForeColor = SystemColors.Control;
            dataGridViewCellStyle5.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = DataGridViewTriState.True;
            ShipsInfoGrid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            ShipsInfoGrid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            ShipsInfoGrid.Cursor = Cursors.Cross;
            dataGridViewCellStyle6.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle6.BackColor = SystemColors.ControlLight;
            dataGridViewCellStyle6.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle6.ForeColor = SystemColors.Control;
            dataGridViewCellStyle6.SelectionBackColor = SystemColors.GradientActiveCaption;
            dataGridViewCellStyle6.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = DataGridViewTriState.False;
            ShipsInfoGrid.DefaultCellStyle = dataGridViewCellStyle6;
            ShipsInfoGrid.EnableHeadersVisualStyles = false;
            ShipsInfoGrid.GridColor = SystemColors.ControlText;
            ShipsInfoGrid.Location = new Point(441, 257);
            ShipsInfoGrid.MultiSelect = false;
            ShipsInfoGrid.Name = "ShipsInfoGrid";
            dataGridViewCellStyle7.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = SystemColors.ActiveCaption;
            dataGridViewCellStyle7.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle7.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle7.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = DataGridViewTriState.True;
            ShipsInfoGrid.RowHeadersDefaultCellStyle = dataGridViewCellStyle7;
            ShipsInfoGrid.RowHeadersVisible = false;
            ShipsInfoGrid.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle8.Alignment = DataGridViewContentAlignment.MiddleCenter;
            ShipsInfoGrid.RowsDefaultCellStyle = dataGridViewCellStyle8;
            ShipsInfoGrid.RowTemplate.Height = 25;
            ShipsInfoGrid.ScrollBars = ScrollBars.None;
            ShipsInfoGrid.Size = new Size(347, 181);
            ShipsInfoGrid.TabIndex = 5;
            // 
            // PlayerIMG
            // 
            PlayerIMG.BackColor = SystemColors.ControlLightLight;
            PlayerIMG.Image = Properties.Resources.user;
            PlayerIMG.Location = new Point(31, 87);
            PlayerIMG.Name = "PlayerIMG";
            PlayerIMG.Size = new Size(87, 86);
            PlayerIMG.SizeMode = PictureBoxSizeMode.Zoom;
            PlayerIMG.TabIndex = 6;
            PlayerIMG.TabStop = false;
            // 
            // PlayerNameLB
            // 
            PlayerNameLB.AutoSize = true;
            PlayerNameLB.Font = new Font("Segoe UI", 27.75F, FontStyle.Regular, GraphicsUnit.Point);
            PlayerNameLB.Location = new Point(154, 109);
            PlayerNameLB.Name = "PlayerNameLB";
            PlayerNameLB.Size = new Size(69, 50);
            PlayerNameLB.TabIndex = 7;
            PlayerNameLB.Text = "Du";
            // 
            // OponentIMG
            // 
            OponentIMG.BackColor = SystemColors.ControlLightLight;
            OponentIMG.Image = Properties.Resources.chip;
            OponentIMG.Location = new Point(31, 277);
            OponentIMG.Name = "OponentIMG";
            OponentIMG.Size = new Size(87, 86);
            OponentIMG.SizeMode = PictureBoxSizeMode.Zoom;
            OponentIMG.TabIndex = 8;
            OponentIMG.TabStop = false;
            // 
            // OponentNameLB
            // 
            OponentNameLB.AutoSize = true;
            OponentNameLB.Font = new Font("Segoe UI", 27.75F, FontStyle.Regular, GraphicsUnit.Point);
            OponentNameLB.Location = new Point(154, 296);
            OponentNameLB.Name = "OponentNameLB";
            OponentNameLB.Size = new Size(187, 50);
            OponentNameLB.TabIndex = 9;
            OponentNameLB.Text = "Computer";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 15F, FontStyle.Regular, GraphicsUnit.Point);
            label1.Location = new Point(474, 51);
            label1.Name = "label1";
            label1.Size = new Size(139, 28);
            label1.TabIndex = 16;
            label1.Text = "Dein code ist:  ";
            // 
            // CopyBT
            // 
            CopyBT.AutoSize = true;
            CopyBT.BackColor = SystemColors.ControlLight;
            CopyBT.FlatAppearance.BorderColor = SystemColors.ControlText;
            CopyBT.FlatStyle = FlatStyle.Flat;
            CopyBT.Location = new Point(623, 86);
            CopyBT.Name = "CopyBT";
            CopyBT.Size = new Size(47, 27);
            CopyBT.TabIndex = 15;
            CopyBT.Text = "copy";
            CopyBT.UseVisualStyleBackColor = false;
            CopyBT.Click += CopyCodeBT_Click;
            // 
            // CodeInputTB
            // 
            CodeInputTB.BackColor = SystemColors.ControlDarkDark;
            CodeInputTB.BorderStyle = BorderStyle.FixedSingle;
            CodeInputTB.ForeColor = SystemColors.Control;
            CodeInputTB.Location = new Point(483, 152);
            CodeInputTB.MaxLength = 25;
            CodeInputTB.Name = "CodeInputTB";
            CodeInputTB.Size = new Size(100, 23);
            CodeInputTB.TabIndex = 14;
            // 
            // ConnectBT
            // 
            ConnectBT.AutoSize = true;
            ConnectBT.BackColor = SystemColors.ControlLight;
            ConnectBT.FlatAppearance.BorderColor = SystemColors.ControlText;
            ConnectBT.FlatStyle = FlatStyle.Flat;
            ConnectBT.Location = new Point(605, 150);
            ConnectBT.Name = "ConnectBT";
            ConnectBT.Size = new Size(75, 27);
            ConnectBT.TabIndex = 13;
            ConnectBT.Text = "Verbinden";
            ConnectBT.UseVisualStyleBackColor = false;
            ConnectBT.Click += ConnectBT_Click;
            // 
            // StartBT
            // 
            StartBT.AutoSize = true;
            StartBT.BackColor = SystemColors.ControlLight;
            StartBT.FlatAppearance.BorderColor = SystemColors.ControlText;
            StartBT.FlatAppearance.BorderSize = 2;
            StartBT.FlatStyle = FlatStyle.Flat;
            StartBT.Font = new Font("Segoe UI", 18F, FontStyle.Regular, GraphicsUnit.Point);
            StartBT.Location = new Point(304, 383);
            StartBT.Name = "StartBT";
            StartBT.Size = new Size(131, 62);
            StartBT.TabIndex = 17;
            StartBT.Text = "Starten";
            StartBT.UseVisualStyleBackColor = false;
            StartBT.Click += StartBT_Click;
            // 
            // CodeLB
            // 
            CodeLB.AutoSize = true;
            CodeLB.BackColor = SystemColors.ControlLightLight;
            CodeLB.BorderStyle = BorderStyle.FixedSingle;
            CodeLB.FlatStyle = FlatStyle.Popup;
            CodeLB.Font = new Font("Trebuchet MS", 15F, FontStyle.Regular, GraphicsUnit.Point);
            CodeLB.Location = new Point(605, 55);
            CodeLB.Name = "CodeLB";
            CodeLB.Size = new Size(78, 28);
            CodeLB.TabIndex = 18;
            CodeLB.Text = "LÄDT...";
            // 
            // StatusLB
            // 
            StatusLB.AutoSize = true;
            StatusLB.BorderStyle = BorderStyle.FixedSingle;
            StatusLB.FlatStyle = FlatStyle.Flat;
            StatusLB.Font = new Font("Trebuchet MS", 15F, FontStyle.Regular, GraphicsUnit.Point);
            StatusLB.Location = new Point(549, 216);
            StatusLB.Name = "StatusLB";
            StatusLB.Size = new Size(164, 28);
            StatusLB.TabIndex = 19;
            StatusLB.Text = "Nicht Verbunden";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 15F, FontStyle.Regular, GraphicsUnit.Point);
            label3.Location = new Point(474, 213);
            label3.Name = "label3";
            label3.Size = new Size(69, 28);
            label3.TabIndex = 20;
            label3.Text = "Status:";
            // 
            // PortLB
            // 
            PortLB.AutoSize = true;
            PortLB.BorderStyle = BorderStyle.FixedSingle;
            PortLB.Font = new Font("Trebuchet MS", 10F, FontStyle.Regular, GraphicsUnit.Point);
            PortLB.Location = new Point(518, 86);
            PortLB.Name = "PortLB";
            PortLB.Size = new Size(56, 20);
            PortLB.TabIndex = 21;
            PortLB.Text = "LÄDT...";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            label2.Location = new Point(483, 89);
            label2.Name = "label2";
            label2.Size = new Size(29, 13);
            label2.TabIndex = 22;
            label2.Text = "Port:";
            // 
            // ChangeDesignBT
            // 
            ChangeDesignBT.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            ChangeDesignBT.AutoSize = true;
            ChangeDesignBT.BackColor = SystemColors.ControlLight;
            ChangeDesignBT.FlatAppearance.BorderColor = SystemColors.ControlText;
            ChangeDesignBT.FlatAppearance.BorderSize = 2;
            ChangeDesignBT.Font = new Font("Segoe UI", 19F, FontStyle.Regular, GraphicsUnit.Pixel);
            ChangeDesignBT.ForeColor = SystemColors.Control;
            ChangeDesignBT.Location = new Point(670, 12);
            ChangeDesignBT.Name = "ChangeDesignBT";
            ChangeDesignBT.Size = new Size(118, 35);
            ChangeDesignBT.TabIndex = 23;
            ChangeDesignBT.Text = "Light Mode";
            ChangeDesignBT.UseVisualStyleBackColor = false;
            ChangeDesignBT.Click += ChangeDesignBT_Click;
            // 
            // LobbyScreen
            // 
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            BackColor = SystemColors.ControlLightLight;
            ClientSize = new Size(800, 450);
            Controls.Add(ChangeDesignBT);
            Controls.Add(label2);
            Controls.Add(PortLB);
            Controls.Add(label3);
            Controls.Add(StatusLB);
            Controls.Add(CodeLB);
            Controls.Add(StartBT);
            Controls.Add(label1);
            Controls.Add(CopyBT);
            Controls.Add(CodeInputTB);
            Controls.Add(ConnectBT);
            Controls.Add(OponentNameLB);
            Controls.Add(OponentIMG);
            Controls.Add(PlayerNameLB);
            Controls.Add(PlayerIMG);
            Controls.Add(ShipsInfoGrid);
            ForeColor = SystemColors.Control;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "LobbyScreen";
            Text = "LobbyScreen";
            Load += LobbyScreen_Load;
            ((System.ComponentModel.ISupportInitialize)ShipsInfoGrid).EndInit();
            ((System.ComponentModel.ISupportInitialize)PlayerIMG).EndInit();
            ((System.ComponentModel.ISupportInitialize)OponentIMG).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView ShipsInfoGrid;
        private PictureBox PlayerIMG;
        private Label PlayerNameLB;
        private PictureBox OponentIMG;
        private Label OponentNameLB;
        private Label label1;
        private Button CopyBT;
        private TextBox CodeInputTB;
        private Button ConnectBT;
        private Button StartBT;
        private Label CodeLB;
        private Label StatusLB;
        private Label label3;
        private Label PortLB;
        private Label label2;
        private Button ChangeDesignBT;
    }
}