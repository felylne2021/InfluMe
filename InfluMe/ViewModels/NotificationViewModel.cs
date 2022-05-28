using InfluMe.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace InfluMe.ViewModels {


    public class NotificationViewModel:BaseViewModel {

        private List<Notification> notifications;
        private bool isEmpty;

        public NotificationViewModel() {
            this.BackButtonCommand = new Command(_ => Application.Current.MainPage.Navigation.PopAsync());
        }

        public bool IsEmpty {
            get {
                return this.isEmpty;
            }

            set {
                if (this.isEmpty == value) {
                    return;
                }

                this.SetProperty(ref this.isEmpty, value);
            }
        }

        public List<Notification> Notifications {
            get {
                return this.notifications;
            }

            set {
                if (this.notifications == value) {
                    return;
                }

                this.SetProperty(ref this.notifications, value);
            }
        }

        public Command BackButtonCommand { get; set; }

    }
}
