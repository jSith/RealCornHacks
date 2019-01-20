import React from 'react';
import './App.css';
import { BrowserRouter as Router, Route } from "react-router-dom";

import Homepage from './components/HomepageComponent';
import CreateAccount from './components/CreateAccountComponent';
import Unsub from './components/UnsubscribeComponent';

const Home = () => <Homepage />;
const Subscribe = () => <CreateAccount />;
const Unsubscribe = () => <Unsub />;

const AppRouter = () => (
    <Router>
        <div>
            <Route exact path="/" component={Home} />
            <Route path="/subscribe/" component={Subscribe} />
            <Route path="/unsubscribe/" component={Unsubscribe} />
        </div>
    </Router>
);

export default AppRouter;
