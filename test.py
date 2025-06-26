# a simple python code to check the api behavior without swagger, by directly making http post request

import requests
import urllib3
from urllib3.exceptions import InsecureRequestWarning

urllib3.disable_warnings(InsecureRequestWarning)  # disable SSL warnings for local dev

short_code = "6YQZY3"
url = f"http://localhost:5009/api/stats/{short_code}"

response = requests.get(url, verify=False)
print(response.status_code)
print(response.text)