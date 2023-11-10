namespace GFS_Spiel
{
    partial class MainGame
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle5 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle6 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle7 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle8 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle9 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle10 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle11 = new DataGridViewCellStyle();
            PlayerFieldGrid = new DataGridView();
            groupBox1 = new GroupBox();
            PlayerShipsInfoGrid = new DataGridView();
            hideBoard = new CheckBox();
            EnemyShipsInfoGrid = new DataGridView();
            groupBox2 = new GroupBox();
            ReadyBT = new Button();
            PlayerInfoLB = new Label();
            TurnArrowTB = new Label();
            EnemyInfoLB = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            CompleteBT = new Button();
            ClearBT = new Button();
            FieldControllGB = new GroupBox();
            EnemyFieldGrid = new DataGridView();
            ChangeDesignBT = new Button();
            ((System.ComponentModel.ISupportInitialize)PlayerFieldGrid).BeginInit();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)PlayerShipsInfoGrid).BeginInit();
            ((System.ComponentModel.ISupportInitialize)EnemyShipsInfoGrid).BeginInit();
            groupBox2.SuspendLayout();
            FieldControllGB.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)EnemyFieldGrid).BeginInit();
            SuspendLayout();
            // 
            // PlayerFieldGrid
            // 
            PlayerFieldGrid.AllowUserToAddRows = false;
            PlayerFieldGrid.AllowUserToDeleteRows = false;
            PlayerFieldGrid.AllowUserToResizeColumns = false;
            PlayerFieldGrid.AllowUserToResizeRows = false;
            PlayerFieldGrid.BackgroundColor = SystemColors.ControlDarkDark;
            PlayerFieldGrid.BorderStyle = BorderStyle.None;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = SystemColors.Control;
            dataGridViewCellStyle1.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle1.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.False;
            PlayerFieldGrid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            PlayerFieldGrid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            PlayerFieldGrid.ColumnHeadersVisible = false;
            PlayerFieldGrid.Cursor = Cursors.Cross;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = SystemColors.ControlLight;
            dataGridViewCellStyle2.Font = new Font("Segoe UI", 18F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle2.ForeColor = SystemColors.Control;
            dataGridViewCellStyle2.SelectionBackColor = SystemColors.GradientActiveCaption;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
            PlayerFieldGrid.DefaultCellStyle = dataGridViewCellStyle2;
            PlayerFieldGrid.GridColor = SystemColors.ControlText;
            PlayerFieldGrid.Location = new Point(120, 110);
            PlayerFieldGrid.MultiSelect = false;
            PlayerFieldGrid.Name = "PlayerFieldGrid";
            PlayerFieldGrid.ReadOnly = true;
            PlayerFieldGrid.RowHeadersVisible = false;
            PlayerFieldGrid.RowHeadersWidth = 62;
            PlayerFieldGrid.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleCenter;
            PlayerFieldGrid.RowsDefaultCellStyle = dataGridViewCellStyle3;
            PlayerFieldGrid.RowTemplate.Height = 25;
            PlayerFieldGrid.ScrollBars = ScrollBars.None;
            PlayerFieldGrid.Size = new Size(500, 500);
            PlayerFieldGrid.TabIndex = 0;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(PlayerShipsInfoGrid);
            groupBox1.ForeColor = SystemColors.Control;
            groupBox1.Location = new Point(120, 640);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(359, 209);
            groupBox1.TabIndex = 2;
            groupBox1.TabStop = false;
            groupBox1.Text = "Deine Schiffe";
            // 
            // PlayerShipsInfoGrid
            // 
            PlayerShipsInfoGrid.AllowUserToAddRows = false;
            PlayerShipsInfoGrid.AllowUserToDeleteRows = false;
            PlayerShipsInfoGrid.AllowUserToResizeColumns = false;
            PlayerShipsInfoGrid.AllowUserToResizeRows = false;
            PlayerShipsInfoGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            PlayerShipsInfoGrid.BackgroundColor = SystemColors.ControlLight;
            PlayerShipsInfoGrid.BorderStyle = BorderStyle.None;
            PlayerShipsInfoGrid.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = SystemColors.ControlDarkDark;
            dataGridViewCellStyle4.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle4.ForeColor = SystemColors.Control;
            dataGridViewCellStyle4.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = DataGridViewTriState.True;
            PlayerShipsInfoGrid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            PlayerShipsInfoGrid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            PlayerShipsInfoGrid.Cursor = Cursors.Cross;
            dataGridViewCellStyle5.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle5.BackColor = SystemColors.ControlLight;
            dataGridViewCellStyle5.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle5.ForeColor = SystemColors.Control;
            dataGridViewCellStyle5.SelectionBackColor = SystemColors.GradientActiveCaption;
            dataGridViewCellStyle5.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = DataGridViewTriState.False;
            PlayerShipsInfoGrid.DefaultCellStyle = dataGridViewCellStyle5;
            PlayerShipsInfoGrid.EnableHeadersVisualStyles = false;
            PlayerShipsInfoGrid.GridColor = SystemColors.ControlText;
            PlayerShipsInfoGrid.Location = new Point(6, 22);
            PlayerShipsInfoGrid.MultiSelect = false;
            PlayerShipsInfoGrid.Name = "PlayerShipsInfoGrid";
            PlayerShipsInfoGrid.ReadOnly = true;
            PlayerShipsInfoGrid.RowHeadersVisible = false;
            PlayerShipsInfoGrid.RowHeadersWidth = 62;
            PlayerShipsInfoGrid.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle6.Alignment = DataGridViewContentAlignment.MiddleCenter;
            PlayerShipsInfoGrid.RowsDefaultCellStyle = dataGridViewCellStyle6;
            PlayerShipsInfoGrid.RowTemplate.Height = 25;
            PlayerShipsInfoGrid.ScrollBars = ScrollBars.None;
            PlayerShipsInfoGrid.Size = new Size(347, 181);
            PlayerShipsInfoGrid.TabIndex = 4;
            // 
            // hideBoard
            // 
            hideBoard.AutoSize = true;
            hideBoard.Location = new Point(626, 110);
            hideBoard.Name = "hideBoard";
            hideBoard.Size = new Size(66, 19);
            hideBoard.TabIndex = 4;
            hideBoard.Text = "ALARM";
            hideBoard.UseVisualStyleBackColor = true;
            hideBoard.CheckedChanged += hideBoard_CheckedChanged;
            // 
            // EnemyShipsInfoGrid
            // 
            EnemyShipsInfoGrid.AllowUserToAddRows = false;
            EnemyShipsInfoGrid.AllowUserToDeleteRows = false;
            EnemyShipsInfoGrid.AllowUserToResizeColumns = false;
            EnemyShipsInfoGrid.AllowUserToResizeRows = false;
            EnemyShipsInfoGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            EnemyShipsInfoGrid.BackgroundColor = SystemColors.ControlLight;
            EnemyShipsInfoGrid.BorderStyle = BorderStyle.None;
            EnemyShipsInfoGrid.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            EnemyShipsInfoGrid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            EnemyShipsInfoGrid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            EnemyShipsInfoGrid.Cursor = Cursors.Cross;
            dataGridViewCellStyle7.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle7.BackColor = SystemColors.ControlLight;
            dataGridViewCellStyle7.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle7.ForeColor = SystemColors.Control;
            dataGridViewCellStyle7.SelectionBackColor = SystemColors.GradientActiveCaption;
            dataGridViewCellStyle7.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = DataGridViewTriState.False;
            EnemyShipsInfoGrid.DefaultCellStyle = dataGridViewCellStyle7;
            EnemyShipsInfoGrid.EnableHeadersVisualStyles = false;
            EnemyShipsInfoGrid.GridColor = SystemColors.ControlText;
            EnemyShipsInfoGrid.Location = new Point(6, 22);
            EnemyShipsInfoGrid.MultiSelect = false;
            EnemyShipsInfoGrid.Name = "EnemyShipsInfoGrid";
            EnemyShipsInfoGrid.ReadOnly = true;
            EnemyShipsInfoGrid.RowHeadersVisible = false;
            EnemyShipsInfoGrid.RowHeadersWidth = 62;
            EnemyShipsInfoGrid.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle8.Alignment = DataGridViewContentAlignment.MiddleCenter;
            EnemyShipsInfoGrid.RowsDefaultCellStyle = dataGridViewCellStyle8;
            EnemyShipsInfoGrid.RowTemplate.Height = 25;
            EnemyShipsInfoGrid.ScrollBars = ScrollBars.None;
            EnemyShipsInfoGrid.Size = new Size(347, 181);
            EnemyShipsInfoGrid.TabIndex = 4;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(EnemyShipsInfoGrid);
            groupBox2.ForeColor = SystemColors.Control;
            groupBox2.Location = new Point(771, 640);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(359, 209);
            groupBox2.TabIndex = 5;
            groupBox2.TabStop = false;
            groupBox2.Text = "Gegner Schiffe";
            // 
            // ReadyBT
            // 
            ReadyBT.AutoSize = true;
            ReadyBT.BackColor = SystemColors.ControlLight;
            ReadyBT.FlatStyle = FlatStyle.Popup;
            ReadyBT.Font = new Font("Segoe UI", 28F, FontStyle.Regular, GraphicsUnit.Pixel);
            ReadyBT.Location = new Point(509, 662);
            ReadyBT.Name = "ReadyBT";
            ReadyBT.Size = new Size(156, 181);
            ReadyBT.TabIndex = 18;
            ReadyBT.Text = "Bereit";
            ReadyBT.UseVisualStyleBackColor = false;
            ReadyBT.Click += ReadyBT_Click;
            // 
            // PlayerInfoLB
            // 
            PlayerInfoLB.AutoSize = true;
            PlayerInfoLB.Font = new Font("Segoe UI", 25F, FontStyle.Bold, GraphicsUnit.Pixel);
            PlayerInfoLB.Location = new Point(180, 66);
            PlayerInfoLB.Name = "PlayerInfoLB";
            PlayerInfoLB.Size = new Size(143, 35);
            PlayerInfoLB.TabIndex = 19;
            PlayerInfoLB.Text = "Auswählen";
            // 
            // TurnArrowTB
            // 
            TurnArrowTB.AutoSize = true;
            TurnArrowTB.BorderStyle = BorderStyle.FixedSingle;
            TurnArrowTB.Font = new Font("Segoe UI", 47F, FontStyle.Regular, GraphicsUnit.Pixel);
            TurnArrowTB.Location = new Point(663, 358);
            TurnArrowTB.Name = "TurnArrowTB";
            TurnArrowTB.Size = new Size(80, 64);
            TurnArrowTB.TabIndex = 20;
            TurnArrowTB.Text = "<-";
            // 
            // EnemyInfoLB
            // 
            EnemyInfoLB.AutoSize = true;
            EnemyInfoLB.Font = new Font("Segoe UI", 25F, FontStyle.Bold, GraphicsUnit.Pixel);
            EnemyInfoLB.Location = new Point(873, 66);
            EnemyInfoLB.Name = "EnemyInfoLB";
            EnemyInfoLB.Size = new Size(143, 35);
            EnemyInfoLB.TabIndex = 21;
            EnemyInfoLB.Text = "Auswählen";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 25F, FontStyle.Regular, GraphicsUnit.Pixel);
            label3.Location = new Point(120, 66);
            label3.Name = "label3";
            label3.Size = new Size(59, 35);
            label3.TabIndex = 22;
            label3.Text = "Du: ";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 25F, FontStyle.Regular, GraphicsUnit.Pixel);
            label4.Location = new Point(771, 66);
            label4.Name = "label4";
            label4.Size = new Size(108, 35);
            label4.TabIndex = 23;
            label4.Text = "Gegner: ";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 25F, FontStyle.Regular, GraphicsUnit.Pixel);
            label5.Location = new Point(667, 312);
            label5.Name = "label5";
            label5.Size = new Size(63, 35);
            label5.TabIndex = 24;
            label5.Text = "Zug:";
            // 
            // CompleteBT
            // 
            CompleteBT.BackColor = SystemColors.ControlLight;
            CompleteBT.FlatStyle = FlatStyle.Popup;
            CompleteBT.Font = new Font("Segoe UI", 28F, FontStyle.Regular, GraphicsUnit.Pixel);
            CompleteBT.Location = new Point(6, 22);
            CompleteBT.Name = "CompleteBT";
            CompleteBT.Size = new Size(124, 57);
            CompleteBT.TabIndex = 25;
            CompleteBT.Text = "Füllen";
            CompleteBT.UseVisualStyleBackColor = false;
            CompleteBT.Click += CompleteBT_Click;
            // 
            // ClearBT
            // 
            ClearBT.BackColor = SystemColors.ControlLight;
            ClearBT.FlatStyle = FlatStyle.Popup;
            ClearBT.Font = new Font("Segoe UI", 26F, FontStyle.Regular, GraphicsUnit.Pixel);
            ClearBT.Location = new Point(132, 22);
            ClearBT.Name = "ClearBT";
            ClearBT.Size = new Size(120, 57);
            ClearBT.TabIndex = 26;
            ClearBT.Text = "Löschen";
            ClearBT.UseVisualStyleBackColor = false;
            ClearBT.Click += ClearBT_Click;
            // 
            // FieldControllGB
            // 
            FieldControllGB.Controls.Add(CompleteBT);
            FieldControllGB.Controls.Add(ClearBT);
            FieldControllGB.ForeColor = SystemColors.Control;
            FieldControllGB.Location = new Point(362, 17);
            FieldControllGB.Name = "FieldControllGB";
            FieldControllGB.Size = new Size(258, 87);
            FieldControllGB.TabIndex = 27;
            FieldControllGB.TabStop = false;
            FieldControllGB.Text = "Feld Kontrollcenter";
            // 
            // EnemyFieldGrid
            // 
            EnemyFieldGrid.AllowUserToAddRows = false;
            EnemyFieldGrid.AllowUserToDeleteRows = false;
            EnemyFieldGrid.AllowUserToResizeColumns = false;
            EnemyFieldGrid.AllowUserToResizeRows = false;
            EnemyFieldGrid.BackgroundColor = SystemColors.ControlDarkDark;
            EnemyFieldGrid.BorderStyle = BorderStyle.None;
            dataGridViewCellStyle9.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle9.BackColor = SystemColors.Control;
            dataGridViewCellStyle9.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle9.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle9.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle9.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle9.WrapMode = DataGridViewTriState.False;
            EnemyFieldGrid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle9;
            EnemyFieldGrid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            EnemyFieldGrid.ColumnHeadersVisible = false;
            EnemyFieldGrid.Cursor = Cursors.Cross;
            dataGridViewCellStyle10.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle10.BackColor = SystemColors.ControlLight;
            dataGridViewCellStyle10.Font = new Font("Segoe UI", 18F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle10.ForeColor = SystemColors.Control;
            dataGridViewCellStyle10.SelectionBackColor = SystemColors.GradientActiveCaption;
            dataGridViewCellStyle10.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle10.WrapMode = DataGridViewTriState.True;
            EnemyFieldGrid.DefaultCellStyle = dataGridViewCellStyle10;
            EnemyFieldGrid.GridColor = SystemColors.ControlText;
            EnemyFieldGrid.Location = new Point(771, 110);
            EnemyFieldGrid.MultiSelect = false;
            EnemyFieldGrid.Name = "EnemyFieldGrid";
            EnemyFieldGrid.ReadOnly = true;
            EnemyFieldGrid.RowHeadersVisible = false;
            EnemyFieldGrid.RowHeadersWidth = 62;
            EnemyFieldGrid.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle11.Alignment = DataGridViewContentAlignment.MiddleCenter;
            EnemyFieldGrid.RowsDefaultCellStyle = dataGridViewCellStyle11;
            EnemyFieldGrid.RowTemplate.Height = 25;
            EnemyFieldGrid.ScrollBars = ScrollBars.None;
            EnemyFieldGrid.SelectionMode = DataGridViewSelectionMode.CellSelect;
            EnemyFieldGrid.Size = new Size(500, 500);
            EnemyFieldGrid.TabIndex = 28;
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
            ChangeDesignBT.Location = new Point(1271, 12);
            ChangeDesignBT.Name = "ChangeDesignBT";
            ChangeDesignBT.Size = new Size(118, 35);
            ChangeDesignBT.TabIndex = 29;
            ChangeDesignBT.Text = "Light Mode";
            ChangeDesignBT.UseVisualStyleBackColor = false;
            ChangeDesignBT.Click += ChangeDesignBT_Click;
            // 
            // MainGame
            // 
            AutoScaleMode = AutoScaleMode.Inherit;
            BackColor = SystemColors.ControlLightLight;
            ClientSize = new Size(1401, 861);
            Controls.Add(ChangeDesignBT);
            Controls.Add(EnemyFieldGrid);
            Controls.Add(FieldControllGB);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(EnemyInfoLB);
            Controls.Add(TurnArrowTB);
            Controls.Add(PlayerInfoLB);
            Controls.Add(ReadyBT);
            Controls.Add(groupBox2);
            Controls.Add(hideBoard);
            Controls.Add(groupBox1);
            Controls.Add(PlayerFieldGrid);
            ForeColor = SystemColors.Control;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "MainGame";
            Text = "Game";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)PlayerFieldGrid).EndInit();
            groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)PlayerShipsInfoGrid).EndInit();
            ((System.ComponentModel.ISupportInitialize)EnemyShipsInfoGrid).EndInit();
            groupBox2.ResumeLayout(false);
            FieldControllGB.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)EnemyFieldGrid).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView PlayerFieldGrid;
        private GroupBox groupBox1;
