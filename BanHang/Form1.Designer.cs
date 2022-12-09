namespace BanHang
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
            this.btnTaoDM = new System.Windows.Forms.Button();
            this.btnLoadDM = new System.Windows.Forms.Button();
            this.tabNH = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.dgNH = new System.Windows.Forms.DataGridView();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.dgHangHoa = new System.Windows.Forms.DataGridView();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.dgNV = new System.Windows.Forms.DataGridView();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.dgLK = new System.Windows.Forms.DataGridView();
            this.dg = new System.Windows.Forms.TabPage();
            this.dgKH = new System.Windows.Forms.DataGridView();
            this.tabNH.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgNH)).BeginInit();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgHangHoa)).BeginInit();
            this.tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgNV)).BeginInit();
            this.tabPage4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgLK)).BeginInit();
            this.dg.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgKH)).BeginInit();
            this.SuspendLayout();
            // 
            // btnTaoDM
            // 
            this.btnTaoDM.Location = new System.Drawing.Point(112, 37);
            this.btnTaoDM.Name = "btnTaoDM";
            this.btnTaoDM.Size = new System.Drawing.Size(134, 29);
            this.btnTaoDM.TabIndex = 0;
            this.btnTaoDM.Text = "Tạo danh mục";
            this.btnTaoDM.UseVisualStyleBackColor = true;
            this.btnTaoDM.Click += new System.EventHandler(this.btnTaoDM_Click);
            // 
            // btnLoadDM
            // 
            this.btnLoadDM.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLoadDM.Location = new System.Drawing.Point(506, 37);
            this.btnLoadDM.Name = "btnLoadDM";
            this.btnLoadDM.Size = new System.Drawing.Size(141, 29);
            this.btnLoadDM.TabIndex = 0;
            this.btnLoadDM.Text = "Load danh mục";
            this.btnLoadDM.UseVisualStyleBackColor = true;
            this.btnLoadDM.Click += new System.EventHandler(this.btnLoadDM_Click);
            // 
            // tabNH
            // 
            this.tabNH.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabNH.Controls.Add(this.tabPage1);
            this.tabNH.Controls.Add(this.tabPage2);
            this.tabNH.Controls.Add(this.tabPage3);
            this.tabNH.Controls.Add(this.tabPage4);
            this.tabNH.Controls.Add(this.dg);
            this.tabNH.Location = new System.Drawing.Point(12, 72);
            this.tabNH.Name = "tabNH";
            this.tabNH.SelectedIndex = 0;
            this.tabNH.Size = new System.Drawing.Size(787, 379);
            this.tabNH.TabIndex = 1;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.dgNH);
            this.tabPage1.Location = new System.Drawing.Point(4, 29);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(779, 346);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Nhóm hàng";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // dgNH
            // 
            this.dgNH.AllowUserToAddRows = false;
            this.dgNH.AllowUserToDeleteRows = false;
            this.dgNH.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgNH.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgNH.Location = new System.Drawing.Point(3, 3);
            this.dgNH.Name = "dgNH";
            this.dgNH.ReadOnly = true;
            this.dgNH.RowHeadersWidth = 51;
            this.dgNH.RowTemplate.Height = 29;
            this.dgNH.Size = new System.Drawing.Size(773, 340);
            this.dgNH.TabIndex = 2;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.dgHangHoa);
            this.tabPage2.Location = new System.Drawing.Point(4, 29);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(779, 346);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Hàng hóa";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // dgHangHoa
            // 
            this.dgHangHoa.AllowUserToAddRows = false;
            this.dgHangHoa.AllowUserToDeleteRows = false;
            this.dgHangHoa.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgHangHoa.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgHangHoa.Location = new System.Drawing.Point(3, 3);
            this.dgHangHoa.Name = "dgHangHoa";
            this.dgHangHoa.ReadOnly = true;
            this.dgHangHoa.RowHeadersWidth = 51;
            this.dgHangHoa.RowTemplate.Height = 29;
            this.dgHangHoa.Size = new System.Drawing.Size(773, 340);
            this.dgHangHoa.TabIndex = 3;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.dgNV);
            this.tabPage3.Location = new System.Drawing.Point(4, 29);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(779, 346);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Nhân viên";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // dgNV
            // 
            this.dgNV.AllowUserToAddRows = false;
            this.dgNV.AllowUserToDeleteRows = false;
            this.dgNV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgNV.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgNV.Location = new System.Drawing.Point(3, 3);
            this.dgNV.Name = "dgNV";
            this.dgNV.ReadOnly = true;
            this.dgNV.RowHeadersWidth = 51;
            this.dgNV.RowTemplate.Height = 29;
            this.dgNV.Size = new System.Drawing.Size(773, 340);
            this.dgNV.TabIndex = 4;
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.dgLK);
            this.tabPage4.Location = new System.Drawing.Point(4, 29);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(779, 346);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "Loại khách";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // dgLK
            // 
            this.dgLK.AllowUserToAddRows = false;
            this.dgLK.AllowUserToDeleteRows = false;
            this.dgLK.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgLK.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgLK.Location = new System.Drawing.Point(3, 3);
            this.dgLK.Name = "dgLK";
            this.dgLK.ReadOnly = true;
            this.dgLK.RowHeadersWidth = 51;
            this.dgLK.RowTemplate.Height = 29;
            this.dgLK.Size = new System.Drawing.Size(773, 340);
            this.dgLK.TabIndex = 4;
            // 
            // dg
            // 
            this.dg.Controls.Add(this.dgKH);
            this.dg.Location = new System.Drawing.Point(4, 29);
            this.dg.Name = "dg";
            this.dg.Padding = new System.Windows.Forms.Padding(3);
            this.dg.Size = new System.Drawing.Size(779, 346);
            this.dg.TabIndex = 4;
            this.dg.Text = "Khách hàng";
            this.dg.UseVisualStyleBackColor = true;
            // 
            // dgKH
            // 
            this.dgKH.AllowUserToAddRows = false;
            this.dgKH.AllowUserToDeleteRows = false;
            this.dgKH.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgKH.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgKH.Location = new System.Drawing.Point(3, 3);
            this.dgKH.Name = "dgKH";
            this.dgKH.ReadOnly = true;
            this.dgKH.RowHeadersWidth = 51;
            this.dgKH.RowTemplate.Height = 29;
            this.dgKH.Size = new System.Drawing.Size(773, 340);
            this.dgKH.TabIndex = 4;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.tabNH);
            this.Controls.Add(this.btnLoadDM);
            this.Controls.Add(this.btnTaoDM);
            this.Name = "Form1";
            this.Text = "Form1";
            this.tabNH.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgNH)).EndInit();
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgHangHoa)).EndInit();
            this.tabPage3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgNV)).EndInit();
            this.tabPage4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgLK)).EndInit();
            this.dg.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgKH)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Button btnTaoDM;
        private Button btnLoadDM;
        private TabControl tabNH;
        private TabPage tabPage1;
        private DataGridView dgNH;
        private TabPage tabPage2;
        private TabPage tabPage3;
        private TabPage tabPage4;
        private DataGridView dgHangHoa;
        private DataGridView dgNV;
        private DataGridView dgLK;
        private TabPage dg;
        private DataGridView dgKH;
    }
}