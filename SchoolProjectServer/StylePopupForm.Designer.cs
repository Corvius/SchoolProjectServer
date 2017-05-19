namespace SchoolProjectServer
{
    partial class StylePopupForm
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.txtNewStyleName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btNewStyleCancel = new System.Windows.Forms.Button();
            this.btNewStyleOk = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 8F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 8F));
            this.tableLayoutPanel1.Controls.Add(this.txtNewStyleName, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.label1, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.btNewStyleCancel, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.btNewStyleOk, 2, 3);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 5;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 8F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 16F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(284, 87);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // txtNewStyleName
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.txtNewStyleName, 2);
            this.txtNewStyleName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtNewStyleName.Location = new System.Drawing.Point(11, 27);
            this.txtNewStyleName.Name = "txtNewStyleName";
            this.txtNewStyleName.Size = new System.Drawing.Size(262, 20);
            this.txtNewStyleName.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.tableLayoutPanel1.SetColumnSpan(this.label1, 2);
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Location = new System.Drawing.Point(11, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(262, 16);
            this.label1.TabIndex = 1;
            this.label1.Text = "Please enter a new style name";
            this.label1.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // btNewStyleCancel
            // 
            this.btNewStyleCancel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btNewStyleCancel.Location = new System.Drawing.Point(11, 53);
            this.btNewStyleCancel.Margin = new System.Windows.Forms.Padding(3, 3, 11, 3);
            this.btNewStyleCancel.Name = "btNewStyleCancel";
            this.btNewStyleCancel.Size = new System.Drawing.Size(120, 22);
            this.btNewStyleCancel.TabIndex = 2;
            this.btNewStyleCancel.Text = "Cancel";
            this.btNewStyleCancel.UseVisualStyleBackColor = true;
            this.btNewStyleCancel.Click += new System.EventHandler(this.btNewStyleCancel_Click);
            // 
            // btNewStyleOk
            // 
            this.btNewStyleOk.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btNewStyleOk.Location = new System.Drawing.Point(153, 53);
            this.btNewStyleOk.Margin = new System.Windows.Forms.Padding(11, 3, 3, 3);
            this.btNewStyleOk.Name = "btNewStyleOk";
            this.btNewStyleOk.Size = new System.Drawing.Size(120, 22);
            this.btNewStyleOk.TabIndex = 3;
            this.btNewStyleOk.Text = "Ok";
            this.btNewStyleOk.UseVisualStyleBackColor = true;
            this.btNewStyleOk.Click += new System.EventHandler(this.btNewStyleOk_Click);
            // 
            // StylePopupForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 87);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "StylePopupForm";
            this.Text = "StylePopupForm";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TextBox txtNewStyleName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btNewStyleCancel;
        private System.Windows.Forms.Button btNewStyleOk;
    }
}