/*
 * Model class for the InputController
 * 
 * This class contains the functions to parse the input string to calculate the word, character, whitespace, and punctuation counts, and returns the formatted JSON response.
 * 
 * Author: Michael Yowell - michael.yowell@gmail.com
 * 
 */

using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Linq;
using System.Web;
using Newtonsoft.Json.Linq;

namespace WordCountWebService.Models {
    public class FileRequestModel {
        private string inputString { get; set; } /* The request string via HTTP POST*/
        private int wordCount { get; set; }  /* The total word count of the request string */
        private int characterCount { get; set; } /* The total character count of the request string */
        private int whitespaceCount { get; set; } /* The total whitespace count of the request string */
        private int punctuationCount { get; set; } /* The total punctuation count of the requrst string */
        private Dictionary<String, int> dict = new Dictionary<string, int>(); /* An ordered dictionary of a word and the amount it has appeared in the input string */

        /* 
         * Default constructor for the FileRequestModel class. 
        */
        public FileRequestModel(string inputString) {
            this.inputString = inputString;

            parseRequestString(inputString);
        }

        /*
         * Parses the input request string.
         * Populates the word count dictionary and finds the total character, word, whitespace, and punctuation counts.
         */

       private void parseRequestString(string inputString) {
            this.whitespaceCount = inputString.Count(char.IsWhiteSpace);
            this.punctuationCount = inputString.Count(char.IsPunctuation);
            this.characterCount = inputString.Length - Regex.Matches(inputString, @"\t|\n|\r").Count;
            inputString = Regex.Replace(inputString, @"\t|\n|\r", ""); // Remove newline characters
            inputString = Regex.Replace(inputString, @"\p{P}", " "); // Remove punctuation

            inputString = inputString.Trim(); // Remove excess whitespace in order to split the string correctly

            string[] words = Regex.Split(inputString, @"\s");
            this.wordCount = words.Length;

            for(int i = 0; i < words.Length; i++) {
                string word = words[i].ToLower();
                if (!this.dict.ContainsKey(word))
                    this.dict.Add(word, 1);
                else
                    this.dict[word] += 1;
            }

            // Sort the words and word count dictionary in descending order
            
            this.dict = this.dict.OrderBy(x => x.Key).ToDictionary(x => x.Key, x => x.Value);
            this.dict = this.dict.OrderByDescending(x => x.Value).ToDictionary(x => x.Key, x => x.Value);           

            // Extracts the top 50 results if there were more than 50 unique words in the input string

            if (dict.Count > 50)
                this.dict = this.dict.Take(50).ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
        }

        /*
         * Creates the JSON response using the word frequency dictionary as well as the word, whitespace, character, and punctuation counts.
         * Returns - The formatted JSONObject containing all the information parsed from the request. 
        */

        public dynamic getFileCountJSONResponse() {
            dynamic fileCountJSONReturn = new JObject();
            fileCountJSONReturn.Frequencies = new JArray() as dynamic;

            foreach(KeyValuePair<String, int> entry in this.dict) {
                dynamic word = new JObject();
                word.Word = entry.Key;
                word.Count = entry.Value;
                fileCountJSONReturn.Frequencies.Add(word);
            }

            fileCountJSONReturn.WordCount = this.wordCount;
            fileCountJSONReturn.WhitespacePercentage = whitespaceCount == 0 ? 0 : (double)100 / (this.characterCount / this.whitespaceCount);
            fileCountJSONReturn.PunctuationPercentage = punctuationCount == 0 ? 0 : ((double)100 / (this.characterCount / this.punctuationCount));

            return fileCountJSONReturn;
        }
    }
}
 