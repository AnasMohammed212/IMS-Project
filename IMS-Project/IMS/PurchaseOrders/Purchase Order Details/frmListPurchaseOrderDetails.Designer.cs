namespace IMS.PurchaseOrders.Purchase_Order_Details
{
    partial class frmListPurchaseOrderDetails
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
            this.components = new System.ComponentModel.Container();
            this.txtFilterValue = new System.Windows.Forms.TextBox();
            this.btnShowAddDetail = new System.Windows.Forms.Button();
            this.lblFilterBy = new System.Windows.Forms.Label();
            this.cbFilterBy = new System.Windows.Forms.ComboBox();
            this.dgvDetails = new System.Windows.Forms.DataGridView();
            this.lblRecordsCount = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.cmsDetails = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.addDetailToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editDetailToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteDetailToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetails)).BeginInit();
            this.cmsDetails.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtFilterValue
            // 
            this.txtFilterValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFilterValue.Location = new System.Drawing.Point(431, 47);
            this.txtFilterValue.Name = "txtFilterValue";
            this.txtFilterValue.Size = new System.Drawing.Size(171, 34);
            this.txtFilterValue.TabIndex = 25;
            this.txtFilterValue.TextChanged += new System.EventHandler(this.txtFilterValue_TextChanged);
            // 
            // btnShowAddDetail
            // 
            this.btnShowAddDetail.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnShowAddDetail.Image = global::IMS.Properties.Resources.Details;
            this.btnShowAddDetail.Location = new System.Drawing.Point(685, 21);
            this.btnShowAddDetail.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnShowAddDetail.Name = "btnShowAddDetail";
            this.btnShowAddDetail.Size = new System.Drawing.Size(85, 80);
            this.btnShowAddDetail.TabIndex = 24;
            this.btnShowAddDetail.UseVisualStyleBackColor = true;
            this.btnShowAddDetail.Click += new System.EventHandler(this.btnShowAddDetail_Click);
            // 
            // lblFilterBy
            // 
            this.lblFilterBy.AutoSize = true;
            this.lblFilterBy.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFilterBy.Location = new System.Drawing.Point(43, 47);
            this.lblFilterBy.Name = "lblFilterBy";
            this.lblFilterBy.Size = new System.Drawing.Size(118, 32);
            this.lblFilterBy.TabIndex = 23;
            this.lblFilterBy.Text = "Filter By";
            // 
            // cbFilterBy
            // 
            this.cbFilterBy.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbFilterBy.FormattingEnabled = true;
            this.cbFilterBy.Items.AddRange(new object[] {
            "None",
            "Detail ID",
            "Product ID"});
            this.cbFilterBy.Location = new System.Drawing.Point(186, 47);
            this.cbFilterBy.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cbFilterBy.Name = "cbFilterBy";
            this.cbFilterBy.Size = new System.Drawing.Size(223, 33);
            this.cbFilterBy.TabIndex = 22;
            this.cbFilterBy.SelectedIndexChanged += new System.EventHandler(this.cbFilterBy_SelectedIndexChanged);
            // 
            // dgvDetails
            // 
            this.dgvDetails.AllowUserToAddRows = false;
            this.dgvDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDetails.ContextMenuStrip = this.cmsDetails;
            this.dgvDetails.Location = new System.Drawing.Point(24, 118);
            this.dgvDetails.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dgvDetails.Name = "dgvDetails";
            this.dgvDetails.ReadOnly = true;
            this.dgvDetails.RowHeadersWidth = 51;
            this.dgvDetails.RowTemplate.Height = 24;
            this.dgvDetails.Size = new System.Drawing.Size(724, 414);
            this.dgvDetails.TabIndex = 21;
            // 
            // lblRecordsCount
            // 
            this.lblRecordsCount.AutoSize = true;
            this.lblRecordsCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRecordsCount.Location = new System.Drawing.Point(18, 563);
            this.lblRecordsCount.Name = "lblRecordsCount";
            this.lblRecordsCount.Size = new System.Drawing.Size(94, 32);
            this.lblRecordsCount.TabIndex = 27;
            this.lblRecordsCount.Text = "?????";
            // 
            // btnClose
            // 
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.Location = new System.Drawing.Point(617, 551);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(131, 44);
            this.btnClose.TabIndex = 26;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // cmsDetails
            // 
            this.cmsDetails.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.cmsDetails.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addDetailToolStripMenuItem,
            this.editDetailToolStripMenuItem,
            this.deleteDetailToolStripMenuItem});
            this.cmsDetails.Name = "contextMenuStrip1";
            this.cmsDetails.Size = new System.Drawing.Size(231, 82);
            // 
            // addDetailToolStripMenuItem
            // 
            this.addDetailToolStripMenuItem.Image = global::IMS.Properties.Resources.Products;
            this.addDetailToolStripMenuItem.Name = "addDetailToolStripMenuItem";
            this.addDetailToolStripMenuItem.Size = new System.Drawing.Size(230, 26);
            this.addDetailToolStripMenuItem.Text = "Add Purchase Order";
            this.addDetailToolStripMenuItem.Click += new System.EventHandler(this.addDetailToolStripMenuItem_Click);
            // 
            // editDetailToolStripMenuItem
            // 
            this.editDetailToolStripMenuItem.Image = global::IMS.Properties.Resources.Edit;
            this.editDetailToolStripMenuItem.Name = "editDetailToolStripMenuItem";
            this.editDetailToolStripMenuItem.Size = new System.Drawing.Size(230, 26);
            this.editDetailToolStripMenuItem.Text = "Edit Purchase Order";
            this.editDetailToolStripMenuItem.Click += new System.EventHandler(this.editDetailToolStripMenuItem_Click);
            // 
            // deleteDetailToolStripMenuItem
            // 
            this.deleteDetailToolStripMenuItem.Image = global::IMS.Properties.Resources.Delete;
            this.deleteDetailToolStripMenuItem.Name = "deleteDetailToolStripMenuItem";
            this.deleteDetailToolStripMenuItem.Size = new System.Drawing.Size(230, 26);
            this.deleteDetailToolStripMenuItem.Text = "Delete Purchase Order";
            this.deleteDetailToolStripMenuItem.Click += new System.EventHandler(this.deleteDetailToolStripMenuItem_Click);
            // 
            // frmListPurchaseOrderDetails
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(779, 619);
            this.Controls.Add(this.lblRecordsCount);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.txtFilterValue);
            this.Controls.Add(this.btnShowAddDetail);
            this.Controls.Add(this.lblFilterBy);
            this.Controls.Add(this.cbFilterBy);
            this.Controls.Add(this.dgvDetails);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmListPurchaseOrderDetails";
            this.Text = "frmListPurchaseOrderDetails";
            this.Load += new System.EventHandler(this.frmListPurchaseOrderDetails_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetails)).EndInit();
            this.cmsDetails.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtFilterValue;
        private System.Windows.Forms.Button btnShowAddDetail;
        private System.Windows.Forms.Label lblFilterBy;
        private System.Windows.Forms.ComboBox cbFilterBy;
        private System.Windows.Forms.DataGridView dgvDetails;
        private System.Windows.Forms.Label lblRecordsCount;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.ContextMenuStrip cmsDetails;
        private System.Windows.Forms.ToolStripMenuItem addDetailToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editDetailToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteDetailToolStripMenuItem;
    }
}