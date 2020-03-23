using MongoDB.Driver;

namespace DAL
{
    public class MongoDbStorage<T>
    {
        private static readonly object SyncObject = new object();
        private static string connectionString = "mongodb://localhost:27017";
        private IMongoClient dbClient;
        private IMongoDatabase database;

        private MongoDbStorage<T> modelContext;

        private static MongoDbStorage<T> instance;

        public static MongoDbStorage<T> Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (SyncObject)
                    {
                        if (instance == null)
                        {
                            instance = new MongoDbStorage<T>();
                        }
                    }
                }

                return instance;
            }
        }

        public MongoDbStorage()
        {
            if (modelContext == null)
            {
                modelContext = this;
                modelContext.dbClient = new MongoClient(connectionString);
                modelContext.database = modelContext.dbClient.GetDatabase("Persons");
                Collection = database.GetCollection<T>("Persons");
            }
        }

        public IMongoCollection<T> Collection { get; set; }
    }
}
