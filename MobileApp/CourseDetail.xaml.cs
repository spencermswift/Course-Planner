using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobileApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CourseDetail : ContentPage
    {
        private SQLiteAsyncConnection _conn;
        private Course _currentCourse;
        public CourseDetail(Course course)
        {
            InitializeComponent();
            _currentCourse = course;
            _conn = DependencyService.Get<ISQLiteDb>().GetConnection();
        }
        protected override void OnAppearing()
        {
            courseName.Text = _currentCourse.CourseName;
            Status.Text = _currentCourse.Status;
            StartDate.Text = _currentCourse.StartDate.ToString("MM/dd/yy");
            EndDate.Text = _currentCourse.EndDate.ToString("MM/dd/yy");
            InstructorName.Text = _currentCourse.InstructorName;
            InstructorPhone.Text = _currentCourse.InstructorPhone;
            InstructorEmail.Text = _currentCourse.InstructorEmail;
            Notes.Text = _currentCourse.Notes;
            NotificationsEnabled.Text = _currentCourse.NotificationEnabled == 1 ? "Yes" : "No";
            base.OnAppearing();

            
        }

        private async void OnButtonClick(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        private async void Edit_Course_Click(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new EditCourse(_currentCourse));
        }

        private async void Drop_Course_Click(object sender, EventArgs e)
        {
            var confirmation = await DisplayAlert("Alert", "Are you sure you want to drop this course?", "Yes", "No");
            if (confirmation)
            {
                await _conn.DeleteAsync(_currentCourse);
                await Navigation.PopModalAsync();
            }
        }

        private async void Assessments_Click(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new AssessmentsPage(_currentCourse));
        }

        private async void ShareButton_Clicked(object sender, EventArgs e)
        {
            await Share.RequestAsync(new ShareTextRequest
            {
                Text = Notes.Text,
                Title = "Share your notes from this course!"
            });
        }
    }
}