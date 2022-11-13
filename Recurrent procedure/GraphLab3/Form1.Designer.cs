namespace GraphLab3 {
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
            this.recursionDepthNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.generateButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.angleNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.recursionDepthNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.angleNumericUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.pictureBox);
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(560, 350);
            this.panel1.TabIndex = 0;
            // 
            // pictureBox
            // 
            this.pictureBox.Location = new System.Drawing.Point(0, 0);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(560, 350);
            this.pictureBox.TabIndex = 0;
            this.pictureBox.TabStop = false;
            // 
            // recursionDepthNumericUpDown
            // 
            this.recursionDepthNumericUpDown.Location = new System.Drawing.Point(175, 371);
            this.recursionDepthNumericUpDown.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.recursionDepthNumericUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.recursionDepthNumericUpDown.Name = "recursionDepthNumericUpDown";
            this.recursionDepthNumericUpDown.Size = new System.Drawing.Size(120, 23);
            this.recursionDepthNumericUpDown.TabIndex = 1;
            this.recursionDepthNumericUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // generateButton
            // 
            this.generateButton.Location = new System.Drawing.Point(301, 369);
            this.generateButton.Name = "generateButton";
            this.generateButton.Size = new System.Drawing.Size(272, 60);
            this.generateButton.TabIndex = 3;
            this.generateButton.Text = "Построить";
            this.generateButton.UseVisualStyleBackColor = true;
            this.generateButton.Click += new System.EventHandler(this.GenerateButtonClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(88, 408);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(81, 15);
            this.label1.TabIndex = 4;
            this.label1.Text = "Введите угол:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 373);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(156, 15);
            this.label2.TabIndex = 6;
            this.label2.Text = "Введите глубину рекурсии:";
            // 
            // angleNumericUpDown
            // 
            this.angleNumericUpDown.DecimalPlaces = 2;
            this.angleNumericUpDown.Location = new System.Drawing.Point(175, 406);
            this.angleNumericUpDown.Maximum = new decimal(new int[] {
            70,
            0,
            0,
            0});
            this.angleNumericUpDown.Minimum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.angleNumericUpDown.Name = "angleNumericUpDown";
            this.angleNumericUpDown.Size = new System.Drawing.Size(120, 23);
            this.angleNumericUpDown.TabIndex = 5;
            this.angleNumericUpDown.Value = new decimal(new int[] {
            45,
            0,
            0,
            0});
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 447);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.angleNumericUpDown);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.generateButton);
            this.Controls.Add(this.recursionDepthNumericUpDown);
            this.Name = "Form1";
            this.Text = "Лабораторная работа 3, Никитин Д.О.";
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.recursionDepthNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.angleNumericUpDown)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox pictureBox;
        private System.Windows.Forms.NumericUpDown recursionDepthNumericUpDown;
        private System.Windows.Forms.Button generateButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown angleNumericUpDown;
    }
}
