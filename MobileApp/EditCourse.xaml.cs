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
    public partial class EditCourse : ContentPage
    {
        private SQLiteAsyncConnection _conn;
        private Course _currentCourse;
        public EditCourse(Course currentCourse)
        {
            InitializeComponent();
            _currentCourse = currentCourse;
            _conn = DependencyService.Get<ISQLiteDb>().GetConnection();
        }

        protected async override void OnAppearing()
        {
            await _conn.CreateTableAsync<Course>();

            CourseName.Text = _currentCourse.CourseName;
            CourseStatus.SelectedItem = _currentCourse.Status;
            StartDate.Date = _currentCourse.StartDate;
            EndDate.Date = _currentCourse.EndDate;
            InstructorName.Text = _currentCourse.InstructorName;
            InstructorPhone.Text = _currentCourse.InstructorPhone;
            InstructorEmail.Text = _currentCourse.InstructorEmail;
            Notes.Text = _currentCourse.Notes;
            if (_currentCourse.NotificationEnabled == 1)
            {
                EnableNotifications.On = true;
            }
            base.OnAppearing();
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            _currentCourse.CourseName = CourseName.Text;
            _currentCourse.Status = (string)CourseStatus.SelectedItem;
            _currentCourse.StartDate = StartDate.Date;
            _currentCourse.EndDate = EndDate.Date;
            _currentCourse.InstructorName = InstructorName.Text;
            _currentCourse.InstructorEmail = InstructorEmail.Text;
            _currentCourse.InstructorPhone = InstructorPhone.Text;
            _currentCourse.Notes = Notes.Text;
            _currentCourse.NotificationEnabled = EnableNotifications.On == true ? 1 : 0;

            if (FieldCheck.IsNull(CourseName.Text) &&
                FieldCheck.IsNull(InstructorName.Text) &&
                FieldCheck.IsNull(InstructorPhone.Text))
            {
                if (FieldCheck.IsValidEmail(InstructorEmail.Text))
                {
                    if (_currentCourse.StartDate < _currentCourse.EndDate)
                    { 
                        await _conn.UpdateAsync(_currentCourse);
                        await Navigation.PopModalAsync();
                    }
                    else await DisplayAlert("Error.", "Please ensure start date is before end date.", "Ok");
                }
                else await DisplayAlert("Error.", "Please ensure all fields are completed.", "Ok");
            }
            else await DisplayAlert("Error.", "Please provide a valid email address", "Ok");
        }
    

        private async void OnButtonClick(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
    }
}