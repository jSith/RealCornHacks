import React, { Component } from 'react';
import { Link } from 'react-router-dom';

class Unsubscribe extends Component {
    render() {
        return (
            <div id="unsubscribe">
                <h2>You're unsubscribed from CodeCrowd</h2>
                <p>Return to the <Link to="/">home page.</Link></p>
            </div>
        );
    }
}

export default Unsubscribe;