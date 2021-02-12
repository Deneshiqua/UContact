using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using UContact.Common.Pager;
using UContact.Web.Models.Persons;

namespace UContact.Web.HttpClients
{
    public class PersonHttpClient
    {
        private readonly HttpClient _httpClient;
        public PersonHttpClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<PersonViewModel>> GetPersons(int take, int skip)
        {
            var response = await _httpClient.GetAsync($"api/v1/mycontacts?take={take}&skip={skip}");

            if (response.IsSuccessStatusCode)
                return await response.Content.ReadFromJsonAsync<List<PersonViewModel>>();

            return null;
        }

        public async Task<PersonViewModel> GetPersonById(Guid id)
        {
            var response = await _httpClient.GetAsync($"api/v1/mycontacts/{id}");
            if (response.IsSuccessStatusCode)
                return await response.Content.ReadFromJsonAsync<PersonViewModel>();

            return null;
        }

        public async Task<PersonViewModel> CreateReport(PersonViewModel model)
        {
            var response = await _httpClient.PostAsJsonAsync("api/v1/mycontacts", model);
            return await response.Content.ReadFromJsonAsync<PersonViewModel>();
        }

        public async Task<PersonViewModel> UpdatePerson(PersonViewModel model)
        {
            var response = await _httpClient.PutAsJsonAsync("api/v1/mycontacts", model);
            return await response.Content.ReadFromJsonAsync<PersonViewModel>();
        }

        public async Task<PersonViewModel> InsertPerson(PersonViewModel model)
        {
            var response = await _httpClient.PostAsJsonAsync("api/v1/mycontacts", model);
            return await response.Content.ReadFromJsonAsync<PersonViewModel>();
        }

        public async Task<bool> DeletePerson(Guid id)
        {
            var response = await _httpClient.DeleteAsync($"api/v1/mycontacts/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}
