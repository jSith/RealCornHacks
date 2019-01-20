import React, { Component } from "react";
import { Link } from "react-router-dom";

class Homepage extends Component {
    render() {
        return (
            <div>
                <h1>Sam put your code in here bb.</h1>
                Click here to <Link to="/subscribe/">Subscribe</Link>
            </div>
        );
    }
}

export default Homepage;