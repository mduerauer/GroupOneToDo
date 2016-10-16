using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http.Formatting;
using GroupOneToDo.Model;
using System.Net.Http;
using NLog;
using System.Net.Http.Headers;

namespace GroupOneToDo.Service.Repository
{
    public class WebApiToDoRepository : IToDoRepository
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        private readonly string _baseUrl;

        private readonly HttpClient _httpClient = new HttpClient();

        public WebApiToDoRepository(string baseUrl)
        {
            this._baseUrl = baseUrl;

            Logger.Debug("Initializing WebAPI ToDo Repostiory with baseUrl {0}", _baseUrl);

            Init();
        }

        private void Init()
        {
            _httpClient.BaseAddress = new Uri(_baseUrl);
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<ToDo> DeleteById(Guid id)
        {
            ToDo todo = await GetById(id);
            HttpResponseMessage response = await _httpClient.DeleteAsync(_baseUrl + "/" + id.ToString());

            if (!response.IsSuccessStatusCode)
            {
                todo = null;
            }
            return todo;
        }

        public async Task<ICollection<ToDo>> FindAll()
        {
            ICollection<ToDo> todos = null;
            HttpResponseMessage response = await _httpClient.GetAsync(_baseUrl);
            if (response.IsSuccessStatusCode)
            {
                todos = await response.Content.ReadAsAsync<ICollection<ToDo>>();
            }
            return todos;
        }

        public async Task<ToDo> GetById(Guid id)
        {
            ToDo todo = null;
            HttpResponseMessage response = await _httpClient.GetAsync(_baseUrl + "/" + id.ToString());
            if (response.IsSuccessStatusCode)
            {
                todo = await response.Content.ReadAsAsync<ToDo>();
            }
            return todo;
        }

        public async Task<ToDo> Create(ToDo entity)
        {
            HttpResponseMessage response = await _httpClient.PostAsJsonAsync(_baseUrl, entity);
            response.EnsureSuccessStatusCode();
            
            return entity;
        }

        public async Task<ToDo> Update(ToDo entity)
        {
            HttpResponseMessage response = await _httpClient.PutAsJsonAsync(_baseUrl + "/" + entity.Id.ToString(), entity);
            response.EnsureSuccessStatusCode();
            
            return entity;
        }
    }
}
