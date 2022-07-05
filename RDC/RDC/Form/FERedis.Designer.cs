
using Sunny.UI;

namespace RDC
{
    partial class FERedis
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
            this.textName = new Sunny.UI.UITextBox();
            this.uiLabel2 = new Sunny.UI.UILabel();
            this.uiLabel3 = new Sunny.UI.UILabel();
            this.uiLabel4 = new Sunny.UI.UILabel();
            this.uiLabel5 = new Sunny.UI.UILabel();
            this.uiLabel6 = new Sunny.UI.UILabel();
            this.textHost = new Sunny.UI.UITextBox();
            this.textPort = new Sunny.UI.UITextBox();
            this.textPassWord = new Sunny.UI.UITextBox();
            this.textUser = new Sunny.UI.UITextBox();
            this.pnlBtm.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlBtm
            // 
            this.pnlBtm.Location = new System.Drawing.Point(1, 304);
            this.pnlBtm.Size = new System.Drawing.Size(518, 55);
            this.pnlBtm.TabIndex = 7;
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(390, 12);
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(275, 12);
            this.btnOK.Click += new System.EventHandler(this.BtnOK_Click);
            // 
            // textName
            // 
            this.textName.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.textName.EnterAsTab = true;
            this.textName.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.textName.Location = new System.Drawing.Point(137, 56);
            this.textName.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.textName.MinimumSize = new System.Drawing.Size(1, 16);
            this.textName.Name = "textName";
            this.textName.Padding = new System.Windows.Forms.Padding(5);
            this.textName.ShowText = false;
            this.textName.Size = new System.Drawing.Size(340, 29);
            this.textName.TabIndex = 0;
            this.textName.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.textName.Watermark = "";
            this.textName.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // uiLabel2
            // 
            this.uiLabel2.AutoSize = true;
            this.uiLabel2.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.uiLabel2.Location = new System.Drawing.Point(56, 59);
            this.uiLabel2.Name = "uiLabel2";
            this.uiLabel2.Size = new System.Drawing.Size(52, 27);
            this.uiLabel2.TabIndex = 4;
            this.uiLabel2.Text = "名称";
            this.uiLabel2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.uiLabel2.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // uiLabel3
            // 
            this.uiLabel3.AutoSize = true;
            this.uiLabel3.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.uiLabel3.Location = new System.Drawing.Point(56, 99);
            this.uiLabel3.Name = "uiLabel3";
            this.uiLabel3.Size = new System.Drawing.Size(52, 27);
            this.uiLabel3.TabIndex = 6;
            this.uiLabel3.Text = "地址";
            this.uiLabel3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.uiLabel3.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // uiLabel4
            // 
            this.uiLabel4.AutoSize = true;
            this.uiLabel4.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.uiLabel4.Location = new System.Drawing.Point(56, 139);
            this.uiLabel4.Name = "uiLabel4";
            this.uiLabel4.Size = new System.Drawing.Size(52, 27);
            this.uiLabel4.TabIndex = 10;
            this.uiLabel4.Text = "端口";
            this.uiLabel4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.uiLabel4.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // uiLabel5
            // 
            this.uiLabel5.AutoSize = true;
            this.uiLabel5.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.uiLabel5.Location = new System.Drawing.Point(56, 179);
            this.uiLabel5.Name = "uiLabel5";
            this.uiLabel5.Size = new System.Drawing.Size(52, 27);
            this.uiLabel5.TabIndex = 12;
            this.uiLabel5.Text = "密码";
            this.uiLabel5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.uiLabel5.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // uiLabel6
            // 
            this.uiLabel6.AutoSize = true;
            this.uiLabel6.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.uiLabel6.Location = new System.Drawing.Point(56, 219);
            this.uiLabel6.Name = "uiLabel6";
            this.uiLabel6.Size = new System.Drawing.Size(72, 27);
            this.uiLabel6.TabIndex = 13;
            this.uiLabel6.Text = "用户名";
            this.uiLabel6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.uiLabel6.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // textHost
            // 
            this.textHost.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.textHost.EnterAsTab = true;
            this.textHost.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.textHost.Location = new System.Drawing.Point(137, 96);
            this.textHost.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.textHost.MinimumSize = new System.Drawing.Size(1, 16);
            this.textHost.Name = "textHost";
            this.textHost.Padding = new System.Windows.Forms.Padding(5);
            this.textHost.ShowText = false;
            this.textHost.Size = new System.Drawing.Size(340, 29);
            this.textHost.TabIndex = 3;
            this.textHost.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.textHost.Watermark = "";
            this.textHost.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // textPort
            // 
            this.textPort.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.textPort.EnterAsTab = true;
            this.textPort.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.textPort.Location = new System.Drawing.Point(137, 138);
            this.textPort.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.textPort.MinimumSize = new System.Drawing.Size(1, 16);
            this.textPort.Name = "textPort";
            this.textPort.Padding = new System.Windows.Forms.Padding(5);
            this.textPort.ShowText = false;
            this.textPort.Size = new System.Drawing.Size(340, 29);
            this.textPort.TabIndex = 4;
            this.textPort.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.textPort.Watermark = "";
            this.textPort.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // textPassWord
            // 
            this.textPassWord.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.textPassWord.EnterAsTab = true;
            this.textPassWord.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.textPassWord.Location = new System.Drawing.Point(137, 179);
            this.textPassWord.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.textPassWord.MinimumSize = new System.Drawing.Size(1, 16);
            this.textPassWord.Name = "textPassWord";
            this.textPassWord.Padding = new System.Windows.Forms.Padding(5);
            this.textPassWord.ShowText = false;
            this.textPassWord.Size = new System.Drawing.Size(340, 29);
            this.textPassWord.TabIndex = 4;
            this.textPassWord.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.textPassWord.Watermark = "";
            this.textPassWord.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // textUser
            // 
            this.textUser.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.textUser.EnterAsTab = true;
            this.textUser.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.textUser.Location = new System.Drawing.Point(137, 219);
            this.textUser.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.textUser.MinimumSize = new System.Drawing.Size(1, 16);
            this.textUser.Name = "textUser";
            this.textUser.Padding = new System.Windows.Forms.Padding(5);
            this.textUser.ShowText = false;
            this.textUser.Size = new System.Drawing.Size(340, 29);
            this.textUser.TabIndex = 4;
            this.textUser.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.textUser.Watermark = "";
            this.textUser.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // FERedis
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(520, 362);
            this.Controls.Add(this.textUser);
            this.Controls.Add(this.textPassWord);
            this.Controls.Add(this.textPort);
            this.Controls.Add(this.textHost);
            this.Controls.Add(this.uiLabel6);
            this.Controls.Add(this.uiLabel5);
            this.Controls.Add(this.uiLabel4);
            this.Controls.Add(this.uiLabel3);
            this.Controls.Add(this.textName);
            this.Controls.Add(this.uiLabel2);
            this.Name = "FERedis";
            this.Text = "Redis";
            this.ZoomScaleRect = new System.Drawing.Rectangle(19, 19, 520, 362);
            this.Controls.SetChildIndex(this.uiLabel2, 0);
            this.Controls.SetChildIndex(this.textName, 0);
            this.Controls.SetChildIndex(this.uiLabel3, 0);
            this.Controls.SetChildIndex(this.uiLabel4, 0);
            this.Controls.SetChildIndex(this.uiLabel5, 0);
            this.Controls.SetChildIndex(this.uiLabel6, 0);
            this.Controls.SetChildIndex(this.pnlBtm, 0);
            this.Controls.SetChildIndex(this.textHost, 0);
            this.Controls.SetChildIndex(this.textPort, 0);
            this.Controls.SetChildIndex(this.textPassWord, 0);
            this.Controls.SetChildIndex(this.textUser, 0);
            this.pnlBtm.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private UITextBox textName;
        private UILabel uiLabel2;
        private UILabel uiLabel3;
        private UILabel uiLabel4;
        private UILabel uiLabel5;
        private UILabel uiLabel6;
        private UITextBox textHost;
        private UITextBox textPort;
        private UITextBox textPassWord;
        private UITextBox textUser;
    }
}