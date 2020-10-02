namespace KinectHandTracking
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
            this.On_btn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // On_btn
            // 
            this.On_btn.Location = new System.Drawing.Point(527, 133);
            this.On_btn.Name = "On_btn";
            this.On_btn.Size = new System.Drawing.Size(75, 23);
            this.On_btn.TabIndex = 0;
            this.On_btn.Text = "On";
            this.On_btn.UseVisualStyleBackColor = true;
            this.On_btn.Click += new System.EventHandler(this.On_btn_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.On_btn);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button On_btn;
    }
}