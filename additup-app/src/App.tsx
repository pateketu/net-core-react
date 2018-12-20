import React, { Component } from 'react';
import './App.css';
import Answer from './components/Answer';

class App extends Component {
  public render() {
    return (
      <div className="App">
        <h1>Add It Up!</h1>

        <Answer a={1} b={2} onAnswer={(answer: number) => {
            // tslint:disable-next-line:no-console
            console.log(answer);
        }}> </Answer>
      </div>
    );
  }
}

export default App;
