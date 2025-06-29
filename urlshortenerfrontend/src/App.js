// Importing the ui logo
import logo from './assets/images/short_url_logo.png';
// Importing the css styles
import './App.css';
import { useState } from 'react';

// React frontend
function App() {

  // State to store the user url input
  const [urlInput, setInput] = useState("");
  // State to store the result of the api call, the shortened version
  const [shortenedUrl, setShortenedUrl] = useState("");


  /**
   * Handles form submission by sending the original URL to the backend API.
   * On success, sets the shortened URL and clears the input field.
   * Logs an error message if the request fails.
   * */
  const handleSubmit = async (e) => {
  e.preventDefault();
  try {
    // Sending a POST request to the backend API with the original url from the input
    const response = await fetch("http://localhost:5009/api/shorten", {
      method: "POST",
      headers: {
        "Content-type": "application/json; charset=UTF-8",
      },
      body: JSON.stringify({
        originalUrl: urlInput,
      }),
    });
    
    // Error handling if the response from the backend is not ok
    if (!response.ok) {
      throw new Error(`Server error: ${response.status}`);
    }
    
    // Fetches the response as text and set the resultUrl state to it
    const resultUrl = await response.text();
    console.log("Shortened URL:", resultUrl);
    setShortenedUrl(resultUrl)
    // Clears the input box
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