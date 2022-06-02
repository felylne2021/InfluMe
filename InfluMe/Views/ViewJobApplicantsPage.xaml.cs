using InfluMe.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace InfluMe.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ViewJobApplicantsPage : ContentPage
	{
		public ViewJobApplicantsPage ()
		{
			InitializeComponent ();
		}

		void OnCheckBoxCheckedChanged(object sender, CheckedChangedEventArgs e) {
			var cb = (CheckBox)sender;
			JobApplicantsViewModel vm = (JobApplicantsViewModel)this.BindingContext;
			vm.Applicants.Where(x => x.influencerId.Equals(cb.BindingContext)).Select(c => { c.isChecked = !c.isChecked; return c; }).ToList();

		}
	}
}