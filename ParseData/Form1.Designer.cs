namespace ParseData
{
    partial class Form1
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置受控資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改
        /// 這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            this.txt_fromday = new System.Windows.Forms.TextBox();
            this.txt_endday = new System.Windows.Forms.TextBox();
            this.btn_ListedAlert = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.btn_OTCAlert = new System.Windows.Forms.Button();
            this.btn_LstopTrade = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txt_fromday
            // 
            this.txt_fromday.Location = new System.Drawing.Point(35, 16);
            this.txt_fromday.Name = "txt_fromday";
            this.txt_fromday.Size = new System.Drawing.Size(114, 22);
            this.txt_fromday.TabIndex = 0;
            this.txt_fromday.Text = "110/12/08";
            this.txt_fromday.TextChanged += new System.EventHandler(this.txt_fromday_TextChanged);
            // 
            // txt_endday
            // 
            this.txt_endday.Location = new System.Drawing.Point(35, 55);
            this.txt_endday.Name = "txt_endday";
            this.txt_endday.Size = new System.Drawing.Size(114, 22);
            this.txt_endday.TabIndex = 1;
            this.txt_endday.Text = "110/12/09";
            this.txt_endday.TextChanged += new System.EventHandler(this.txt_endday_TextChanged);
            // 
            // btn_ListedAlert
            // 
            this.btn_ListedAlert.Location = new System.Drawing.Point(53, 92);
            this.btn_ListedAlert.Name = "btn_ListedAlert";
            this.btn_ListedAlert.Size = new System.Drawing.Size(75, 23);
            this.btn_ListedAlert.TabIndex = 2;
            this.btn_ListedAlert.Text = "ListedAlert";
            this.btn_ListedAlert.UseVisualStyleBackColor = true;
            this.btn_ListedAlert.Click += new System.EventHandler(this.btn_ListedAlert_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(73, 179);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(32, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "Status";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // btn_OTCAlert
            // 
            this.btn_OTCAlert.Location = new System.Drawing.Point(53, 121);
            this.btn_OTCAlert.Name = "btn_OTCAlert";
            this.btn_OTCAlert.Size = new System.Drawing.Size(75, 23);
            this.btn_OTCAlert.TabIndex = 4;
            this.btn_OTCAlert.Text = "OTCAlert";
            this.btn_OTCAlert.UseVisualStyleBackColor = true;
            this.btn_OTCAlert.Click += new System.EventHandler(this.btn_OTCAlert_Click);
            // 
            // btn_LstopTrade
            // 
            this.btn_LstopTrade.Location = new System.Drawing.Point(53, 150);
            this.btn_LstopTrade.Name = "btn_LstopTrade";
            this.btn_LstopTrade.Size = new System.Drawing.Size(75, 23);
            this.btn_LstopTrade.TabIndex = 5;
            this.btn_LstopTrade.Text = "LstopTrade";
            this.btn_LstopTrade.UseVisualStyleBackColor = true;
            this.btn_LstopTrade.Click += new System.EventHandler(this.btn_LstopTrade_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(186, 199);
            this.Controls.Add(this.btn_LstopTrade);
            this.Controls.Add(this.btn_OTCAlert);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btn_ListedAlert);
            this.Controls.Add(this.txt_endday);
            this.Controls.Add(this.txt_fromday);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txt_fromday;
        private System.Windows.Forms.TextBox txt_endday;
        private System.Windows.Forms.Button btn_ListedAlert;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btn_OTCAlert;
        private System.Windows.Forms.Button btn_LstopTrade;
    }
}

