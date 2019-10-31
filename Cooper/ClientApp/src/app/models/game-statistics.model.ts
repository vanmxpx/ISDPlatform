import { User } from './user';
import { Game } from './game-model';

export class GameStatistics {
    public User: User;
    public Game: Game;
    public DateOfLastGame: number;
    public WinGames: number;
    public LoseGames: number;
    public BestScore: number;
}
