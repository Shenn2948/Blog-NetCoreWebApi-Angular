export class Comment {
  constructor(
    public id: string,
    public createdDate: Date,
    public content: string,
    public articleId: string
  ) { }
}
