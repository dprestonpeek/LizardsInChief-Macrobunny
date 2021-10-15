
namespace LICStatsSystem
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
            this.Weapons = new System.Windows.Forms.Label();
            this.Powerups = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.comboBox3 = new System.Windows.Forms.ComboBox();
            this.comboBox4 = new System.Windows.Forms.ComboBox();
            this.comboBox5 = new System.Windows.Forms.ComboBox();
            this.comboBox6 = new System.Windows.Forms.ComboBox();
            this.comboBox7 = new System.Windows.Forms.ComboBox();
            this.comboBox8 = new System.Windows.Forms.ComboBox();
            this.Agility = new System.Windows.Forms.Label();
            this.Strength = new System.Windows.Forms.Label();
            this.Spirit = new System.Windows.Forms.Label();
            this.Speed = new System.Windows.Forms.Label();
            this.Karma = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // Weapons
            // 
            this.Weapons.AutoSize = true;
            this.Weapons.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Weapons.Location = new System.Drawing.Point(481, 164);
            this.Weapons.Name = "Weapons";
            this.Weapons.Size = new System.Drawing.Size(137, 28);
            this.Weapons.TabIndex = 0;
            this.Weapons.Text = "Weapons:   +0";
            // 
            // Powerups
            // 
            this.Powerups.AutoSize = true;
            this.Powerups.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Powerups.Location = new System.Drawing.Point(481, 233);
            this.Powerups.Name = "Powerups";
            this.Powerups.Size = new System.Drawing.Size(140, 28);
            this.Powerups.TabIndex = 1;
            this.Powerups.Text = "Powerups:   +0";
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "None",
            "Speed",
            "Agility",
            "Strength",
            "Spirit",
            "Karma"});
            this.comboBox1.Location = new System.Drawing.Point(12, 65);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(198, 36);
            this.comboBox1.TabIndex = 2;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.SelectedItem);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label3.Location = new System.Drawing.Point(12, 23);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(95, 28);
            this.label3.TabIndex = 3;
            this.label3.Text = "Inventory";
            // 
            // comboBox2
            // 
            this.comboBox2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox2.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Items.AddRange(new object[] {
            "None",
            "Speed",
            "Agility",
            "Strength",
            "Spirit",
            "Karma"});
            this.comboBox2.Location = new System.Drawing.Point(12, 107);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(198, 36);
            this.comboBox2.TabIndex = 4;
            this.comboBox2.SelectedIndexChanged += new System.EventHandler(this.SelectedItem);
            // 
            // comboBox3
            // 
            this.comboBox3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox3.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.comboBox3.FormattingEnabled = true;
            this.comboBox3.Items.AddRange(new object[] {
            "None",
            "Speed",
            "Agility",
            "Strength",
            "Spirit",
            "Karma"});
            this.comboBox3.Location = new System.Drawing.Point(12, 149);
            this.comboBox3.Name = "comboBox3";
            this.comboBox3.Size = new System.Drawing.Size(198, 36);
            this.comboBox3.TabIndex = 5;
            this.comboBox3.SelectedIndexChanged += new System.EventHandler(this.SelectedItem);
            // 
            // comboBox4
            // 
            this.comboBox4.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox4.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.comboBox4.FormattingEnabled = true;
            this.comboBox4.Items.AddRange(new object[] {
            "None",
            "Speed",
            "Agility",
            "Strength",
            "Spirit",
            "Karma"});
            this.comboBox4.Location = new System.Drawing.Point(12, 191);
            this.comboBox4.Name = "comboBox4";
            this.comboBox4.Size = new System.Drawing.Size(198, 36);
            this.comboBox4.TabIndex = 6;
            this.comboBox4.SelectedIndexChanged += new System.EventHandler(this.SelectedItem);
            // 
            // comboBox5
            // 
            this.comboBox5.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox5.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.comboBox5.FormattingEnabled = true;
            this.comboBox5.Items.AddRange(new object[] {
            "None",
            "Speed",
            "Agility",
            "Strength",
            "Spirit",
            "Karma"});
            this.comboBox5.Location = new System.Drawing.Point(12, 233);
            this.comboBox5.Name = "comboBox5";
            this.comboBox5.Size = new System.Drawing.Size(198, 36);
            this.comboBox5.TabIndex = 7;
            this.comboBox5.SelectedIndexChanged += new System.EventHandler(this.SelectedItem);
            // 
            // comboBox6
            // 
            this.comboBox6.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox6.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.comboBox6.FormattingEnabled = true;
            this.comboBox6.Items.AddRange(new object[] {
            "None",
            "Speed",
            "Agility",
            "Strength",
            "Spirit",
            "Karma"});
            this.comboBox6.Location = new System.Drawing.Point(12, 275);
            this.comboBox6.Name = "comboBox6";
            this.comboBox6.Size = new System.Drawing.Size(198, 36);
            this.comboBox6.TabIndex = 8;
            this.comboBox6.SelectedIndexChanged += new System.EventHandler(this.SelectedItem);
            // 
            // comboBox7
            // 
            this.comboBox7.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox7.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.comboBox7.FormattingEnabled = true;
            this.comboBox7.Items.AddRange(new object[] {
            "None",
            "Speed",
            "Agility",
            "Strength",
            "Spirit",
            "Karma"});
            this.comboBox7.Location = new System.Drawing.Point(12, 317);
            this.comboBox7.Name = "comboBox7";
            this.comboBox7.Size = new System.Drawing.Size(198, 36);
            this.comboBox7.TabIndex = 9;
            this.comboBox7.SelectedIndexChanged += new System.EventHandler(this.SelectedItem);
            // 
            // comboBox8
            // 
            this.comboBox8.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox8.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.comboBox8.FormattingEnabled = true;
            this.comboBox8.Items.AddRange(new object[] {
            "None",
            "Speed",
            "Agility",
            "Strength",
            "Spirit",
            "Karma"});
            this.comboBox8.Location = new System.Drawing.Point(12, 359);
            this.comboBox8.Name = "comboBox8";
            this.comboBox8.Size = new System.Drawing.Size(198, 36);
            this.comboBox8.TabIndex = 10;
            this.comboBox8.SelectedIndexChanged += new System.EventHandler(this.SelectedItem);
            // 
            // Agility
            // 
            this.Agility.AutoSize = true;
            this.Agility.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Agility.Location = new System.Drawing.Point(256, 157);
            this.Agility.Name = "Agility";
            this.Agility.Size = new System.Drawing.Size(113, 28);
            this.Agility.TabIndex = 12;
            this.Agility.Text = "Agility:   +0";
            // 
            // Strength
            // 
            this.Strength.AutoSize = true;
            this.Strength.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Strength.Location = new System.Drawing.Point(250, 88);
            this.Strength.Name = "Strength";
            this.Strength.Size = new System.Drawing.Size(131, 28);
            this.Strength.TabIndex = 11;
            this.Strength.Text = "Strength:   +0";
            // 
            // Spirit
            // 
            this.Spirit.AutoSize = true;
            this.Spirit.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Spirit.Location = new System.Drawing.Point(266, 301);
            this.Spirit.Name = "Spirit";
            this.Spirit.Size = new System.Drawing.Size(103, 28);
            this.Spirit.TabIndex = 14;
            this.Spirit.Text = "Spirit:   +0";
            // 
            // Speed
            // 
            this.Speed.AutoSize = true;
            this.Speed.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Speed.Location = new System.Drawing.Point(258, 233);
            this.Speed.Name = "Speed";
            this.Speed.Size = new System.Drawing.Size(111, 28);
            this.Speed.TabIndex = 13;
            this.Speed.Text = "Speed:   +0";
            // 
            // Karma
            // 
            this.Karma.AutoSize = true;
            this.Karma.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Karma.Location = new System.Drawing.Point(258, 362);
            this.Karma.Name = "Karma";
            this.Karma.Size = new System.Drawing.Size(112, 28);
            this.Karma.TabIndex = 15;
            this.Karma.Text = "Karma:   +0";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(633, 450);
            this.Controls.Add(this.Karma);
            this.Controls.Add(this.Spirit);
            this.Controls.Add(this.Speed);
            this.Controls.Add(this.Agility);
            this.Controls.Add(this.Strength);
            this.Controls.Add(this.comboBox8);
            this.Controls.Add(this.comboBox7);
            this.Controls.Add(this.comboBox6);
            this.Controls.Add(this.comboBox5);
            this.Controls.Add(this.comboBox4);
            this.Controls.Add(this.comboBox3);
            this.Controls.Add(this.comboBox2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.Powerups);
            this.Controls.Add(this.Weapons);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label Weapons;
        private System.Windows.Forms.Label Powerups;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.ComboBox comboBox3;
        private System.Windows.Forms.ComboBox comboBox4;
        private System.Windows.Forms.ComboBox comboBox5;
        private System.Windows.Forms.ComboBox comboBox6;
        private System.Windows.Forms.ComboBox comboBox7;
        private System.Windows.Forms.ComboBox comboBox8;
        private System.Windows.Forms.Label Agility;
        private System.Windows.Forms.Label Strength;
        private System.Windows.Forms.Label Spirit;
        private System.Windows.Forms.Label Speed;
        private System.Windows.Forms.Label Karma;
    }
}

