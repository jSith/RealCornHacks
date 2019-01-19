import React, { Component } from 'react';
import Credentials from './CredentialsComponent.js';

class CreateAccount extends Component {
    render() {
        return (
            <div>
                <h1 id="createHeader">Explore new repositories</h1>
                <Credentials />
            </div>
        )
    }
}

export default CreateAccount;