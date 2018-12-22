import React, { Component } from 'react';
import './App.css';
import Answer from './components/Answer';
import Timer from './components/Timer';
import Game from './domain/game';
import * as gameService from './domain/gameService';

interface IAppState {
  expired: boolean;
  game?: Game;
}

class App extends Component<any, IAppState> {

  constructor(props: any) {
    super(props);
    this.state = { expired: false};
  }

  public async componentDidMount() {
    const game = await gameService.start();
    this.setState({game});
  }

  public render() {
    if (!this.state.game) {
      return <React.Fragment>
              {this.header()}
              <div>Loading...</div>
          </React.Fragment>;
    }
    return (
      <div className="App">
        {this.header()}
        {!this.state.expired  ?
              this.equation(this.state.game)
            : this.reStartButton()
        }
      </div>
    );
  }

  private header() {
    return  <h1>Add It Up!</h1>;
  }
  private equation(game: Game) {
    return <Timer seconds={game.timeFrameSeconds}
                  onExpired={this.onExpired}>
          <b>Your current level is {game.level}</b>
          <Answer a={game.a} b={game.b} onAnswer={(answer: number) => {
            // Post the answer to
          }}> </Answer>

      </Timer>;
  }

  private reStartButton() {
    return <div><button onClick={() => this.setState({expired: false})}>Re-Start</button></div>;
  }

  private onExpired = () => {
    this.setState({expired: true});
  }
}

export default App;
