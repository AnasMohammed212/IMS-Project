namespace IMS.Suppliers
{
    partial class frmListSuppliers
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
            this.dgvSuppliers = new System.Windows.Forms.DataGridView();
            this.lblRecordsCount = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.btnShowAddUpdateProduct = new System.Windows.Forms.Button();
            this.cmsSupplier = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.addSupplierToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editSupplierToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteSupplierToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.sendEmailToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.phonToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSuppliers)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.cmsSupplier.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtFilterValue
            // 
            this.txtFilterValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFilterValue.Location = new System.Drawing.Point(438, 57);
            this.txtFilterValue.Name = "txtFilterValue";
            this.txtFilterValue.Size = new System.Drawing.Size(171, 34);
            this.txtFilterValue.TabIndex = 30;
            this.txtFilterValue.TextChanged += new System.EventHandler(this.txtFilterValue_TextChanged);
            // 
            // lblFilterBy
            // 
            this.lblFilterBy.AutoSize = true;
            this.lblFilterBy.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFilterBy.Location = new System.Drawing.Point(50, 57);
            this.lblFilterBy.Name = "lblFilterBy";
            this.lblFilterBy.Size = new System.Drawing.Size(118, 32);
            this.lblFilterBy.TabIndex = 28;
            this.lblFilterBy.Text = "Filter By";
            // 
            // cbFilterBy
            // 
            this.cbFilterBy.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbFilterBy.FormattingEnabled = true;
            this.cbFilterBy.Items.AddRange(new object[] {
            "None",
            "Supplier ID",
            "Supplier Name",
            "Contact Person ID",
            "Phone",
            "Email"});
            this.cbFilterBy.Location = new System.Drawing.Point(193, 57);
            this.cbFilterBy.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cbFilterBy.Name = "cbFilterBy";
            this.cbFilterBy.Size = new System.Drawing.Size(223, 33);
            this.cbFilterBy.TabIndex = 27;
            // 
            // dgvSuppliers
            // 
            this.dgvSuppliers.AllowUserToAddRows = false;
            this.dgvSuppliers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSuppliers.ContextMenuStrip = this.cmsSupplier;
            this.dgvSuppliers.Location = new System.Drawing.Point(31, 128);
            this.dgvSuppliers.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dgvSuppliers.Name = "dgvSuppliers";
            this.dgvSuppliers.ReadOnly = true;
            this.dgvSuppliers.RowHeadersWidth = 51;
            this.dgvSuppliers.RowTemplate.Height = 24;
            this.dgvSuppliers.Size = new System.Drawing.Size(1200, 414);
            this.dgvSuppliers.TabIndex = 26;
            // 
            // lblRecordsCount
            // 
            this.lblRecordsCount.AutoSize = true;
            this.lblRecordsCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRecordsCount.Location = new System.Drawing.Point(25, 576);
            this.lblRecordsCount.Name = "lblRecordsCount";
            this.lblRecordsCount.Size = new System.Drawing.Size(94, 32);
            this.lblRecordsCount.TabIndex = 32;
            this.lblRecordsCount.Text = "?????";
            // 
            // btnClose
            // 
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.Location = new System.Drawing.Point(1100, 572);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(131, 44);
            this.btnClose.TabIndex = 31;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // btnShowAddUpdateProduct
            // 
            this.btnShowAddUpdateProduct.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnShowAddUpdateProduct.Image = global::IMS.Properties.Resources.Supplier;
            this.btnShowAddUpdateProduct.Location = new System.Drawing.Point(1146, 34);
            this.btnShowAddUpdateProduct.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnShowAddUpdateProduct.Name = "btnShowAddUpdateProduct";
            this.btnShowAddUpdateProduct.Size = new System.Drawing.Size(85, 80);
            this.btnShowAddUpdateProduct.TabIndex = 29;
            this.btnShowAddUpdateProduct.UseVisualStyleBackColor = true;
            this.btnShowAddUpdateProduct.Click += new System.EventHandler(this.btnShowAddUpdateProduct_Click);
            // 
            // cmsSupplier
            // 
            this.cmsSupplier.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.cmsSupplier.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addSupplierToolStripMenuItem,
            this.editSupplierToolStripMenuItem,
            this.deleteSupplierToolStripMenuItem,
            this.toolStripSeparator1,
            this.sendEmailToolStripMenuItem,
            this.phonToolStripMenuItem});
            this.cmsSupplier.Name = "contextMenuStrip1";
            this.cmsSupplier.Size = new System.Drawing.Size(215, 168);
            // 
            // addSupplierToolStripMenuItem
            // 
            this.addSupplierToolStripMenuItem.Image = global::IMS.Properties.Resources.Supplier;
            this.addSupplierToolStripMenuItem.Name = "addSupplierToolStripMenuItem";
            this.addSupplierToolStripMenuItem.Size = new System.Drawing.Size(185, 26);
            this.addSupplierToolStripMenuItem.Text = "Add Supplier";
            this.addSupplierToolStripMenuItem.Click += new System.EventHandler(this.addSupplierToolStripMenuItem_Click);
            // 
            // editSupplierToolStripMenuItem
            // 
            this.editSupplierToolStripMenuItem.Image = global::IMS.Properties.Resources.Edit;
            this.editSupplierToolStripMenuItem.Name = "editSupplierToolStripMenuItem";
            this.editSupplierToolStripMenuItem.Size = new System.Drawing.Size(185, 26);
            this.editSupplierToolStripMenuItem.Text = "Edit Supplier";
            this.editSupplierToolStripMenuItem.Click += new System.EventHandler(this.editSupplierToolStripMenuItem_Click);
            // 
            // deleteSupplierToolStripMenuItem
            // 
            this.deleteSupplierToolStripMenuItem.Image = global::IMS.Properties.Resources.Delete;
            this.deleteSupplierToolStripMenuItem.Name = "deleteSupplierToolStripMenuItem";
            this.deleteSupplierToolStripMenuItem.Size = new System.Drawing.Size(214, 26);
            this.deleteSupplierToolStripMenuItem.Text = "Delete Supplier";
            this.deleteSupplierToolStripMenuItem.Click += new System.EventHandler(this.deleteSupplierToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(182, 6);
            // 
            // sendEmailToolStripMenuItem
            // 
            this.sendEmailToolStripMenuItem.Image = global::IMS.Properties.Resources.Email;
            this.sendEmailToolStripMenuItem.Name = "sendEmailToolStripMenuItem";
            this.sendEmailToolStripMenuItem.Size = new System.Drawing.Size(185, 26);
            this.sendEmailToolStripMenuItem.Text = "Send Email";
            // 
            // phonToolStripMenuItem
            // 
            this.phonToolStripMenuItem.Image = global::IMS.Properties.Resources.Phone;
            this.phonToolStripMenuItem.Name = "phonToolStripMenuItem";
            this.phonToolStripMenuItem.Size = new System.Drawing.Size(185, 26);
            this.phonToolStripMenuItem.Text = "Phone Call";
            // 
            // frmListSuppliers
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1265, 632);
            this.Controls.Add(this.lblRecordsCount);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.txtFilterValue);
            this.Controls.Add(this.btnShowAddUpdateProduct);
            this.Controls.Add(this.lblFilterBy);
            this.Controls.Add(this.cbFilterBy);
            this.Controls.Add(this.dgvSuppliers);
            this.Name = "frmListSuppliers";
            this.Text = "frmListSuppliers";
            this.Load += new System.EventHandler(this.frmListSuppliers_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvSuppliers)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.cmsSupplier.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtFilterValue;
        private System.Windows.Forms.Button btnShowAddUpdateProduct;
        private System.Windows.Forms.Label lblFilterBy;
        private System.Windows.Forms.ComboBox cbFilterBy;
        private System.Windows.Forms.DataGridView dgvSuppliers;
        private System.Windows.Forms.Label lblRecordsCount;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.ContextMenuStrip cmsSupplier;
        private System.Windows.Forms.ToolStripMenuItem addSupplierToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editSupplierToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteSupplierToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem sendEmailToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem phonToolStripMenuItem;
    }
}