# Course Planner
 


INTRODUCTION As a competent mobile application developer, your understanding of mobile application structure and design will help you to develop applications to meet customer requirements.

For this task, you will develop a multiple-screen mobile application for WGU students to track their academic terms, courses associated with each term, and assessments associated with each course. The application will allow students to enter, edit, and delete term, course, and assessment data. It should provide summary and detailed views of courses for each term, provide notifications for upcoming performance and objective assessments, and notify students of deadlines and tracking milestones even after the application is closed. You will create a wireframe as a visual guide, representing the skeletal framework of the application. Your application will use an SQLite database.

This task will allow you to demonstrate your ability to apply the skills learned in the course and help you to apply these skills in a familiar, real-world scenario.

REQUIREMENTS Note: The assessment must be submitted using the Xamarin.Forms framework.

For this assessment, you are welcome to use these plugins:

https://docs.microsoft.com/en-us/xamarin/essentials/

https://github.com/edsnider/LocalNotificationsPlugin

https://docs.microsoft.com/en-us/dotnet/api/Xamarin.Forms.Picker?view=xamarin-forms

https://docs.microsoft.com/en-us/dotnet/api/Xamarin.Forms.DatePicker?view=xamarin-forms

All other external plugins and libraries are not allowed in the project.

The submission needs to include a zip file with the project file and folder structure intact for the Visual Studio IDE.

Your submission must be your original work. No more than a combined total of 30% of a submission can be directly quoted or closely paraphrased from sources, even if cited correctly. Use the report provided when submitting your task as a guide.

You must use the rubric to direct the creation of your submission because it provides detailed criteria that will be used to evaluate your work. Each requirement below may be evaluated by more than one rubric aspect. The rubric aspect titles may contain hyperlinks to relevant portions of the course.

A. Draw a low-fidelity wireframe for your mobile application, that includes all of the following requirements:

Note: This assessment requires you to submit pictures, graphics, and/or diagrams. Each file must be an attachment no larger than 30 MB in size. Diagrams must be original and may be hand-drawn or drawn using a graphics program. Do not use CAD programs because attachments will be too large.

    As many academic terms as needed; each term should include the addition, editing, deletion, and storage of all the following:

• the term title

• start and anticipated end dates (use a DatePicker)

    Add six courses for each term. For each course, include the addition, editing, deletion, and storage of all the following course information:

• course name

• start and anticipated end dates (use a DatePicker)

• course status (use a Picker)

• course instructor’s information: name, phone, email

• add, share, and display optional notes

• set notifications for the start and end dates of each course

• display of a detailed view of each course, including the due date

• editing of the detailed course view screen

    Add two assessments for each course; each assessment should include the addition, editing, deletion, and storage of all the following assessment information:

• one objective assessment

• one performance assessment

• name of both assessments

• set notifications for the start and end dates of each assessment

B. Create within the Xamarin.Forms framework, a mobile application aligned to the wireframe drawn in part A, and include all of these features:

    Provide an interface for all the following information for as many academic terms as needed:

• academic term title (e.g., Term 1, Term 2, Spring term)

• start and end dates (use a DatePicker)

                   You are welcome to use the following plugin for this step:

                    https://docs.microsoft.com/en-us/dotnet/api/Xamarin.Forms.DatePicker?view=xamarin-forms

    Provide an interface that allows the user to access all the following features for each academic term:

• add and display a list of six courses for each term

• display a detailed view of each term, including all the information from part B1

    Provide the interface that allows the user to access and edit all the following details for each course:

• course title

• start and anticipated end dates (use a DatePicker)

  You are welcome to use the following plugin for this step:

                     https://docs.microsoft.com/en-us/dotnet/api/Xamarin.Forms.DatePicker?view=xamarin-forms

• course status (e.g., in progress, completed, dropped, plan to take) (use a Picker)

                   You are welcome to use the following plugin for this step:

                     https://docs.microsoft.com/en-us/dotnet/api/Xamarin.Forms.Picker?view=xamarin-forms

• the course instructor’s name, phone number, and email address

    include validation to prevent the user from saving a null value (e.g., an invalid email address)

    Create features that allow the user to do all the following for each course:

• add two assessments: one performance assessment and one objective assessment

• add and display optional notes

• enter, edit, and delete course information

• display an editable detailed view of the course, including the due date

• set alerts (e.g., notifications) for the start and end date of the course

  You are welcome to use the following plugin for this step:

   https://github.com/edsnider/LocalNotificationsPlugin

• share notes via a sharing feature (e.g., email, SMS)

 You are welcome to use the following plugins for this step:

                    https://docs.microsoft.com/en-us/xamarin/essentials/

    Provide an interface for the user to do all the following for each assessment:

• include the names and due dates

• enter, edit, and delete assessment information

• set notifications for anticipated due dates

 You are welcome to use the following plugin for this step:

  https://github.com/edsnider/LocalNotificationsPlugin

    Write code to create a set of data for evaluation purposes, including one term and one course from part B3, and include the two assessments from part B4 for that course. Include your own name, phone number, and email address as the course instructor for the course.

C. Examine the wireframe from part A, to determine any changes that you made during the development of the mobile application.

    Explain the reasons for any changes made during the development of the mobile application, by adding comments within the wireframe. If no changes were made, include that comment.

    Confirm that, after the inclusion of the changes made during development, the wireframe aligns with the mobile application.

D. Provide a complete and operational source file for your mobile application.

E. Acknowledge sources, using APA-formatted in-text citations and references, for content that is quoted, paraphrased, or summarized.

F. Demonstrate professional communication in the content and presentation of your submission.
