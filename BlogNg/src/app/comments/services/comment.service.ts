import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';
import { Comment } from '../models/comment';

@Injectable({
  providedIn: 'root'
})
export class CommentService {

  private url = environment.apiUrl + 'comments';

  constructor(private http: HttpClient) { }

  getComments(): Observable<Array<Comment>> {
    return this.http.get<Array<Comment>>(this.url);
  }

  getComment(commentId: string): Observable<Comment> {
    return this.http.get<Comment>(`${this.url}/${commentId}`);
  }

  addComment(comment: Comment): Observable<Comment> {
    return this.http.post<Comment>(this.url, comment);
  }

  updateComment(comment: Comment): Observable<Comment> {
    return this.http.put<Comment>(`${this.url}/${comment.id}`, comment);
  }

  deleteComment(commentId: string): Observable<object> {
    return this.http.delete<object>(`${this.url}/${commentId}`);
  }
}
