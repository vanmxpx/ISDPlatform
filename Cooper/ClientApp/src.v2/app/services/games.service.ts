import { Injectable } from '@angular/core';
import { Game } from '@models';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class GamesService {

  private static readonly getGamesUrl = "/games";

  getData(): Observable<Game[]> {   
    return this.httpClient.get<Game[]>(GamesService.getGamesUrl);
  }

  getGame(name: string): Observable<Game> {
    const url = GamesService.getGamesUrl + "/" + name;
    return this.httpClient.get<Game>(url);
  }

  constructor(private httpClient: HttpClient) { }

  mockedGames: Game[] = [
    {
      name: 'tanks',
      logoUrl: 'assets/imageKeeper/WebTanks.png',
      link: 'tanks',
      description:'The Biggest War EverThe Biggest War EveThe Biggest War EveThe Biggest War EveEveThe Biggest War EveEveThe Biggest War EveEveThe Biggest War EveEveThe Biggest War EveEveThe Biggest War EveEveThe Biggest War EveEveThe Biggest War Eve EveThe Biggest War EveThe Biggest War EveThe Biggest War EveThe Biggest War Eve',
      genre: 'War'
    },
    {
      name: 'fortnite',
      logoUrl: 'https://cdn2.unrealengine.com/Fortnite%2FBattle-pass%2FSeason-7%2Fseason7_plane-2024x1139-a974df2b274a4254b43387ef34ab40c1b42250a9.jpg',
      link: 'fortnite',
      description:'The Biggest War Ever',
      genre: 'War'
    }, 
    {
      name: 'Mafia 2',
      logoUrl: 'https://upload.wikimedia.org/wikipedia/ru/thumb/d/d6/MafiaII.jpg/270px-MafiaII.jpg',
      link: 'Mafia2',
      description:'The Biggest War Ever',
      genre: 'War'
    },
    {
      name: 'tanks',
      logoUrl: 'assets/imageKeeper/WebTanks.png',
      link: 'tanks',
      description:'The Biggest War EverThe Biggest War EveThe Biggest War EveThe Biggest War EveEveThe Biggest War EveEveThe Biggest War EveEveThe Biggest War EveEveThe Biggest War EveEveThe Biggest War EveEveThe Biggest War EveEveThe Biggest War Eve EveThe Biggest War EveThe Biggest War EveThe Biggest War EveThe Biggest War Eve',
      genre: 'War'
    },
    {
      name: 'fortnite',
      logoUrl: 'https://cdn2.unrealengine.com/Fortnite%2FBattle-pass%2FSeason-7%2Fseason7_plane-2024x1139-a974df2b274a4254b43387ef34ab40c1b42250a9.jpg',
      link: 'fortnite',
      description:'The Biggest War Ever',
      genre: 'War'
    }, 
    {
      name: 'Mafia 2',
      logoUrl: 'https://upload.wikimedia.org/wikipedia/ru/thumb/d/d6/MafiaII.jpg/270px-MafiaII.jpg',
      link: 'Mafia2',
      description:'The Biggest War Ever',
      genre: 'War'
    },
    {
      name: 'tanks',
      logoUrl: 'assets/imageKeeper/WebTanks.png',
      link: 'tanks',
      description:'The Biggest War EverThe Biggest War EveThe Biggest War EveThe Biggest War EveEveThe Biggest War EveEveThe Biggest War EveEveThe Biggest War EveEveThe Biggest War EveEveThe Biggest War EveEveThe Biggest War EveEveThe Biggest War Eve EveThe Biggest War EveThe Biggest War EveThe Biggest War EveThe Biggest War Eve',
      genre: 'War'
    },
    {
      name: 'fortnite',
      logoUrl: 'https://cdn2.unrealengine.com/Fortnite%2FBattle-pass%2FSeason-7%2Fseason7_plane-2024x1139-a974df2b274a4254b43387ef34ab40c1b42250a9.jpg',
      link: 'fortnite',
      description:'The Biggest War Ever',
      genre: 'War'
    }, 
    {
      name: 'Mafia 2',
      logoUrl: 'https://upload.wikimedia.org/wikipedia/ru/thumb/d/d6/MafiaII.jpg/270px-MafiaII.jpg',
      link: 'Mafia2',
      description:'The Biggest War Ever',
      genre: 'War'
    },
    {
      name: 'tanks',
      logoUrl: 'assets/imageKeeper/WebTanks.png',
      link: 'tanks',
      description:'The Biggest War EverThe Biggest War EveThe Biggest War EveThe Biggest War EveEveThe Biggest War EveEveThe Biggest War EveEveThe Biggest War EveEveThe Biggest War EveEveThe Biggest War EveEveThe Biggest War EveEveThe Biggest War Eve EveThe Biggest War EveThe Biggest War EveThe Biggest War EveThe Biggest War Eve',
      genre: 'War'
    },
    {
      name: 'fortnite',
      logoUrl: 'https://cdn2.unrealengine.com/Fortnite%2FBattle-pass%2FSeason-7%2Fseason7_plane-2024x1139-a974df2b274a4254b43387ef34ab40c1b42250a9.jpg',
      link: 'fortnite',
      description:'The Biggest War Ever',
      genre: 'War'
    }, 
    {
      name: 'Mafia 2',
      logoUrl: 'https://upload.wikimedia.org/wikipedia/ru/thumb/d/d6/MafiaII.jpg/270px-MafiaII.jpg',
      link: 'Mafia2',
      description:'The Biggest War Ever',
      genre: 'War'
    },
    {
      name: 'fortnite',
      logoUrl: 'https://cdn2.unrealengine.com/Fortnite%2FBattle-pass%2FSeason-7%2Fseason7_plane-2024x1139-a974df2b274a4254b43387ef34ab40c1b42250a9.jpg',
      link: 'fortnite',
      description:'The Biggest War Ever',
      genre: 'War'
    }, 
    {
      name: 'Mafia 2',
      logoUrl: 'https://upload.wikimedia.org/wikipedia/ru/thumb/d/d6/MafiaII.jpg/270px-MafiaII.jpg',
      link: 'Mafia2',
      description:'The Biggest War Ever',
      genre: 'War'
    },
    {
      name: 'tanks',
      logoUrl: 'assets/imageKeeper/WebTanks.png',
      link: 'tanks',
      description:'The Biggest War EverThe Biggest War EveThe Biggest War EveThe Biggest War EveEveThe Biggest War EveEveThe Biggest War EveEveThe Biggest War EveEveThe Biggest War EveEveThe Biggest War EveEveThe Biggest War EveEveThe Biggest War Eve EveThe Biggest War EveThe Biggest War EveThe Biggest War EveThe Biggest War Eve',
      genre: 'War'
    },
    {
      name: 'fortnite',
      logoUrl: 'https://cdn2.unrealengine.com/Fortnite%2FBattle-pass%2FSeason-7%2Fseason7_plane-2024x1139-a974df2b274a4254b43387ef34ab40c1b42250a9.jpg',
      link: 'fortnite',
      description:'The Biggest War Ever',
      genre: 'War'
    }, 
    {
      name: 'Mafia 2',
      logoUrl: 'https://upload.wikimedia.org/wikipedia/ru/thumb/d/d6/MafiaII.jpg/270px-MafiaII.jpg',
      link: 'Mafia2',
      description:'The Biggest War Ever',
      genre: 'War'
    },
    {
      name: 'tanks',
      logoUrl: 'assets/imageKeeper/WebTanks.png',
      link: 'tanks',
      description:'The Biggest War EverThe Biggest War EveThe Biggest War EveThe Biggest War EveEveThe Biggest War EveEveThe Biggest War EveEveThe Biggest War EveEveThe Biggest War EveEveThe Biggest War EveEveThe Biggest War EveEveThe Biggest War Eve EveThe Biggest War EveThe Biggest War EveThe Biggest War EveThe Biggest War Eve',
      genre: 'War'
    }
  ]
}
