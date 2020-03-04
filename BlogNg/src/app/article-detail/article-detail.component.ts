import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Article } from '../articles/models/article';
import { ArticleService } from '../articles/services/article.service';
import { CommentService } from '../comments/services/comment.service';

@Component({
  selector: 'app-article-detail',
  templateUrl: './article-detail.component.html',
  styleUrls: ['./article-detail.component.css']
})
export class ArticleDetailComponent implements OnInit {

  article = new Article('0', ' ', undefined, ' ', undefined);
  existed = false;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private commentService: CommentService,
    private articleService: ArticleService) { }

  ngOnInit() {
    this.route.params.subscribe(p => {
      if (p.id === undefined) { return; }
      this.articleService.getArticle(p.id).subscribe(c => this.article = c);
      this.existed = true;
    });
  }
}
