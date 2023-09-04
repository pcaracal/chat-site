import '../css/login.css';

export default function Login() {
  return (
    <div className="Login container">
      <h1 className='main-title'>Le Chat</h1>
      <article>
        <form>
          <input id="username" type="text" name="username" placeholder="Username" required />
          <input id="password" type="text" name="password" placeholder="Password" required />
          <small>Your data is stored safely and encrypted and we do not have access to it.</small>
          <button type='submit' onClick={(e) => e.preventDefault()}>Login</button>
        </form>
      </article>
    </div>
  );
}