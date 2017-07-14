using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AP_CARGO_SERVICE
{
    public class SyncTables : INotifyPropertyChanged
    {
        private bool _isSelected;
        private TableStatus _status;
        public event PropertyChangedEventHandler PropertyChanged;

        public SyncTables()
        {
            Status = TableStatus.Good;
            isSelected = false;
        }

        [DisplayName("Tables")]
        public string TableName { get; set; }

        [DisplayName("Status")]
        public TableStatus Status
        {
            get
            {
                return _status;
            }
            set
            {
                if (_status != value)
                {
                    _status = value;
                    OnPropertyChanged(new PropertyChangedEventArgs("Status"));
                }
            }
        }

        [DisplayName("Select")]
        public bool isSelected
        {
            get
            {
                return _isSelected;
            }
            set
            {
                if (_isSelected != value)
                {
                    _isSelected = value;
                    OnPropertyChanged(new PropertyChangedEventArgs("isSelected"));
                }
            }
        }

        public void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, e);
            }
        }
    }

    public enum TableStatus
    {
        Fresh, // currently provision
        Good, //sync working
        Bad, //sync not working
        Provisioned,
        Deprovisioned,
        ErrorProvision,
        ErrorDeprovision
    }

}
