import {Component} from '@angular/core';
import {ApiService} from "../api.service";
import {Router} from "@angular/router";
import {ToastrService} from "ngx-toastr";

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent {
  constructor(private apiService: ApiService, private router: Router, private _toastr: ToastrService) {
  }

  setBearerToken(token: string) {
    const fullToken = `Bearer ${token}`;
    sessionStorage.setItem("Authorization", fullToken);
  }

  setUserIdName(userId: string, username: string) {
    sessionStorage.setItem("userId", userId);
    sessionStorage.setItem("username", username);
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

          this.apiService.loginGet().subscribe({
            next: (response: any) => {
              console.log("Check login successful", response);
              this.setUserIdName(response.userId, response.username);
            }
          });

          this.router.navigate(["/overview"]);
          this._toastr.success("Login successful", "Welcome back");
        },
        error: (error: any) => {
          this._toastr.error("Username or password incorrect", "Login failed");
        }
      });
    }
  }

  handleRegister(event: Event) {
    event.preventDefault();
    if (this._username.trim() && this._password && this.passwordRepeatValid) {
      const e_username: string = this._username.trim().toLowerCase();
      const e_password: string = this._password;
      console.log("Registering, api not sent yet")
      this.apiService.registerPost(e_username, e_password).subscribe({
        next: (response: any) => {
          this.isLogin = true;
          this._toastr.success("Registration successful", "Welcome");
        },
        error: (error: any) => {
          // TODO: Handle stuff like 409
          this._toastr.error("Username already exists", "Registration failed");
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
