using Bookstore.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bookstore.Services
{
    public class BookService
    {
        private readonly IMongoCollection<Book> _books;
        public BookService(IBookstoreDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _books = database.GetCollection<Book>(settings.BooksCollectionName);
        }
        public List<Book> GetAll() => _books.Find(x => true).ToList();
        public Book GetById(string id) => _books.Find(x => x.Id == id).FirstOrDefault();
        public Book Create(Book book)
        {
            _books.InsertOne(book);
            return book;
        }
        public void Update(string id, Book bookIn) =>
            _books.ReplaceOne(x => x.Id == id, bookIn);
        public void Remove(string id) => _books.DeleteOne(x => x.Id == id);
        public void Remove(Book book) => _books.DeleteOne(x => x.Id == book.Id);
    }
}
