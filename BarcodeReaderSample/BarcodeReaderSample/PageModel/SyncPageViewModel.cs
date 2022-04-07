using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BarcodeReaderSample.API;
using BarcodeReaderSample.Database;
using BarcodeReaderSample.Models;
using Entities;
using NobelXamarin.Helpers;
using TraceIQ.Expeditor.API;
using TraceIQ.Expeditor.Models;
using TraceIQ.Expeditor.PageModels;
using Xamarin.Forms;

namespace BarcodeReaderSample.PageModel
{
    public class SyncPageViewModel : BaseViewModel
    {
        public ObservableCollection<SyncModel> SyncModels { get; set; }
        private readonly SynchronizationContext _mUiContext = SynchronizationContext.Current;
        public Command StartReceivingCommand { get; set; }

        public SyncPageViewModel(INavigation navigation)
        {
            Navigation = navigation;
            DbService = new DbService();
            BaseApiService = new BaseApiService();

            StartReceivingCommand = new Command(SyncData);

            SyncModels = new ObservableCollection<SyncModel>();
        }

        public void SyncData()
        {
            SyncModels.Clear();

            var syncTypes = Enum.GetValues(typeof(SyncDataTypes)).Cast<SyncDataTypes>().ToList().Select(s =>
                new SyncModel
                {
                    SyncStatus = SyncStatus.Waiting,
                    SyncType = s,
                    SyncDataTypeString = s.GetDisplayName(),
                }).OrderBy(s => s.SyncType).ToList();

            foreach (var syncModel in syncTypes)
            {
                Task.Run(() =>
                {
                    switch (syncModel.SyncType)
                    {
                        case SyncDataTypes.Product:

                            var getProducts = BaseApiService.GetProducts(RestContext.User.TransportId);
                            if (getProducts.Result != OperationStatus.Success)
                                syncModel.SyncStatus = SyncStatus.Error;
                            else
                            {
                                if (!getProducts.Value.Any())
                                    syncModel.SyncStatus = SyncStatus.NoData;
                                else
                                {
                                    var addProducts = DbService.AddProducts(getProducts.Value);
                                    syncModel.SyncStatus = addProducts.Result != OperationStatus.Success ? SyncStatus.Error : SyncStatus.Completed;
                                }
                            }

                            break;
                        case SyncDataTypes.Suppliers:

                            var getSuppliers = BaseApiService.GetSuppliers(RestContext.User.TransportId);
                            if (getSuppliers.Result != OperationStatus.Success)
                                syncModel.SyncStatus = SyncStatus.Error;
                            else
                            {
                                if (!getSuppliers.Value.Any())
                                    syncModel.SyncStatus = SyncStatus.NoData;
                                else
                                {
                                    var addSuppliers = DbService.AddSuppliers(getSuppliers.Value);
                                    syncModel.SyncStatus = addSuppliers.Result != OperationStatus.Success ? SyncStatus.Error : SyncStatus.Completed;
                                }
                            }


                            break;
                        case SyncDataTypes.Orders:

                            var getOrders = BaseApiService.GetOrders(RestContext.User.TransportId);
                            if (getOrders.Result != OperationStatus.Success)
                                syncModel.SyncStatus = SyncStatus.Error;
                            else
                            {
                                if (!getOrders.Value.Any())
                                    syncModel.SyncStatus = SyncStatus.NoData;
                                else
                                {
                                    var addOrders = DbService.AddOrders(getOrders.Value);
                                    syncModel.SyncStatus = addOrders.Result != OperationStatus.Success ? SyncStatus.Error : SyncStatus.Completed;
                                }
                            }

                            break;
                        case SyncDataTypes.OrderDetails:

                            var getOrderDetails = BaseApiService.GetOrderDetails(RestContext.User.TransportId);
                            if (getOrderDetails.Result != OperationStatus.Success)
                                syncModel.SyncStatus = SyncStatus.Error;
                            else
                            {
                                if (!getOrderDetails.Value.Any())
                                    syncModel.SyncStatus = SyncStatus.NoData;
                                else
                                {
                                    var addOrderDetails = DbService.AddOrderDetails(getOrderDetails.Value);
                                    syncModel.SyncStatus = addOrderDetails.Result != OperationStatus.Success ? SyncStatus.Error : SyncStatus.Completed;
                                }
                            }


                            break;
                        case SyncDataTypes.CodesMapping:
                            var getCodesMapping = BaseApiService.GetCodeMapping(RestContext.User.TransportId);
                            if (getCodesMapping.Result != OperationStatus.Success)
                                syncModel.SyncStatus = SyncStatus.Error;
                            else
                            {
                                if (!getCodesMapping.Value.Any())
                                    syncModel.SyncStatus = SyncStatus.NoData;
                                else
                                {
                                    var addCodeMapping = DbService.AddCodeMappings(getCodesMapping.Value);
                                    syncModel.SyncStatus = addCodeMapping.Result != OperationStatus.Success ? SyncStatus.Error : SyncStatus.Completed;
                                }
                            }

                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }

                    _mUiContext.Post(o =>
                    {
                        syncModel.SyncStatusString = syncModel.SyncStatus.GetDisplayName();
                        SyncModels.Add(syncModel);
                    }, null);
                });

                Thread.Sleep(300);
            }
        }
    }
}
