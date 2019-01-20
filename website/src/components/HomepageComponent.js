import React, { Component } from "react";
import { Button } from 'reactstrap';
import { Link } from "react-router-dom";
import './homepage.css';

class Homepage extends Component {
    render() {
        return (
        <div>
            <div id="header">
                <div id="headerStatement"><strong>Search less. Code more.</strong></div>
                <ul id = "benefitList">
                    <li>Build your resume</li>
                    <li>Collaborate with others</li>
                    <li>Help develop the world</li>
                </ul>
            </div>

            <Link to="/subscribe/"><Button id="subscribeHomepage" color="primary" size = 'lg'>Subscribe</Button></Link>

            <div id="businessTextBox">
                Need help attracting developers to your open source repository? 
                Consider becoming one of our valued sponsors and gain increased publicity through our weekly newsletter. 
                For more information, contact us at <a href="mailto: email@address.com">email@address.com</a>.
            </div>

            <img id="emailImage" src="https://conversionmarketing.org/wp-content/uploads/2018/04/Email-Campaign.png" ></img>
        </div>
        );
    }
}

export default Homepage;