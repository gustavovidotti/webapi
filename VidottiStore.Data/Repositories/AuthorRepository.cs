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
    public class AuthorRepository : IAuthorRepository
    {
        private VidottiStoreDataContext _db;

        public AuthorRepository()
        {
            _db = new VidottiStoreDataContext();
        }


        public void Create(Author entity)
        {
            _db._Authors.Add(entity);
            _db.SaveChanges();
        }

        public void Delete(int id)
        {
            _db._Authors.Remove(_db._Authors.Find(id));
            _db.SaveChanges();
        }

        public void Dispose()
        {
            _db.Dispose();
        }

        public Author Get(int id)
        {
            return _db._Authors.Find(id);
        }

        public List<Author> GET(int skip = 0, int take = 25)
        {
            return _db._Authors.OrderBy(x => x.FirstName).Skip(skip).Take(take).ToList();
        }

        public void Update(Author entity)
        {
            _db.Entry<Author>(entity).State = EntityState.Modified;
            _db.SaveChanges();
        }
    }
}
