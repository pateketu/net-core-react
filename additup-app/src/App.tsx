import React, { Component } from 'react';
import './App.css';
import Answer from './components/Answer';
import Timer from './components/Timer';
interface IAppState {
  expired: boolean;
}
class App extends Component<any, IAppState> {

  constructor(props: any) {
    super(props);
    this.state = {expired: false};
  }
  public render() {
    return (
      <div className="App">
        <h1>Add It Up!</h1>
        {!this.state.expired  ?
          <Timer seconds={20} onExpired={this.onExpired}>
            <Answer a={1} b={2} onAnswer={(answer: number) => {
                // tslint:disable-next-line:no-console
                console.log(answer);
            }}> </Answer>
          </Timer> : <div>Restart</div>
        }
      </div>
    );
  }

  private onExpired = () => {
    this.setState({expired: true});
  }
}

export default App;
