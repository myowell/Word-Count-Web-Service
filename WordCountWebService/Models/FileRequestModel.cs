using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WordCountWebService.Models {
    public class FileRequestModel {
        private string inputString { get; set; }
        private int WordCount { get; set; }
        private int whiteSpaceCount { get; set; }
        private int punctuationCount { get; set; }
        private SortedDictionary<String, int> dict { get; set; }

        public FileRequestModel(string inputString) {
            this.inputString = inputString;
        }



    }
}