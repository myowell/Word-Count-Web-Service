### Word-Count-Web-Service
## A RESTful web service which accepts an HTTP POST request of raw/plain text data, calculates the word frequencies, whitespace percentage, and punctuation percentage. Returns a JSON string containing the result of the analysis.

### Getting Started
Requires the latets visual studio (https://www.visualstudio.com/downloads/) and .NET framework.

To Run the service on your local machine, simply pull the project into your workspace and launch with your browser of choice using the IIS Express button as seen below:

![img](https://i.imgur.com/i8M8m6R.png)

If you receive a HTTP Error 403.14 - Forbidden error when launching the service this may indicate an issue with your version of IIS, see the following link for a possible resolution. Regardless the service will still be able to accept requests and respond correctly.

https://support.microsoft.com/en-us/help/942062/http-error-403-14-forbidden-error-when-you-open-an-iis-7-0-webpage

### Use
To send a request to the service while it is running on your local machine you may use one of the following popular browser extensions or methods:

Postman (Google Chrome): https://chrome.google.com/webstore/detail/postman/fhbjgbiflinjbdggehcddcbncdddomop?hl=en
RESTClient (Firefox): https://addons.mozilla.org/en-US/firefox/addon/restclient/
The curl command (command line): http://www.codingpedia.org/ama/how-to-test-a-rest-api-from-command-line-with-curl/

For the best compapability across tesitng tools ensure that your request header contains the attribute "Content-Type" with the value "text/plain; charset=utf-8".

Only POST type requests are supported, attempting any other type of request will result in an error.

The service only supports request containing non-null and non-empty strings, sending a request with one will result in an error. Note that in the context of this service, newline characters are considered to be neither whitespaces nor characters.

A successful request and response will return a JSON string with the results of the string analysis.

For example the following request:

```
one, two, three
three, one, four four four four four eight
three six nine six eight eight
```

Will return the following JSON string:

```
{
  "Frequencies": [{
    "Word": "four",
    "Count": 5
  }, {
    "Word": "eight",
    "Count": 2
  }, {
    "Word": "one",
    "Count": 2
  }, {
    "Word": "six",
    "Count": 2
  }, {
    "Word": "eightthree",
    "Count": 1
  }, {
    "Word": "nine",
    "Count": 1
  }, {
    "Word": "threethree",
    "Count": 1
  }, {
    "Word": "two",
    "Count": 1
  }],
  "WordCount": 15,
  "WhitespacePercentage": 20.0,
  "PunctuationPercentage": 4.7619047619047619
}
```

Note that while a request containing nothing but punctuation will be accepted, this service's logic cannot always parse such a request correctly. Thus it is considered undefined behavior and will lead to unexpected results.
