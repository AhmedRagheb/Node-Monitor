using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using NodeMonitor.Client.Connector;
using NodeMonitor.Models;

namespace NodeMonitor.Client.Forms
{
    public partial class SampleDataTypesForm : Form
    {
        internal int[] SelectedSampleTypes;
        public SampleDataTypesForm()
        {
            InitializeComponent();
            BindList();
        }

        private List<SampleType> GetSampleDataTypes()
        {
            var sampleDataTypes = new List<SampleType>();
            if (ConnectorTest.IsChannelOpen())
            {
                sampleDataTypes = Connector.Connector.RemoteObject.GetSampleTypes();
            }
            else
            {
                MessageBox.Show(Resources.Resources.ConnectedError, Resources.Resources.Error, MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            return sampleDataTypes;
        }

        public void BindList()
        {
            SampleDataTypesList.Items.Clear();
            List<SampleType> types = GetSampleDataTypes();
            SampleDataTypesList.DataSource = types;
            SampleDataTypesList.DisplayMember = "Name";
            SampleDataTypesList.ValueMember = "Id";
        }

        private void DoneBtnClick(object sender, System.EventArgs e)
        {
            var items = SampleDataTypesList.SelectedItems.Cast<SampleType>();
            SelectedSampleTypes = items.Select(x => x.Id).ToArray();
            this.Close();
        }
    }
}
