namespace GraphLab2 {
    partial class Form1 {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.panel1 = new System.Windows.Forms.Panel();
            this.pictureBox = new System.Windows.Forms.PictureBox();
            this.infoLabel = new System.Windows.Forms.Label();
            this.generateButton = new System.Windows.Forms.Button();
            this.clipButton = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.pictureBox);
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(712, 712);
            this.panel1.TabIndex = 0;
            // 
            // pictureBox
            // 
            this.pictureBox.Location = new System.Drawing.Point(0, 0);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(712, 712);
            this.pictureBox.TabIndex = 0;
            this.pictureBox.TabStop = false;
            // 
            // infoLabel
            // 
            this.infoLabel.AutoSize = true;
            this.infoLabel.Location = new System.Drawing.Point(13, 755);
            this.infoLabel.Name = "infoLabel";
            this.infoLabel.Size = new System.Drawing.Size(187, 15);
            this.infoLabel.TabIndex = 1;
            this.infoLabel.Text = "Количество прямоугольников: 0";
            // 
            // generateButton
            // 
            this.generateButton.Location = new System.Drawing.Point(368, 739);
            this.generateButton.Name = "generateButton";
            this.generateButton.Size = new System.Drawing.Size(175, 46);
            this.generateButton.TabIndex = 2;
            this.generateButton.Text = "Сгенерировать";
            this.generateButton.UseVisualStyleBackColor = true;
            this.generateButton.Click += new System.EventHandler(this.GenerateButtonClick);
            // 
            // clipButton
            // 
            this.clipButton.Location = new System.Drawing.Point(549, 739);
            this.clipButton.Name = "clipButton";
            this.clipButton.Size = new System.Drawing.Size(175, 46);
            this.clipButton.TabIndex = 3;
            this.clipButton.Text = "Отсечь";
            this.clipButton.UseVisualStyleBackColor = true;
            this.clipButton.Click += new System.EventHandler(this.ClipButtonClick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(736, 797);
            this.Controls.Add(this.clipButton);
            this.Controls.Add(this.generateButton);
            this.Controls.Add(this.infoLabel);
            this.Controls.Add(this.panel1);
            this.Name = "Form1";
            this.Text = "Лабораторная работа 2, Никитин Д.О.";
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox pictureBox;
        private System.Windows.Forms.Label infoLabel;
        private System.Windows.Forms.Button generateButton;
        private System.Windows.Forms.Button clipButton;
    }
}
