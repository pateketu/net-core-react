
import Game from './game';
import { Promise } from 'q';
import { GameResult } from './gameResult';

export async function start() {
    return Promise<Game>((resolve) => {
        fetch('http://localhost:55999/api/game')
            .then((response) => response.json())
            .then((game: Game) => {
                resolve(game);
            });
    });
}

export async function answer(id: string, ans: number) {
    return Promise<GameResult>((resolve) => {
        fetch(new Request(`http://localhost:55999/api/game/${id}/${ans}`, { method: 'post' }))
             .then((response) => response.json())
             .then((gameResult: GameResult) => {
                resolve(gameResult);
             });
    });
}
