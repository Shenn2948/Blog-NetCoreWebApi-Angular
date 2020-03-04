import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ArticleService } from '../../articles/services/article.service';
import { Comment } from '../../comments/models/comment';
import { User } from '../../users/models/User';
import { AuthService } from '../../users/services/auth.service';

@Component({
  selector: 'app-comment-list',
  templateUrl: './comment-list.component.html',
  styleUrls: ['./comment-list.component.css']
})
export class CommentListComponent implements OnInit {

  comments: Comment[];
  existed = false;
  user = new User('0', ' ', ' ');

  constructor(
    private route: ActivatedRoute,
    private authService: AuthService,
    private articleService: ArticleService,
  ) { }

  ngOnInit() {
    this.route.params.subscribe(p => {
      if (p.id === undefined) { return; }
      this.articleService.getCommentsOfArticle(p.id).subscribe(c => this.comments = c);
      this.authService.getUser().subscribe(x => this.user = x);
      this.existed = true;
    });
  }
}
