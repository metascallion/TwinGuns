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
            this.button1.Location = new System.Drawing.Point(529, 657);
            this.button1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(249, 28);
            this.button1.TabIndex = 0;
            this.button1.Text = "Save Animator";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(53, 86);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "Filename:";
            // 
            // FileName
            // 
            this.FileName.Location = new System.Drawing.Point(147, 82);
            this.FileName.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.FileName.Name = "FileName";
            this.FileName.Size = new System.Drawing.Size(272, 22);
            this.FileName.TabIndex = 2;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(1060, 336);
            this.button2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(275, 28);
            this.button2.TabIndex = 3;
            this.button2.Text = "Add Animation";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(895, 91);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(109, 17);
            this.label2.TabIndex = 4;
            this.label2.Text = "Animationname:";
            // 
            // AnimationName
            // 
            this.AnimationName.Location = new System.Drawing.Point(1043, 86);
            this.AnimationName.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.AnimationName.Name = "AnimationName";
            this.AnimationName.Size = new System.Drawing.Size(291, 22);
            this.AnimationName.TabIndex = 5;
            // 
            // StartFrame
            // 
            this.StartFrame.Location = new System.Drawing.Point(1043, 145);
            this.StartFrame.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.StartFrame.Name = "StartFrame";
            this.StartFrame.Size = new System.Drawing.Size(291, 22);
            this.StartFrame.TabIndex = 6;
            // 
            // EndFrame
            // 
            this.EndFrame.Location = new System.Drawing.Point(1043, 204);
            this.EndFrame.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.EndFrame.Name = "EndFrame";
            this.EndFrame.Size = new System.Drawing.Size(291, 22);
            this.EndFrame.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(917, 154);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(78, 17);
            this.label3.TabIndex = 8;
            this.label3.Text = "Startframe:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(917, 213);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(73, 17);
            this.label4.TabIndex = 9;
            this.label4.Text = "Endframe:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(73, 149);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(46, 17);
            this.label5.TabIndex = 11;
            this.label5.Text = "Rows:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(83, 213);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(39, 17);
            this.label6.TabIndex = 12;
            this.label6.Text = "Cols:";
            // 
            // Rows
            // 
            this.Rows.Location = new System.Drawing.Point(147, 145);
            this.Rows.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Rows.Name = "Rows";
            this.Rows.Size = new System.Drawing.Size(272, 22);
            this.Rows.TabIndex = 13;
            // 
            // Cols
            // 
            this.Cols.Location = new System.Drawing.Point(147, 204);
            this.Cols.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Cols.Name = "Cols";
            this.Cols.Size = new System.Drawing.Size(272, 22);
            this.Cols.TabIndex = 14;
            // 
            // Animations
            // 
            this.Animations.FormattingEnabled = true;
            this.Animations.ItemHeight = 16;
            this.Animations.Location = new System.Drawing.Point(147, 272);
            this.Animations.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Animations.Name = "Animations";
            this.Animations.Size = new System.Drawing.Size(559, 116);
            this.Animations.TabIndex = 15;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(53, 272);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(81, 17);
            this.label7.TabIndex = 16;
            this.label7.Text = "Animations:";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1388, 718);
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
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "Form1";
            this.Text = "Animator Creater";
            this.Load += new System.EventHandler(this.Form1_Load);
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

