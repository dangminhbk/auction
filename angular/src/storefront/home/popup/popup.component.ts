import { Component, Injector, Input, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AppComponentBase } from '@shared/app-component-base';
import { BsModalRef } from 'ngx-bootstrap/modal';

@Component({
  selector: 'app-popup',
  templateUrl: './popup.component.html',
  styleUrls: ['./popup.component.css']
})
export class PopupComponent extends AppComponentBase implements OnInit {

  @Input() image;
  @Input() link;

  constructor(
    injectot: Injector,
    private bsModalRef: BsModalRef,
    private router: Router
  ) {
    super(injectot);
  }
  ngOnInit(): void {
  }

  close() {
    this.bsModalRef.hide();
  }

}
