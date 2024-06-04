﻿using Authentication_API.Models;
using MongoDB.Driver;
using System.Collections;

namespace Authentication_API.Services.DataServices
{
    public class GuestService
    {
        private readonly IMongoCollection<Guest> _collection;

        public GuestService(IConfiguration config, MongoDbConnectionService service)
        {
            var connection_string = config.GetSection("MongoDB:TableGuests").Get<string>();
            _collection = service.Database.GetCollection<Guest>(connection_string);
        }

        public async Task<Guest> CreateAsync(Guest model)
        {
            await _collection.InsertOneAsync(model);
            return model;
        }
    }
}