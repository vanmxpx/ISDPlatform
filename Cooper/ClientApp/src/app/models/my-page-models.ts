export interface Game {
    name: string;
    logo: string;
  }
  
  export interface Comment {
    name: string;
    comment: string;
    avatar: string;
  }
  
  export interface CommonChat {
    name: string;
    message: string;
    avatar: string;
    date: string;
  }

  export interface ProfileList {
    Nickname: string;
    PhotoURL: string;
  }

  export class Profile{
    nickname: string = "";
    name: string = "";
    photoUrl: string = "";
    email: string = "";
    isMy: boolean = false;
  }