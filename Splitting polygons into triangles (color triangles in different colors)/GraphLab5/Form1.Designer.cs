namespace GraphLab5 {
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
            this.vertexCountNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.generateButton = new System.Windows.Forms.Button();
            this.triangulateButton = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.vertexCountNumericUpDown)).BeginInit();
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
            // vertexCountNumericUpDown
            // 
            this.vertexCountNumericUpDown.Location = new System.Drawing.Point(185, 730);
            this.vertexCountNumericUpDown.Minimum = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.vertexCountNumericUpDown.Name = "vertexCountNumericUpDown";
            this.vertexCountNumericUpDown.Size = new System.Drawing.Size(120, 23);
            this.vertexCountNumericUpDown.TabIndex = 1;
            this.vertexCountNumericUpDown.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 733);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(167, 15);
            this.label1.TabIndex = 2;
            this.label1.Text = "Введите количество вершин:";
            // 
            // generateButton
            // 
            this.generateButton.Location = new System.Drawing.Point(311, 728);
            this.generateButton.Name = "generateButton";
            this.generateButton.Size = new System.Drawing.Size(200, 25);
            this.generateButton.TabIndex = 3;
            this.generateButton.Text = "Построить";
            this.generateButton.UseVisualStyleBackColor = true;
            this.generateButton.Click += new System.EventHandler(this.GenerateButtonClick);
            // 
            // triangulateButton
            // 
            this.triangulateButton.Location = new System.Drawing.Point(524, 728);
            this.triangulateButton.Name = "triangulateButton";
            this.triangulateButton.Size = new System.Drawing.Size(200, 25);
            this.triangulateButton.TabIndex = 4;
            this.triangulateButton.Text = "Разбить";
            this.triangulateButton.UseVisualStyleBackColor = true;
            this.triangulateButton.Click += new System.EventHandler(this.TriangulateButtonClick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(736, 761);
            this.Controls.Add(this.triangulateButton);
            this.Controls.Add(this.generateButton);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.vertexCountNumericUpDown);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.Name = "Form1";
            this.Text = "Лабораторная работа 5, Никитин Д.О.";
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.vertexCountNumericUpDown)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox pictureBox;
        private System.Windows.Forms.NumericUpDown vertexCountNumericUpDown;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button generateButton;
        private System.Windows.Forms.Button triangulateButton;
    }
}
