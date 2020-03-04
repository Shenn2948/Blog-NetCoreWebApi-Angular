import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ArticlesListComponent } from './articles/lists/articles-list.component';
import { ArticleDetailComponent } from './article-detail/article-detail.component';
import { CommentListComponent } from './comments/lists/comment-list.component';
import { AddArticleFormComponent } from './articles/forms/add-article-form.component';
import { RegisterComponent } from './users/forms/register.component';
import { LoginComponent } from './users/forms/login.component';
import { CommentsFormComponent } from './comments/forms/comments-form.component';

const routes: Routes = [
  { path: '', redirectTo: '/articles', pathMatch: 'full' },
  { path: 'articles', component: ArticlesListComponent },
  { path: 'articles/:id', component: ArticleDetailComponent },
  { path: 'articles/:id/comments', component: CommentListComponent },
  { path: 'article', component: AddArticleFormComponent },
  { path: 'users/register', component: RegisterComponent },
  { path: 'users/login', component: LoginComponent },
  { path: 'articles/:articleId', component: CommentsFormComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
