import { Component, OnInit, Input, ViewChild } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { CommentService } from '../services/comment.service';
import { AuthService } from '../../users/services/auth.service';
import { Comment } from '../../comments/models/comment';
import {
  ReactiveFormsModule,
  FormsModule,
  FormGroup,
  FormControl,
  Validators,
  FormBuilder
} from '@angular/forms';

@Component({
  selector: 'app-comments-form',
  templateUrl: './comments-form.component.html',
  styleUrls: ['./comments-form.component.css']
})

export class CommentsFormComponent implements OnInit {
  comment = new Comment('0', undefined, ' ', '0');
  articleId: string;
  @Input() comments: Comment[];

  commentContent: FormControl;
  commentsForm: FormGroup;

  constructor(
    public auth: AuthService,
    private route: ActivatedRoute,
    private commentService: CommentService
  ) { }

  createFormControls() {
    this.commentContent = new FormControl('');
  }

  createForm() {
    this.commentsForm = new FormGroup({ commentContent: this.commentContent });
  }

  ngOnInit() {
    this.route.params.subscribe(p => {
      if (p.id === undefined) {
        return;
      } else {
        this.createFormControls();
        this.createForm();
        this.articleId = p.id;
      }
    });
  }

  onSubmit() {
    this.comment.articleId = this.articleId;
    this.comment.content = this.commentContent.value;

    this.commentService.addComment(this.comment).subscribe(comment => {
      this.comments.push(comment);
    });

    this.commentsForm.reset();
  }
}
