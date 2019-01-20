import React, { Component } from 'react';
import { Button } from 'reactstrap';

import Credentials from './CredentialsComponent';
import Survey from './SurveyComponent';
import Questions from '../data/SurveyQuestions';

class CreateAccount extends Component {
    constructor(props) {
        super(props);

        this.state = {
            survey: {}
        };

        this.onInputChange = this.onInputChange.bind(this);
        this.onSubscribe = this.onSubscribe.bind(this);
    }

    onInputChange(question, optionName) {
        this.setState(state => {
            let previousValue = false;

            if (state.survey[question] === undefined) {
                state.survey[question] = {};
            } else if (state.survey[question][optionName]) {
                previousValue = true;
            }

            state.survey[question][optionName] = !previousValue;

            return state;
        });

        console.log(this.state);
    }

    onSubscribe() {
        const getMultiselect = (question) => {
            let result = [];
            const ref = this.state.survey[question];

            if (ref !== undefined) {
                result = Object.keys(ref).filter(key => ref[key]);
            }

            return result;
        };

        const topics = getMultiselect(Questions.topics);
        const languages = getMultiselect(Questions.languages);
        const sizes = getMultiselect(Questions.size);

        alert("Thank you for subscribing! Check your email for your first newsletter!"
         + `\nTopics: ${topics}\nLanguages: ${languages}\nSizes: ${sizes}`
        );
    }

    render() {
        return (
            <div id="createAccount">
                <h1 id="createHeader">Explore new repositories</h1>
                <Credentials />
                <Survey onInputChange={this.onInputChange}/>
                <Button id="subscribeButton" color="primary" size = 'lg' onClick={this.onSubscribe}>Subscribe</Button>
            </div>
        )
    }
}

export default CreateAccount;