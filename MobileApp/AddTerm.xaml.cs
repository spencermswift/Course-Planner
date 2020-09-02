using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobileApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddTerm : ContentPage
    {
        public MainPage _mainPage;
        private SQLiteAsyncConnection _conn;
        
        public AddTerm(MainPage mainPage)
        {
            InitializeComponent();
            _mainPage = mainPage;
            _conn = DependencyService.Get<ISQLiteDb>().GetConnection();
        }
        private async void OnButtonClick(object sender, EventArgs e)
        {

            await Navigation.PopModalAsync();
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            var term = new Term();
            term.Title = TermTitle.Text;
            term.StartDate = startDate.Date;
            term.EndDate = endDate.Date;

            if (FieldCheck.IsNull(term.Title))
            {
                if (term.StartDate < term.EndDate)
                {
                    await _conn.InsertAsync(term);
                    _mainPage._termList.Add(term);
                    await Navigation.PopModalAsync();
                }
                else await DisplayAlert("Error.", "Please ensure start date is before end date.", "Ok");
            }
            else await DisplayAlert("Error.", "Please ensure all fields are filled in.", "Ok");
        }
    }
}