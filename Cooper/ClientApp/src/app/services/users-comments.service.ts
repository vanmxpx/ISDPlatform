import { Injectable } from '@angular/core';
import { Comment } from '../models/my-page-models';

@Injectable({
  providedIn: 'root'
})
export class UsersCommentsService {

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

  getData(): Comment[] {        
  return this.comments;
  }
  
  constructor() { }
}
