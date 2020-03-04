import { HttpClient, HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';

import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatInputModule } from '@angular/material/input';
import { MatListModule } from '@angular/material/list';
import { MatToolbarModule } from '@angular/material/toolbar';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { ArticlesListComponent } from './articles/lists/articles-list.component';
import { ArticleDetailComponent } from './article-detail/article-detail.component';
import { ArticleService } from './articles/services/article.service';
import { CommentsFormComponent } from './comments/forms/comments-form.component';
import { CommentListComponent } from './comments/lists/comment-list.component';
import { AddArticleFormComponent } from './articles/forms/add-article-form.component';
import { RegisterComponent } from './users/forms/register.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { AuthService } from './users/services/auth.service';
import { AuthInterceptor } from './auth.interceptor';
import { LoginComponent } from './users/forms/login.component';
import { NavComponent } from './nav/nav.component';

@NgModule({
  declarations: [
    AppComponent,
    ArticlesListComponent,
    ArticleDetailComponent,
    CommentsFormComponent,
    CommentListComponent,
    AddArticleFormComponent,
    RegisterComponent,
    LoginComponent,
    NavComponent
  ],
  imports: [
    BrowserModule,
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    MatButtonModule,
    MatInputModule,
    MatCardModule,
    MatListModule,
    MatToolbarModule,
  ],
  providers: [
    HttpClient,
    ArticleService,
    AuthService,
    {
      provide: HTTP_INTERCEPTORS,
      useClass: AuthInterceptor,
      multi: true
    }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
