# a simple python code to check the api behavior without swagger, by directly making http post request

import requests
import urllib3
from urllib3.exceptions import InsecureRequestWarning

urllib3.disable_warnings(InsecureRequestWarning)  # disable SSL warnings for local dev

url = "http://localhost:5009/api/shorten"
data = {
    "originalUrl": "http://learn.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/required",
}

response = requests.post(url, json=data, verify=False)
print(response.text)
