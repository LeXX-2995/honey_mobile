using System;
using System.Collections.Generic;
using System.Text;
using NobelXamarin.Helpers;
using TraceIQ.Expeditor.Models;

namespace BarcodeReaderSample.Models
{
    public class SyncModel : ModelObject
    {
        private SyncDataTypes _syncType;

        public SyncDataTypes SyncType
        {
            get => _syncType;
            set
            {
                _syncType = value;
                OnPropertyChanged(nameof(SyncType));
            }
        }


        private SyncStatus _syncStatus;

        public SyncStatus SyncStatus
        {
            get => _syncStatus;
            set
            {
                _syncStatus = value;
                OnPropertyChanged(nameof(SyncStatus));
            }
        }

        private string _syncDataTypeString;

        public string SyncDataTypeString
        {
            get => _syncDataTypeString;
            set
            {
                _syncDataTypeString = value;
                OnPropertyChanged(nameof(SyncDataTypeString));
            }
        }

        private string _syncStatusString;

        public string SyncStatusString
        {
            get => _syncStatusString;
            set
            {
                _syncStatusString = value;
                OnPropertyChanged(nameof(SyncStatusString));
            }
        }


    }
}
