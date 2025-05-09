namespace HeavenTasteBakery
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            label1 = new Label();
            label2 = new Label();
            label4 = new Label();
            Password = new TextBox();
            Username = new TextBox();
            checkBox1 = new CheckBox();
            button1 = new Button();
            button2 = new Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = Color.Transparent;
            label1.Font = new Font("Bell MT", 16F, FontStyle.Underline);
            label1.ForeColor = Color.Transparent;
            label1.Location = new Point(220, 150);
            label1.Margin = new Padding(2, 0, 2, 0);
            label1.Name = "label1";
            label1.Size = new Size(133, 30);
            label1.TabIndex = 0;
            label1.Text = "UserName";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = Color.Transparent;
            label2.Font = new Font("Bell MT", 16F, FontStyle.Underline);
            label2.ForeColor = Color.Transparent;
            label2.Location = new Point(220, 239);
            label2.Margin = new Padding(2, 0, 2, 0);
            label2.Name = "label2";
            label2.Size = new Size(121, 30);
            label2.TabIndex = 1;
            label2.Text = "Password";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.BackColor = Color.Transparent;
            label4.Font = new Font("Cambria", 20F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label4.ForeColor = Color.Snow;
            label4.Location = new Point(268, 7);
            label4.Margin = new Padding(2, 0, 2, 0);
            label4.Name = "label4";
            label4.Size = new Size(320, 40);
            label4.TabIndex = 3;
            label4.Text = "Heaven Taste Bakery";
            // 
            // Password
            // 
            Password.BackColor = Color.White;
            Password.BorderStyle = BorderStyle.None;
            Password.Location = new Point(437, 243);
            Password.Margin = new Padding(2, 2, 2, 2);
            Password.Multiline = true;
            Password.Name = "Password";
            Password.PasswordChar = '*';
            Password.Size = new Size(172, 29);
            Password.TabIndex = 10;
            Password.Text = "admin";
            // 
            // Username
            // 
            Username.BackColor = Color.White;
            Username.BorderStyle = BorderStyle.None;
            Username.Location = new Point(437, 150);
            Username.Margin = new Padding(2, 2, 2, 2);
            Username.Multiline = true;
            Username.Name = "Username";
            Username.Size = new Size(172, 29);
            Username.TabIndex = 9;
            Username.Text = "admin";
            // 
            // checkBox1
            // 
            checkBox1.AutoSize = true;
            checkBox1.BackColor = Color.Transparent;
            checkBox1.Font = new Font("Modern No. 20", 10.999999F, FontStyle.Underline, GraphicsUnit.Point, 0);
            checkBox1.ForeColor = Color.Transparent;
            checkBox1.Location = new Point(437, 286);
            checkBox1.Margin = new Padding(2, 2, 2, 2);
            checkBox1.Name = "checkBox1";
            checkBox1.Size = new Size(151, 25);
            checkBox1.TabIndex = 11;
            checkBox1.Text = "Show Password";
            checkBox1.UseVisualStyleBackColor = false;
            checkBox1.CheckedChanged += checkBox1_CheckedChanged;
            // 
            // button1
            // 
            button1.BackColor = Color.Transparent;
            button1.BackgroundImageLayout = ImageLayout.Zoom;
            button1.FlatStyle = FlatStyle.Popup;
            button1.Font = new Font("Calisto MT", 14F, FontStyle.Underline, GraphicsUnit.Point, 0);
            button1.ForeColor = Color.White;
            button1.Location = new Point(268, 362);
            button1.Margin = new Padding(2, 2, 2, 2);
            button1.Name = "button1";
            button1.Size = new Size(108, 39);
            button1.TabIndex = 8;
            button1.Text = "Submit";
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.BackColor = Color.Transparent;
            button2.BackgroundImageLayout = ImageLayout.Zoom;
            button2.FlatStyle = FlatStyle.Popup;
            button2.Font = new Font("Calisto MT", 14F, FontStyle.Underline, GraphicsUnit.Point, 0);
            button2.ForeColor = Color.White;
            button2.Location = new Point(449, 362);
            button2.Margin = new Padding(2, 2, 2, 2);
            button2.Name = "button2";
            button2.Size = new Size(108, 39);
            button2.TabIndex = 12;
            button2.Text = "Cancel";
            button2.UseVisualStyleBackColor = false;
            button2.Click += button2_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(807, 438);
            Controls.Add(button2);
            Controls.Add(checkBox1);
            Controls.Add(Password);
            Controls.Add(Username);
            Controls.Add(button1);
            Controls.Add(label4);
            Controls.Add(label2);
            Controls.Add(label1);
            Margin = new Padding(2, 2, 2, 2);
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Form1";
            Load += Form1_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private Label label4;
        private TextBox Password;
        private TextBox Username;
        private CheckBox checkBox1;
        private Button button1;
        private Button button2;
    }
}
