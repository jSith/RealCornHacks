# CodeCrowd
Codehacks 2019

January 19-20

Authors: Joe Cowman, Sam Futterman, John Harkendorff, and Jessica Smith


CodeCrowd is an email notification software to help pair programmers with open-source repositories that meet their skill-level and interests.

By signing up with their email, individuals can choose their proficient languages, decide their technical topics of interest, tell their
overall experience with open-source software, and choose the size of team they like to work with.

All of these factors are taken into consideration to introduce software developers to repositories that perfectly match their interest.

Each week, an automated email is sent to the user with a list of 5 of the best-matching repositories that we believe the user will
most enjoy working on.

# Tech Stack
CodeCrowd is implemented in .NET Core version 2.2. The user interface is written with an HTML/CSS/ReactJS stack before AJAX calls are sent
back and forth between the C# back-end to transfer data. Information from Github is obtained with the Github API found at
https://api.github.com/.

CodeCrowd is not intended to be a one-time message sent, but is meant to be consistently used to introduce developers to new and trending
open-source repositories that can change as a developer's interests and skills change. In order to do this, user data, including their email,
password, and preferences are saved in a MySQL database to be sent week after week. Emails are built through the MailMessage class and 
sent via an SmtpClient.

# How to Run
Inside Visual Studio, select Cornhacks 2019 as the project solution and click IIS Express. 
