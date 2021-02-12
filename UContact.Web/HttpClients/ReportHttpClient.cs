using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using UContact.Common.Pager;
using UContact.Web.Models.Persons;
using UContact.Web.Models.Reports;

namespace UContact.Web.HttpClients
{
    public class ReportHttpClient
    {
        private readonly HttpClient _httpClient;
        public ReportHttpClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<ReportViewModel>> GetReports(int take, int skip)
        {
            var response = await _httpClient.GetAsync($"api/v1/myreports?take={take}&skip={skip}");

            if (response.IsSuccessStatusCode)
                return await response.Content.ReadFromJsonAsync<List<ReportViewModel>>();

            return null;
        }

        public async Task<ReportViewModel> GetReportById(Guid id)
        {
            var response = await _httpClient.GetAsync($"api/v1/myreports/{id}");
            if (response.IsSuccessStatusCode)
                return await response.Content.ReadFromJsonAsync<ReportViewModel>();

            return null;
        }
        public async Task<ReportViewModel> CreateReport(ReportViewModel model)
        {
            var response = await _httpClient.PostAsJsonAsync("api/v1/myreports", model);
            return await response.Content.ReadFromJsonAsync<ReportViewModel>();
        }
    }
}
