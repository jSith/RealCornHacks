import React, { Component } from 'react';
import SurveyQuestion from './SurveyQuestionComponent';

const topics = ["Web Development", "Back End", "Machine Learning", "Game Engines", "Mobile Development", "Operating Systems", "Cybersecurity", "Testing", "DevOps", "Virtual Reality", "Data Science", "Databases", "APIs", "Web Frameworks"];
const languages = ["Java", "C", "C#", "C++", "R", "Python", "PHP", "JavaScript", "CSS", "HTML", "Visual Basic"];
const sizes = ["Less than 10", "10 to 100", "Over 100"];
const beginnerResponses = ["Yes, only show me good first issues.", "No, show me all relevant issues."];


class Survey extends Component {
    render() {
        return (
            <div className="survey">
                <SurveyQuestion question = "What topics do you want to explore?" options={topics} numCols="3"/>
                <SurveyQuestion question = "What languages do you want to use?" options={languages} numCols="3"/>
                <SurveyQuestion question = "How many contributors are you looking to work with?" options={sizes} numCols="3"/>
                <SurveyQuestion question = "Are you new to open source dvelopment?" options={beginnerResponses} numCols="2"/>
            </div>
        )
    }
}

export default Survey;