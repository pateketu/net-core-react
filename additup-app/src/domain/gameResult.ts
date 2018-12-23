import Game from './game';

export class GameResult {
    public inCorrectAnswer: boolean = false;
    public game: Game | undefined;
    public allLevelsFinished: boolean = false;
}
