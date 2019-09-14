namespace Newspaper
{
    partial class frmNewspaper
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmNewspaper));
            this.btnTeacher = new System.Windows.Forms.Button();
            this.labTips = new System.Windows.Forms.Label();
            this.labProgress = new System.Windows.Forms.Label();
            this.btnDelDir = new System.Windows.Forms.Button();
            this.btnEducation = new System.Windows.Forms.Button();
            this.btnMerge = new System.Windows.Forms.Button();
            this.dtpStart = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.dtpEnd = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.cbAutoMergePdf = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // btnTeacher
            // 
            this.btnTeacher.Location = new System.Drawing.Point(69, 129);
            this.btnTeacher.Name = "btnTeacher";
            this.btnTeacher.Size = new System.Drawing.Size(172, 42);
            this.btnTeacher.TabIndex = 0;
            this.btnTeacher.Text = "中国教师报";
            this.btnTeacher.UseVisualStyleBackColor = true;
            this.btnTeacher.Click += new System.EventHandler(this.btnTeacher_Click);
            // 
            // labTips
            // 
            this.labTips.AutoSize = true;
            this.labTips.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.labTips.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.labTips.Location = new System.Drawing.Point(0, 301);
            this.labTips.Name = "labTips";
            this.labTips.Size = new System.Drawing.Size(0, 13);
            this.labTips.TabIndex = 1;
            // 
            // labProgress
            // 
            this.labProgress.AutoSize = true;
            this.labProgress.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labProgress.ForeColor = System.Drawing.Color.Red;
            this.labProgress.Location = new System.Drawing.Point(42, 17);
            this.labProgress.Name = "labProgress";
            this.labProgress.Size = new System.Drawing.Size(0, 20);
            this.labProgress.TabIndex = 2;
            // 
            // btnDelDir
            // 
            this.btnDelDir.Location = new System.Drawing.Point(69, 220);
            this.btnDelDir.Name = "btnDelDir";
            this.btnDelDir.Size = new System.Drawing.Size(172, 42);
            this.btnDelDir.TabIndex = 3;
            this.btnDelDir.Text = "删除空文件夹";
            this.btnDelDir.UseVisualStyleBackColor = true;
            this.btnDelDir.Click += new System.EventHandler(this.btnDelDir_Click);
            // 
            // btnEducation
            // 
            this.btnEducation.Location = new System.Drawing.Point(301, 129);
            this.btnEducation.Name = "btnEducation";
            this.btnEducation.Size = new System.Drawing.Size(172, 42);
            this.btnEducation.TabIndex = 4;
            this.btnEducation.Text = "中国教育报";
            this.btnEducation.UseVisualStyleBackColor = true;
            this.btnEducation.Click += new System.EventHandler(this.btnEducation_Click);
            // 
            // btnMerge
            // 
            this.btnMerge.Location = new System.Drawing.Point(301, 220);
            this.btnMerge.Name = "btnMerge";
            this.btnMerge.Size = new System.Drawing.Size(172, 42);
            this.btnMerge.TabIndex = 5;
            this.btnMerge.Text = "合并PDF";
            this.btnMerge.UseVisualStyleBackColor = true;
            this.btnMerge.Click += new System.EventHandler(this.btnMerge_Click);
            // 
            // dtpStart
            // 
            this.dtpStart.Location = new System.Drawing.Point(89, 54);
            this.dtpStart.Name = "dtpStart";
            this.dtpStart.Size = new System.Drawing.Size(172, 20);
            this.dtpStart.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(28, 58);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "开始日期:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.ForeColor = System.Drawing.Color.Red;
            this.label2.Location = new System.Drawing.Point(291, 17);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(0, 20);
            this.label2.TabIndex = 2;
            // 
            // dtpEnd
            // 
            this.dtpEnd.Location = new System.Drawing.Point(338, 54);
            this.dtpEnd.Name = "dtpEnd";
            this.dtpEnd.Size = new System.Drawing.Size(172, 20);
            this.dtpEnd.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(277, 58);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(58, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "结束日期:";
            // 
            // cbAutoMergePdf
            // 
            this.cbAutoMergePdf.AutoSize = true;
            this.cbAutoMergePdf.Checked = true;
            this.cbAutoMergePdf.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbAutoMergePdf.Location = new System.Drawing.Point(89, 92);
            this.cbAutoMergePdf.Name = "cbAutoMergePdf";
            this.cbAutoMergePdf.Size = new System.Drawing.Size(98, 17);
            this.cbAutoMergePdf.TabIndex = 8;
            this.cbAutoMergePdf.Text = "是否自动合并";
            this.cbAutoMergePdf.UseVisualStyleBackColor = true;
            // 
            // frmNewspaper
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(560, 314);
            this.Controls.Add(this.cbAutoMergePdf);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.dtpEnd);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dtpStart);
            this.Controls.Add(this.btnMerge);
            this.Controls.Add(this.btnEducation);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnDelDir);
            this.Controls.Add(this.labProgress);
            this.Controls.Add(this.labTips);
            this.Controls.Add(this.btnTeacher);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmNewspaper";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "报纸资源下载工具";
            this.Load += new System.EventHandler(this.frmNewspaper_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnTeacher;
        private System.Windows.Forms.Label labTips;
        private System.Windows.Forms.Label labProgress;
        private System.Windows.Forms.Button btnDelDir;
        private System.Windows.Forms.Button btnEducation;
        private System.Windows.Forms.Button btnMerge;
        private System.Windows.Forms.DateTimePicker dtpStart;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtpEnd;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox cbAutoMergePdf;
    }
}

