using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Plugin.LocalNotifications;
using System.Runtime.InteropServices;

namespace MobileApp
{
    [Table("Terms")]
    public class Term
    {
        [PrimaryKey, AutoIncrement, Column("_id")]
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
    [Table("Courses")]
    public class Course
    {
        [PrimaryKey, AutoIncrement, Column("_id")]
        public int Id { get; set; }
        public int Term { get; set; }
        public string CourseName { get; set; }
        public string Status { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string InstructorName { get; set; }
        public string InstructorPhone { get; set; }
        public string InstructorEmail { get; set; }
        public string Notes { get; set; }
        public int NotificationEnabled { get; set; }
    }
    [Table("Assessments")]
    public class Assessment
    {
        [PrimaryKey, AutoIncrement, Column("_id")]
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Type { get; set; }
        public int Course { get; set; }
        public int NotificationEnabled { get; set; }
    }
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        private SQLiteAsyncConnection _conn;
        public ObservableCollection<Term> _termList;
        private bool pushNotification = true;
        public MainPage()
        {
            InitializeComponent();
            _conn = DependencyService.Get<ISQLiteDb>().GetConnection();
            termListView.ItemTapped += new EventHandler<ItemTappedEventArgs>(Term_Click);
            
        }
        protected override async void OnAppearing()
        {
            await _conn.CreateTableAsync<Term>();
            await _conn.CreateTableAsync<Course>();
            await _conn.CreateTableAsync<Assessment>();

            var termList = await _conn.Table<Term>().ToListAsync();
            var courseList = await _conn.Table<Course>().ToListAsync();
            var assessmentList = await _conn.Table<Assessment>().ToListAsync();

            //populate app with dummy data if not data exists.
            if(!termList.Any())
            {
                var dummyTerm = new Term();
                dummyTerm.Title = "Term 1";
                dummyTerm.StartDate = new DateTime(2020, 06, 01);
                dummyTerm.EndDate = new DateTime(2020, 12, 30);
                await _conn.InsertAsync(dummyTerm);
                termList.Add(dummyTerm);

                var dummyCourse = new Course();
                dummyCourse.CourseName = "Mobile Application";
                dummyCourse.StartDate = new DateTime(2020, 06, 01);
                dummyCourse.StartDate = new DateTime(2020, 06, 30);
                dummyCourse.Status = "In-Progress";
                dummyCourse.InstructorName = "Spencer Swift";
                dummyCourse.InstructorPhone = "503-931-3919";
                dummyCourse.InstructorEmail = "sswift8@wgu.edu";
                dummyCourse.NotificationEnabled = 1;
                dummyCourse.Notes = "This class is awesome!";
                dummyCourse.Term = dummyTerm.Id;
                await _conn.InsertAsync(dummyCourse);

                var dummyObjectiveAssessment = new Assessment();
                dummyObjectiveAssessment.Title = "Test Assessment 1";
                dummyObjectiveAssessment.StartDate = new DateTime(2020, 06, 01);
                dummyObjectiveAssessment.EndDate = new DateTime(2020, 06, 30);
                dummyObjectiveAssessment.Course = dummyCourse.Id;
                dummyObjectiveAssessment.Type = "Objective";
                dummyObjectiveAssessment.NotificationEnabled = 1;
                await _conn.InsertAsync(dummyObjectiveAssessment);

                var dummyPerformanceAssessment = new Assessment();
                dummyPerformanceAssessment.Title = "Test Assessment 2";
                dummyPerformanceAssessment.StartDate = new DateTime(2020, 06, 01);
                dummyPerformanceAssessment.EndDate = new DateTime(2020, 06, 30);
                dummyPerformanceAssessment.Course = dummyCourse.Id;
                dummyPerformanceAssessment.Type = "Performance";
                dummyPerformanceAssessment.NotificationEnabled = 1;
                await _conn.InsertAsync(dummyPerformanceAssessment);
            }

            //notification handling
            if(pushNotification == true)
            {
                pushNotification = false;
                int courseId = 0;
                foreach (Course course in courseList)
                {
                    courseId++;
                    if(course.NotificationEnabled == 1)
                    {
                        if (course.StartDate == DateTime.Today)
                            CrossLocalNotifications.Current.Show("Reminder", $"{course.CourseName} begins today!", courseId);
                        if (course.EndDate == DateTime.Today)
                            CrossLocalNotifications.Current.Show("Reminder", $"{course.CourseName} ends today!", courseId);
                    }
                }

                int assessmentId = courseId;
                foreach(Assessment assessment in assessmentList)
                {
                    assessmentId++;
                    if(assessment.NotificationEnabled == 1)
                    {
                        if (assessment.StartDate == DateTime.Today)
                            CrossLocalNotifications.Current.Show("Reminder", $"{assessment.Title} begins today!", assessmentId);
                        if (assessment.EndDate == DateTime.Today)
                            CrossLocalNotifications.Current.Show("Reminder", $"{assessment.Title} ends today!", assessmentId);
                    }
                }
            }

            _termList = new ObservableCollection<Term>(termList);
            termListView.ItemsSource = _termList;
            base.OnAppearing();
        }
        private async void OnButtonClick(object sender, EventArgs e)
        {
            
            await Navigation.PushModalAsync(new AddTerm(this));
        }

        async private void Term_Click(object sender, ItemTappedEventArgs e)
        {
            Term term = (Term)e.Item;
            await Navigation.PushModalAsync(new TermDetail(term));
        }
    }
}
