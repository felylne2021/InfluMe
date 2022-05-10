using InfluMe.Helpers;
using InfluMe.Services;
using InfluMe.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: ExportFont("Montserrat-Bold.ttf",Alias="Montserrat-Bold")]
     [assembly: ExportFont("Montserrat-Medium.ttf", Alias = "Montserrat-Medium")]
     [assembly: ExportFont("Montserrat-Regular.ttf", Alias = "Montserrat-Regular")]
     [assembly: ExportFont("Montserrat-SemiBold.ttf", Alias = "Montserrat-SemiBold")]
     [assembly: ExportFont("UIFontIcons.ttf", Alias = "FontIcons")]
[assembly: ExportFont("Montserrat-Bold.ttf",Alias="Montserrat-Bold")]
     [assembly: ExportFont("Montserrat-Medium.ttf", Alias = "Montserrat-Medium")]
     [assembly: ExportFont("Montserrat-Regular.ttf", Alias = "Montserrat-Regular")]
     [assembly: ExportFont("Montserrat-SemiBold.ttf", Alias = "Montserrat-SemiBold")]
     [assembly: ExportFont("UIFontIcons.ttf", Alias = "FontIcons")]
namespace InfluMe
{
    public partial class App : Application
    {
        public static string ImageServerPath { get; } = "https://cdn.syncfusion.com/essential-ui-kit-for-xamarin.forms/common/uikitimages/";

        public App()
        {
            InitializeComponent();
            DependencyService.Register<MockDataStore>();

            bool isLoggedIn = Current.Properties.ContainsKey("IsLoggedIn") ? Convert.ToBoolean(Current.Properties["IsLoggedIn"].ToString()) : false;
            string userId = Current.Properties.ContainsKey("UserId") ? Current.Properties["UserId"].ToString() : "";
            string userType = Current.Properties.ContainsKey("UserType") ? Current.Properties["UserType"].ToString() : "";


            if (!isLoggedIn) {
                //Load if Not Logged In
                Current.Properties["IsLoggedIn"] = Boolean.FalseString;
                MainPage = new NavigationPage(new MainLoginPage());
            }
            else {
                //Load if Logged In
                if (Application.Current.Properties["UserType"].Equals(UserTypeEnum.Influencer.ToString()))
                    MainPage = new NavigationPage(new ViewProfilePage());
                else MainPage = new NavigationPage(new AdminHomePage());
            }

        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
