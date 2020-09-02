using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobileApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AssessmentsPage : ContentPage
    {
        private Course _currentCourse;
        private SQLiteAsyncConnection _conn;
        private ObservableCollection<Assessment> _assessmentList;
        public AssessmentsPage(Course currentCourse)
        {
            InitializeComponent();
            CourseName.Text = currentCourse.CourseName;
            _currentCourse = currentCourse;
            _conn = DependencyService.Get<ISQLiteDb>().GetConnection();
            assessmentListView.ItemTapped += new EventHandler<ItemTappedEventArgs>(Assessment_Tapped);

        }

        protected override async void OnAppearing()
        {
            CourseName.Text = _currentCourse.CourseName;
            await _conn.CreateTableAsync<Assessment>();
            var assessmentList = await _conn.QueryAsync<Assessment>($"Select * From Assessments Where Course = '{_currentCourse.Id}'");
            _assessmentList = new ObservableCollection<Assessment>(assessmentList);
            assessmentListView.ItemsSource = _assessmentList;
            base.OnAppearing();
        }

        private async void OnButtonClick(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        private async void Add_Assessment_Click(object sender, EventArgs e)
        {
            await _conn.CreateTableAsync<Assessment>();
            var assessmentCount = await _conn.QueryAsync<Assessment>($"Select Type From Assessments Where Course = '{_currentCourse.Id}'");
            if (assessmentCount.Count == 2)
            {
                await DisplayAlert("Alert", "You cannot add more than two exams. Please remove an exam and try again", "Ok");
            }
            else await Navigation.PushModalAsync(new AddAssessment(_currentCourse));
        }
        private async void Assessment_Tapped(object sender, ItemTappedEventArgs e)
        {
            Assessment assessment = (Assessment)e.Item;
            await Navigation.PushModalAsync(new AssessmentDetail(assessment));
        }
    }
}