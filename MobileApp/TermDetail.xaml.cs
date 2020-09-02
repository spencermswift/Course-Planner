using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobileApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TermDetail : ContentPage
    {
        private SQLiteAsyncConnection _conn;
        private ObservableCollection<Course> _courseList;
        private Term _currentTerm;
        public TermDetail(Term term)
        {
            InitializeComponent();
            Title = term.Title;
            _currentTerm = term;
            _conn = DependencyService.Get<ISQLiteDb>().GetConnection();
            courseListView.ItemTapped += new EventHandler<ItemTappedEventArgs>(Course_Tapped);
        }
        protected override async void OnAppearing()
        {
            termTitle.Text = _currentTerm.Title;
            TermDetailStart.Text = _currentTerm.StartDate.ToString("MM/dd/yy");
            TermDetailEnd.Text = _currentTerm.EndDate.ToString("MM/dd/yy");
            await _conn.CreateTableAsync<Course>();
            var courseList = await _conn.QueryAsync<Course>($"Select * From Courses Where Term = '{_currentTerm.Id}'");
            _courseList = new ObservableCollection<Course>(courseList);
            courseListView.ItemsSource = _courseList;
            base.OnAppearing();
        }
        private async void OnButtonClick(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();    
        }

        private async void Course_Tapped(object sender, ItemTappedEventArgs e)
        {
            Course course = (Course)e.Item;
            await Navigation.PushModalAsync(new CourseDetail(course));
        }

        private async void Add_Course_Click(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new AddCourse(_currentTerm));
        }

        private async void Edit_Term_Click(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new EditTerm(_currentTerm));

        }

        private async void Drop_Term_Click(object sender, EventArgs e)
        {
            var confirmation = await DisplayAlert("Alert", "Are you sure you want to drop this term?", "Yes", "No");
            if (confirmation)
            {
                await _conn.DeleteAsync(_currentTerm);
                await Navigation.PopModalAsync();
            }
        }
    }
}