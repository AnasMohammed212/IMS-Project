namespace IMS.SaleOrders
{
    partial class frmListSaleOrders
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
            this.lblFilterBy = new System.Windows.Forms.Label();
            this.cbFilterBy = new System.Windows.Forms.ComboBox();
            this.dgvSaleOrders = new System.Windows.Forms.DataGridView();
            this.cmsSaleOrder = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.showSaleOrderInfoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addSaleOrderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editSaleOrderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteSaleOrderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addDetailsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lblRecordsCount = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnShowAddUpdateSaleOrder = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSaleOrders)).BeginInit();
            this.cmsSaleOrder.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtFilterValue
            // 
            this.txtFilterValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFilterValue.Location = new System.Drawing.Point(430, 51);
            this.txtFilterValue.Name = "txtFilterValue";
            this.txtFilterValue.Size = new System.Drawing.Size(171, 34);
            this.txtFilterValue.TabIndex = 25;
            this.txtFilterValue.TextChanged += new System.EventHandler(this.txtFilterValue_TextChanged);
            // 
            // lblFilterBy
            // 
            this.lblFilterBy.AutoSize = true;
            this.lblFilterBy.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFilterBy.Location = new System.Drawing.Point(42, 51);
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
            "Order ID",
            "Customer Name",
            "Created By",
            "Status"});
            this.cbFilterBy.Location = new System.Drawing.Point(185, 51);
            this.cbFilterBy.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cbFilterBy.Name = "cbFilterBy";
            this.cbFilterBy.Size = new System.Drawing.Size(223, 33);
            this.cbFilterBy.TabIndex = 22;
            this.cbFilterBy.SelectedIndexChanged += new System.EventHandler(this.cbFilterBy_SelectedIndexChanged);
            // 
            // dgvSaleOrders
            // 
            this.dgvSaleOrders.AllowUserToAddRows = false;
            this.dgvSaleOrders.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvSaleOrders.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSaleOrders.ContextMenuStrip = this.cmsSaleOrder;
            this.dgvSaleOrders.Location = new System.Drawing.Point(23, 122);
            this.dgvSaleOrders.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dgvSaleOrders.Name = "dgvSaleOrders";
            this.dgvSaleOrders.ReadOnly = true;
            this.dgvSaleOrders.RowHeadersWidth = 51;
            this.dgvSaleOrders.RowTemplate.Height = 24;
            this.dgvSaleOrders.Size = new System.Drawing.Size(1200, 414);
            this.dgvSaleOrders.TabIndex = 21;
            // 
            // cmsSaleOrder
            // 
            this.cmsSaleOrder.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.cmsSaleOrder.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showSaleOrderInfoToolStripMenuItem,
            this.addSaleOrderToolStripMenuItem,
            this.editSaleOrderToolStripMenuItem,
            this.deleteSaleOrderToolStripMenuItem,
            this.addDetailsToolStripMenuItem});
            this.cmsSaleOrder.Name = "contextMenuStrip1";
            this.cmsSaleOrder.Size = new System.Drawing.Size(223, 134);
            // 
            // showSaleOrderInfoToolStripMenuItem
            // 
            this.showSaleOrderInfoToolStripMenuItem.Image = global::IMS.Properties.Resources.PersonInfo;
            this.showSaleOrderInfoToolStripMenuItem.Name = "showSaleOrderInfoToolStripMenuItem";
            this.showSaleOrderInfoToolStripMenuItem.Size = new System.Drawing.Size(222, 26);
            this.showSaleOrderInfoToolStripMenuItem.Text = "Show Sale Order Info";
            this.showSaleOrderInfoToolStripMenuItem.Click += new System.EventHandler(this.showSaleOrderInfoToolStripMenuItem_Click);
            // 
            // addSaleOrderToolStripMenuItem
            // 
            this.addSaleOrderToolStripMenuItem.Image = global::IMS.Properties.Resources.Products;
            this.addSaleOrderToolStripMenuItem.Name = "addSaleOrderToolStripMenuItem";
            this.addSaleOrderToolStripMenuItem.Size = new System.Drawing.Size(222, 26);
            this.addSaleOrderToolStripMenuItem.Text = "Add Sale Order";
            this.addSaleOrderToolStripMenuItem.Click += new System.EventHandler(this.addSaleOrderToolStripMenuItem_Click);
            // 
            // editSaleOrderToolStripMenuItem
            // 
            this.editSaleOrderToolStripMenuItem.Image = global::IMS.Properties.Resources.Edit;
            this.editSaleOrderToolStripMenuItem.Name = "editSaleOrderToolStripMenuItem";
            this.editSaleOrderToolStripMenuItem.Size = new System.Drawing.Size(222, 26);
            this.editSaleOrderToolStripMenuItem.Text = "Edit Sale Order";
            this.editSaleOrderToolStripMenuItem.Click += new System.EventHandler(this.editSaleOrderToolStripMenuItem_Click);
            // 
            // deleteSaleOrderToolStripMenuItem
            // 
            this.deleteSaleOrderToolStripMenuItem.Image = global::IMS.Properties.Resources.Delete;
            this.deleteSaleOrderToolStripMenuItem.Name = "deleteSaleOrderToolStripMenuItem";
            this.deleteSaleOrderToolStripMenuItem.Size = new System.Drawing.Size(222, 26);
            this.deleteSaleOrderToolStripMenuItem.Text = "Delete Sale Order";
            this.deleteSaleOrderToolStripMenuItem.Click += new System.EventHandler(this.deleteSaleOrderToolStripMenuItem_Click);
            // 
            // addDetailsToolStripMenuItem
            // 
            this.addDetailsToolStripMenuItem.Image = global::IMS.Properties.Resources.Details;
            this.addDetailsToolStripMenuItem.Name = "addDetailsToolStripMenuItem";
            this.addDetailsToolStripMenuItem.Size = new System.Drawing.Size(222, 26);
            this.addDetailsToolStripMenuItem.Text = "Purchase Sale Details";
            this.addDetailsToolStripMenuItem.Click += new System.EventHandler(this.addDetailsToolStripMenuItem_Click);
            // 
            // lblRecordsCount
            // 
            this.lblRecordsCount.AutoSize = true;
            this.lblRecordsCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRecordsCount.Location = new System.Drawing.Point(17, 575);
            this.lblRecordsCount.Name = "lblRecordsCount";
            this.lblRecordsCount.Size = new System.Drawing.Size(94, 32);
            this.lblRecordsCount.TabIndex = 27;
            this.lblRecordsCount.Text = "?????";
            // 
            // btnClose
            // 
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.Location = new System.Drawing.Point(1092, 563);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(131, 44);
            this.btnClose.TabIndex = 26;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnShowAddUpdateSaleOrder
            // 
            this.btnShowAddUpdateSaleOrder.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnShowAddUpdateSaleOrder.Image = global::IMS.Properties.Resources.Products;
            this.btnShowAddUpdateSaleOrder.Location = new System.Drawing.Point(1138, 28);
            this.btnShowAddUpdateSaleOrder.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnShowAddUpdateSaleOrder.Name = "btnShowAddUpdateSaleOrder";
            this.btnShowAddUpdateSaleOrder.Size = new System.Drawing.Size(85, 80);
            this.btnShowAddUpdateSaleOrder.TabIndex = 24;
            this.btnShowAddUpdateSaleOrder.UseVisualStyleBackColor = true;
            this.btnShowAddUpdateSaleOrder.Click += new System.EventHandler(this.btnShowAddUpdateSaleOrder_Click);
            // 
            // frmListSaleOrders
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1250, 634);
            this.Controls.Add(this.lblRecordsCount);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.txtFilterValue);
            this.Controls.Add(this.btnShowAddUpdateSaleOrder);
            this.Controls.Add(this.lblFilterBy);
            this.Controls.Add(this.cbFilterBy);
            this.Controls.Add(this.dgvSaleOrders);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmListSaleOrders";
            this.Text = "frmListSaleOrders";
            this.Load += new System.EventHandler(this.frmListSaleOrders_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvSaleOrders)).EndInit();
            this.cmsSaleOrder.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtFilterValue;
        private System.Windows.Forms.Button btnShowAddUpdateSaleOrder;
        private System.Windows.Forms.Label lblFilterBy;
        private System.Windows.Forms.ComboBox cbFilterBy;
        private System.Windows.Forms.DataGridView dgvSaleOrders;
        private System.Windows.Forms.Label lblRecordsCount;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.ContextMenuStrip cmsSaleOrder;
        private System.Windows.Forms.ToolStripMenuItem showSaleOrderInfoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addSaleOrderToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editSaleOrderToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteSaleOrderToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addDetailsToolStripMenuItem;
    }
}