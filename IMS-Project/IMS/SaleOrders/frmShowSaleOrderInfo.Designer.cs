namespace IMS.SaleOrders
{
    partial class frmShowSaleOrderInfo
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
            this.ctrlSaleOrderInfo1 = new IMS.SaleOrders.ctrlSaleOrderInfo();
            this.btnClose = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // ctrlSaleOrderInfo1
            // 
            this.ctrlSaleOrderInfo1.Location = new System.Drawing.Point(12, 21);
            this.ctrlSaleOrderInfo1.Name = "ctrlSaleOrderInfo1";
            this.ctrlSaleOrderInfo1.Size = new System.Drawing.Size(555, 258);
            this.ctrlSaleOrderInfo1.TabIndex = 0;
            // 
            // btnClose
            // 
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.Location = new System.Drawing.Point(477, 304);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(90, 38);
            this.btnClose.TabIndex = 107;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // frmShowSaleOrderInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(588, 354);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.ctrlSaleOrderInfo1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmShowSaleOrderInfo";
            this.Text = "frmSaleOrderInfo";
            this.Load += new System.EventHandler(this.frmShowSaleOrderInfo_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private ctrlSaleOrderInfo ctrlSaleOrderInfo1;
        private System.Windows.Forms.Button btnClose;
    }
}