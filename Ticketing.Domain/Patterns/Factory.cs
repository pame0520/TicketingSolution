using System;
using System.IO;
using Ticketing.Domain.Data;

namespace Ticketing.Patterns
{
    public static class RepositoryFactory
    {
        
        public static IJsonRepository<T> Create<T>(string path) where T : class
        {
            return new JsonFileRepository<T>(path);
        }
    }
}
