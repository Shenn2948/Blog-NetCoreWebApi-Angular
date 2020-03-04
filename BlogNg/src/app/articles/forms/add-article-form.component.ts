import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Article } from '../../articles/models/article';
import { ArticleService } from '../../articles/services/article.service';

@Component({
  selector: 'app-add-article-form',
  templateUrl: './add-article-form.component.html',
  styleUrls: ['./add-article-form.component.css']
})
export class AddArticleFormComponent implements OnInit {

  article = new Article('0', ' ', undefined, ' ', undefined);
  existed = false;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private articleService: ArticleService) { }

  ngOnInit() {
    this.route.params.subscribe(p => {
      if (p.id === undefined) { return; }
      this.articleService.getArticle(p.id).subscribe(c => this.article = c);
      this.existed = true;
    });
  }

  navigateBack() {
    this.router.navigate(['/articles']);
  }

  onCancel() {
    this.navigateBack();
  }

  onSubmit() {
    if (this.existed) {
      this.articleService.updateArticle(this.article).subscribe(() => this.navigateBack());
    } else {
      this.articleService.addArticle(this.article).subscribe(() => this.navigateBack());
    }
  }

}
