import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';
import { Article } from '../models/article';
import { Comment } from '../../comments/models/comment';

@Injectable()
export class ArticleService {

  private url = environment.apiUrl + 'articles';

  constructor(private http: HttpClient) { }

  getArticles(): Observable<Array<Article>> {
    return this.http.get<Array<Article>>(this.url);
  }

  getArticle(articleId: string): Observable<Article> {
    return this.http.get<Article>(`${this.url}/${articleId}`);
  }

  getCommentsOfArticle(articleId: string): Observable<Array<Comment>> {
    return this.http.get<Array<Comment>>(`${this.url}/${articleId}/comments`);
  }

  addArticle(article: Article): Observable<Article> {
    return this.http.post<Article>(this.url, article);
  }

  updateArticle(article: Article): Observable<object> {
    return this.http.put<Article>(`${this.url}/${article.id}`, article);
  }

  deleteArticle(articleId: string): Observable<object> {
    return this.http.delete<object>(`${this.url}/${articleId}`);
  }
}
