namespace GFS_Spiel
{
    partial class SelectScreen
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
            HostBT = new Button();
            JoinBT = new Button();
            label1 = new Label();
            ChangeDesignBT = new Button();
            SuspendLayout();
            // 
            // HostBT
            // 
            HostBT.BackColor = SystemColors.ControlLight;
            HostBT.FlatAppearance.BorderColor = SystemColors.ControlText;
            HostBT.FlatAppearance.BorderSize = 2;
            HostBT.FlatStyle = FlatStyle.Flat;
            HostBT.Font = new Font("Segoe UI", 40F, FontStyle.Regular, GraphicsUnit.Point);
            HostBT.ForeColor = SystemColors.Control;
            HostBT.Location = new Point(93, 208);
            HostBT.Name = "HostBT";
            HostBT.Size = new Size(199, 86);
            HostBT.TabIndex = 0;
            HostBT.Text = "Host";
            HostBT.UseVisualStyleBackColor = false;
            HostBT.Click += HostBT_Click;
            // 
            // JoinBT
            // 
            JoinBT.BackColor = SystemColors.ControlLight;
            JoinBT.FlatAppearance.BorderColor = SystemColors.ControlText;
            JoinBT.FlatAppearance.BorderSize = 2;
            JoinBT.FlatStyle = FlatStyle.Flat;
            JoinBT.Font = new Font("Segoe UI", 40F, FontStyle.Regular, GraphicsUnit.Point);
            JoinBT.ForeColor = SystemColors.Control;
            JoinBT.Location = new Point(391, 208);
            JoinBT.Name = "JoinBT";
            JoinBT.Size = new Size(199, 86);
            JoinBT.TabIndex = 1;
            JoinBT.Text = "Join";
            JoinBT.UseVisualStyleBackColor = false;
            JoinBT.Click += JoinBT_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = SystemColors.ControlLightLight;
            label1.Font = new Font("Segoe UI", 30F, FontStyle.Regular, GraphicsUnit.Point);
            label1.ForeColor = SystemColors.Control;
            label1.Location = new Point(177, 65);
            label1.Name = "label1";
            label1.Size = new Size(332, 54);
            label1.TabIndex = 2;
            label1.Text = "Schiffe versenken";
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
            ChangeDesignBT.Location = new Point(554, 12);
            ChangeDesignBT.Name = "ChangeDesignBT";
            ChangeDesignBT.Size = new Size(118, 35);
            ChangeDesignBT.TabIndex = 3;
            ChangeDesignBT.Text = "Light Mode";
            ChangeDesignBT.UseVisualStyleBackColor = false;
            ChangeDesignBT.Click += ChangeDesignBT_Click;
            // 
            // SelectScreen
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ControlLightLight;
            ClientSize = new Size(684, 390);
            Controls.Add(ChangeDesignBT);
            Controls.Add(label1);
            Controls.Add(JoinBT);
            Controls.Add(HostBT);
            ForeColor = SystemColors.Control;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "SelectScreen";
            Text = "PreGame";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button HostBT;
        private Button JoinBT;
        private Label label1;
        private Button ChangeDesignBT;
    }
}