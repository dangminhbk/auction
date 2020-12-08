import { Component, OnInit } from '@angular/core';
import { AppAuthService } from '@shared/auth/app-auth.service';

@Component({
  selector: 'app-logout',
  templateUrl: './logout.component.html',
  styleUrls: ['./logout.component.css']
})
export class LogoutComponent implements OnInit {
  constructor(private _authService: AppAuthService) { }
  ngOnInit(): void {
    this.logout();
  }

  logout(): void {
    this._authService.logout();
  }
}
