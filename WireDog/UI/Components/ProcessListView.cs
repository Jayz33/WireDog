using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using WireDog.Delegation;
using WireDog.Native;

namespace WireDog.UI.Components
{
    public class ProcessListView : ListView
    {
        public event ProcessCheckedHandler ProcessChecked;
        public event ProcessesRemovedHandler ProcessesRemoved;

        private readonly ImageList _iconList;
        private IEnumerable<int> _currentProcessIds;

        public ProcessListView()
        {
            _currentProcessIds = new List<int>();

            _iconList = new ImageList();
            _iconList.ImageSize = new Size(32, 32);
            LargeImageList = _iconList;

            CheckBoxes = true;

            ItemChecked += ProcessListView_ItemChecked;

            Reload();
        }

        public void Reload()
        {
            var processes = GetProcesses();
            var addedProcesses = processes.Where(process => !_currentProcessIds.Any(processId => process.Id == processId));
            var newProcessIds = processes.Select(p => p.Id);
            var removedProcessIds = _currentProcessIds.Where(processId => !newProcessIds.Any(newProcessId => newProcessId == processId));

            foreach (var addedProcess in addedProcesses)
                AddProcess(addedProcess);

            foreach (var removedProcessId in removedProcessIds)
                RemoveProcess(removedProcessId);

            NotifyRemovedProcesses(removedProcessIds);
            _currentProcessIds = newProcessIds;
        }

        private void AddProcess(Process process)
        {
            try
            {
                var item = new ListViewItem();
                var key = process.Id.ToString();

                item.Text = string.Format("{0} ({1})", Path.GetFileName(process.MainModule.FileName), process.Id);
                item.Name = key;
                item.Tag = process;
                item.ImageKey = key;

                var icon = Icon.ExtractAssociatedIcon(process.MainModule.FileName).ToBitmap();
                _iconList.Images.Add(key, icon);

                Items.Add(item);
            }
            catch (Win32Exception) { }
        }

        private void RemoveProcess(int id)
        {
            var key = id.ToString();
            var processItem = Items.Find(key, false).FirstOrDefault();
            if (processItem == null)
                return;
            _iconList.Images.RemoveByKey(key);
            Items.Remove(processItem);
        }

        private void NotifyRemovedProcesses(IEnumerable<int> removedProcesses)
        {
            if (ProcessesRemoved == null)
                return;

            ProcessesRemoved(removedProcesses);
        }

        private void ProcessListView_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            if (ProcessChecked == null)
                return;

            var item = e.Item;
            var process = (Process)item.Tag;
            ProcessChecked(process, item.Checked);
        }

        private IEnumerable<Process> GetProcesses()
        {
            var currentProcessId = Process.GetCurrentProcess().Id;
            return Process.GetProcesses()
                .Where(p => Is32BitProcess(p) && p.Id != currentProcessId)
                .ToList();
        }

        private bool Is32BitProcess(Process process)
        {
            bool is32BitProcess = false;
            bool retVal = false;
            try
            {
                is32BitProcess = NativeMethods.IsWow64Process(process.Handle, out retVal);
            }
            catch (Exception) { };
            return is32BitProcess && retVal;
        }
    }
}
