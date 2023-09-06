import {useState} from 'react';
import '../css/login.css';
import {sha256} from 'js-sha256';

export default function Login() {
  const [username, setUsername] = useState('');
  const [password, setPassword] = useState('');
  return (
    <div className="Login container">
      <h1 className='main-title'>Le Chat</h1>
      <article>
        <form>
          <input id="username" type="text" name="username" placeholder="Username" required value={username}
                 onChange={(e) => setUsername(e.target.value)} aria-invalid={!username.trim()}/>
          <input id="password" type="password" name="password" placeholder="Password" required value={password}
                 onChange={(e) => setPassword(e.target.value)} aria-invalid={!password}/>
          <small>Your data is stored safely and we do not have access to it.</small>
          <button type='submit' onClick={(e) => {
            e.preventDefault();
            if (username.trim() && password) {
              const e_username: string = sha256(username.trim());
              const e_password: string = sha256(password);
              // TODO: Send encrypted username and password to backend. -> Backend returns a cookie if login successful
              // Cookie acquired -> redirect to overview site

              // Delete log later
              console.log(username.trim() + " -> " + e_username);
              console.log(password + " -> " + e_password);
            }
          }}>Login
          </button>
        </form>
      </article>
    </div>
  );
}