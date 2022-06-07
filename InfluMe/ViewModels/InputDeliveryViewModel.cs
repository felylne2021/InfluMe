using InfluMe.DataService;
using InfluMe.Models;
using InfluMe.Views;
using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace InfluMe.ViewModels {
    public class InputDeliveryViewModel {

        private JobDataService service = new JobDataService();
        private JobAppliedResponse selected;

        public InputDeliveryViewModel() {
            this.InitializeProperties();
            this.BackButtonCommand = new Command(_ => Application.Current.MainPage.Navigation.PopAsync());
            this.SaveCommand = new Command(SaveClicked);
        }

        public string DeliveryCompany { get; set; }
        public string DeliveryReceipt { get; set; }

        private void InitializeProperties() {
            
        }

        public JobAppliedResponse Selected {
            get {
                return this.selected;
            }

            set {
                if (value != null) {
                    selected = value;
                }
            }
        }

        private async void SaveClicked() {
            this.DeliveryCompany = DeliveryCourier.Couriers.FirstOrDefault(x => x.courierName == DeliveryCompany).courierCode;

            Delivery delivery = new Delivery() { appliedJobId = Selected.appliedJobId, deliveryCompany = DeliveryCompany, deliveryReceipt = DeliveryReceipt, deliveryItemName = Selected.job.jobProduct, deliveryStatus = "On Delivery" };
            try {
                await service.SaveDelivery(delivery);
                await Application.Current.MainPage.Navigation.PushPopupAsync(new InfoPopupPage("Delivery Saved"));
                await Application.Current.MainPage.Navigation.PopAsync();
            }
            catch (Exception ex) { await Application.Current.MainPage.Navigation.PushPopupAsync(new ErrorPopupPage()); 
            }
        }

        public Command BackButtonCommand { get; set; }
        public Command SaveCommand { get; set; }
    }
}