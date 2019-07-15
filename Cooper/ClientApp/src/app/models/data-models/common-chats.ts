export interface CommonChatData {
    content: string;
    createDate: number;
    author: AuthorData;
  }

  export interface AuthorData{
      nickname: string;
      photoUrl: string;
  }