
namespace ProjectGroup.view
{
    partial class authorDetails
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
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.txtAuthorPhone = new DevExpress.XtraEditors.TextEdit();
            this.txtAuthorEmail = new DevExpress.XtraEditors.TextEdit();
            this.txtAuthorname = new DevExpress.XtraEditors.TextEdit();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.lblAuthorName = new DevExpress.XtraLayout.LayoutControlItem();
            this.lblAuthorEmail = new DevExpress.XtraLayout.LayoutControlItem();
            this.lblAuthorPhone = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.dxErrorProvider1 = new DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtAuthorPhone.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAuthorEmail.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAuthorname.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblAuthorName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblAuthorEmail)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblAuthorPhone)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dxErrorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.btnSave);
            this.layoutControl1.Controls.Add(this.btnCancel);
            this.layoutControl1.Controls.Add(this.txtAuthorPhone);
            this.layoutControl1.Controls.Add(this.txtAuthorEmail);
            this.layoutControl1.Controls.Add(this.txtAuthorname);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.Root;
            this.layoutControl1.Size = new System.Drawing.Size(749, 161);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(420, 90);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(157, 27);
            this.btnSave.StyleController = this.layoutControl1;
            this.btnSave.TabIndex = 8;
            this.btnSave.Text = "Save";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.CausesValidation = false;
            this.btnCancel.Location = new System.Drawing.Point(581, 90);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(156, 27);
            this.btnCancel.StyleController = this.layoutControl1;
            this.btnCancel.TabIndex = 7;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // txtAuthorPhone
            // 
            this.txtAuthorPhone.Location = new System.Drawing.Point(100, 64);
            this.txtAuthorPhone.Name = "txtAuthorPhone";
            this.txtAuthorPhone.Size = new System.Drawing.Size(637, 22);
            this.txtAuthorPhone.StyleController = this.layoutControl1;
            this.txtAuthorPhone.TabIndex = 6;
            this.txtAuthorPhone.Validating += new System.ComponentModel.CancelEventHandler(this.txtAuthorPhone_Validating);
            this.txtAuthorPhone.Validated += new System.EventHandler(this.txtAuthorPhone_Validated);
            // 
            // txtAuthorEmail
            // 
            this.txtAuthorEmail.Location = new System.Drawing.Point(100, 38);
            this.txtAuthorEmail.Name = "txtAuthorEmail";
            this.txtAuthorEmail.Size = new System.Drawing.Size(637, 22);
            this.txtAuthorEmail.StyleController = this.layoutControl1;
            this.txtAuthorEmail.TabIndex = 5;
            this.txtAuthorEmail.Validating += new System.ComponentModel.CancelEventHandler(this.txtAuthorEmail_Validating);
            this.txtAuthorEmail.Validated += new System.EventHandler(this.txtAuthorEmail_Validated);
            // 
            // txtAuthorname
            // 
            this.txtAuthorname.Location = new System.Drawing.Point(100, 12);
            this.txtAuthorname.Name = "txtAuthorname";
            this.txtAuthorname.Size = new System.Drawing.Size(637, 22);
            this.txtAuthorname.StyleController = this.layoutControl1;
            this.txtAuthorname.TabIndex = 4;
            this.txtAuthorname.Validating += new System.ComponentModel.CancelEventHandler(this.txtAuthorname_Validating);
            this.txtAuthorname.Validated += new System.EventHandler(this.txtAuthorname_Validated);
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lblAuthorName,
            this.lblAuthorEmail,
            this.lblAuthorPhone,
            this.emptySpaceItem1,
            this.layoutControlItem4,
            this.layoutControlItem5});
            this.Root.Name = "Root";
            this.Root.OptionsItemText.TextToControlDistance = 2;
            this.Root.Size = new System.Drawing.Size(749, 161);
            this.Root.TextVisible = false;
            // 
            // lblAuthorName
            // 
            this.lblAuthorName.Control = this.txtAuthorname;
            this.lblAuthorName.Location = new System.Drawing.Point(0, 0);
            this.lblAuthorName.Name = "lblAuthorName";
            this.lblAuthorName.Size = new System.Drawing.Size(729, 26);
            this.lblAuthorName.Text = "Author Name: ";
            this.lblAuthorName.TextSize = new System.Drawing.Size(86, 16);
            // 
            // lblAuthorEmail
            // 
            this.lblAuthorEmail.Control = this.txtAuthorEmail;
            this.lblAuthorEmail.Location = new System.Drawing.Point(0, 26);
            this.lblAuthorEmail.Name = "lblAuthorEmail";
            this.lblAuthorEmail.Size = new System.Drawing.Size(729, 26);
            this.lblAuthorEmail.Text = "Author Email: ";
            this.lblAuthorEmail.TextSize = new System.Drawing.Size(86, 16);
            // 
            // lblAuthorPhone
            // 
            this.lblAuthorPhone.Control = this.txtAuthorPhone;
            this.lblAuthorPhone.Location = new System.Drawing.Point(0, 52);
            this.lblAuthorPhone.Name = "lblAuthorPhone";
            this.lblAuthorPhone.Size = new System.Drawing.Size(729, 26);
            this.lblAuthorPhone.Text = "Author Phone: ";
            this.lblAuthorPhone.TextSize = new System.Drawing.Size(86, 16);
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.Location = new System.Drawing.Point(0, 78);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(408, 63);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.btnCancel;
            this.layoutControlItem4.Location = new System.Drawing.Point(569, 78);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(160, 63);
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextVisible = false;
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.btnSave;
            this.layoutControlItem5.Location = new System.Drawing.Point(408, 78);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(161, 63);
            this.layoutControlItem5.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem5.TextVisible = false;
            // 
            // dxErrorProvider1
            // 
            this.dxErrorProvider1.ContainerControl = this;
            // 
            // authorDetails
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(749, 161);
            this.Controls.Add(this.layoutControl1);
            this.Name = "authorDetails";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Author Details";
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtAuthorPhone.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAuthorEmail.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAuthorname.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblAuthorName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblAuthorEmail)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblAuthorPhone)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dxErrorProvider1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraEditors.SimpleButton btnSave;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.TextEdit txtAuthorPhone;
        private DevExpress.XtraEditors.TextEdit txtAuthorEmail;
        private DevExpress.XtraEditors.TextEdit txtAuthorname;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraLayout.LayoutControlItem lblAuthorName;
        private DevExpress.XtraLayout.LayoutControlItem lblAuthorEmail;
        private DevExpress.XtraLayout.LayoutControlItem lblAuthorPhone;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider dxErrorProvider1;
    }
}