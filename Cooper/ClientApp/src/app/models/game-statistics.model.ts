import { User } from './user';
import { Game } from './game-model';

export class GameStatistics {
    public user: User;
    public game: Game;
    public dateOfLastGame: number;
    public winGames: number;
    public loseGames: number;
    public bestScore: number;
}
