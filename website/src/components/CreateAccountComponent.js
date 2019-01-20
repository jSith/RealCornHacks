import React, { Component } from 'react';
import { Button } from 'reactstrap';

import Credentials from './CredentialsComponent';
import Survey from './SurveyComponent';
import { Questions, Sizes, isBeginner as BeginVals } from '../data/SurveyQuestions';
import CredentialFields from '../data/CredentialFields';

class CreateAccount extends Component {
    constructor(props) {
        super(props);

        this.state = {
            survey: {},
            emailValid: true,
            passValid: true
        };

        this.onCredentialInputChange = this.onCredentialInputChange.bind(this);
        this.onSurveyInputChange = this.onSurveyInputChange.bind(this);
        this.getPreferences = this.getPreferences.bind(this);
        this.onSubscribe = this.onSubscribe.bind(this);
    }

    onCredentialInputChange(field, value) {
        this.setState({ [field]: value });

        if (field === CredentialFields.email) {
            this.setState({ emailValid: true });
        }
    }

    onSurveyInputChange(question, optionName, radio = false) {
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

    getPreferences() {
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

        return {
            topics,
            languages,
            sizes,
            isBeginner
        };
    }

    onSubscribe() {
        const email = this.state.email;
        const password = this.state.password;
        const preference = this.getPreferences();

        if (email === undefined) {
            alert("Please enter a valid email address.");
            this.setState({ emailValid: false });
        } else if (password === undefined) {
            alert("Please enter a password.");
            this.setState({ passValid: false });
        } else if (password !== this.state.confirmPassword) {
            alert("Passwords do not match.");
            this.setState({ passValid: false });
        } else {
            this.setState({ emailValid: true, passValid: true });
            alert(`Thank you for subscribing! Check your email for your first newsletter!\n${JSON.stringify(preference)}\n${email}|${password}`);
        }
    }

    render() {
        return (
            <div id="createAccount">
                <h1 id="createHeader">Explore new repositories</h1>
                <Credentials onInputChange={this.onCredentialInputChange} emailValid={this.state.emailValid} passValid={this.state.passValid}/>
                <Survey onInputChange={this.onSurveyInputChange}/>
                <Button id="subscribeButton" color="primary" size = 'lg' onClick={this.onSubscribe}>Subscribe</Button>
            </div>
        )
    }
}

export default CreateAccount;