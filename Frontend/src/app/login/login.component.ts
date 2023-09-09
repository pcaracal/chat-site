import {Component} from '@angular/core';
import {sha256} from "js-sha256";
import {ApiService} from "../api.service";
import * as bcrypt from "bcryptjs";
import {Router} from "@angular/router";

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent {
  constructor(private apiService: ApiService, private router: Router) {
  }

  setBearerToken(token: string) {
    const fullToken = `Bearer ${token}`;
    sessionStorage.setItem("Authorization", fullToken);
  }

  private _username: string = "";
  private _password: string = "";
  private _passwordRepeat: string = "";

  loginSuccessful: boolean = false;
  isLogin: boolean = true;
  passwordRepeatValid: boolean = false;

  keyUpUsername(event: KeyboardEvent) {
    this._username = (event.target as HTMLInputElement).value;
  }

  keyUpPassword(event: KeyboardEvent) {
    this._password = (event.target as HTMLInputElement).value;
    this.passwordRepeatValid = this._password === this._passwordRepeat;
  }

  keyUpPasswordRepeat(event: KeyboardEvent) {
    this._passwordRepeat = (event.target as HTMLInputElement).value;
    this.passwordRepeatValid = this._password === this._passwordRepeat;
  }

  handleLogin(event: Event) {
    event.preventDefault();
    if (this._username.trim() && this._password) {
      const e_username: string = this._username.trim().toLowerCase();
      const e_password: string = this._password;

      this.apiService.loginPost(e_username, e_password).subscribe({
        next: (response: any) => {
          console.log("Login successful", response);
          this.setBearerToken(response.token);
          this.router.navigate(["/overview"]);
        },
        error: (error: any) => {
          // TODO: Handle stuff like 401
          // console.log("Login failed", error);
        }
      });
    }
  }

  handleRegister(event: Event) {
    event.preventDefault();
    if (this._username.trim() && this._password && this.passwordRepeatValid) {
      const e_username: string = this._username.trim().toLowerCase();
      const e_password: string = this._password;

      this.apiService.registerPost(e_username, e_password).subscribe({
        next: (response: any) => {
          console.log("Register successful", response);
          this.setBearerToken(response.token);
        },
        error: (error: any) => {
          // TODO: Handle stuff like 401, 409, etc.
        }
      });
    }
  }

  // Redundant code delete later
  handleCheckLogin(event: Event) {
    event.preventDefault();
    this.apiService.loginGet().subscribe({
      next: (response: any) => {
        console.log("Check login successful", response);
        this.loginSuccessful = true;
      },
      error: (error: any) => {
        console.log("Check login failed", error);
        this.loginSuccessful = false;
      }
    });
  }
}
