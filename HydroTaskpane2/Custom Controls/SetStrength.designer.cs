namespace HydroTaskpane2.Custom_Controls
{
    partial class SetStrength
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
            this.tensileTextBox = new System.Windows.Forms.TextBox();
            this.yieldTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.OkButton = new System.Windows.Forms.Button();
            this.CancelButton = new System.Windows.Forms.Button();
            this.tensileGroup = new System.Windows.Forms.GroupBox();
            this.yieldGroup = new System.Windows.Forms.GroupBox();
            this.tensileGroup.SuspendLayout();
            this.yieldGroup.SuspendLayout();
            this.SuspendLayout();
            // 
            // tensileTextBox
            // 
            this.tensileTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tensileTextBox.Location = new System.Drawing.Point(6, 19);
            this.tensileTextBox.Name = "tensileTextBox";
            this.tensileTextBox.Size = new System.Drawing.Size(232, 20);
            this.tensileTextBox.TabIndex = 0;
            this.tensileTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tensileTextBox_KeyPress);
            // 
            // yieldTextBox
            // 
            this.yieldTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.yieldTextBox.Location = new System.Drawing.Point(6, 19);
            this.yieldTextBox.Name = "yieldTextBox";
            this.yieldTextBox.Size = new System.Drawing.Size(232, 20);
            this.yieldTextBox.TabIndex = 1;
            this.yieldTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.yieldTextBox_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(256, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(39, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "N/mm²";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(256, 26);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(39, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "N/mm²";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(24, 25);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(290, 13);
            this.label5.TabIndex = 6;
            this.label5.Text = "Please provide the Strength values for the selected material:";
            // 
            // OkButton
            // 
            this.OkButton.BackColor = System.Drawing.SystemColors.ControlLight;
            this.OkButton.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.Info;
            this.OkButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.OkButton.ForeColor = System.Drawing.SystemColors.ControlText;
            this.OkButton.Location = new System.Drawing.Point(27, 213);
            this.OkButton.Name = "OkButton";
            this.OkButton.Size = new System.Drawing.Size(112, 23);
            this.OkButton.TabIndex = 7;
            this.OkButton.Text = "OK";
            this.OkButton.UseVisualStyleBackColor = false;
            this.OkButton.Click += new System.EventHandler(this.OkButton_Click);
            // 
            // CancelButton
            // 
            this.CancelButton.BackColor = System.Drawing.SystemColors.ControlLight;
            this.CancelButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.CancelButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CancelButton.ForeColor = System.Drawing.SystemColors.ControlText;
            this.CancelButton.Location = new System.Drawing.Point(147, 213);
            this.CancelButton.Name = "CancelButton";
            this.CancelButton.Size = new System.Drawing.Size(112, 23);
            this.CancelButton.TabIndex = 8;
            this.CancelButton.Text = "Cancel";
            this.CancelButton.UseVisualStyleBackColor = false;
            this.CancelButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // tensileGroup
            // 
            this.tensileGroup.Controls.Add(this.tensileTextBox);
            this.tensileGroup.Controls.Add(this.label1);
            this.tensileGroup.Location = new System.Drawing.Point(27, 130);
            this.tensileGroup.Name = "tensileGroup";
            this.tensileGroup.Size = new System.Drawing.Size(306, 56);
            this.tensileGroup.TabIndex = 9;
            this.tensileGroup.TabStop = false;
            this.tensileGroup.Text = "Tensile strength";
            // 
            // yieldGroup
            // 
            this.yieldGroup.Controls.Add(this.yieldTextBox);
            this.yieldGroup.Controls.Add(this.label2);
            this.yieldGroup.Location = new System.Drawing.Point(27, 59);
            this.yieldGroup.Name = "yieldGroup";
            this.yieldGroup.Size = new System.Drawing.Size(306, 56);
            this.yieldGroup.TabIndex = 10;
            this.yieldGroup.TabStop = false;
            this.yieldGroup.Text = "Yield strength";
            // 
            // SetStrength
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(365, 272);
            this.ControlBox = false;
            this.Controls.Add(this.yieldGroup);
            this.Controls.Add(this.CancelButton);
            this.Controls.Add(this.OkButton);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.tensileGroup);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SetStrength";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Set Strength";
            this.tensileGroup.ResumeLayout(false);
            this.tensileGroup.PerformLayout();
            this.yieldGroup.ResumeLayout(false);
            this.yieldGroup.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tensileTextBox;
        private System.Windows.Forms.TextBox yieldTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button OkButton;
        private System.Windows.Forms.Button CancelButton;
        private System.Windows.Forms.GroupBox tensileGroup;
        private System.Windows.Forms.GroupBox yieldGroup;
    }
}