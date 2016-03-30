namespace NodeMonitor.Client.Forms
{
    partial class SampleDataTypesForm
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
            this.SampleDataTypesList = new System.Windows.Forms.ListBox();
            this.DoneBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // SampleDataTypesList
            // 
            this.SampleDataTypesList.FormattingEnabled = true;
            this.SampleDataTypesList.Location = new System.Drawing.Point(12, 14);
            this.SampleDataTypesList.Name = "SampleDataTypesList";
            this.SampleDataTypesList.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
            this.SampleDataTypesList.Size = new System.Drawing.Size(212, 173);
            this.SampleDataTypesList.TabIndex = 0;
            // 
            // DoneBtn
            // 
            this.DoneBtn.Location = new System.Drawing.Point(13, 198);
            this.DoneBtn.Name = "DoneBtn";
            this.DoneBtn.Size = new System.Drawing.Size(75, 23);
            this.DoneBtn.TabIndex = 1;
            this.DoneBtn.Text = "Done";
            this.DoneBtn.UseVisualStyleBackColor = true;
            this.DoneBtn.Click += new System.EventHandler(this.DoneBtnClick);
            // 
            // SampleDataTypesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(236, 233);
            this.Controls.Add(this.DoneBtn);
            this.Controls.Add(this.SampleDataTypesList);
            this.Name = "SampleDataTypesForm";
            this.Text = "Sample Data Types";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox SampleDataTypesList;
        private System.Windows.Forms.Button DoneBtn;
    }
}