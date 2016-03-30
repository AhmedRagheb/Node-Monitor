using System;
using System.Data;
using System.Windows.Forms;
using NodeMonitor.Client.Connector;

namespace NodeMonitor.Client.Forms
{
    public partial class ClientForm : Form
    {
        private int[] _sampleTypes;
        private Timer _timer;
        private bool _timerStart;
        

        public ClientForm()
        {
            InitializeComponent();
            InitConnection();
            DataGridViewSampleData.AutoGenerateColumns = false;
        }

        private void InitConnection()
        {
            Connector.Connector.ConnectToServer();

            if (ConnectorTest.IsChannelOpen())
            {
                MessageBox.Show(Resources.Resources.ConnectedSuccess, Resources.Resources.Success, MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show(Resources.Resources.ConnectedError, Resources.Resources.Error, MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        internal void InitTimer()
        {
            _timer = new Timer();
            _timer.Tick += (sender, args) => BindDataGrid();
            _timer.Interval = 1000;
            _timer.Start();
            _timerStart = true;
        }

        #region Menu Actions

        private void RunToolStripMenuItemClick(object sender, EventArgs e)
        {
            if (ConnectorTest.IsChannelOpen())
            {
                if (!_timerStart)
                {
                    var typesForm = new SampleDataTypesForm();
                    typesForm.ShowDialog();
                    _sampleTypes = typesForm.SelectedSampleTypes;

                    InitTimer();
                }
            }
            else
            {
                MessageBox.Show(Resources.Resources.ConnectedError, Resources.Resources.Error, MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void StopToolStripMenuItemClick(object sender, EventArgs e)
        {
            if (_timer != null)
                _timer.Dispose();
            _sampleTypes = null;
            _timerStart = false;
        }

        private void ExitToolStripMenuItemClick(object sender, EventArgs e)
        {
            if (_timer != null)  
                _timer.Dispose();

            _timerStart = false;
            Close();
        }

        #endregion

        #region DataGrid

        private void DataGridViewSampleDataCellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (DataGridViewSampleData.Columns[e.ColumnIndex].Name == "TimeStamp")
            {
                Helper.ShortFormDateFormat(e);
            }
        }

        private void BindDataGrid()
        {
            if (ConnectorTest.IsChannelOpen())
            {
                var data = Connector.Connector.RemoteObject.Fetch(_sampleTypes);
                var scrollingRowIndex = DataGridViewSampleData.FirstDisplayedScrollingRowIndex;

                var currentRow = DataGridViewSampleData.CurrentCell != null
                                     ? DataGridViewSampleData.CurrentCell.RowIndex
                                     : -1;

                DataTable bindedData;
                var currentDataSource = DataGridViewSampleData.DataSource as DataTable;
                var newData = Helper.ToDataTable(data);

                if (currentDataSource != null)
                {
                    currentDataSource.Merge(newData);
                    bindedData = currentDataSource;
                }
                else
                {
                    bindedData = newData;
                }

                DataGridViewSampleData.DataSource = bindedData;
                DataGridViewSampleData.Update();


                if (scrollingRowIndex > -1)
                    DataGridViewSampleData.FirstDisplayedScrollingRowIndex = scrollingRowIndex;

                if (currentRow > -1)
                    DataGridViewSampleData.Rows[currentRow].Selected = true;
            }
            else
            {
                _timer.Dispose();
                MessageBox.Show(Resources.Resources.ConnectedError, Resources.Resources.Error, MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        #endregion
    }
}
