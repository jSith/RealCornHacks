import React, { Component } from 'react';
import { CustomInput, Col, Row, Container } from 'reactstrap';
import "./components.css";

// Expects props question, options, and numCols

class SurveyQuestion extends Component {
    constructor(props) {
        super(props);

        this.organizeColumns = this.organizeColumns.bind(this);
    }

    organizeColumns() {
        const inputType = this.props.radio ? "radio" : "checkbox";

        const checkboxes = this.props.options.map(name => 
            <Col className="option" key={"col" + name}>
                <CustomInput 
                    type={inputType} 
                    id={name} 
                    name={this.props.question} 
                    key={"box" + name} 
                    label={name}
                    onChange={() => this.props.onChange(this.props.question, name)}
                />
            </Col>
        );

        const numCols = Number(this.props.numCols);

        const missingCols = checkboxes.length % numCols;

        for (let i = 0; i < missingCols; i++) {
            checkboxes.push(<Col key={i}></Col>);
        }

        const rows = [];

        for (let i = 0; i <= checkboxes.length - (checkboxes.length % numCols); i += numCols) {
            rows.push(<Row key={i}>{checkboxes.slice(i, i + numCols)}</Row>);
        }

        return rows;
    }

    render() {
        const rows = this.organizeColumns();
        return (
            <div className="survey-question">
                <h5>{this.props.question}</h5>
                <Container>
                    {rows}
                </Container>
            </div>
        );
    }
}

export default SurveyQuestion;