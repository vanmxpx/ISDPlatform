import { Component, Inject } from '@angular/core';
import { Http, Headers } from '@angular/http';
import { Router, ActivatedRoute } from '@angular/router';
import { GameService } from '../../services/gameservice.service'
@Component({
  selector: 'fetchgame',
  templateUrl: './fetchgame.component.html'
})
export class FetchGameComponent {
  public gameList: GameData[];
  constructor(public http: Http, private _router: Router, private _gameService: GameService) {
    this.getGames();
  }
  getGames() {
    this._gameService.getGames().subscribe(
      data => this.gameList = data
    )
  }
  //delete(gameID) {
  //  var ans = confirm("Do you want to delete customer with Id: " + gameID);
  //  if (ans) {
  //    this._gameService.deleteGame(gameID).subscribe((data) => {
  //      this.getGames();
  //    }, error => console.error(error))
  //  }
  //}
}
interface GameData {
  id: number;
  name: string;
}
