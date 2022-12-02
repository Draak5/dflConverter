namespace dflConverter
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
            this.inputTextBox = new System.Windows.Forms.RichTextBox();
            this.outputTextBox = new System.Windows.Forms.RichTextBox();
            this.convertButton = new System.Windows.Forms.Button();
            this.gzipOutput = new System.Windows.Forms.RichTextBox();
            this.decompressText = new System.Windows.Forms.RichTextBox();
            this.decompressButton = new System.Windows.Forms.Button();
            this.noParseToggle = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // inputTextBox
            // 
            this.inputTextBox.BackColor = System.Drawing.SystemColors.WindowText;
            this.inputTextBox.ForeColor = System.Drawing.SystemColors.Window;
            this.inputTextBox.Location = new System.Drawing.Point(12, 12);
            this.inputTextBox.Name = "inputTextBox";
            this.inputTextBox.Size = new System.Drawing.Size(1047, 601);
            this.inputTextBox.TabIndex = 0;
            this.inputTextBox.Text = "function test() {\n\tplayer.sendMessage(\"hello world\");\n}\n\n";
            // 
            // outputTextBox
            // 
            this.outputTextBox.BackColor = System.Drawing.SystemColors.WindowText;
            this.outputTextBox.ForeColor = System.Drawing.SystemColors.Window;
            this.outputTextBox.Location = new System.Drawing.Point(1134, 12);
            this.outputTextBox.Name = "outputTextBox";
            this.outputTextBox.Size = new System.Drawing.Size(288, 601);
            this.outputTextBox.TabIndex = 1;
            this.outputTextBox.Text = "";
            // 
            // convertButton
            // 
            this.convertButton.Location = new System.Drawing.Point(1065, 296);
            this.convertButton.Name = "convertButton";
            this.convertButton.Size = new System.Drawing.Size(63, 23);
            this.convertButton.TabIndex = 2;
            this.convertButton.Text = "Convert";
            this.convertButton.UseVisualStyleBackColor = true;
            this.convertButton.Click += new System.EventHandler(this.convertButton_Click);
            // 
            // gzipOutput
            // 
            this.gzipOutput.BackColor = System.Drawing.SystemColors.WindowText;
            this.gzipOutput.ForeColor = System.Drawing.SystemColors.Window;
            this.gzipOutput.Location = new System.Drawing.Point(580, 648);
            this.gzipOutput.Name = "gzipOutput";
            this.gzipOutput.Size = new System.Drawing.Size(839, 91);
            this.gzipOutput.TabIndex = 3;
            this.gzipOutput.Text = "";
            // 
            // decompressText
            // 
            this.decompressText.BackColor = System.Drawing.SystemColors.WindowText;
            this.decompressText.ForeColor = System.Drawing.SystemColors.Window;
            this.decompressText.Location = new System.Drawing.Point(12, 648);
            this.decompressText.Name = "decompressText";
            this.decompressText.Size = new System.Drawing.Size(518, 91);
            this.decompressText.TabIndex = 4;
            this.decompressText.Text = "";
            // 
            // decompressButton
            // 
            this.decompressButton.Location = new System.Drawing.Point(12, 619);
            this.decompressButton.Name = "decompressButton";
            this.decompressButton.Size = new System.Drawing.Size(77, 23);
            this.decompressButton.TabIndex = 5;
            this.decompressButton.Text = "Decompress";
            this.decompressButton.UseVisualStyleBackColor = true;
            this.decompressButton.Click += new System.EventHandler(this.decompressButton_Click);
            // 
            // noParseToggle
            // 
            this.noParseToggle.AutoSize = true;
            this.noParseToggle.Location = new System.Drawing.Point(95, 623);
            this.noParseToggle.Name = "noParseToggle";
            this.noParseToggle.Size = new System.Drawing.Size(81, 17);
            this.noParseToggle.TabIndex = 6;
            this.noParseToggle.Text = "Don\'t Parse";
            this.noParseToggle.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.WindowFrame;
            this.ClientSize = new System.Drawing.Size(1431, 751);
            this.Controls.Add(this.noParseToggle);
            this.Controls.Add(this.decompressButton);
            this.Controls.Add(this.decompressText);
            this.Controls.Add(this.gzipOutput);
            this.Controls.Add(this.convertButton);
            this.Controls.Add(this.outputTextBox);
            this.Controls.Add(this.inputTextBox);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox inputTextBox;
        private System.Windows.Forms.RichTextBox outputTextBox;
        private System.Windows.Forms.Button convertButton;
        private System.Windows.Forms.RichTextBox gzipOutput;
        private System.Windows.Forms.RichTextBox decompressText;
        private System.Windows.Forms.Button decompressButton;
        private System.Windows.Forms.CheckBox noParseToggle;
    }
}

