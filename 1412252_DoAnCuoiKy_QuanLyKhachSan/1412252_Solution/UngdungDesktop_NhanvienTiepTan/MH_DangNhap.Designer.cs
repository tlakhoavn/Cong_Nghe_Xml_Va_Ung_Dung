namespace UngdungDesktop_NhanvienTiepTan
{
    partial class MH_DangNhap
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MH_DangNhap));
            this.btnDangNhap = new System.Windows.Forms.Button();
            this.labelTenDangNhap = new System.Windows.Forms.Label();
            this.labelMatKhau = new System.Windows.Forms.Label();
            this.txtbTenDangNhap = new System.Windows.Forms.TextBox();
            this.txtbMatKhau = new System.Windows.Forms.TextBox();
            this.groupBoxDangNhap = new System.Windows.Forms.GroupBox();
            this.labelChaoMung = new System.Windows.Forms.Label();
            this.btnImgLogo = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBoxDangNhap.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnDangNhap
            // 
            resources.ApplyResources(this.btnDangNhap, "btnDangNhap");
            this.btnDangNhap.Name = "btnDangNhap";
            this.btnDangNhap.UseVisualStyleBackColor = true;
            this.btnDangNhap.Click += new System.EventHandler(this.btnDangNhap_Click);
            // 
            // labelTenDangNhap
            // 
            resources.ApplyResources(this.labelTenDangNhap, "labelTenDangNhap");
            this.labelTenDangNhap.Name = "labelTenDangNhap";
            // 
            // labelMatKhau
            // 
            resources.ApplyResources(this.labelMatKhau, "labelMatKhau");
            this.labelMatKhau.Name = "labelMatKhau";
            // 
            // txtbTenDangNhap
            // 
            resources.ApplyResources(this.txtbTenDangNhap, "txtbTenDangNhap");
            this.txtbTenDangNhap.Name = "txtbTenDangNhap";
            // 
            // txtbMatKhau
            // 
            resources.ApplyResources(this.txtbMatKhau, "txtbMatKhau");
            this.txtbMatKhau.Name = "txtbMatKhau";
            this.txtbMatKhau.UseSystemPasswordChar = true;
            this.txtbMatKhau.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtbMatKhau_KeyDown);
            // 
            // groupBoxDangNhap
            // 
            this.groupBoxDangNhap.BackColor = System.Drawing.Color.Tan;
            this.groupBoxDangNhap.Controls.Add(this.txtbMatKhau);
            this.groupBoxDangNhap.Controls.Add(this.btnDangNhap);
            this.groupBoxDangNhap.Controls.Add(this.txtbTenDangNhap);
            this.groupBoxDangNhap.Controls.Add(this.labelTenDangNhap);
            this.groupBoxDangNhap.Controls.Add(this.labelMatKhau);
            resources.ApplyResources(this.groupBoxDangNhap, "groupBoxDangNhap");
            this.groupBoxDangNhap.Name = "groupBoxDangNhap";
            this.groupBoxDangNhap.TabStop = false;
            // 
            // labelChaoMung
            // 
            resources.ApplyResources(this.labelChaoMung, "labelChaoMung");
            this.labelChaoMung.Name = "labelChaoMung";
            // 
            // btnImgLogo
            // 
            this.btnImgLogo.FlatAppearance.BorderSize = 0;
            resources.ApplyResources(this.btnImgLogo, "btnImgLogo");
            this.btnImgLogo.Name = "btnImgLogo";
            this.btnImgLogo.TabStop = false;
            this.btnImgLogo.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.ForeColor = System.Drawing.Color.MidnightBlue;
            this.label1.Name = "label1";
            // 
            // MH_DangNhap
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.OrangeRed;
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnImgLogo);
            this.Controls.Add(this.labelChaoMung);
            this.Controls.Add(this.groupBoxDangNhap);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "MH_DangNhap";
            this.Load += new System.EventHandler(this.MH_DangNhap_Load);
            this.groupBoxDangNhap.ResumeLayout(false);
            this.groupBoxDangNhap.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnDangNhap;
        private System.Windows.Forms.Label labelTenDangNhap;
        private System.Windows.Forms.Label labelMatKhau;
        private System.Windows.Forms.TextBox txtbTenDangNhap;
        private System.Windows.Forms.TextBox txtbMatKhau;
        private System.Windows.Forms.GroupBox groupBoxDangNhap;
        private System.Windows.Forms.Label labelChaoMung;
        private System.Windows.Forms.Button btnImgLogo;
        private System.Windows.Forms.Label label1;
    }
}