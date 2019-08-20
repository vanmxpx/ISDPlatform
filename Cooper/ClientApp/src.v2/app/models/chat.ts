import {User, Message} from '@models';

export class Chat {
    public id: number;
    public chatName: string;
    public isOnetoOneChat: boolean;
    public participants: User[];
    public messages: Message[];
    public chatPhotoURL: string;
}
