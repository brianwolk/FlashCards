using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace FlashCards.Models
{
    public class FlashCardModel
    {

        public FlashCardModel(string name, string summary)
        {
            Name = name;
            Summary = summary;


        }
        public ObjectId _id { get; set; }

        public string Name { get; set; } = "";
        public string Summary { get; set; } = "";
    }
}