import {useState} from 'react';
import '../css/login.css';
import {sha256} from 'js-sha256';

export default function Login() {
  const [username, setUsername] = useState('');
  const [password, setPassword] = useState('');

  const handleLogin = async (e_username: string, e_password: string) => {
    try {
      const requestBody = {
        username: e_username,
        password: e_password,
      };

      const response = await fetch('http://localhost:5147/api/login', {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
        },
        body: JSON.stringify(requestBody),
      });
      if (response.ok) {
        // Successful request
        const responseData = await response.json();
        console.log('Login successful:', responseData);
      } else {
        // Failed request
        console.error('Login failed:', response.status, response.statusText);
      }
    } catch (error) {
      console.error('An error occurred:', error);
    }
  };

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

              handleLogin(e_username, e_password).then(void 0);
              // TODO: void 0 -> replace with set cookie to browser
              // Cookie acquired -> redirect to overview site
            }
          }}>Login
          </button>
        </form>
      </article>
    </div>
  );
}