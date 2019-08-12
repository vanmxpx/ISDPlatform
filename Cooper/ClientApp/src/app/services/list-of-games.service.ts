import { Injectable } from '@angular/core';
import { Game } from '../models/my-page-models';

@Injectable({
  providedIn: 'root'
})
export class ListOfGamesService {
  games: Game[] = [
    {
      name: 'tanks',
      logo: 'assets/imageKeeper/WebTanks.png'
    },
    {
      name: 'fortnite',
      logo: 'https://cdn2.unrealengine.com/Fortnite%2FBattle-pass%2FSeason-7%2Fseason7_plane-2024x1139-a974df2b274a4254b43387ef34ab40c1b42250a9.jpg'
    }, 
    {
      name: 'Mafia 2',
      logo: 'https://upload.wikimedia.org/wikipedia/ru/thumb/d/d6/MafiaII.jpg/270px-MafiaII.jpg'
    },
    {
      name: 'tanks',
      logo: 'assets/imageKeeper/WebTanks.png'
    },
    {
      name: 'fortnite',
      logo: 'https://cdn2.unrealengine.com/Fortnite%2FBattle-pass%2FSeason-7%2Fseason7_plane-2024x1139-a974df2b274a4254b43387ef34ab40c1b42250a9.jpg'
    }, 
    {
      name: 'Mafia 2',
      logo: 'https://upload.wikimedia.org/wikipedia/ru/thumb/d/d6/MafiaII.jpg/270px-MafiaII.jpg'
    },
    {
      name: 'tanks',
      logo: 'assets/imageKeeper/WebTanks.png'
    },
    {
      name: 'fortnite',
      logo: 'https://cdn2.unrealengine.com/Fortnite%2FBattle-pass%2FSeason-7%2Fseason7_plane-2024x1139-a974df2b274a4254b43387ef34ab40c1b42250a9.jpg'
    }, 
    {
      name: 'Mafia 2',
      logo: 'https://upload.wikimedia.org/wikipedia/ru/thumb/d/d6/MafiaII.jpg/270px-MafiaII.jpg'
    }
  ]

  getData(): Game[] {        
    return this.games;
  }

  constructor() { }
  
}
