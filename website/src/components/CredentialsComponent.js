import React, { Component } from 'react';
import { Form, Input, FormGroup, Label } from 'reactstrap';
import './components.css';

class Credentials extends Component {
    render() {
        return (
            <div className="credentials">
                <Form>
                    <FormGroup id="emailGroup">
                        <Label for="email" className="credentials-label"><strong>Email</strong></Label>
                        <Input type="email" className="credentials-input" name="email" id="email" placeholder="example@email.com" />
                    </FormGroup>
                    
                    <FormGroup id="passwordGroup">
                        <Label for= "password" className="credentials-label"><strong>Password</strong></Label>
                        <Input type="password" className="credentials-input" name="password" id="password" />
                    </FormGroup>
                    <FormGroup>
                        <Label for="confirmPassword" className="credentials-label"><strong>Confirm password</strong></Label>
                        <Input type="password" className="credentials-input" name="confirmPassword" id="confirmPassword" />
                    </FormGroup>
                </Form>
            </div>
        );
    }
}

export default Credentials;
