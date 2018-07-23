namespace TricksterPacketman
{
    partial class ListForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ListForm));
            this.PacketList = new System.Windows.Forms.ListView();
            this.IndexColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.TimeColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.PortColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.TypeColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.OpcodeColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SizeColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.PauseBtn = new System.Windows.Forms.Button();
            this.StatusStrip = new System.Windows.Forms.StatusStrip();
            this.StatusLbl = new System.Windows.Forms.ToolStripStatusLabel();
            this.InfoTxt = new System.Windows.Forms.TextBox();
            this.HexTxt = new System.Windows.Forms.TextBox();
            this.CopyHexBtn = new System.Windows.Forms.Button();
            this.StatusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // PacketList
            // 
            this.PacketList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PacketList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.IndexColumnHeader,
            this.TimeColumnHeader,
            this.PortColumnHeader,
            this.TypeColumnHeader,
            this.OpcodeColumnHeader,
            this.SizeColumnHeader});
            this.PacketList.FullRowSelect = true;
            this.PacketList.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.PacketList.Location = new System.Drawing.Point(12, 63);
            this.PacketList.Name = "PacketList";
            this.PacketList.Size = new System.Drawing.Size(716, 461);
            this.PacketList.TabIndex = 0;
            this.PacketList.UseCompatibleStateImageBehavior = false;
            this.PacketList.View = System.Windows.Forms.View.Details;
            this.PacketList.SelectedIndexChanged += new System.EventHandler(this.PacketList_SelectedIndexChanged);
            // 
            // IndexColumnHeader
            // 
            this.IndexColumnHeader.DisplayIndex = 5;
            this.IndexColumnHeader.Text = "Index";
            this.IndexColumnHeader.Width = 100;
            // 
            // TimeColumnHeader
            // 
            this.TimeColumnHeader.DisplayIndex = 0;
            this.TimeColumnHeader.Text = "Time";
            this.TimeColumnHeader.Width = 120;
            // 
            // PortColumnHeader
            // 
            this.PortColumnHeader.DisplayIndex = 1;
            this.PortColumnHeader.Text = "Port";
            this.PortColumnHeader.Width = 120;
            // 
            // TypeColumnHeader
            // 
            this.TypeColumnHeader.DisplayIndex = 2;
            this.TypeColumnHeader.Text = "Type";
            this.TypeColumnHeader.Width = 120;
            // 
            // OpcodeColumnHeader
            // 
            this.OpcodeColumnHeader.DisplayIndex = 3;
            this.OpcodeColumnHeader.Text = "Opcode";
            this.OpcodeColumnHeader.Width = 100;
            // 
            // SizeColumnHeader
            // 
            this.SizeColumnHeader.DisplayIndex = 4;
            this.SizeColumnHeader.Text = "Size";
            this.SizeColumnHeader.Width = 100;
            // 
            // PauseBtn
            // 
            this.PauseBtn.Location = new System.Drawing.Point(12, 12);
            this.PauseBtn.Name = "PauseBtn";
            this.PauseBtn.Size = new System.Drawing.Size(174, 35);
            this.PauseBtn.TabIndex = 2;
            this.PauseBtn.Text = "Pause Output";
            this.PauseBtn.UseVisualStyleBackColor = true;
            // 
            // StatusStrip
            // 
            this.StatusStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.StatusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.StatusLbl});
            this.StatusStrip.Location = new System.Drawing.Point(0, 542);
            this.StatusStrip.Name = "StatusStrip";
            this.StatusStrip.Size = new System.Drawing.Size(1206, 24);
            this.StatusStrip.TabIndex = 3;
            this.StatusStrip.Text = "statusStrip1";
            // 
            // StatusLbl
            // 
            this.StatusLbl.Name = "StatusLbl";
            this.StatusLbl.Size = new System.Drawing.Size(79, 19);
            this.StatusLbl.Text = "Capturing";
            // 
            // InfoTxt
            // 
            this.InfoTxt.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.InfoTxt.BackColor = System.Drawing.Color.White;
            this.InfoTxt.Location = new System.Drawing.Point(734, 63);
            this.InfoTxt.Multiline = true;
            this.InfoTxt.Name = "InfoTxt";
            this.InfoTxt.ReadOnly = true;
            this.InfoTxt.Size = new System.Drawing.Size(460, 133);
            this.InfoTxt.TabIndex = 4;
            // 
            // HexTxt
            // 
            this.HexTxt.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.HexTxt.BackColor = System.Drawing.Color.White;
            this.HexTxt.Location = new System.Drawing.Point(734, 202);
            this.HexTxt.Multiline = true;
            this.HexTxt.Name = "HexTxt";
            this.HexTxt.ReadOnly = true;
            this.HexTxt.Size = new System.Drawing.Size(460, 322);
            this.HexTxt.TabIndex = 5;
            // 
            // CopyHexBtn
            // 
            this.CopyHexBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.CopyHexBtn.Location = new System.Drawing.Point(734, 12);
            this.CopyHexBtn.Name = "CopyHexBtn";
            this.CopyHexBtn.Size = new System.Drawing.Size(460, 35);
            this.CopyHexBtn.TabIndex = 6;
            this.CopyHexBtn.Text = "Copy Selected Packet";
            this.CopyHexBtn.UseVisualStyleBackColor = true;
            // 
            // ListForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1206, 566);
            this.Controls.Add(this.CopyHexBtn);
            this.Controls.Add(this.HexTxt);
            this.Controls.Add(this.InfoTxt);
            this.Controls.Add(this.StatusStrip);
            this.Controls.Add(this.PauseBtn);
            this.Controls.Add(this.PacketList);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "ListForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Trickster Packetman";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.ListForm_FormClosed);
            this.Load += new System.EventHandler(this.ListForm_Load);
            this.StatusStrip.ResumeLayout(false);
            this.StatusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView PacketList;
        private System.Windows.Forms.Button PauseBtn;
        private System.Windows.Forms.StatusStrip StatusStrip;
        private System.Windows.Forms.ToolStripStatusLabel StatusLbl;
        private System.Windows.Forms.TextBox InfoTxt;
        private System.Windows.Forms.TextBox HexTxt;
        private System.Windows.Forms.ColumnHeader TimeColumnHeader;
        private System.Windows.Forms.ColumnHeader PortColumnHeader;
        private System.Windows.Forms.ColumnHeader TypeColumnHeader;
        private System.Windows.Forms.ColumnHeader OpcodeColumnHeader;
        private System.Windows.Forms.ColumnHeader SizeColumnHeader;
        private System.Windows.Forms.Button CopyHexBtn;
        private System.Windows.Forms.ColumnHeader IndexColumnHeader;
    }
}