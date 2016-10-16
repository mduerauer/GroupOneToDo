using System;
using System.Collections.Generic;
using GroupOneToDo.Model;
using System.Net;
using System.Threading.Tasks;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using NLog;

namespace GroupOneToDo.Service.Repository
{
    public class DocumentDbToDoRepository : IToDoRepository
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        private const string DatabaseName = "todoDatabase";

        private const string CollectionName = "todoCollection";

        private readonly IDocumentClient _documentClient;

        public DocumentDbToDoRepository(string endpointUri, string primaryKey)
        {
            _documentClient = new DocumentClient(new Uri(endpointUri), primaryKey);

            Logger.Info("Creating Database if not exists.");
            var dbResponse = CreateDatabaseIfNotExists(DatabaseName).Result;

            Logger.Info("Creating DocumentCollection if not exists.");
            var collectionResponse = CreateDocumentCollectionIfNotExists(DatabaseName, CollectionName).Result;

        }
        
        public async Task<ToDo> GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<ICollection<ToDo>> FindAll()
        {
            throw new NotImplementedException();
        }

        public async Task<ToDo> Create(ToDo entity)
        {
            throw new NotImplementedException();
        }

        public async Task<ToDo> Update(ToDo entity)
        {
            throw new NotImplementedException();
        }

        public async Task<ToDo> DeleteById(Guid id)
        {
            throw new NotImplementedException();
        }

        private async Task<ResourceResponse<Database>>  CreateDatabaseIfNotExists(string databaseName)
        {
            ResourceResponse<Database> result;

            // Check to verify a database with the id=FamilyDB does not exist
            try
            {
                result = await _documentClient.ReadDatabaseAsync(UriFactory.CreateDatabaseUri(databaseName)).ConfigureAwait(false);
                Logger.Info("Found {0}", databaseName);
            }
            catch (DocumentClientException de)
            {
                // If the database does not exist, create a new database
                if (de.StatusCode == HttpStatusCode.NotFound)
                {
                    result = await _documentClient.CreateDatabaseAsync(new Database { Id = databaseName }).ConfigureAwait(false);
                    Logger.Info("Created {0}", databaseName);
                }
                else
                {
                    throw;
                }
            }

            return result;
        }

        private async Task<ResourceResponse<DocumentCollection>> CreateDocumentCollectionIfNotExists(string databaseName, string collectionName)
        {
            ResourceResponse<DocumentCollection> result;

            try
            {
                result = await _documentClient.ReadDocumentCollectionAsync(UriFactory.CreateDocumentCollectionUri(databaseName, collectionName)).ConfigureAwait(false);
                Logger.Info("Found {0}", collectionName);
            }
            catch (DocumentClientException de)
            {
                // If the document collection does not exist, create a new collection
                if (de.StatusCode == HttpStatusCode.NotFound)
                {
                    var collectionInfo = new DocumentCollection
                    {
                        Id = collectionName,
                        IndexingPolicy = new IndexingPolicy(new RangeIndex(DataType.String) {Precision = -1})
                    };

                    // Optionally, you can configure the indexing policy of a collection. Here we configure collections for maximum query flexibility 
                    // including string range queries. 

                    // DocumentDB collections can be reserved with throughput specified in request units/second. 1 RU is a normalized request equivalent to the read
                    // of a 1KB document.  Here we create a collection with 400 RU/s. 
                    result = await _documentClient.CreateDocumentCollectionAsync(
                        UriFactory.CreateDatabaseUri(databaseName),
                        new DocumentCollection { Id = collectionName },
                        new RequestOptions { OfferThroughput = 400 }).ConfigureAwait(false);

                    Logger.Info("Created {0}", collectionName);
                }
                else
                {
                    throw;
                }
            }

            return result;
        }
    }
}