#pragma warning disable CS0169 // Das Feld "MainGame.ShipsInfoGrid" wird nie verwendet.
        private DataGridView ShipsInfoGrid;
#pragma warning restore CS0169 // Das Feld "MainGame.ShipsInfoGrid" wird nie verwendet.
        private CheckBox hideBoard;
#pragma warning disable CS0169 // Das Feld "MainGame.dataGridView1" wird nie verwendet.
        private DataGridView dataGridView1;
#pragma warning restore CS0169 // Das Feld "MainGame.dataGridView1" wird nie verwendet.
        private GroupBox groupBox2;
        private DataGridView PlayerShipsInfoGrid;
        private DataGridView EnemyShipsInfoGrid;
        private Button ReadyBT;
        private Label PlayerInfoLB;
        private Label TurnArrowTB;
        private Label EnemyInfoLB;
        private Label label3;
        private Label label4;
        private Label label5;
        private Button CompleteBT;
        private Button ClearBT;
#pragma warning disable CS0169 // Das Feld "MainGame.groupBox3" wird nie verwendet.
        private GroupBox groupBox3;
#pragma warning restore CS0169 // Das Feld "MainGame.groupBox3" wird nie verwendet.
        private GroupBox FieldControllGB;
        private DataGridView EnemyFieldGrid;
        private Button ChangeDesignBT;
    }
}