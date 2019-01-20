import React, { Component } from 'react';
import { Button } from 'reactstrap';
import Credentials from './CredentialsComponent.js';
import Survey from './SurveyComponent.js';

class CreateAccount extends Component {
    render() {
        return (
            <div id="createAccount">
                <h1 id="createHeader">Explore new repositories</h1>
                <Credentials />
                <Survey />
                <Button id="subscribeButton" color="primary" size = 'lg' onClick={() => 
                    alert("Thank you for subscribing! Check your email for your first newsletter!")
                }>Subscribe</Button>
            </div>
        )
    }
}

export default CreateAccount;