namespace PDherlper
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.handleVictimLSCheckBox = new System.Windows.Forms.CheckBox();
            this.button1 = new System.Windows.Forms.Button();
            this.handleAmmoLSCheckBox = new System.Windows.Forms.CheckBox();
            this.handleFlintCheckBox = new System.Windows.Forms.CheckBox();
            this.handleMulholandCheckBox = new System.Windows.Forms.CheckBox();
            this.handleIdleewodCheckBox = new System.Windows.Forms.CheckBox();
            this.nameTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.shouldShowPassCheckBox = new System.Windows.Forms.CheckBox();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.moscowTimeDiffDropdown = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.showWantedCheckBox = new System.Windows.Forms.CheckBox();
            this.enablePaintballNotificationCheckBox = new System.Windows.Forms.CheckBox();
            this.enableSMSInfoCheckBox = new System.Windows.Forms.CheckBox();
            this.logTextBox = new System.Windows.Forms.RichTextBox();
            this.RobCodeWord = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tagTextBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.moscowTimeDiffDropdown)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.handleVictimLSCheckBox);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.handleAmmoLSCheckBox);
            this.groupBox1.Controls.Add(this.handleFlintCheckBox);
            this.groupBox1.Controls.Add(this.handleMulholandCheckBox);
            this.groupBox1.Controls.Add(this.handleIdleewodCheckBox);
            this.groupBox1.Location = new System.Drawing.Point(13, 13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(195, 174);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Ограбления";
            this.groupBox1.Enter += new System.EventHandler(this.GroupBox1_Enter);
            // 
            // handleVictimLSCheckBox
            // 
            this.handleVictimLSCheckBox.AutoSize = true;
            this.handleVictimLSCheckBox.Checked = true;
            this.handleVictimLSCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.handleVictimLSCheckBox.Location = new System.Drawing.Point(6, 111);
            this.handleVictimLSCheckBox.Name = "handleVictimLSCheckBox";
            this.handleVictimLSCheckBox.Size = new System.Drawing.Size(70, 17);
            this.handleVictimLSCheckBox.TabIndex = 5;
            this.handleVictimLSCheckBox.Text = "Victim LS";
            this.handleVictimLSCheckBox.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.IndianRed;
            this.button1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.button1.Location = new System.Drawing.Point(6, 134);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(121, 31);
            this.button1.TabIndex = 4;
            this.button1.Text = "Сбросить тайминги";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // handleAmmoLSCheckBox
            // 
            this.handleAmmoLSCheckBox.AutoSize = true;
            this.handleAmmoLSCheckBox.Checked = true;
            this.handleAmmoLSCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.handleAmmoLSCheckBox.Location = new System.Drawing.Point(6, 88);
            this.handleAmmoLSCheckBox.Name = "handleAmmoLSCheckBox";
            this.handleAmmoLSCheckBox.Size = new System.Drawing.Size(75, 17);
            this.handleAmmoLSCheckBox.TabIndex = 3;
            this.handleAmmoLSCheckBox.Text = "AMMO LS";
            this.handleAmmoLSCheckBox.UseVisualStyleBackColor = true;
            // 
            // handleFlintCheckBox
            // 
            this.handleFlintCheckBox.AutoSize = true;
            this.handleFlintCheckBox.Checked = true;
            this.handleFlintCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.handleFlintCheckBox.Location = new System.Drawing.Point(6, 65);
            this.handleFlintCheckBox.Name = "handleFlintCheckBox";
            this.handleFlintCheckBox.Size = new System.Drawing.Size(69, 17);
            this.handleFlintCheckBox.TabIndex = 2;
            this.handleFlintCheckBox.Text = "Flint 24-7";
            this.handleFlintCheckBox.UseVisualStyleBackColor = true;
            // 
            // handleMulholandCheckBox
            // 
            this.handleMulholandCheckBox.AutoSize = true;
            this.handleMulholandCheckBox.Checked = true;
            this.handleMulholandCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.handleMulholandCheckBox.Location = new System.Drawing.Point(6, 42);
            this.handleMulholandCheckBox.Name = "handleMulholandCheckBox";
            this.handleMulholandCheckBox.Size = new System.Drawing.Size(99, 17);
            this.handleMulholandCheckBox.TabIndex = 1;
            this.handleMulholandCheckBox.Text = "Mulholand 24-7";
            this.handleMulholandCheckBox.UseVisualStyleBackColor = true;
            // 
            // handleIdleewodCheckBox
            // 
            this.handleIdleewodCheckBox.AutoSize = true;
            this.handleIdleewodCheckBox.Checked = true;
            this.handleIdleewodCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.handleIdleewodCheckBox.Location = new System.Drawing.Point(6, 19);
            this.handleIdleewodCheckBox.Name = "handleIdleewodCheckBox";
            this.handleIdleewodCheckBox.Size = new System.Drawing.Size(93, 17);
            this.handleIdleewodCheckBox.TabIndex = 0;
            this.handleIdleewodCheckBox.Text = "Idlewood 24-7";
            this.handleIdleewodCheckBox.UseVisualStyleBackColor = true;
            // 
            // nameTextBox
            // 
            this.nameTextBox.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.nameTextBox.Location = new System.Drawing.Point(715, 27);
            this.nameTextBox.Name = "nameTextBox";
            this.nameTextBox.Size = new System.Drawing.Size(211, 22);
            this.nameTextBox.TabIndex = 1;
            this.nameTextBox.Text = "Лейтенант";
            this.nameTextBox.TextChanged += new System.EventHandler(this.TextBox1_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(712, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(182, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Ваше звание (используется в /shi)";
            // 
            // shouldShowPassCheckBox
            // 
            this.shouldShowPassCheckBox.AutoSize = true;
            this.shouldShowPassCheckBox.Location = new System.Drawing.Point(715, 55);
            this.shouldShowPassCheckBox.Name = "shouldShowPassCheckBox";
            this.shouldShowPassCheckBox.Size = new System.Drawing.Size(133, 17);
            this.shouldShowPassCheckBox.TabIndex = 3;
            this.shouldShowPassCheckBox.Text = "Показывать паспорт";
            this.shouldShowPassCheckBox.UseVisualStyleBackColor = true;
            // 
            // linkLabel1
            // 
            this.linkLabel1.ActiveLinkColor = System.Drawing.Color.Silver;
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.linkLabel1.LinkColor = System.Drawing.Color.Silver;
            this.linkLabel1.Location = new System.Drawing.Point(769, 730);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(164, 13);
            this.linkLabel1.TabIndex = 4;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "by FoxPro [Dima_Galkin], Legacy";
            this.linkLabel1.VisitedLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LinkLabel1_LinkClicked);
            // 
            // moscowTimeDiffDropdown
            // 
            this.moscowTimeDiffDropdown.Location = new System.Drawing.Point(722, 685);
            this.moscowTimeDiffDropdown.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.moscowTimeDiffDropdown.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            -2147483648});
            this.moscowTimeDiffDropdown.Name = "moscowTimeDiffDropdown";
            this.moscowTimeDiffDropdown.Size = new System.Drawing.Size(29, 20);
            this.moscowTimeDiffDropdown.TabIndex = 5;
            this.moscowTimeDiffDropdown.ValueChanged += new System.EventHandler(this.NumericUpDown1_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(757, 687);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(181, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Разница с московским временем";
            this.label2.Click += new System.EventHandler(this.Label2_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.showWantedCheckBox);
            this.groupBox2.Controls.Add(this.enablePaintballNotificationCheckBox);
            this.groupBox2.Controls.Add(this.enableSMSInfoCheckBox);
            this.groupBox2.Location = new System.Drawing.Point(715, 101);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(218, 97);
            this.groupBox2.TabIndex = 7;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Дополнительно";
            // 
            // showWantedCheckBox
            // 
            this.showWantedCheckBox.AutoSize = true;
            this.showWantedCheckBox.Location = new System.Drawing.Point(15, 69);
            this.showWantedCheckBox.Name = "showWantedCheckBox";
            this.showWantedCheckBox.Size = new System.Drawing.Size(199, 17);
            this.showWantedCheckBox.TabIndex = 2;
            this.showWantedCheckBox.Text = "Отображение сводок над головой";
            this.showWantedCheckBox.UseVisualStyleBackColor = true;
            // 
            // enablePaintballNotificationCheckBox
            // 
            this.enablePaintballNotificationCheckBox.AutoSize = true;
            this.enablePaintballNotificationCheckBox.Location = new System.Drawing.Point(15, 46);
            this.enablePaintballNotificationCheckBox.Name = "enablePaintballNotificationCheckBox";
            this.enablePaintballNotificationCheckBox.Size = new System.Drawing.Size(161, 17);
            this.enablePaintballNotificationCheckBox.TabIndex = 1;
            this.enablePaintballNotificationCheckBox.Text = "Уведомление о пеинтболе";
            this.enablePaintballNotificationCheckBox.UseVisualStyleBackColor = true;
            // 
            // enableSMSInfoCheckBox
            // 
            this.enableSMSInfoCheckBox.AutoSize = true;
            this.enableSMSInfoCheckBox.Location = new System.Drawing.Point(15, 23);
            this.enableSMSInfoCheckBox.Name = "enableSMSInfoCheckBox";
            this.enableSMSInfoCheckBox.Size = new System.Drawing.Size(168, 17);
            this.enableSMSInfoCheckBox.TabIndex = 0;
            this.enableSMSInfoCheckBox.Text = "Входящее СМС над головой";
            this.enableSMSInfoCheckBox.UseVisualStyleBackColor = true;
            this.enableSMSInfoCheckBox.CheckedChanged += new System.EventHandler(this.CheckBox5_CheckedChanged);
            // 
            // logTextBox
            // 
            this.logTextBox.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.logTextBox.ForeColor = System.Drawing.Color.Coral;
            this.logTextBox.Location = new System.Drawing.Point(12, 647);
            this.logTextBox.Name = "logTextBox";
            this.logTextBox.ReadOnly = true;
            this.logTextBox.Size = new System.Drawing.Size(457, 96);
            this.logTextBox.TabIndex = 8;
            this.logTextBox.Text = "";
            // 
            // RobCodeWord
            // 
            this.RobCodeWord.Location = new System.Drawing.Point(12, 235);
            this.RobCodeWord.Name = "RobCodeWord";
            this.RobCodeWord.Size = new System.Drawing.Size(196, 20);
            this.RobCodeWord.TabIndex = 3;
            this.RobCodeWord.TextChanged += new System.EventHandler(this.RobCodeWord_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 213);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(272, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Что нужно написать в /fb, чтобы отправить тайминг";
            // 
            // tagTextBox
            // 
            this.tagTextBox.Location = new System.Drawing.Point(12, 281);
            this.tagTextBox.Name = "tagTextBox";
            this.tagTextBox.Size = new System.Drawing.Size(100, 20);
            this.tagTextBox.TabIndex = 9;
            this.tagTextBox.TextChanged += new System.EventHandler(this.TagTextBox_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 265);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(79, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "Тэг для рации";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.BackgroundImage = global::PDherlper.Properties.Resources.Logolspd;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.ClientSize = new System.Drawing.Size(945, 752);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.tagTextBox);
            this.Controls.Add(this.RobCodeWord);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.logTextBox);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.moscowTimeDiffDropdown);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.linkLabel1);
            this.Controls.Add(this.shouldShowPassCheckBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.nameTextBox);
            this.Controls.Add(this.groupBox1);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.DoubleBuffered = true;
            this.ForeColor = System.Drawing.Color.Chartreuse;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "Form1";
            this.Text = "PDHerlper by FoxPro для гребаного Samp-Rp.Ru ема-е";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.moscowTimeDiffDropdown)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox handleAmmoLSCheckBox;
        private System.Windows.Forms.CheckBox handleFlintCheckBox;
        private System.Windows.Forms.CheckBox handleMulholandCheckBox;
        private System.Windows.Forms.CheckBox handleIdleewodCheckBox;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox nameTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox shouldShowPassCheckBox;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown moscowTimeDiffDropdown;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox enableSMSInfoCheckBox;
        private System.Windows.Forms.CheckBox showWantedCheckBox;
        private System.Windows.Forms.CheckBox enablePaintballNotificationCheckBox;
        private System.Windows.Forms.CheckBox handleVictimLSCheckBox;
        private System.Windows.Forms.RichTextBox logTextBox;
        private System.Windows.Forms.TextBox RobCodeWord;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tagTextBox;
        private System.Windows.Forms.Label label4;
    }
}

