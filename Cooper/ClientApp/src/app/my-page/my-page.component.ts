import { Component, OnInit } from '@angular/core';
import { MatCard } from '@angular/material';

export interface Game {
  name: string;
  logo: string;
}

export interface Comment {
  name: string;
  comment: string;
  avatar: string;
}

@Component({
  selector: 'app-my-page',
  templateUrl: './my-page.component.html',
  styleUrls: ['./my-page.component.css']
})
export class MyPageComponent implements OnInit {

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

comments: Comment[]=
[{
  name: 'User1',
  comment: 'You are cool',
  avatar:'assets/imageKeeper/robot.png'
},
{
  name: 'User2',
  comment: 'You  not cool',
  avatar:'assets/imageKeeper/robot.png'
},
{
  name: 'User1',
  comment: 'You are robot',
  avatar:'assets/imageKeeper/robot.png'
},
{
  name: 'User1',
  comment: 'You are TEST',
  avatar:'assets/imageKeeper/robot.png'
}]
  constructor() { }

  ngOnInit() {
  }

}
