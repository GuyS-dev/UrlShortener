import logo from 'D:/url-shortener-guys/urlshortenerfrontend/src/assets/images/short_url_logo.png';
import './App.css';
import { useState } from 'react';

function App() {

  const [input, setInput] = useState("");

  const handleSubmit = (e) => {
    e.preventDefault();
    alert(`You typed: ${input}`);
  };

  return (
    <div className="App">
      <header className="App-header">
        <img src={logo} className="App-logo" alt="logo" />
        <p className='Instruction'>
          Enter the URL to shorten it!
        </p>

      </header>

      <form onSubmit={handleSubmit} className="form">
        <input
          type="text"
          placeholder="Type something..."
          value={input}
          onChange={(e) => setInput(e.target.value)}
          className="input-box"
        />
        <button type="submit" className="submit-btn">
          Submit
        </button>
      </form>
    </div>
  );
}

export default App;