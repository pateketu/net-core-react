
import Game from './game';
import { Promise } from 'q';

export async function start() {
    return Promise<Game>((resolve) => {
        resolve(new Game());
    });
}
