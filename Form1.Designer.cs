using System;

namespace LagerKapazität
{
    partial class Form1
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
            this.textBox1Username = new System.Windows.Forms.TextBox();
            this.textBox2Passwort = new System.Windows.Forms.TextBox();
            this.checkBox1PasswortAnzeigen = new System.Windows.Forms.CheckBox();
            this.checkBox2PasswortVergessen = new System.Windows.Forms.CheckBox();
            this.button1Anmelden = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // textBox1Username
            // 
            this.textBox1Username.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.textBox1Username.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox1Username.ForeColor = System.Drawing.SystemColors.Desktop;
            this.textBox1Username.Location = new System.Drawing.Point(1194, 116);
            this.textBox1Username.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.textBox1Username.Multiline = true;
            this.textBox1Username.Name = "textBox1Username";
            this.textBox1Username.Size = new System.Drawing.Size(606, 76);
            this.textBox1Username.TabIndex = 0;
            this.textBox1Username.Text = "Benutzername eingeben";
            this.textBox1Username.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textBox1Username.TextChanged += new System.EventHandler(this.textBox1Username_TextChanged);
            // 
            // textBox2Passwort
            // 
            this.textBox2Passwort.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.textBox2Passwort.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox2Passwort.ForeColor = System.Drawing.SystemColors.Desktop;
            this.textBox2Passwort.Location = new System.Drawing.Point(1194, 284);
            this.textBox2Passwort.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.textBox2Passwort.Multiline = true;
            this.textBox2Passwort.Name = "textBox2Passwort";
            this.textBox2Passwort.Size = new System.Drawing.Size(606, 76);
            this.textBox2Passwort.TabIndex = 1;
            this.textBox2Passwort.Text = "Passwort eingeben";
            this.textBox2Passwort.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textBox2Passwort.TextChanged += new System.EventHandler(this.textBox2Passwort_TextChanged);
            // 
            // checkBox1PasswortAnzeigen
            // 
            this.checkBox1PasswortAnzeigen.AutoSize = true;
            this.checkBox1PasswortAnzeigen.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBox1PasswortAnzeigen.Location = new System.Drawing.Point(405, 459);
            this.checkBox1PasswortAnzeigen.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.checkBox1PasswortAnzeigen.Name = "checkBox1PasswortAnzeigen";
            this.checkBox1PasswortAnzeigen.Size = new System.Drawing.Size(353, 44);
            this.checkBox1PasswortAnzeigen.TabIndex = 2;
            this.checkBox1PasswortAnzeigen.Text = "Passwort anzeigen";
            this.checkBox1PasswortAnzeigen.UseVisualStyleBackColor = true;
            this.checkBox1PasswortAnzeigen.CheckedChanged += new System.EventHandler(this.checkBox1PasswortAnzeigen_CheckedChanged);
            // 
            // checkBox2PasswortVergessen
            // 
            this.checkBox2PasswortVergessen.AutoSize = true;
            this.checkBox2PasswortVergessen.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBox2PasswortVergessen.Location = new System.Drawing.Point(405, 609);
            this.checkBox2PasswortVergessen.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.checkBox2PasswortVergessen.Name = "checkBox2PasswortVergessen";
            this.checkBox2PasswortVergessen.Size = new System.Drawing.Size(373, 44);
            this.checkBox2PasswortVergessen.TabIndex = 3;
            this.checkBox2PasswortVergessen.Text = "Passwort vergessen";
            this.checkBox2PasswortVergessen.UseVisualStyleBackColor = true;
            this.checkBox2PasswortVergessen.CheckedChanged += new System.EventHandler(this.checkBox2PasswortVergessen_CheckedChanged);
            // 
            // button1Anmelden
            // 
            this.button1Anmelden.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.button1Anmelden.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1Anmelden.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.button1Anmelden.Location = new System.Drawing.Point(1374, 550);
            this.button1Anmelden.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.button1Anmelden.Name = "button1Anmelden";
            this.button1Anmelden.Size = new System.Drawing.Size(302, 111);
            this.button1Anmelden.TabIndex = 4;
            this.button1Anmelden.Text = "anmelden";
            this.button1Anmelden.UseVisualStyleBackColor = false;
            this.button1Anmelden.Click += new System.EventHandler(this.button1Anmelden_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(396, 162);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(519, 52);
            this.label1.TabIndex = 5;
            this.label1.Text = "Benutzername eingeben :";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(396, 298);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(416, 52);
            this.label2.TabIndex = 6;
            this.label2.Text = "Passwort eingeben :";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(2160, 1252);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button1Anmelden);
            this.Controls.Add(this.checkBox2PasswortVergessen);
            this.Controls.Add(this.checkBox1PasswortAnzeigen);
            this.Controls.Add(this.textBox2Passwort);
            this.Controls.Add(this.textBox1Username);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void checkBox2PasswortVergessen_CheckedChanged(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
        }

        private void textBox2Passwort_TextChanged(object sender, EventArgs e)
        {
            // Optional: Implementiere hier den Code, der ausgeführt werden soll, wenn sich der Text ändert
        }

        private void textBox1Username_TextChanged(object sender, EventArgs e)
        {
            // Optional: Implementiere hier den Code, der ausgeführt werden soll, wenn sich der Text ändert
        }


        #endregion

        private System.Windows.Forms.TextBox textBox1Username;
        private System.Windows.Forms.TextBox textBox2Passwort;
        private System.Windows.Forms.CheckBox checkBox1PasswortAnzeigen;
        private System.Windows.Forms.CheckBox checkBox2PasswortVergessen;
        private System.Windows.Forms.Button button1Anmelden;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}

