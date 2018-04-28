using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VidottiStore.Data.DataContexts;
using VidottiStore.Domain;
using VidottiStore.Domain.Contract;

namespace VidottiStore.Data.Repositories
{
    public class BookRepository : IBookRepository
    {
        private VidottiStoreDataContext _db;

        public BookRepository()
        {
            _db = new VidottiStoreDataContext();
        }

        public void Create(Book entity)
        {
            _db._Books.Add(entity);
            _db.SaveChanges();
        }

        public void Delete(int id)
        {
            _db._Books.Remove(_db._Books.Find(id));
            _db.SaveChanges();
        }

        public void Dispose()
        {
            _db.Dispose();
        }

        public Book Get(int id)
        {
            return _db._Books.Find(id);
        }

        public List<Book> GET(int skip = 0, int take = 25)
        {
            return _db._Books.OrderBy(x => x.Title).Skip(skip).Take(take).ToList();
        }

        public Book GetWithAuthors(int id)
        {
            throw new NotImplementedException();
        }

        public List<Book> GetWithAuthors(int skip = 0, int take = 25)
        {
            return _db._Books.Include(x => x.Authors).OrderBy(x => x.Title).Skip(skip).Take(take).ToList();
            // pode-se usar .Include("Authors) ou include(x => x.Authors)  se chorar adicionar system.data.entity

        }

        public void Update(Book entity)
        {
            _db.Entry<Book>(entity).State = EntityState.Modified;
            _db.SaveChanges();
        }
    }
}
