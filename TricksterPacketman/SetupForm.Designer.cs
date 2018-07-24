namespace TricksterPacketman
{
    partial class SetupForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SetupForm));
            this.cmbAdapter = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.StatusStrip = new System.Windows.Forms.StatusStrip();
            this.StatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.BtnSelect = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.TxtPorts = new System.Windows.Forms.TextBox();
            this.BtnPreset_rTO = new System.Windows.Forms.Button();
            this.BtnPreset_LTO = new System.Windows.Forms.Button();
            this.BtnPreset_tTO = new System.Windows.Forms.Button();
            this.StatusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmbAdapter
            // 
            this.cmbAdapter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbAdapter.Enabled = false;
            this.cmbAdapter.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbAdapter.FormattingEnabled = true;
            this.cmbAdapter.Location = new System.Drawing.Point(12, 33);
            this.cmbAdapter.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cmbAdapter.Name = "cmbAdapter";
            this.cmbAdapter.Size = new System.Drawing.Size(417, 28);
            this.cmbAdapter.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(8, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(116, 20);
            this.label1.TabIndex = 1;
            this.label1.Text = "Choose Adapter";
            // 
            // StatusStrip
            // 
            this.StatusStrip.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.StatusStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.StatusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.StatusLabel});
            this.StatusStrip.Location = new System.Drawing.Point(0, 204);
            this.StatusStrip.Name = "StatusStrip";
            this.StatusStrip.Size = new System.Drawing.Size(441, 25);
            this.StatusStrip.TabIndex = 2;
            // 
            // StatusLabel
            // 
            this.StatusLabel.BackColor = System.Drawing.SystemColors.Control;
            this.StatusLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.StatusLabel.Name = "StatusLabel";
            this.StatusLabel.Size = new System.Drawing.Size(125, 20);
            this.StatusLabel.Text = "Loading adapters";
            // 
            // BtnSelect
            // 
            this.BtnSelect.Enabled = false;
            this.BtnSelect.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnSelect.Location = new System.Drawing.Point(12, 150);
            this.BtnSelect.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.BtnSelect.Name = "BtnSelect";
            this.BtnSelect.Size = new System.Drawing.Size(75, 36);
            this.BtnSelect.TabIndex = 1;
            this.BtnSelect.Text = "Select";
            this.BtnSelect.UseVisualStyleBackColor = true;
            this.BtnSelect.Click += new System.EventHandler(this.BtnSelect_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 72);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(189, 20);
            this.label2.TabIndex = 4;
            this.label2.Text = "Ports (separated by spaces)";
            // 
            // TxtPorts
            // 
            this.TxtPorts.Location = new System.Drawing.Point(12, 97);
            this.TxtPorts.Name = "TxtPorts";
            this.TxtPorts.Size = new System.Drawing.Size(417, 27);
            this.TxtPorts.TabIndex = 5;
            this.TxtPorts.Text = "9980 10006 13336 22006 23006 24006 25006 26006";
            // 
            // BtnPreset_rTO
            // 
            this.BtnPreset_rTO.Location = new System.Drawing.Point(384, 150);
            this.BtnPreset_rTO.Name = "BtnPreset_rTO";
            this.BtnPreset_rTO.Size = new System.Drawing.Size(45, 30);
            this.BtnPreset_rTO.TabIndex = 4;
            this.BtnPreset_rTO.Text = "rTO";
            this.BtnPreset_rTO.UseVisualStyleBackColor = true;
            // 
            // BtnPreset_LTO
            // 
            this.BtnPreset_LTO.Location = new System.Drawing.Point(333, 150);
            this.BtnPreset_LTO.Name = "BtnPreset_LTO";
            this.BtnPreset_LTO.Size = new System.Drawing.Size(45, 30);
            this.BtnPreset_LTO.TabIndex = 3;
            this.BtnPreset_LTO.Text = "LTO";
            this.BtnPreset_LTO.UseVisualStyleBackColor = true;
            this.BtnPreset_LTO.Click += new System.EventHandler(this.BtnPreset_LTO_Click);
            // 
            // BtnPreset_tTO
            // 
            this.BtnPreset_tTO.Location = new System.Drawing.Point(282, 150);
            this.BtnPreset_tTO.Name = "BtnPreset_tTO";
            this.BtnPreset_tTO.Size = new System.Drawing.Size(45, 30);
            this.BtnPreset_tTO.TabIndex = 2;
            this.BtnPreset_tTO.Text = "tTO";
            this.BtnPreset_tTO.UseVisualStyleBackColor = true;
            this.BtnPreset_tTO.Click += new System.EventHandler(this.BtnPreset_tTO_Click);
            // 
            // SetupForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(441, 229);
            this.Controls.Add(this.BtnPreset_tTO);
            this.Controls.Add(this.BtnPreset_LTO);
            this.Controls.Add(this.BtnPreset_rTO);
            this.Controls.Add(this.TxtPorts);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.BtnSelect);
            this.Controls.Add(this.StatusStrip);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmbAdapter);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(459, 276);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(459, 276);
            this.Name = "SetupForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Trickster Packetman";
            this.Load += new System.EventHandler(this.SetupForm_Load);
            this.StatusStrip.ResumeLayout(false);
            this.StatusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbAdapter;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.StatusStrip StatusStrip;
        private System.Windows.Forms.ToolStripStatusLabel StatusLabel;
        private System.Windows.Forms.Button BtnSelect;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox TxtPorts;
        private System.Windows.Forms.Button BtnPreset_rTO;
        private System.Windows.Forms.Button BtnPreset_LTO;
        private System.Windows.Forms.Button BtnPreset_tTO;
    }
}

