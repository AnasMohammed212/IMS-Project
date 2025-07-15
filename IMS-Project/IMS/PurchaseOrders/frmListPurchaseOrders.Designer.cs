namespace IMS
{
    partial class frmListPurchaseOrders
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
            this.btnShowAddUpdateProduct = new System.Windows.Forms.Button();
            this.lblFilterBy = new System.Windows.Forms.Label();
            this.cbFilterBy = new System.Windows.Forms.ComboBox();
            this.dgvPurchaseOrders = new System.Windows.Forms.DataGridView();
            this.cmsPurchaseOrder = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.showPurchaseOrderInfoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addPurchaseOrderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editPurchaseOrderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deletePurchaseOrderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lblRecordsCount = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPurchaseOrders)).BeginInit();
            this.cmsPurchaseOrder.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtFilterValue
            // 
            this.txtFilterValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFilterValue.Location = new System.Drawing.Point(438, 55);
            this.txtFilterValue.Name = "txtFilterValue";
            this.txtFilterValue.Size = new System.Drawing.Size(171, 34);
            this.txtFilterValue.TabIndex = 20;
            this.txtFilterValue.TextChanged += new System.EventHandler(this.txtFilterValue_TextChanged);
            // 
            // btnShowAddUpdateProduct
            // 
            this.btnShowAddUpdateProduct.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnShowAddUpdateProduct.Image = global::IMS.Properties.Resources.Products;
            this.btnShowAddUpdateProduct.Location = new System.Drawing.Point(1146, 32);
            this.btnShowAddUpdateProduct.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnShowAddUpdateProduct.Name = "btnShowAddUpdateProduct";
            this.btnShowAddUpdateProduct.Size = new System.Drawing.Size(85, 80);
            this.btnShowAddUpdateProduct.TabIndex = 19;
            this.btnShowAddUpdateProduct.UseVisualStyleBackColor = true;
            this.btnShowAddUpdateProduct.Click += new System.EventHandler(this.btnShowAddUpdateProduct_Click);
            // 
            // lblFilterBy
            // 
            this.lblFilterBy.AutoSize = true;
            this.lblFilterBy.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFilterBy.Location = new System.Drawing.Point(50, 55);
            this.lblFilterBy.Name = "lblFilterBy";
            this.lblFilterBy.Size = new System.Drawing.Size(118, 32);
            this.lblFilterBy.TabIndex = 18;
            this.lblFilterBy.Text = "Filter By";
            // 
            // cbFilterBy
            // 
            this.cbFilterBy.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbFilterBy.FormattingEnabled = true;
            this.cbFilterBy.Items.AddRange(new object[] {
            "None",
            "Order ID",
            "Supplier Name",
            "Created By",
            "Status"});
            this.cbFilterBy.Location = new System.Drawing.Point(193, 55);
            this.cbFilterBy.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cbFilterBy.Name = "cbFilterBy";
            this.cbFilterBy.Size = new System.Drawing.Size(223, 33);
            this.cbFilterBy.TabIndex = 17;
            this.cbFilterBy.SelectedIndexChanged += new System.EventHandler(this.cbFilterBy_SelectedIndexChanged);
            // 
            // dgvPurchaseOrders
            // 
            this.dgvPurchaseOrders.AllowUserToAddRows = false;
            this.dgvPurchaseOrders.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPurchaseOrders.ContextMenuStrip = this.cmsPurchaseOrder;
            this.dgvPurchaseOrders.Location = new System.Drawing.Point(31, 126);
            this.dgvPurchaseOrders.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dgvPurchaseOrders.Name = "dgvPurchaseOrders";
            this.dgvPurchaseOrders.ReadOnly = true;
            this.dgvPurchaseOrders.RowHeadersWidth = 51;
            this.dgvPurchaseOrders.RowTemplate.Height = 24;
            this.dgvPurchaseOrders.Size = new System.Drawing.Size(1200, 414);
            this.dgvPurchaseOrders.TabIndex = 16;
            // 
            // cmsPurchaseOrder
            // 
            this.cmsPurchaseOrder.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.cmsPurchaseOrder.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showPurchaseOrderInfoToolStripMenuItem,
            this.addPurchaseOrderToolStripMenuItem,
            this.editPurchaseOrderToolStripMenuItem,
            this.deletePurchaseOrderToolStripMenuItem});
            this.cmsPurchaseOrder.Name = "contextMenuStrip1";
            this.cmsPurchaseOrder.Size = new System.Drawing.Size(253, 136);
            // 
            // showPurchaseOrderInfoToolStripMenuItem
            // 
            this.showPurchaseOrderInfoToolStripMenuItem.Image = global::IMS.Properties.Resources.PersonInfo;
            this.showPurchaseOrderInfoToolStripMenuItem.Name = "showPurchaseOrderInfoToolStripMenuItem";
            this.showPurchaseOrderInfoToolStripMenuItem.Size = new System.Drawing.Size(252, 26);
            this.showPurchaseOrderInfoToolStripMenuItem.Text = "Show Purchase Order Info";
            this.showPurchaseOrderInfoToolStripMenuItem.Click += new System.EventHandler(this.showPurchaseOrderInfoToolStripMenuItem_Click);
            // 
            // addPurchaseOrderToolStripMenuItem
            // 
            this.addPurchaseOrderToolStripMenuItem.Image = global::IMS.Properties.Resources.Products;
            this.addPurchaseOrderToolStripMenuItem.Name = "addPurchaseOrderToolStripMenuItem";
            this.addPurchaseOrderToolStripMenuItem.Size = new System.Drawing.Size(252, 26);
            this.addPurchaseOrderToolStripMenuItem.Text = "Add Purchase Order";
            this.addPurchaseOrderToolStripMenuItem.Click += new System.EventHandler(this.addPurchaseOrderToolStripMenuItem_Click);
            // 
            // editPurchaseOrderToolStripMenuItem
            // 
            this.editPurchaseOrderToolStripMenuItem.Image = global::IMS.Properties.Resources.Edit;
            this.editPurchaseOrderToolStripMenuItem.Name = "editPurchaseOrderToolStripMenuItem";
            this.editPurchaseOrderToolStripMenuItem.Size = new System.Drawing.Size(252, 26);
            this.editPurchaseOrderToolStripMenuItem.Text = "Edit Purchase Order";
            this.editPurchaseOrderToolStripMenuItem.Click += new System.EventHandler(this.editPurchaseOrderToolStripMenuItem_Click);
            // 
            // deletePurchaseOrderToolStripMenuItem
            // 
            this.deletePurchaseOrderToolStripMenuItem.Image = global::IMS.Properties.Resources.Delete;
            this.deletePurchaseOrderToolStripMenuItem.Name = "deletePurchaseOrderToolStripMenuItem";
            this.deletePurchaseOrderToolStripMenuItem.Size = new System.Drawing.Size(252, 26);
            this.deletePurchaseOrderToolStripMenuItem.Text = "Delete Purchase Order";
            this.deletePurchaseOrderToolStripMenuItem.Click += new System.EventHandler(this.deletePurchaseOrderToolStripMenuItem_Click);
            // 
            // lblRecordsCount
            // 
            this.lblRecordsCount.AutoSize = true;
            this.lblRecordsCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRecordsCount.Location = new System.Drawing.Point(25, 565);
            this.lblRecordsCount.Name = "lblRecordsCount";
            this.lblRecordsCount.Size = new System.Drawing.Size(94, 32);
            this.lblRecordsCount.TabIndex = 22;
            this.lblRecordsCount.Text = "?????";
            // 
            // btnClose
            // 
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.Location = new System.Drawing.Point(1100, 553);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(131, 44);
            this.btnClose.TabIndex = 21;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // frmListPurchaseOrders
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1257, 626);
            this.Controls.Add(this.lblRecordsCount);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.txtFilterValue);
            this.Controls.Add(this.btnShowAddUpdateProduct);
            this.Controls.Add(this.lblFilterBy);
            this.Controls.Add(this.cbFilterBy);
            this.Controls.Add(this.dgvPurchaseOrders);
            this.Name = "frmListPurchaseOrders";
            this.Text = "frmListPurchaseOrders";
            this.Load += new System.EventHandler(this.frmListPurchaseOrders_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPurchaseOrders)).EndInit();
            this.cmsPurchaseOrder.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtFilterValue;
        private System.Windows.Forms.Button btnShowAddUpdateProduct;
        private System.Windows.Forms.Label lblFilterBy;
        private System.Windows.Forms.ComboBox cbFilterBy;
        private System.Windows.Forms.DataGridView dgvPurchaseOrders;
        private System.Windows.Forms.Label lblRecordsCount;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.ContextMenuStrip cmsPurchaseOrder;
        private System.Windows.Forms.ToolStripMenuItem showPurchaseOrderInfoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addPurchaseOrderToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editPurchaseOrderToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deletePurchaseOrderToolStripMenuItem;
    }
}