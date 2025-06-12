import logo from './assets/images/short_url_logo.png';
import './App.css';
import { useState } from 'react';

function App() {

  const [urlInput, setInput] = useState("");
  const [shortenedUrl, setShortenedUrl] = useState("");

  /*const handleSubmit = (e) => {
    e.preventDefault();
    fetch("http://localhost:5009/api/shorten", {
      method: "POST",
      body: JSON.stringify({
        originalUrl: urlInput,
      }),
      headers: {
        "Content-type": "application/json; charset=UTF-8"
      }
    });
  };*/

  const handleSubmit = async (e) => {
  e.preventDefault();
  try {
    const response = await fetch("http://localhost:5009/api/shorten", {
      method: "POST",
      headers: {
        "Content-type": "application/json; charset=UTF-8",
      },
      body: JSON.stringify({
        originalUrl: urlInput,
      }),
    });

    if (!response.ok) {
      throw new Error(`Server error: ${response.status}`);
    }

    const resultUrl = await response.text();
    console.log("Shortened URL:", resultUrl);
    setShortenedUrl(resultUrl)
    setInput("");
  } catch (error) {
    console.error("Fetch failed:", error.message);
  }
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
          id="urlInput"
          placeholder="Type something..."
          value={urlInput}
          onChange={(e) => setInput(e.target.value)}
          className="input-box"
        />
        <button type="submit" className="submit-btn">
          Submit
        </button>
      </form>
      {shortenedUrl && (
        <p className='result'>
          Shortened URL: <a href={shortenedUrl}>{shortenedUrl}</a>
        </p>
      )}
      <p className='credits'>
        • By Guy Singer •
      </p>
    </div>
  );
}

export default App;