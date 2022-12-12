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
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.dgHangHoa = new System.Windows.Forms.DataGridView();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.dgNV = new System.Windows.Forms.DataGridView();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.dg = new System.Windows.Forms.TabPage();
            this.dgKH = new System.Windows.Forms.DataGridView();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.dgCTCNo = new System.Windows.Forms.DataGridView();
            this.tabPage6 = new System.Windows.Forms.TabPage();
            this.dgTongHopCN = new System.Windows.Forms.DataGridView();
            this.btnTHCN = new System.Windows.Forms.Button();
            this.dgTheKho = new System.Windows.Forms.DataGridView();
            this.dgTonKho = new System.Windows.Forms.DataGridView();
            this.btnTHTK = new System.Windows.Forms.Button();
            this.tabNH.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgHangHoa)).BeginInit();
            this.tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgNV)).BeginInit();
            this.tabPage4.SuspendLayout();
            this.dg.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgKH)).BeginInit();
            this.tabPage5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgCTCNo)).BeginInit();
            this.tabPage6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgTongHopCN)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgTheKho)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgTonKho)).BeginInit();
            this.SuspendLayout();
            // 
            // btnTaoDM
            // 
            this.btnTaoDM.Location = new System.Drawing.Point(57, 37);
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
            this.btnLoadDM.Location = new System.Drawing.Point(228, 37);
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
            this.tabNH.Controls.Add(this.tabPage5);
            this.tabNH.Controls.Add(this.tabPage6);
            this.tabNH.Location = new System.Drawing.Point(12, 72);
            this.tabNH.Name = "tabNH";
            this.tabNH.SelectedIndex = 0;
            this.tabNH.Size = new System.Drawing.Size(787, 379);
            this.tabNH.TabIndex = 1;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.dgTheKho);
            this.tabPage1.Location = new System.Drawing.Point(4, 29);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(779, 346);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Thẻ kho";
            this.tabPage1.UseVisualStyleBackColor = true;
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
            this.tabPage4.Controls.Add(this.dgTonKho);
            this.tabPage4.Location = new System.Drawing.Point(4, 29);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(779, 346);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "Tổng hợp Tồn kho";
            this.tabPage4.UseVisualStyleBackColor = true;
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
            // tabPage5
            // 
            this.tabPage5.Controls.Add(this.dgCTCNo);
            this.tabPage5.Location = new System.Drawing.Point(4, 29);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage5.Size = new System.Drawing.Size(779, 346);
            this.tabPage5.TabIndex = 5;
            this.tabPage5.Text = "CT Công nợ";
            this.tabPage5.UseVisualStyleBackColor = true;
            // 
            // dgCTCNo
            // 
            this.dgCTCNo.AllowUserToAddRows = false;
            this.dgCTCNo.AllowUserToDeleteRows = false;
            this.dgCTCNo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgCTCNo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgCTCNo.Location = new System.Drawing.Point(3, 3);
            this.dgCTCNo.Name = "dgCTCNo";
            this.dgCTCNo.ReadOnly = true;
            this.dgCTCNo.RowHeadersWidth = 51;
            this.dgCTCNo.RowTemplate.Height = 29;
            this.dgCTCNo.Size = new System.Drawing.Size(773, 340);
            this.dgCTCNo.TabIndex = 5;
            // 
            // tabPage6
            // 
            this.tabPage6.Controls.Add(this.dgTongHopCN);
            this.tabPage6.Location = new System.Drawing.Point(4, 29);
            this.tabPage6.Name = "tabPage6";
            this.tabPage6.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage6.Size = new System.Drawing.Size(779, 346);
            this.tabPage6.TabIndex = 6;
            this.tabPage6.Text = "Tổng hợp CN";
            this.tabPage6.UseVisualStyleBackColor = true;
            // 
            // dgTongHopCN
            // 
            this.dgTongHopCN.AllowUserToAddRows = false;
            this.dgTongHopCN.AllowUserToDeleteRows = false;
            this.dgTongHopCN.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgTongHopCN.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgTongHopCN.Location = new System.Drawing.Point(3, 3);
            this.dgTongHopCN.Name = "dgTongHopCN";
            this.dgTongHopCN.ReadOnly = true;
            this.dgTongHopCN.RowHeadersWidth = 51;
            this.dgTongHopCN.RowTemplate.Height = 29;
            this.dgTongHopCN.Size = new System.Drawing.Size(773, 340);
            this.dgTongHopCN.TabIndex = 6;
            // 
            // btnTHCN
            // 
            this.btnTHCN.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnTHCN.Location = new System.Drawing.Point(425, 37);
            this.btnTHCN.Name = "btnTHCN";
            this.btnTHCN.Size = new System.Drawing.Size(141, 29);
            this.btnTHCN.TabIndex = 0;
            this.btnTHCN.Text = "Tổng hợp CN";
            this.btnTHCN.UseVisualStyleBackColor = true;
            this.btnTHCN.Click += new System.EventHandler(this.btnTHCN_Click);
            // 
            // dgTheKho
            // 
            this.dgTheKho.AllowUserToAddRows = false;
            this.dgTheKho.AllowUserToDeleteRows = false;
            this.dgTheKho.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgTheKho.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgTheKho.Location = new System.Drawing.Point(3, 3);
            this.dgTheKho.Name = "dgTheKho";
            this.dgTheKho.ReadOnly = true;
            this.dgTheKho.RowHeadersWidth = 51;
            this.dgTheKho.RowTemplate.Height = 29;
            this.dgTheKho.Size = new System.Drawing.Size(773, 340);
            this.dgTheKho.TabIndex = 4;
            // 
            // dgTonKho
            // 
            this.dgTonKho.AllowUserToAddRows = false;
            this.dgTonKho.AllowUserToDeleteRows = false;
            this.dgTonKho.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgTonKho.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgTonKho.Location = new System.Drawing.Point(3, 3);
            this.dgTonKho.Name = "dgTonKho";
            this.dgTonKho.ReadOnly = true;
            this.dgTonKho.RowHeadersWidth = 51;
            this.dgTonKho.RowTemplate.Height = 29;
            this.dgTonKho.Size = new System.Drawing.Size(773, 340);
            this.dgTonKho.TabIndex = 4;
            // 
            // btnTHTK
            // 
            this.btnTHTK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnTHTK.Location = new System.Drawing.Point(606, 37);
            this.btnTHTK.Name = "btnTHTK";
            this.btnTHTK.Size = new System.Drawing.Size(141, 29);
            this.btnTHTK.TabIndex = 0;
            this.btnTHTK.Text = "Tổng hợp TK";
            this.btnTHTK.UseVisualStyleBackColor = true;
            this.btnTHTK.Click += new System.EventHandler(this.btnTHTK_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.tabNH);
            this.Controls.Add(this.btnTHTK);
            this.Controls.Add(this.btnTHCN);
            this.Controls.Add(this.btnLoadDM);
            this.Controls.Add(this.btnTaoDM);
            this.Name = "Form1";
            this.Text = "Form1";
            this.tabNH.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgHangHoa)).EndInit();
            this.tabPage3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgNV)).EndInit();
            this.tabPage4.ResumeLayout(false);
            this.dg.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgKH)).EndInit();
            this.tabPage5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgCTCNo)).EndInit();
            this.tabPage6.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgTongHopCN)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgTheKho)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgTonKho)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Button btnTaoDM;
        private Button btnLoadDM;
        private TabControl tabNH;
        private TabPage tabPage1;
        private TabPage tabPage2;
        private TabPage tabPage3;
        private TabPage tabPage4;
        private DataGridView dgHangHoa;
        private DataGridView dgNV;
        private TabPage dg;
        private DataGridView dgKH;
        private TabPage tabPage5;
        private DataGridView dgCTCNo;
        private TabPage tabPage6;
        private DataGridView dgTongHopCN;
        private Button btnTHCN;
        private DataGridView dgTheKho;
        private DataGridView dgTonKho;
        private Button btnTHTK;
    }
}