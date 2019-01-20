import React, { Component } from 'react';
import './App.css';
import CreateAccount from './components/CreateAccountComponent';
import { BrowserRouter as Router, Route, Link } from "react-router-dom";

// class App extends Component {
//   render() {
//     return (
//       <div className="App">
//         <CreateAccount />
//       </div>
//     );
//   }
// }

const Home = () => <div>Click here to <Link to="/subscribe/">Subscribe</Link></div>;
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
