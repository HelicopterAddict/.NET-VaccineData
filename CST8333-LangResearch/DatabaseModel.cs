//<Author = Anoopbir Singh Sidhu>
//<Sno = 040984994>
//Code derived from https://www.youtube.com/watch?v=69WBy4MHYUw

using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Generic;

namespace Model
{
    /// <summary>
    /// Class for persistence methods i.e. communication with the database
    /// </summary>
    public class DatabaseModel
    {
        /// <summary>
        /// Database object of the IMongoDatabase provided by the Mongo nu.get package
        /// </summary>
        private IMongoDatabase db;

        /// <summary>
        /// Name of the collection(table) to perform CRUD on
        /// </summary>
        const string table = "VaccineData2";

        /// <summary>
        /// Constructor which initializes the IMongoDatabase
        /// </summary>
        /// <param name="database">Database to access the collection</param>
        public DatabaseModel(string database)
        {
            var client = new MongoClient();
            db = client.GetDatabase(database);
        }

        /// <summary>
        /// Method to insert a record of type T into the database, Validation done in VaccineView <see cref="View.VaccineView"/>
        /// </summary>
        /// <typeparam name="T">For current scope of this project T is confined to VaccineData <see cref="VaccineData"/></typeparam>
        /// <param name="record">The record to be inserted</param>
        public void InsertRecord<T>(T record)
        {
            var collection = db.GetCollection<T>(table);
            collection.InsertOne(record);
        }

        /// <summary>
        /// Loads records
        /// </summary>
        /// <typeparam name="T">For current scope of this project T is confined to VaccineData <see cref="VaccineData"/></typeparam>
        /// <returns>All found records in a list format</returns>
        public List<T> LoadRecords<T>()
        {
            var collection = db.GetCollection<T>(table);
            return collection.Find(new BsonDocument()).ToList();
        }

        /// <summary>
        /// Loads a record with the provided Id
        /// </summary>
        /// <typeparam name="T">For current scope of this project T is confined to VaccineData <see cref="VaccineData"/></typeparam>
        /// <param name="id">The id of the record to be found</param>
        /// <returns>The first record with that Id</returns>
        public T LoadRecordById<T>(ObjectId id)
        {
            var collection = db.GetCollection<T>(table);
            var filter = Builders<T>.Filter.Eq("Id", id);

            return collection.Find(filter).First();
        }

        /// <summary>
        /// Updates an existing record, can also be used to insert new records (not used for inserting in this project)
        /// </summary>
        /// <typeparam name="T">For current scope of this project T is confined to VaccineData <see cref="VaccineData"/></typeparam>
        /// <param name="id">The id of the record to be found</param>
        /// <param name="record"></param>
        public void UpdateRecord<T>(ObjectId id, T record)
        {
            var collection = db.GetCollection<T>(table);
            collection.ReplaceOne(new BsonDocument("_id", id), record, new ReplaceOptions { IsUpsert = true });
        }

        /// <summary>
        /// Deletes a record with the matching Id
        /// </summary>
        /// <typeparam name="T">For current scope of this project T is confined to VaccineData <see cref="VaccineData"/></typeparam>
        /// <param name="id">The id of the record to be found</param>
        public void DeleteRecord<T>(ObjectId id)
        {
            var collection = db.GetCollection<T>(table);
            var filter = Builders<T>.Filter.Eq("Id", id);
            collection.DeleteOne(filter);
        }

        /// <summary>
        /// Returns the value of an object with a particular Id in a JSON string format
        /// </summary>
        /// <typeparam name="T">For current scope of this project T is confined to VaccineData <see cref="VaccineData"/></typeparam>
        /// <param name="id">The id of the object</param>
        /// <returns>The value of the object in String JSON form</returns>
        public string GetJsonById<T>(ObjectId id)
        {
            return LoadRecordById<T>(id).ToJson();
        }
    }
}
