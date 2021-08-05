namespace ImageFileLoader {
    partial class FormMain {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent() {
            ShimLib.ImageBoxOption imageBoxOption1 = new ShimLib.ImageBoxOption();
            this.imgBox = new ShimLib.ImageBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnOpenGdiPlus = new System.Windows.Forms.Button();
            this.dlgOpen = new System.Windows.Forms.OpenFileDialog();
            this.btnOpenOpenCV = new System.Windows.Forms.Button();
            this.btnOpenFreeImage = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // imgBox
            // 
            this.imgBox.BackColor = System.Drawing.Color.CornflowerBlue;
            this.imgBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.imgBox.Location = new System.Drawing.Point(0, 0);
            this.imgBox.Name = "imgBox";
            imageBoxOption1.CenterLineColor = System.Drawing.Color.Yellow;
            imageBoxOption1.InfoFont = ShimLib.EFont.unifont_13_0_06_bdf;
            imageBoxOption1.RoiRectangleColor = System.Drawing.Color.Blue;
            imageBoxOption1.TimeCheckCount = 100;
            imageBoxOption1.UseDrawCenterLine = true;
            imageBoxOption1.UseDrawCursorInfo = true;
            imageBoxOption1.UseDrawDebugInfo = true;
            imageBoxOption1.UseDrawPixelValue = true;
            imageBoxOption1.UseDrawRoiRectangles = true;
            imageBoxOption1.UseParallelToDraw = true;
            this.imgBox.Option = imageBoxOption1;
            this.imgBox.Size = new System.Drawing.Size(670, 539);
            this.imgBox.SzPan = new System.Drawing.Size(2, 2);
            this.imgBox.TabIndex = 0;
            this.imgBox.Text = "imageBox1";
            this.imgBox.ZoomLevel = 0;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnOpenFreeImage);
            this.panel1.Controls.Add(this.btnOpenOpenCV);
            this.panel1.Controls.Add(this.btnOpenGdiPlus);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel1.Location = new System.Drawing.Point(670, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(200, 539);
            this.panel1.TabIndex = 1;
            // 
            // btnOpenGdiPlus
            // 
            this.btnOpenGdiPlus.Location = new System.Drawing.Point(6, 12);
            this.btnOpenGdiPlus.Name = "btnOpenGdiPlus";
            this.btnOpenGdiPlus.Size = new System.Drawing.Size(93, 23);
            this.btnOpenGdiPlus.TabIndex = 0;
            this.btnOpenGdiPlus.Text = "GDI+";
            this.btnOpenGdiPlus.UseVisualStyleBackColor = true;
            this.btnOpenGdiPlus.Click += new System.EventHandler(this.btnOpenGdiPlus_Click);
            // 
            // dlgOpen
            // 
            this.dlgOpen.FileName = "openFileDialog1";
            this.dlgOpen.Filter = "Image Files(*.BMP;*.JPG;*.PNG;*.TIF)|*.BMP;*.JPG;*.GIF;*.TIF";
            // 
            // btnOpenOpenCV
            // 
            this.btnOpenOpenCV.Location = new System.Drawing.Point(6, 41);
            this.btnOpenOpenCV.Name = "btnOpenOpenCV";
            this.btnOpenOpenCV.Size = new System.Drawing.Size(93, 23);
            this.btnOpenOpenCV.TabIndex = 1;
            this.btnOpenOpenCV.Text = "OpenCV";
            this.btnOpenOpenCV.UseVisualStyleBackColor = true;
            this.btnOpenOpenCV.Click += new System.EventHandler(this.btnOpenOpenCV_Click);
            // 
            // btnOpenFreeImage
            // 
            this.btnOpenFreeImage.Location = new System.Drawing.Point(6, 70);
            this.btnOpenFreeImage.Name = "btnOpenFreeImage";
            this.btnOpenFreeImage.Size = new System.Drawing.Size(93, 23);
            this.btnOpenFreeImage.TabIndex = 2;
            this.btnOpenFreeImage.Text = "FreeImage";
            this.btnOpenFreeImage.UseVisualStyleBackColor = true;
            this.btnOpenFreeImage.Click += new System.EventHandler(this.btnOpenFreeImage_Click);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(870, 539);
            this.Controls.Add(this.imgBox);
            this.Controls.Add(this.panel1);
            this.Name = "FormMain";
            this.Text = "Form1";
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private ShimLib.ImageBox imgBox;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnOpenGdiPlus;
        private System.Windows.Forms.OpenFileDialog dlgOpen;
        private System.Windows.Forms.Button btnOpenOpenCV;
        private System.Windows.Forms.Button btnOpenFreeImage;
    }
}

