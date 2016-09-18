using System.ComponentModel.Design;
namespace WireDog.UI
{
    partial class MainForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.gbProcesses = new System.Windows.Forms.GroupBox();
            this.btnReload = new System.Windows.Forms.Button();
            this.processListView = new WireDog.UI.Components.ProcessListView();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.socketEventModelBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.gbOverview = new System.Windows.Forms.GroupBox();
            this.byteViewer = new System.ComponentModel.Design.ByteViewer();
            this.gbDetails = new System.Windows.Forms.GroupBox();
            this.socketEventTypeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.timestampDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.processNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.localAddressDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.remoteAddressDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gbProcesses.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.socketEventModelBindingSource)).BeginInit();
            this.gbOverview.SuspendLayout();
            this.gbDetails.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbProcesses
            // 
            this.gbProcesses.Controls.Add(this.btnReload);
            this.gbProcesses.Controls.Add(this.processListView);
            this.gbProcesses.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbProcesses.Location = new System.Drawing.Point(12, 12);
            this.gbProcesses.Name = "gbProcesses";
            this.gbProcesses.Size = new System.Drawing.Size(651, 173);
            this.gbProcesses.TabIndex = 0;
            this.gbProcesses.TabStop = false;
            this.gbProcesses.Text = "Processes";
            // 
            // btnReload
            // 
            this.btnReload.Location = new System.Drawing.Point(290, 19);
            this.btnReload.Name = "btnReload";
            this.btnReload.Size = new System.Drawing.Size(75, 23);
            this.btnReload.TabIndex = 1;
            this.btnReload.Text = "Reload";
            this.btnReload.UseVisualStyleBackColor = true;
            // 
            // processListView
            // 
            this.processListView.Location = new System.Drawing.Point(6, 51);
            this.processListView.Name = "processListView";
            this.processListView.Size = new System.Drawing.Size(634, 116);
            this.processListView.TabIndex = 0;
            this.processListView.UseCompatibleStateImageBehavior = false;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.socketEventTypeDataGridViewTextBoxColumn,
            this.timestampDataGridViewTextBoxColumn,
            this.processNameDataGridViewTextBoxColumn,
            this.localAddressDataGridViewTextBoxColumn,
            this.remoteAddressDataGridViewTextBoxColumn});
            this.dataGridView1.DataSource = this.socketEventModelBindingSource;
            this.dataGridView1.Location = new System.Drawing.Point(6, 17);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(634, 156);
            this.dataGridView1.TabIndex = 1;
            // 
            // socketEventModelBindingSource
            // 
            this.socketEventModelBindingSource.DataSource = typeof(WireDog.UI.Models.SocketEventModel);
            // 
            // gbOverview
            // 
            this.gbOverview.Controls.Add(this.dataGridView1);
            this.gbOverview.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbOverview.Location = new System.Drawing.Point(12, 202);
            this.gbOverview.Name = "gbOverview";
            this.gbOverview.Size = new System.Drawing.Size(651, 179);
            this.gbOverview.TabIndex = 2;
            this.gbOverview.TabStop = false;
            this.gbOverview.Text = "Overview";
            // 
            // byteViewer
            // 
            this.byteViewer.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Inset;
            this.byteViewer.ColumnCount = 1;
            this.byteViewer.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.byteViewer.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.byteViewer.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.byteViewer.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.byteViewer.Location = new System.Drawing.Point(6, 19);
            this.byteViewer.Name = "byteViewer";
            this.byteViewer.RowCount = 1;
            this.byteViewer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.byteViewer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.byteViewer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.byteViewer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.byteViewer.Size = new System.Drawing.Size(634, 94);
            this.byteViewer.TabIndex = 0;
            // 
            // gbDetails
            // 
            this.gbDetails.Controls.Add(this.byteViewer);
            this.gbDetails.Location = new System.Drawing.Point(12, 398);
            this.gbDetails.Name = "gbDetails";
            this.gbDetails.Size = new System.Drawing.Size(651, 144);
            this.gbDetails.TabIndex = 3;
            this.gbDetails.TabStop = false;
            this.gbDetails.Text = "Details";
            // 
            // socketEventTypeDataGridViewTextBoxColumn
            // 
            this.socketEventTypeDataGridViewTextBoxColumn.DataPropertyName = "SocketEventType";
            this.socketEventTypeDataGridViewTextBoxColumn.HeaderText = "Type";
            this.socketEventTypeDataGridViewTextBoxColumn.Name = "socketEventTypeDataGridViewTextBoxColumn";
            this.socketEventTypeDataGridViewTextBoxColumn.ReadOnly = true;
            this.socketEventTypeDataGridViewTextBoxColumn.Width = 50;
            // 
            // timestampDataGridViewTextBoxColumn
            // 
            this.timestampDataGridViewTextBoxColumn.DataPropertyName = "Timestamp";
            dataGridViewCellStyle1.Format = "HH:mm:ss.fff";
            this.timestampDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle1;
            this.timestampDataGridViewTextBoxColumn.HeaderText = "Timestamp";
            this.timestampDataGridViewTextBoxColumn.Name = "timestampDataGridViewTextBoxColumn";
            this.timestampDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // processNameDataGridViewTextBoxColumn
            // 
            this.processNameDataGridViewTextBoxColumn.DataPropertyName = "ProcessName";
            this.processNameDataGridViewTextBoxColumn.HeaderText = "ProcessName";
            this.processNameDataGridViewTextBoxColumn.Name = "processNameDataGridViewTextBoxColumn";
            this.processNameDataGridViewTextBoxColumn.ReadOnly = true;
            this.processNameDataGridViewTextBoxColumn.Width = 150;
            // 
            // localAddressDataGridViewTextBoxColumn
            // 
            this.localAddressDataGridViewTextBoxColumn.DataPropertyName = "LocalAddress";
            this.localAddressDataGridViewTextBoxColumn.HeaderText = "LocalAddress";
            this.localAddressDataGridViewTextBoxColumn.Name = "localAddressDataGridViewTextBoxColumn";
            this.localAddressDataGridViewTextBoxColumn.ReadOnly = true;
            this.localAddressDataGridViewTextBoxColumn.Width = 145;
            // 
            // remoteAddressDataGridViewTextBoxColumn
            // 
            this.remoteAddressDataGridViewTextBoxColumn.DataPropertyName = "RemoteAddress";
            this.remoteAddressDataGridViewTextBoxColumn.HeaderText = "RemoteAddress";
            this.remoteAddressDataGridViewTextBoxColumn.Name = "remoteAddressDataGridViewTextBoxColumn";
            this.remoteAddressDataGridViewTextBoxColumn.ReadOnly = true;
            this.remoteAddressDataGridViewTextBoxColumn.Width = 145;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(673, 554);
            this.Controls.Add(this.gbDetails);
            this.Controls.Add(this.gbOverview);
            this.Controls.Add(this.gbProcesses);
            this.Name = "MainForm";
            this.Text = "WireDog";
            this.gbProcesses.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.socketEventModelBindingSource)).EndInit();
            this.gbOverview.ResumeLayout(false);
            this.gbDetails.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbProcesses;
        private System.Windows.Forms.Button btnReload;
        private Components.ProcessListView processListView;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.BindingSource socketEventModelBindingSource;
        private System.Windows.Forms.GroupBox gbOverview;
        private ByteViewer byteViewer;
        private System.Windows.Forms.GroupBox gbDetails;
        private System.Windows.Forms.DataGridViewTextBoxColumn socketEventTypeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn timestampDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn processNameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn localAddressDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn remoteAddressDataGridViewTextBoxColumn;
    }
}

