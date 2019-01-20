import React, { Component } from 'react';
import * as axios from 'axios';
import SurveyQuestion from './SurveyQuestionComponent';
import { Questions, Sizes, isBeginner } from '../data/SurveyQuestions';

// Fallback values in case we have a backend error
const TOPICS = ["Web Development", "Back End", "Machine Learning", "Game Engines", "Mobile Development", "Operating Systems", "Cybersecurity", "Testing", "DevOps", "Virtual Reality", "Data Science", "Databases", "APIs", "Web Frameworks"];
const LANGUAGES = ["Java", "C", "C#", "C++", "R", "Python", "PHP", "JavaScript", "CSS", "HTML", "Visual Basic"];

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

        axios.get(`${process.env.REACT_APP_SERVICE_URL}/api/preferences`).then(payload => {
            const data = payload.data;
            console.log(data);

            if (data.topics !== undefined) {
                topics = data.topics;
            }
            if (data.languages !== undefined) {
                languages = data.languages;
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
                <SurveyQuestion onChange={this.props.onInputChange} 
                    key="1" 
                    question={Questions.topics} 
                    options={this.state.topics} 
                    numCols="3"/>
                <SurveyQuestion onChange={this.props.onInputChange} 
                    key="2" 
                    question={Questions.languages} 
                    options={this.state.languages} 
                    numCols="3"/>
                <SurveyQuestion onChange={this.props.onInputChange} 
                    key="3" 
                    question={Questions.size} 
                    options={Sizes} 
                    numCols="3"/>
                <SurveyQuestion onChange={(a, b) => this.props.onInputChange(a, b, true)} 
                    key="4" 
                    question={Questions.beginner} 
                    options={[isBeginner.true, isBeginner.false]} 
                    numCols="2" 
                    radio />
            </div>
        ) : <div></div>;
    }
}

export default Survey;