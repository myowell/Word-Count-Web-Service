using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Linq;
using System.Web;

namespace WordCountWebService.Models {
    public class FileRequestModel {
        private string inputString { get; set; } /* The request string via HTTP POST*/
        private int wordCount { get; set; }  /* The total word count of the request string */
        private int characterCount { get; set; } /* The total character count of the request string */
        private int whitespaceCount { get; set; } /* The total whitespace count of the request string */
        private int punctuationCount { get; set; } /* The total punctuation count of the requrst string */
        private SortedDictionary<String, int> dict { get; set; } /* An ordered dictionary of a word and the amount it has appeared in the input string */
        private string fileCountJSONReturn { get; set; } /* A response string serialized into JSON format */

        /* 
         * Default constructor for the FileRequestModel class. 
        */
        public FileRequestModel(string inputString) {
            this.inputString = inputString;
        }

        /*
         * Parses the input request string.
         * Populates the word count dictionary and finds the total character, word, whitespace, and punctuation counts.
         */

       private void parseRequestString(string inputString) {
            this.whitespaceCount = inputString.Count(char.IsWhiteSpace);
            this.punctuationCount = inputString.Count(char.IsPunctuation);
            this.characterCount = inputString.Length - Regex.Matches(inputString, @"\t|\n|\r").Count;
            inputString = Regex.Replace(inputString, @"\t|\n|\r", " "); // Remove newline characters and replace with spaces
            inputString = Regex.Replace(inputString, @"\p{P}", ""); // Remove punctuation

            string[] words = inputString.Split(' ');
            this.wordCount = words.Length;

            for(int i = 0; i < words.Length; i++) {
                string word = words[i].ToLower();
                if (this.dict.ContainsKey(word))
                    this.dict.Add(word, 1);
                else
                    this.dict[word] += 1;
            }

            // Extracts the top 50 results if there were more than 50 unique words in the input string

            if(dict.Count > 50)
                this.dict = new SortedDictionary<String, int>((from entry in dict orderby entry.Value descending select entry).Take(50).ToDictionary(pair => pair.Key, pair => pair.Value));
        }
    }
}
 