import React from 'react';
import './App.css';
import { BrowserRouter as Router, Route } from "react-router-dom";

import Homepage from './components/HomepageComponent';
import CreateAccount from './components/CreateAccountComponent';

const Home = () => <Homepage />;
const Subscribe = () => <CreateAccount />;

const AppRouter = () => (
    <Router>
        <div>
            <Route exact path="/" component={Home} />
            <Route path="/subscribe/" component={Subscribe} />
        </div>
    </Router>
);

export default AppRouter;
