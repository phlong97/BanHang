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
            this.dgDanhMuc = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgDanhMuc)).BeginInit();
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
            // dgDanhMuc
            // 
            this.dgDanhMuc.AllowUserToAddRows = false;
            this.dgDanhMuc.AllowUserToDeleteRows = false;
            this.dgDanhMuc.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgDanhMuc.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgDanhMuc.Location = new System.Drawing.Point(12, 114);
            this.dgDanhMuc.Name = "dgDanhMuc";
            this.dgDanhMuc.ReadOnly = true;
            this.dgDanhMuc.RowHeadersWidth = 51;
            this.dgDanhMuc.RowTemplate.Height = 29;
            this.dgDanhMuc.Size = new System.Drawing.Size(776, 324);
            this.dgDanhMuc.TabIndex = 1;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.dgDanhMuc);
            this.Controls.Add(this.btnLoadDM);
            this.Controls.Add(this.btnTaoDM);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.dgDanhMuc)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Button btnTaoDM;
        private Button btnLoadDM;
        private DataGridView dgDanhMuc;
    }
}