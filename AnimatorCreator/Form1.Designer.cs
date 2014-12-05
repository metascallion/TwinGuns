namespace AnimatorCreator
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
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.FileName = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.AnimationName = new System.Windows.Forms.TextBox();
            this.StartFrame = new System.Windows.Forms.TextBox();
            this.EndFrame = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.Rows = new System.Windows.Forms.TextBox();
            this.Cols = new System.Windows.Forms.TextBox();
            this.Animations = new System.Windows.Forms.ListBox();
            this.label7 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(397, 534);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(187, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Save Animator";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(40, 70);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Filename:";
            // 
            // FileName
            // 
            this.FileName.Location = new System.Drawing.Point(110, 67);
            this.FileName.Name = "FileName";
            this.FileName.Size = new System.Drawing.Size(205, 20);
            this.FileName.TabIndex = 2;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(795, 273);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(206, 23);
            this.button2.TabIndex = 3;
            this.button2.Text = "Add Animation";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(671, 74);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(82, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Animationname:";
            // 
            // AnimationName
            // 
            this.AnimationName.Location = new System.Drawing.Point(782, 70);
            this.AnimationName.Name = "AnimationName";
            this.AnimationName.Size = new System.Drawing.Size(219, 20);
            this.AnimationName.TabIndex = 5;
            // 
            // StartFrame
            // 
            this.StartFrame.Location = new System.Drawing.Point(782, 118);
            this.StartFrame.Name = "StartFrame";
            this.StartFrame.Size = new System.Drawing.Size(219, 20);
            this.StartFrame.TabIndex = 6;
            // 
            // EndFrame
            // 
            this.EndFrame.Location = new System.Drawing.Point(782, 166);
            this.EndFrame.Name = "EndFrame";
            this.EndFrame.Size = new System.Drawing.Size(219, 20);
            this.EndFrame.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(688, 125);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(58, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Startframe:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(688, 173);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(55, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "Endframe:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(55, 121);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(37, 13);
            this.label5.TabIndex = 11;
            this.label5.Text = "Rows:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(62, 173);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(30, 13);
            this.label6.TabIndex = 12;
            this.label6.Text = "Cols:";
            // 
            // Rows
            // 
            this.Rows.Location = new System.Drawing.Point(110, 118);
            this.Rows.Name = "Rows";
            this.Rows.Size = new System.Drawing.Size(205, 20);
            this.Rows.TabIndex = 13;
            // 
            // Cols
            // 
            this.Cols.Location = new System.Drawing.Point(110, 166);
            this.Cols.Name = "Cols";
            this.Cols.Size = new System.Drawing.Size(205, 20);
            this.Cols.TabIndex = 14;
            // 
            // Animations
            // 
            this.Animations.FormattingEnabled = true;
            this.Animations.Location = new System.Drawing.Point(110, 221);
            this.Animations.Name = "Animations";
            this.Animations.Size = new System.Drawing.Size(420, 95);
            this.Animations.TabIndex = 15;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(40, 221);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(61, 13);
            this.label7.TabIndex = 16;
            this.label7.Text = "Animations:";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1041, 583);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.Animations);
            this.Controls.Add(this.Cols);
            this.Controls.Add(this.Rows);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.EndFrame);
            this.Controls.Add(this.StartFrame);
            this.Controls.Add(this.AnimationName);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.FileName);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = "Animator Creater";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox FileName;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox AnimationName;
        private System.Windows.Forms.TextBox StartFrame;
        private System.Windows.Forms.TextBox EndFrame;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox Rows;
        private System.Windows.Forms.TextBox Cols;
        private System.Windows.Forms.ListBox Animations;
        private System.Windows.Forms.Label label7;
    }
}

