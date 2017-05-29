namespace SchoolProjectServer
{
    partial class MainForm
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.tlpMainLayout = new System.Windows.Forms.TableLayoutPanel();
            this.txtServerURL = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtServerPort = new System.Windows.Forms.TextBox();
            this.btConnectToDatabase = new System.Windows.Forms.Button();
            this.cbStyles = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btAddNewStyle = new System.Windows.Forms.Button();
            this.btRemoveStyle = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.dgwStyleElements = new System.Windows.Forms.DataGridView();
            this.btUpdateServer = new System.Windows.Forms.Button();
            this.btReloadStyle = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.pbStyleImage = new System.Windows.Forms.PictureBox();
            this.txtImagePath = new System.Windows.Forms.TextBox();
            this.btLoadImageURL = new System.Windows.Forms.Button();
            this.btResetConnectionFields = new System.Windows.Forms.Button();
            this.btClearImage = new System.Windows.Forms.Button();
            this.tmrRecheckTweets = new System.Windows.Forms.Timer(this.components);
            this.lineSeparator1 = new SchoolProjectServer.LineSeparator();
            this.tlpMainLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgwStyleElements)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbStyleImage)).BeginInit();
            this.SuspendLayout();
            // 
            // tlpMainLayout
            // 
            this.tlpMainLayout.ColumnCount = 9;
            this.tlpMainLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 8F));
            this.tlpMainLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpMainLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 16F));
            this.tlpMainLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpMainLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 16F));
            this.tlpMainLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tlpMainLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tlpMainLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tlpMainLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 8F));
            this.tlpMainLayout.Controls.Add(this.txtServerURL, 1, 2);
            this.tlpMainLayout.Controls.Add(this.label1, 1, 1);
            this.tlpMainLayout.Controls.Add(this.label2, 3, 1);
            this.tlpMainLayout.Controls.Add(this.txtServerPort, 3, 2);
            this.tlpMainLayout.Controls.Add(this.btConnectToDatabase, 5, 2);
            this.tlpMainLayout.Controls.Add(this.cbStyles, 5, 10);
            this.tlpMainLayout.Controls.Add(this.label4, 5, 9);
            this.tlpMainLayout.Controls.Add(this.btAddNewStyle, 5, 17);
            this.tlpMainLayout.Controls.Add(this.btRemoveStyle, 5, 18);
            this.tlpMainLayout.Controls.Add(this.label5, 1, 9);
            this.tlpMainLayout.Controls.Add(this.dgwStyleElements, 1, 10);
            this.tlpMainLayout.Controls.Add(this.btUpdateServer, 5, 21);
            this.tlpMainLayout.Controls.Add(this.btReloadStyle, 5, 20);
            this.tlpMainLayout.Controls.Add(this.lineSeparator1, 1, 4);
            this.tlpMainLayout.Controls.Add(this.label3, 5, 12);
            this.tlpMainLayout.Controls.Add(this.pbStyleImage, 5, 13);
            this.tlpMainLayout.Controls.Add(this.txtImagePath, 5, 14);
            this.tlpMainLayout.Controls.Add(this.btLoadImageURL, 5, 15);
            this.tlpMainLayout.Controls.Add(this.btResetConnectionFields, 7, 2);
            this.tlpMainLayout.Controls.Add(this.btClearImage, 6, 15);
            this.tlpMainLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpMainLayout.Location = new System.Drawing.Point(0, 0);
            this.tlpMainLayout.Name = "tlpMainLayout";
            this.tlpMainLayout.RowCount = 23;
            this.tlpMainLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 8F));
            this.tlpMainLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpMainLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpMainLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 14F));
            this.tlpMainLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 4F));
            this.tlpMainLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMainLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpMainLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpMainLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpMainLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpMainLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpMainLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 16F));
            this.tlpMainLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpMainLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 200F));
            this.tlpMainLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpMainLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tlpMainLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.tlpMainLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tlpMainLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tlpMainLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.tlpMainLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tlpMainLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tlpMainLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 8F));
            this.tlpMainLayout.Size = new System.Drawing.Size(584, 595);
            this.tlpMainLayout.TabIndex = 0;
            // 
            // txtServerURL
            // 
            this.txtServerURL.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtServerURL.Location = new System.Drawing.Point(11, 24);
            this.txtServerURL.Name = "txtServerURL";
            this.txtServerURL.Size = new System.Drawing.Size(162, 20);
            this.txtServerURL.TabIndex = 0;
            this.txtServerURL.Text = "localhost";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Location = new System.Drawing.Point(8, 8);
            this.label1.Margin = new System.Windows.Forms.Padding(0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(168, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Database URL";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Location = new System.Drawing.Point(192, 8);
            this.label2.Margin = new System.Windows.Forms.Padding(0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(168, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Port";
            // 
            // txtServerPort
            // 
            this.txtServerPort.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtServerPort.Location = new System.Drawing.Point(195, 24);
            this.txtServerPort.Name = "txtServerPort";
            this.txtServerPort.Size = new System.Drawing.Size(162, 20);
            this.txtServerPort.TabIndex = 3;
            // 
            // btConnectToDatabase
            // 
            this.tlpMainLayout.SetColumnSpan(this.btConnectToDatabase, 2);
            this.btConnectToDatabase.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btConnectToDatabase.Location = new System.Drawing.Point(379, 24);
            this.btConnectToDatabase.Name = "btConnectToDatabase";
            this.btConnectToDatabase.Size = new System.Drawing.Size(134, 23);
            this.btConnectToDatabase.TabIndex = 4;
            this.btConnectToDatabase.Text = "Create DB connection";
            this.btConnectToDatabase.UseVisualStyleBackColor = true;
            this.btConnectToDatabase.Click += new System.EventHandler(this.btConnectToDatabase_Click);
            // 
            // cbStyles
            // 
            this.tlpMainLayout.SetColumnSpan(this.cbStyles, 3);
            this.cbStyles.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cbStyles.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbStyles.FormattingEnabled = true;
            this.cbStyles.Location = new System.Drawing.Point(379, 101);
            this.cbStyles.Name = "cbStyles";
            this.cbStyles.Size = new System.Drawing.Size(194, 21);
            this.cbStyles.TabIndex = 6;
            this.cbStyles.SelectedIndexChanged += new System.EventHandler(this.cbStyles_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.tlpMainLayout.SetColumnSpan(this.label4, 3);
            this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label4.Location = new System.Drawing.Point(376, 85);
            this.label4.Margin = new System.Windows.Forms.Padding(0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(200, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Styles";
            // 
            // btAddNewStyle
            // 
            this.tlpMainLayout.SetColumnSpan(this.btAddNewStyle, 3);
            this.btAddNewStyle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btAddNewStyle.Location = new System.Drawing.Point(379, 446);
            this.btAddNewStyle.Name = "btAddNewStyle";
            this.btAddNewStyle.Size = new System.Drawing.Size(194, 22);
            this.btAddNewStyle.TabIndex = 9;
            this.btAddNewStyle.Text = "Add New Style";
            this.btAddNewStyle.UseVisualStyleBackColor = true;
            this.btAddNewStyle.Click += new System.EventHandler(this.btAddNewStyle_Click);
            // 
            // btRemoveStyle
            // 
            this.tlpMainLayout.SetColumnSpan(this.btRemoveStyle, 3);
            this.btRemoveStyle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btRemoveStyle.Location = new System.Drawing.Point(379, 474);
            this.btRemoveStyle.Name = "btRemoveStyle";
            this.btRemoveStyle.Size = new System.Drawing.Size(194, 22);
            this.btRemoveStyle.TabIndex = 10;
            this.btRemoveStyle.Text = "Remove Style";
            this.btRemoveStyle.UseVisualStyleBackColor = true;
            this.btRemoveStyle.Click += new System.EventHandler(this.btRemoveStyle_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label5.Location = new System.Drawing.Point(8, 85);
            this.label5.Margin = new System.Windows.Forms.Padding(0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(168, 13);
            this.label5.TabIndex = 11;
            this.label5.Text = "Style Elements";
            // 
            // dgwStyleElements
            // 
            this.dgwStyleElements.AllowUserToOrderColumns = true;
            this.dgwStyleElements.AllowUserToResizeColumns = false;
            this.dgwStyleElements.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.ControlLight;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgwStyleElements.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgwStyleElements.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.tlpMainLayout.SetColumnSpan(this.dgwStyleElements, 3);
            this.dgwStyleElements.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgwStyleElements.Location = new System.Drawing.Point(11, 101);
            this.dgwStyleElements.MultiSelect = false;
            this.dgwStyleElements.Name = "dgwStyleElements";
            this.dgwStyleElements.RowHeadersVisible = false;
            this.tlpMainLayout.SetRowSpan(this.dgwStyleElements, 12);
            this.dgwStyleElements.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dgwStyleElements.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgwStyleElements.Size = new System.Drawing.Size(346, 483);
            this.dgwStyleElements.TabIndex = 12;
            this.dgwStyleElements.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgwStyleElements_CellEndEdit);
            // 
            // btUpdateServer
            // 
            this.tlpMainLayout.SetColumnSpan(this.btUpdateServer, 3);
            this.btUpdateServer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btUpdateServer.Location = new System.Drawing.Point(379, 562);
            this.btUpdateServer.Name = "btUpdateServer";
            this.btUpdateServer.Size = new System.Drawing.Size(194, 22);
            this.btUpdateServer.TabIndex = 13;
            this.btUpdateServer.Text = "Update Database Server";
            this.btUpdateServer.UseVisualStyleBackColor = true;
            this.btUpdateServer.Click += new System.EventHandler(this.btUpdateServer_Click);
            // 
            // btReloadStyle
            // 
            this.tlpMainLayout.SetColumnSpan(this.btReloadStyle, 3);
            this.btReloadStyle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btReloadStyle.Location = new System.Drawing.Point(379, 534);
            this.btReloadStyle.Name = "btReloadStyle";
            this.btReloadStyle.Size = new System.Drawing.Size(194, 22);
            this.btReloadStyle.TabIndex = 14;
            this.btReloadStyle.Text = "Reload Style";
            this.btReloadStyle.UseVisualStyleBackColor = true;
            this.btReloadStyle.Click += new System.EventHandler(this.btReloadStyle_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.tlpMainLayout.SetColumnSpan(this.label3, 3);
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Location = new System.Drawing.Point(376, 141);
            this.label3.Margin = new System.Windows.Forms.Padding(0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(200, 13);
            this.label3.TabIndex = 16;
            this.label3.Text = "Style picture";
            // 
            // pbStyleImage
            // 
            this.pbStyleImage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tlpMainLayout.SetColumnSpan(this.pbStyleImage, 3);
            this.pbStyleImage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pbStyleImage.Location = new System.Drawing.Point(376, 154);
            this.pbStyleImage.Margin = new System.Windows.Forms.Padding(0);
            this.pbStyleImage.Name = "pbStyleImage";
            this.pbStyleImage.Size = new System.Drawing.Size(200, 200);
            this.pbStyleImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbStyleImage.TabIndex = 17;
            this.pbStyleImage.TabStop = false;
            // 
            // txtImagePath
            // 
            this.tlpMainLayout.SetColumnSpan(this.txtImagePath, 3);
            this.txtImagePath.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtImagePath.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.txtImagePath.Location = new System.Drawing.Point(376, 357);
            this.txtImagePath.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.txtImagePath.Name = "txtImagePath";
            this.txtImagePath.Size = new System.Drawing.Size(200, 23);
            this.txtImagePath.TabIndex = 18;
            // 
            // btLoadImageURL
            // 
            this.btLoadImageURL.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btLoadImageURL.Location = new System.Drawing.Point(379, 386);
            this.btLoadImageURL.Name = "btLoadImageURL";
            this.btLoadImageURL.Size = new System.Drawing.Size(94, 22);
            this.btLoadImageURL.TabIndex = 19;
            this.btLoadImageURL.Text = "LoadURL";
            this.btLoadImageURL.UseVisualStyleBackColor = true;
            this.btLoadImageURL.Click += new System.EventHandler(this.btOpenImage_Click);
            // 
            // btResetConnectionFields
            // 
            this.btResetConnectionFields.Location = new System.Drawing.Point(519, 24);
            this.btResetConnectionFields.Name = "btResetConnectionFields";
            this.btResetConnectionFields.Size = new System.Drawing.Size(54, 23);
            this.btResetConnectionFields.TabIndex = 21;
            this.btResetConnectionFields.Text = "Reset";
            this.btResetConnectionFields.UseVisualStyleBackColor = true;
            this.btResetConnectionFields.Click += new System.EventHandler(this.btResetConnectionFields_Click);
            // 
            // btClearImage
            // 
            this.tlpMainLayout.SetColumnSpan(this.btClearImage, 2);
            this.btClearImage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btClearImage.Location = new System.Drawing.Point(479, 386);
            this.btClearImage.Name = "btClearImage";
            this.btClearImage.Size = new System.Drawing.Size(94, 22);
            this.btClearImage.TabIndex = 22;
            this.btClearImage.Text = "Clear picture";
            this.btClearImage.UseVisualStyleBackColor = true;
            this.btClearImage.Click += new System.EventHandler(this.btClearImage_Click);
            // 
            // tmrRecheckTweets
            // 
            this.tmrRecheckTweets.Interval = 3000;
            this.tmrRecheckTweets.Tick += new System.EventHandler(this.tmrRecheckTweets_Tick);
            // 
            // lineSeparator1
            // 
            this.tlpMainLayout.SetColumnSpan(this.lineSeparator1, 7);
            this.lineSeparator1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lineSeparator1.Location = new System.Drawing.Point(11, 67);
            this.lineSeparator1.MaximumSize = new System.Drawing.Size(2000, 2);
            this.lineSeparator1.MinimumSize = new System.Drawing.Size(0, 2);
            this.lineSeparator1.Name = "lineSeparator1";
            this.lineSeparator1.Size = new System.Drawing.Size(562, 2);
            this.lineSeparator1.TabIndex = 15;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 595);
            this.Controls.Add(this.tlpMainLayout);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MinimumSize = new System.Drawing.Size(600, 600);
            this.Name = "MainForm";
            this.Text = "TTS Server";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Shown += new System.EventHandler(this.MainForm_Shown);
            this.tlpMainLayout.ResumeLayout(false);
            this.tlpMainLayout.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgwStyleElements)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbStyleImage)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpMainLayout;
        private System.Windows.Forms.TextBox txtServerURL;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtServerPort;
        private System.Windows.Forms.Button btConnectToDatabase;
        private System.Windows.Forms.ComboBox cbStyles;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btAddNewStyle;
        private System.Windows.Forms.Button btRemoveStyle;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DataGridView dgwStyleElements;
        private System.Windows.Forms.Button btUpdateServer;
        private System.Windows.Forms.Button btReloadStyle;
        private LineSeparator lineSeparator1;
        private System.Windows.Forms.Timer tmrRecheckTweets;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.PictureBox pbStyleImage;
        private System.Windows.Forms.TextBox txtImagePath;
        private System.Windows.Forms.Button btLoadImageURL;
        private System.Windows.Forms.Button btResetConnectionFields;
        private System.Windows.Forms.Button btClearImage;
    }
}

