using VidottiStore.Data.Repositories;
using VidottiStore.Domain;
using VidottiStore.Domain.Contract;
using VidottiStore.Utils.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace VidottiStore.Api.Controllers
{
    [RoutePrefix("api/public/v1")]  // mudando a rota padrão  para esse controller 
    public class BookController : ApiController
    {

        private IBookRepository _repository;       // usando a DI

        public BookController(IBookRepository repository)   // usando DI
        {
            this._repository = repository;
        }

        #region[Read]

        [Route("livros")]       // rota para esse metodo    api/api/public/v1/livros
        [DeflateCompression]    // comprimento os pacotes, esse atribito esta na classe DeflateCompressionAttribute
                                // [CacheOutput(ClientTimeSpan =100, ServerTimeSpan = 100)]    // Cacheando a Aplicação com tempo de 100 min. no client e no server
        [BasicAuthentication]
        public Task<HttpResponseMessage> Get()
        {
            var _response = new HttpResponseMessage();

            try
            {
                // var livros = _repository.GET();   // vai fazer a busca no banco

                var livros = _repository.GetWithAuthors();   // vai fazer a busca no banco
                if (livros != null && livros.Count > 0)  // se livors vor diferente nullo e maior que  0                
                    _response = Request.CreateResponse(HttpStatusCode.OK, livros); // retotorna o resultado e  manda uma mennsagem de ok com os objetos livros 

                else
                    _response = Request.CreateResponse(HttpStatusCode.NoContent, "Nenhum livro foi encontrado"); // se não encontrar nenhum livro ou for nullo

            }
            catch (Exception)
            {
                _response = Request.CreateResponse(HttpStatusCode.InternalServerError, "Falha ao se  conectar ao banco de dados");   // erro no babco de dados 
            }

            var tsc = new TaskCompletionSource<HttpResponseMessage>();
            tsc.SetResult(_response);
            return tsc.Task;
        }

        #endregion

        #region[Create]

        [HttpPost]
        [Route("livros")]
        public Task<HttpResponseMessage> Post(Book book)
        {
            var response = new HttpResponseMessage();

            try
            {
                _repository.Create(book);
                response = Request.CreateResponse(HttpStatusCode.Created, book);
            }
            catch
            {
                response = Request.CreateResponse(HttpStatusCode.BadRequest, "fallha ao criar o livro");
                throw;
            }

            var tsc = new TaskCompletionSource<HttpResponseMessage>();
            tsc.SetResult(response);
            return tsc.Task;
        }

        #endregion

        #region[upDate]

        [HttpPut]
        [Route("livros")]
        public Task<HttpResponseMessage> Put(Book book)
        {
            var response = new HttpResponseMessage();

            try
            {
                _repository.Update(book);
                response = Request.CreateResponse(HttpStatusCode.OK, book);
            }
            catch (Exception)
            {
                response = Request.CreateResponse(HttpStatusCode.BadRequest, "Falha ao Atualizar o livro");
                throw;
            }

            var tsc = new TaskCompletionSource<HttpResponseMessage>();
            tsc.SetResult(response);
            return tsc.Task;
        }

        #endregion

        #region[delete]

        [HttpDelete]
        [Route("livros/{id}")]
        public Task<HttpResponseMessage> Delete(int id)
        {
            var response = new HttpResponseMessage();

            try
            {
                _repository.Delete(id);
                response = Request.CreateResponse(HttpStatusCode.OK, "Livro removido com sucesso");
            }
            catch (Exception)
            {
                response = Request.CreateResponse(HttpStatusCode.BadRequest, "Fallha ao tentar remover o livro");
                throw;
            }


            var tsc = new TaskCompletionSource<HttpResponseMessage>();
            tsc.SetResult(response);
            return tsc.Task;
        }

        #endregion

        protected override void Dispose(bool disposing)
        {
            _repository.Dispose();
            base.Dispose(disposing);
        }
    }
}