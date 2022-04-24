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
        private bool _isSyncButtonEnabled;
        public bool IsSyncButtonEnabled
        {
            get => _isSyncButtonEnabled;
            set
            {
                _isSyncButtonEnabled = value;
                OnPropertyChanged(nameof(IsSyncButtonEnabled));
            }
        }

        public SyncPageViewModel(INavigation navigation)
        {
            Navigation = navigation;
            DbService = new DbService();
            BaseApiService = new BaseApiService();

            StartReceivingCommand = new Command(SyncData);

            SyncModels = new ObservableCollection<SyncModel>();
            IsSyncButtonEnabled = true;
        }

        public async void SyncData()
        {
            IsSyncButtonEnabled = false;

            var anyIncompleteOrders = DbService.AnyInCompleteOrders();
            if (anyIncompleteOrders.Result != OperationStatus.Success)
            {
                await Application.Current.MainPage.DisplayAlert("Ошибка", anyIncompleteOrders.ErrorMessage, "ОК");
                return;
            }

            var getReportReturns = DbService.GetIncompleteReportReturns();
            if(getReportReturns.Result != OperationStatus.Success)
            {
                await Application.Current.MainPage.DisplayAlert("Ошибка", getReportReturns.ErrorMessage, "ОК");
                return;
            }

            if (getReportReturns.Value.Any())
            {  
                var getReportReturnsApi = BaseApiService.CheckReportReturnStatus(getReportReturns.Value.Select(s => s.Id).ToList());
                if (getReportReturnsApi.Result != OperationStatus.Success)
                {
                    await Application.Current.MainPage.DisplayAlert("Ошибка", getReportReturnsApi.ErrorMessage, "ОК");
                    return;
                }

                var updateReports = DbService.UpdateReportReturns(getReportReturnsApi.Value);
                if (updateReports.Result != OperationStatus.Success)
                {
                    await Application.Current.MainPage.DisplayAlert("Ошибка", updateReports.ErrorMessage, "ОК");
                    return;
                }

                if (getReportReturnsApi.Value.Any(s => s.ReturnStatus != ReturnStatus.Completed))
                {
                    await Application.Current.MainPage.DisplayAlert("Предупреждение", "У вас есть незавершенные документы возвратов", "ОК");
                    return;
                }
            }

            var getGoodsOnStock = DbService.GetGoodsOnStock();
            if(getGoodsOnStock.Result != OperationStatus.Success)
            {
                await Application.Current.MainPage.DisplayAlert("Ошибка", getGoodsOnStock.ErrorMessage, "ОК");
                return;
            }

            if (getGoodsOnStock.Value.Any())
            {
                var checkWithReportReturns = DbService.CheckGoodsOnStockWithReportReturns(getGoodsOnStock.Value);
                if (checkWithReportReturns.Result != OperationStatus.Success)
                {
                    await Application.Current.MainPage.DisplayAlert("Ошибка", checkWithReportReturns.ErrorMessage, "ОК");
                    return;
                }
            }

            var deleteAllData = DbService.DeleteAllData();
            if(deleteAllData.Result != OperationStatus.Success)
            {
                await Application.Current.MainPage.DisplayAlert("Ошибка", deleteAllData.ErrorMessage, "ОК");
                return;
            }

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
                await Task.Run(() =>
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

                                    if (addCodeMapping.Result != OperationStatus.Success)
                                    {

                                        _mUiContext.Post(async o =>
                                        {
                                            await Application.Current.MainPage.DisplayAlert("Ошибка", addCodeMapping.ErrorMessage, "ОК");
                                        }, null);
                                        return;
                                    }

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
            }
            
            IsSyncButtonEnabled = true;
        }
    }
}
