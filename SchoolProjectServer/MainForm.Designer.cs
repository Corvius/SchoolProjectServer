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
            this.tmrRecheckTweets = new System.Windows.Forms.Timer(this.components);
            this.lineSeparator1 = new SchoolProjectServer.LineSeparator();
            this.tlpMainLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgwStyleElements)).BeginInit();
            this.SuspendLayout();
            // 
            // tlpMainLayout
            // 
            this.tlpMainLayout.ColumnCount = 7;
            this.tlpMainLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 8F));
            this.tlpMainLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tlpMainLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 16F));
            this.tlpMainLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tlpMainLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 16F));
            this.tlpMainLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tlpMainLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 22F));
            this.tlpMainLayout.Controls.Add(this.txtServerURL, 1, 2);
            this.tlpMainLayout.Controls.Add(this.label1, 1, 1);
            this.tlpMainLayout.Controls.Add(this.label2, 3, 1);
            this.tlpMainLayout.Controls.Add(this.txtServerPort, 3, 2);
            this.tlpMainLayout.Controls.Add(this.btConnectToDatabase, 5, 2);
            this.tlpMainLayout.Controls.Add(this.cbStyles, 5, 10);
            this.tlpMainLayout.Controls.Add(this.label4, 5, 9);
            this.tlpMainLayout.Controls.Add(this.btAddNewStyle, 5, 12);
            this.tlpMainLayout.Controls.Add(this.btRemoveStyle, 5, 13);
            this.tlpMainLayout.Controls.Add(this.label5, 1, 9);
            this.tlpMainLayout.Controls.Add(this.dgwStyleElements, 1, 10);
            this.tlpMainLayout.Controls.Add(this.btUpdateServer, 5, 16);
            this.tlpMainLayout.Controls.Add(this.btReloadStyle, 5, 15);
            this.tlpMainLayout.Controls.Add(this.lineSeparator1, 1, 4);
            this.tlpMainLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpMainLayout.Location = new System.Drawing.Point(0, 0);
            this.tlpMainLayout.Name = "tlpMainLayout";
            this.tlpMainLayout.RowCount = 19;
            this.tlpMainLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 8F));
            this.tlpMainLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpMainLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpMainLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 14F));
            this.tlpMainLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 4F));
            this.tlpMainLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 16F));
            this.tlpMainLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpMainLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpMainLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpMainLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpMainLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpMainLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.tlpMainLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tlpMainLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tlpMainLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.tlpMainLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tlpMainLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tlpMainLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMainLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 8F));
            this.tlpMainLayout.Size = new System.Drawing.Size(584, 561);
            this.tlpMainLayout.TabIndex = 0;
            this.tlpMainLayout.Paint += new System.Windows.Forms.PaintEventHandler(this.tlpMainLayout_Paint);
            // 
            // txtServerURL
            // 
            this.txtServerURL.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtServerURL.Location = new System.Drawing.Point(11, 24);
            this.txtServerURL.Name = "txtServerURL";
            this.txtServerURL.Size = new System.Drawing.Size(168, 20);
            this.txtServerURL.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Location = new System.Drawing.Point(11, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(168, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Database URL";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Location = new System.Drawing.Point(201, 8);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(168, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Port";
            // 
            // txtServerPort
            // 
            this.txtServerPort.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtServerPort.Location = new System.Drawing.Point(201, 24);
            this.txtServerPort.Name = "txtServerPort";
            this.txtServerPort.Size = new System.Drawing.Size(168, 20);
            this.txtServerPort.TabIndex = 3;
            // 
            // btConnectToDatabase
            // 
            this.btConnectToDatabase.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btConnectToDatabase.Location = new System.Drawing.Point(391, 24);
            this.btConnectToDatabase.Name = "btConnectToDatabase";
            this.btConnectToDatabase.Size = new System.Drawing.Size(168, 23);
            this.btConnectToDatabase.TabIndex = 4;
            this.btConnectToDatabase.Text = "Connect to Database";
            this.btConnectToDatabase.UseVisualStyleBackColor = true;
            // 
            // cbStyles
            // 
            this.cbStyles.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cbStyles.FormattingEnabled = true;
            this.cbStyles.Location = new System.Drawing.Point(391, 100);
            this.cbStyles.Name = "cbStyles";
            this.cbStyles.Size = new System.Drawing.Size(168, 21);
            this.cbStyles.TabIndex = 6;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label4.Location = new System.Drawing.Point(391, 84);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(168, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Styles";
            // 
            // btAddNewStyle
            // 
            this.btAddNewStyle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btAddNewStyle.Location = new System.Drawing.Point(391, 159);
            this.btAddNewStyle.Name = "btAddNewStyle";
            this.btAddNewStyle.Size = new System.Drawing.Size(168, 22);
            this.btAddNewStyle.TabIndex = 9;
            this.btAddNewStyle.Text = "Add New Style";
            this.btAddNewStyle.UseVisualStyleBackColor = true;
            // 
            // btRemoveStyle
            // 
            this.btRemoveStyle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btRemoveStyle.Location = new System.Drawing.Point(391, 187);
            this.btRemoveStyle.Name = "btRemoveStyle";
            this.btRemoveStyle.Size = new System.Drawing.Size(168, 22);
            this.btRemoveStyle.TabIndex = 10;
            this.btRemoveStyle.Text = "Remove Style";
            this.btRemoveStyle.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label5.Location = new System.Drawing.Point(11, 84);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(168, 13);
            this.label5.TabIndex = 11;
            this.label5.Text = "Style Elements";
            // 
            // dgwStyleElements
            // 
            this.dgwStyleElements.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.tlpMainLayout.SetColumnSpan(this.dgwStyleElements, 3);
            this.dgwStyleElements.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgwStyleElements.Location = new System.Drawing.Point(11, 100);
            this.dgwStyleElements.Name = "dgwStyleElements";
            this.tlpMainLayout.SetRowSpan(this.dgwStyleElements, 8);
            this.dgwStyleElements.Size = new System.Drawing.Size(358, 450);
            this.dgwStyleElements.TabIndex = 12;
            // 
            // btUpdateServer
            // 
            this.btUpdateServer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btUpdateServer.Location = new System.Drawing.Point(391, 275);
            this.btUpdateServer.Name = "btUpdateServer";
            this.btUpdateServer.Size = new System.Drawing.Size(168, 22);
            this.btUpdateServer.TabIndex = 13;
            this.btUpdateServer.Text = "Update Server";
            this.btUpdateServer.UseVisualStyleBackColor = true;
            // 
            // btReloadStyle
            // 
            this.btReloadStyle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btReloadStyle.Location = new System.Drawing.Point(391, 247);
            this.btReloadStyle.Name = "btReloadStyle";
            this.btReloadStyle.Size = new System.Drawing.Size(168, 22);
            this.btReloadStyle.TabIndex = 14;
            this.btReloadStyle.Text = "Reload Style";
            this.btReloadStyle.UseVisualStyleBackColor = true;
            this.btReloadStyle.Click += new System.EventHandler(this.btReloadStyle_Click);
            // 
            // tmrRecheckTweets
            // 
            this.tmrRecheckTweets.Interval = 3000;
            this.tmrRecheckTweets.Tick += new System.EventHandler(this.tmrRecheckTweets_Tick);
            // 
            // lineSeparator1
            // 
            this.tlpMainLayout.SetColumnSpan(this.lineSeparator1, 5);
            this.lineSeparator1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lineSeparator1.Location = new System.Drawing.Point(11, 67);
            this.lineSeparator1.MaximumSize = new System.Drawing.Size(2000, 2);
            this.lineSeparator1.MinimumSize = new System.Drawing.Size(0, 2);
            this.lineSeparator1.Name = "lineSeparator1";
            this.lineSeparator1.Size = new System.Drawing.Size(548, 2);
            this.lineSeparator1.TabIndex = 15;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 561);
            this.Controls.Add(this.tlpMainLayout);
            this.MinimumSize = new System.Drawing.Size(600, 600);
            this.Name = "MainForm";
            this.Text = "TTS Server";
            this.Shown += new System.EventHandler(this.MainForm_Shown);
            this.tlpMainLayout.ResumeLayout(false);
            this.tlpMainLayout.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgwStyleElements)).EndInit();
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
    }
}

