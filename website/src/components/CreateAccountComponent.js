import React, { Component } from 'react';
import { Button } from 'reactstrap';

import Credentials from './CredentialsComponent';
import Survey from './SurveyComponent';
import { Questions, Sizes, isBeginner as BeginVals } from '../data/SurveyQuestions';

class CreateAccount extends Component {
    constructor(props) {
        super(props);

        this.state = {
            survey: {}
        };

        this.onInputChange = this.onInputChange.bind(this);
        this.onSubscribe = this.onSubscribe.bind(this);
    }

    onInputChange(question, optionName, radio = false) {
        this.setState(state => {
            let previousValue = false;

            if (state.survey[question] === undefined || radio) {
                state.survey[question] = {};
            } else if (state.survey[question][optionName]) {
                previousValue = true;
            }

            state.survey[question][optionName] = !previousValue;

            return state;
        });
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

        const sizes = getMultiselect(Questions.size).map(resp => 
            Sizes.findIndex(val => val === resp) + 1);

        const beginResp = getMultiselect(Questions.beginner)[0];
        let isBeginner = undefined;
        if (beginResp === BeginVals.true) {
            isBeginner = true;
        } else if (beginResp === BeginVals.false) { 
            isBeginner = false;
        }

        const preference = {
            topics,
            languages,
            sizes,
            isBeginner
        };

        alert(`Thank you for subscribing! Check your email for your first newsletter!\n${JSON.stringify(preference)}`);
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