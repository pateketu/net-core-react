
import Game from './game';
import { Promise } from 'q';
import { GameResult } from './gameResult';

export async function start() {
    return Promise<Game>((resolve) => {
        resolve(new Game());
    });
}

export async function answer(ans: number) {
    return Promise<GameResult>((resolve) => {
        resolve(new GameResult());
    });
}
