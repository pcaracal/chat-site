import {Component} from '@angular/core';
import {sha256} from "js-sha256";
import {ApiService} from "../api.service";

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent {
  constructor(private apiService: ApiService) {
  }

  setBearerToken(token: string) {
    const fullToken = `Bearer ${token}`;
    localStorage.setItem("Authorization", token);
  }

  private _username: string = "";
  private _password: string = "";

  keyUpUsername(event: KeyboardEvent) {
    this._username = (event.target as HTMLInputElement).value;
  }

  keyUpPassword(event: KeyboardEvent) {
    this._password = (event.target as HTMLInputElement).value;
  }

  handleLogin(event: Event) {
    event.preventDefault();
    if (this._username.trim() && this._password) {
      const e_username: string = this._username.trim().toLowerCase();
      const e_password: string = sha256(this._password);
      // Remove log later
      console.log(`Username: ${e_username} | Password: ${e_password}`);

      this.apiService.loginPost(e_username, e_password).subscribe({
        next: (response: any) => {
          console.log("Login successful", response);
          this.setBearerToken(response.token);
        },
        error: (error) => {
          console.log("Login failed", error);
        }
      });
    }
  }
}
