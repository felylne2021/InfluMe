using InfluMe.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InfluMe.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace InfluMe.Views {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NotificationListPage : ContentPage {
        public NotificationListPage(List<Notification> notifications) {
            InitializeComponent();
            BindingContext = new NotificationViewModel() { Notifications = notifications, IsEmpty = (notifications.Count == 0)};
        }
    }
}