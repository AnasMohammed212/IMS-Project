namespace IMS.PurchaseOrders
{
    partial class frmShowPurchaseOrderInfo
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
            this.ctrlPurchaseOrderCard1 = new IMS.PurchaseOrders.ctrlPurchaseOrderCard();
            this.btnClose = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // ctrlPurchaseOrderCard1
            // 
            this.ctrlPurchaseOrderCard1.Location = new System.Drawing.Point(0, 1);
            this.ctrlPurchaseOrderCard1.Name = "ctrlPurchaseOrderCard1";
            this.ctrlPurchaseOrderCard1.Size = new System.Drawing.Size(555, 268);
            this.ctrlPurchaseOrderCard1.TabIndex = 0;
            // 
            // btnClose
            // 
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.Location = new System.Drawing.Point(465, 304);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(90, 38);
            this.btnClose.TabIndex = 106;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // frmShowPurchaseOrderInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(588, 354);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.ctrlPurchaseOrderCard1);
            this.Name = "frmShowPurchaseOrderInfo";
            this.Text = "frmShowPurchaseOrderInfo";
            this.Load += new System.EventHandler(this.frmShowPurchaseOrderInfo_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private ctrlPurchaseOrderCard ctrlPurchaseOrderCard1;
        private System.Windows.Forms.Button btnClose;
    }
}