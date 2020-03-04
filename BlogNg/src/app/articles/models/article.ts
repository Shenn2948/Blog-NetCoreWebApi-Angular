import { User } from 'src/app/users/models/User';

export class Article {
  constructor(
    public id: string,
    public title: string,
    public createdDate: Date,
    public content: string,
    public user: User
  ) { }
}
