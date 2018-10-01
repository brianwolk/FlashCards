using FlashCards.Models;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Newtonsoft.Json;

namespace FlashCards.Controllers
{
    public class FlashCardController : ApiController
    {
        /// <summary>
        /// Gets List of FlashCards
        /// </summary>
        /// <returns>List of FlashCards</returns>
        // GET: api/FlashCard
        public List<FlashCardModel> Get()
        {
            List<FlashCardModel> flashCards = new List<FlashCardModel>();

            var client = new MongoClient("mongodb://bwolky:Bw2189114.@ds019980.mlab.com:19980/flashcardsdb");
            IMongoDatabase db = client.GetDatabase("flashcardsdb");
            IMongoCollection<BsonDocument> collection = db.GetCollection<BsonDocument>("flashcards");
            var docs = collection.Find(new BsonDocument()).ToList();

            foreach (BsonDocument doc in docs)
            {
                flashCards.Add(BsonSerializer.Deserialize<FlashCardModel>(doc));
            }

            return flashCards;
        }

        /// <summary>
        /// Add a flashcard with the name and summary, autogen ID in db
        /// </summary>
        /// <param name="name">name of flashcard</param>
        /// <param name="summary">summary of flashcard</param>
        [Route("api/FlashCard/{name}/{summary}")]
        [HttpPost]
        public void Post(string name, string summary)
        {
            FlashCardModel card = new FlashCardModel(name, summary);
            var client = new MongoClient("mongodb://bwolky:Bw2189114.@ds019980.mlab.com:19980/flashcardsdb");
            IMongoDatabase db = client.GetDatabase("flashcardsdb");
            IMongoCollection<BsonDocument> collection = db.GetCollection<BsonDocument>("flashcards");
            collection.InsertOne(card.ToBsonDocument());

        }

        /// <summary>
        /// Add a flashcard with JSON in body
        /// </summary>
        /// <param name="cardString">json already</param>
        [Route("api/FlashCard/add")]
        [HttpPost]
        public void Post([FromBody] string cardString)
        {
            FlashCardModel card = JsonConvert.DeserializeObject<FlashCardModel>(cardString);
            var client = new MongoClient("mongodb://bwolky:Bw2189114.@ds019980.mlab.com:19980/flashcardsdb");
            IMongoDatabase db = client.GetDatabase("flashcardsdb");
            IMongoCollection<BsonDocument> collection = db.GetCollection<BsonDocument>("flashcards");
            collection.InsertOne(card.ToBsonDocument());

        }

    }
}
