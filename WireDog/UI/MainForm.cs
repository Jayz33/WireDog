using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using WireDog.Delegation;
using WireDog.UI.Components;
using WireDog.UI.Models;

namespace WireDog.UI
{
    public partial class MainForm : Form
    {
        public event ProcessCheckedHandler ProcessChecked;
        public event ProcessesRemovedHandler ProcessesRemoved;

        public MainForm()
        {
            InitializeComponent();
            BindControls();
        }

        public void AddSocketEventModelToGrid(SocketEventModel model)
        {
            socketEventModelBindingSource.Add(model); 
            dataGridView1.FirstDisplayedScrollingRowIndex = dataGridView1.RowCount - 1;
        }

        private void BindControls()
        {
            btnReload.Click += btnReload_Click;
            processListView.ProcessesRemoved += processListView_ProcessesRemoved;
            processListView.ProcessChecked += processListView_ProcessChecked;
            dataGridView1.RowStateChanged += dataGridView1_RowStateChanged;
        }

        private void dataGridView1_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
        {
            var row = e.Row;
            if (row.Selected)
            {
                var packetEvent = (SocketEventModel)row.DataBoundItem;
                byteViewer.SetBytes(packetEvent.Data);
            }
        }

        private void btnReload_Click(object sender, EventArgs e)
        {
            processListView.Reload();
        }

        private void processListView_ProcessesRemoved(IEnumerable<int> processIds)
        {
            ProcessesRemoved(processIds);
        }

        private void processListView_ProcessChecked(Process process, bool isChecked)
        {
            ProcessChecked(process, isChecked);
        }
    }
}
