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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.tlpMainLayout = new System.Windows.Forms.TableLayoutPanel();
            this.txtServerURL = new System.Windows.Forms.TextBox();
            this.lblDbUrl = new System.Windows.Forms.Label();
            this.btStartServer = new System.Windows.Forms.Button();
            this.lblConsoleOutput = new System.Windows.Forms.Label();
            this.txtConsoleOutput = new SchoolProjectServer.RichTextBoxExt();
            this.tmrRecheckTweets = new System.Windows.Forms.Timer(this.components);
            this.tlpMainLayout.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlpMainLayout
            // 
            this.tlpMainLayout.ColumnCount = 5;
            this.tlpMainLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 12F));
            this.tlpMainLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.tlpMainLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.tlpMainLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tlpMainLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 18F));
            this.tlpMainLayout.Controls.Add(this.txtServerURL, 1, 2);
            this.tlpMainLayout.Controls.Add(this.lblDbUrl, 1, 1);
            this.tlpMainLayout.Controls.Add(this.btStartServer, 3, 2);
            this.tlpMainLayout.Controls.Add(this.lblConsoleOutput, 1, 4);
            this.tlpMainLayout.Controls.Add(this.txtConsoleOutput, 1, 5);
            this.tlpMainLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpMainLayout.Location = new System.Drawing.Point(0, 0);
            this.tlpMainLayout.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tlpMainLayout.Name = "tlpMainLayout";
            this.tlpMainLayout.RowCount = 7;
            this.tlpMainLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 13F));
            this.tlpMainLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpMainLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpMainLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 13F));
            this.tlpMainLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpMainLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMainLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 13F));
            this.tlpMainLayout.Size = new System.Drawing.Size(584, 761);
            this.tlpMainLayout.TabIndex = 0;
            // 
            // txtServerURL
            // 
            this.txtServerURL.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtServerURL.Location = new System.Drawing.Point(16, 39);
            this.txtServerURL.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtServerURL.Name = "txtServerURL";
            this.txtServerURL.Size = new System.Drawing.Size(310, 29);
            this.txtServerURL.TabIndex = 0;
            this.txtServerURL.Text = "localhost";
            // 
            // lblDbUrl
            // 
            this.lblDbUrl.AutoSize = true;
            this.lblDbUrl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblDbUrl.Location = new System.Drawing.Point(12, 13);
            this.lblDbUrl.Margin = new System.Windows.Forms.Padding(0);
            this.lblDbUrl.Name = "lblDbUrl";
            this.lblDbUrl.Size = new System.Drawing.Size(318, 21);
            this.lblDbUrl.TabIndex = 1;
            this.lblDbUrl.Text = "Database URL";
            // 
            // btStartServer
            // 
            this.btStartServer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btStartServer.Location = new System.Drawing.Point(358, 39);
            this.btStartServer.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btStartServer.Name = "btStartServer";
            this.btStartServer.Size = new System.Drawing.Size(204, 32);
            this.btStartServer.TabIndex = 4;
            this.btStartServer.Text = "Start Server";
            this.btStartServer.UseVisualStyleBackColor = true;
            this.btStartServer.Click += new System.EventHandler(this.btStartServer_Click);
            // 
            // lblConsoleOutput
            // 
            this.lblConsoleOutput.AutoSize = true;
            this.lblConsoleOutput.Location = new System.Drawing.Point(16, 89);
            this.lblConsoleOutput.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblConsoleOutput.Name = "lblConsoleOutput";
            this.lblConsoleOutput.Size = new System.Drawing.Size(119, 21);
            this.lblConsoleOutput.TabIndex = 5;
            this.lblConsoleOutput.Text = "Console Output";
            // 
            // txtConsoleOutput
            // 
            this.txtConsoleOutput.BackColor = System.Drawing.Color.Silver;
            this.tlpMainLayout.SetColumnSpan(this.txtConsoleOutput, 3);
            this.txtConsoleOutput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtConsoleOutput.Enabled = false;
            this.txtConsoleOutput.Location = new System.Drawing.Point(16, 115);
            this.txtConsoleOutput.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtConsoleOutput.Name = "txtConsoleOutput";
            this.txtConsoleOutput.ReadOnly = true;
            this.txtConsoleOutput.Size = new System.Drawing.Size(546, 628);
            this.txtConsoleOutput.TabIndex = 6;
            this.txtConsoleOutput.Text = "";
            // 
            // tmrRecheckTweets
            // 
            this.tmrRecheckTweets.Interval = 3000;
            this.tmrRecheckTweets.Tick += new System.EventHandler(this.tmrRecheckTweets_Tick);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 761);
            this.Controls.Add(this.tlpMainLayout);
            this.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MinimumSize = new System.Drawing.Size(600, 800);
            this.Name = "MainForm";
            this.Text = "TTS Server";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.tlpMainLayout.ResumeLayout(false);
            this.tlpMainLayout.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpMainLayout;
        private System.Windows.Forms.Button btStartServer;
        private System.Windows.Forms.TextBox txtServerURL;
        private System.Windows.Forms.Label lblDbUrl;
        private System.Windows.Forms.Label lblConsoleOutput;
        private System.Windows.Forms.Timer tmrRecheckTweets;
    }
}

