import React, { Component } from 'react';
import './App.css';
import Answer from './components/Answer';
import Timer from './components/Timer';
import Game from './domain/game';
import * as gameService from './domain/gameService';

interface IAppState {
  expired: boolean;
  finished: boolean;
  inCorrect: boolean;
  gameLoaded: boolean;
  game?: Game;
}

class App extends Component<any, IAppState> {

  constructor(props: any) {
    super(props);
    this.state = {
      expired: false,
      finished: false,
      gameLoaded: false,
      inCorrect: false,
    };
  }

  public async componentDidMount() {
   const game = await gameService.start();
   this.setState({game, gameLoaded: true});
  }

  public render() {
    return (
      <div className="App">
        {this.header()}
        {this.loading()
            || this.expired()
            || this.equation(this.state.game)}
      </div>
    );
  }

  private header() {
    return  <h1>Add It Up!</h1>;
  }

  private loading() {
      return !this.state.gameLoaded && <div>Loading ...</div>;
  }

  private expired() {
    return this.state.expired && this.reStartButton();
  }

  private equation(game: Game | undefined) {

    return game && <Timer seconds={game.timeFrameSeconds}
                  onExpired={this.onExpired}>
          <b>Your current level is {game.level}</b>
          <Answer a={game.a} b={game.b} onAnswer={this.onAnswer}> </Answer>

      </Timer>;

  }

  private reStartButton() {
    return <div><button onClick={() => this.setState({expired: false})}>Re-Start</button></div>;
  }

  private onExpired = () => {
    this.setState({expired: true});
  }

  private onAnswer = async (answer: number) => {

    this.setState({gameLoaded: false});

    const result = await gameService.answer(answer);

    if (result.allLevelsFinished) {
        this.setState({finished: true});
      } else if (result.inCorrectAnswer) {
        this.setState({inCorrect: true});
      } else if (result.game) {
        this.setState({game: result.game});
      }
  }
}

export default App;
