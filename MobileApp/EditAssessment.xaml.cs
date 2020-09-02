using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobileApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditAssessment : ContentPage
    {
        private Assessment _assessment;
        private SQLiteAsyncConnection _conn;
        
        public EditAssessment(Assessment assessment)
        {
            InitializeComponent();
            _assessment = assessment;
            _conn = DependencyService.Get<ISQLiteDb>().GetConnection();
        }

        protected async override void OnAppearing()
        {
            await _conn.CreateTableAsync<Assessment>();

            AssessmentName.Text = _assessment.Title;
            StartDate.Date = _assessment.StartDate;
            EndDate.Date = _assessment.EndDate;
            
            if (_assessment.NotificationEnabled == 1)
            {
                EnableNotifications.On = true;
            }
            base.OnAppearing();
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            _assessment.Title = AssessmentName.Text;
            _assessment.StartDate = StartDate.Date;
            _assessment.EndDate = EndDate.Date;
            _assessment.NotificationEnabled = EnableNotifications.On == true ? 1 : 0;

            if (FieldCheck.IsNull(AssessmentName.Text))
            {
                if (_assessment.StartDate < _assessment.EndDate)
                {
                    await _conn.UpdateAsync(_assessment);
                    await Navigation.PopModalAsync();
                }
                else await DisplayAlert("Error.", "Please ensure start date is before end date.", "Ok");
            }
            else await DisplayAlert("Error.", "Please ensure all fields are completed.", "Ok");
        }


        private async void OnButtonClick(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
    }
}