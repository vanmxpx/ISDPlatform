import {User, Message} from '@models';

export class Chat {
    public id: number;
    public chatName: string;
    public chatPhotoURL: string;
    public unreadMessagesAmount: number;
    public participants: User[];
    public messages: Message[];
}
