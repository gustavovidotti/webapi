using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VidottiStore.Domain.Contract
{
    public interface IBookRepository : IRepository<Book>
    {
        List<Book> GetWithAuthors(int skip = 0, int take = 25);     // trazer com os autores vai ser utlizado para uso de metodo aninhado
        Book GetWithAuthors(int id);
    }
}
