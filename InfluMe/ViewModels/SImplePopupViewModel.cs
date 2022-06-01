using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace InfluMe.ViewModels {
    public class SimplePopupViewModel : BaseViewModel{

        
        public SimplePopupViewModel() {
            this.ClosePopupCommand = new Command(_ => Application.Current.MainPage.Navigation.PopPopupAsync());
        }

        public Command ClosePopupCommand { get; set; } 
        
    }
}
