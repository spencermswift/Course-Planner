using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobileApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditTerm : ContentPage
    {
        private SQLiteAsyncConnection _conn;
        private Term _term;
        public EditTerm(Term currentTerm)
        {
            InitializeComponent();
            _term = currentTerm;
            _conn = DependencyService.Get<ISQLiteDb>().GetConnection();
        }
        protected override async void OnAppearing()
        {
            await _conn.CreateTableAsync<Term>();
            TermTitle.Text = _term.Title;
            startDate.Date = _term.StartDate;
            endDate.Date = _term.EndDate;
            base.OnAppearing();
            
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            _term.Title = TermTitle.Text;
            _term.StartDate = startDate.Date;
            _term.EndDate = endDate.Date;

            if (FieldCheck.IsNull(_term.Title))
            {
                if (_term.StartDate < _term.EndDate)
                {

                    await _conn.UpdateAsync(_term);
                    await Navigation.PopModalAsync();
                }
                else await DisplayAlert("Error.", "Please ensure start date is before end date.", "Ok");
            } else await DisplayAlert("Error.", "Please ensure all fields are completed.", "Ok");
        }

        private async void OnButtonClick(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
    }

        
    }
