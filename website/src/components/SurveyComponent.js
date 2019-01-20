import React, { Component } from 'react';
import * as axios from 'axios';
import SurveyQuestion from './SurveyQuestionComponent';
import Questions from '../data/SurveyQuestions';

const TOPICS = ["Web Development", "Back End", "Machine Learning", "Game Engines", "Mobile Development", "Operating Systems", "Cybersecurity", "Testing", "DevOps", "Virtual Reality", "Data Science", "Databases", "APIs", "Web Frameworks"];
const LANGUAGES = ["Java", "C", "C#", "C++", "R", "Python", "PHP", "JavaScript", "CSS", "HTML", "Visual Basic"];
const SIZES = ["Less than 10", "10 to 100", "Over 100"];
const BEGINNER_RESPONSES = ["Yes, only show me good first issues.", "No, show me all relevant issues."];


class Survey extends Component {
    constructor(props) {
        super(props);

        this.state = {
            isLoaded: false,
            topics: [],
            languages: []
        };

        // Fallback values in case we have a backend error
        let topics = TOPICS;
        let languages = LANGUAGES;

        axios.get(`${process.env.REACT_APP_SERVICE_URL}/api/preferences`).then(data => {
            if (data.Topics !== undefined) {
                topics = data.Topics;
            }
            if (data.Lanuages !== undefined) {
                languages = data.Languages;
            }
        }).catch(error => {
            console.log(error);
        }).then(() => {
            this.setState({
                isLoaded: true,
                topics,
                languages
            });
        });
    }

    render() {
        return this.state.isLoaded ? (
            <div className="survey">
                <SurveyQuestion onChange={this.props.onInputChange} key="1" question={Questions.topics} options={this.state.topics} numCols="3"/>
                <SurveyQuestion onChange={this.props.onInputChange} key="2" question={Questions.languages} options={this.state.languages} numCols="3"/>
                <SurveyQuestion onChange={this.props.onInputChange} key="3" question={Questions.size} options={SIZES} numCols="3"/>
                <SurveyQuestion onChange={this.props.onInputChange} key="4" question={Questions.beginner} options={BEGINNER_RESPONSES} numCols="2" radio />
            </div>
        ) : <div></div>;
    }
}

export default Survey;