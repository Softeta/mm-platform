using API.WebClients.Clients.DanishCompaniesService.Models;
using API.WebClients.Constants;
using Domain.Seedwork.Exceptions;
using Newtonsoft.Json;
using System.Text;

namespace API.WebClients.Clients.DanishCompaniesService
{
    public class DasnishCvrWebApiClient : IDasnishCvrWebApiClient
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public DasnishCvrWebApiClient(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<CrvServiceResponse?> GetAsync(int pageSize, string search, string[]? searchAfter)
        {
            var httpClient = _httpClientFactory.CreateClient(ApiClients.DanishRegistryCenterClient);

            var requestJson = BuildRequestJson(pageSize, search, searchAfter);
            var payload = new StringContent(requestJson, Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync(string.Empty, payload);

            return await GetResponse(response);
        }

        private static string BuildRequestJson(int pageSize, string search, string[]? searchAfter)
        {
            var query = new QueryRequest
            {
                Size = pageSize,
                SearchAfter = searchAfter,
                Sort = new Sort
                {
                    Field = SortType.Asc
                },
                Query = new Query
                {
                    Bool = new MustBool
                    {
                        Must = new Must[]
                        {
                            new Must
                            {
                                Bool = new ShouldBool
                                {
                                    Should = new Should[]
                                    {
                                        new Should
                                        {
                                            Match = new CompanyTypeMatch
                                            {
                                                Value = CompanyTypes.IVS
                                            }
                                        },
                                        new Should
                                        {
                                            Match = new CompanyTypeMatch
                                            {
                                                Value = CompanyTypes.ApS
                                            }
                                        },
                                        new Should
                                        {
                                            Match = new CompanyTypeMatch
                                            {
                                                Value = CompanyTypes.AS
                                            }
                                        }
                                    }
                                }
                            },
                            new Must
                            {
                                Bool = new ShouldBool
                                {
                                    Should = new Should[]
                                    {
                                        new Should
                                        {
                                            Match = new CountryCodeMatch
                                            {
                                                Value = CountryCodes.DK
                                            }
                                        }
                                    }
                                }
                            },
                            new Must
                            {
                                Bool = new ShouldBool
                                {
                                    Should = new Should[]
                                    {
                                        new Should
                                        {
                                            Match = new CvrNumberMatch
                                            {
                                                Value = search
                                            }
                                        },
                                        new PhraseMatchPrefixShould
                                        {
                                            Match = new NamePhrasePrefixMatch
                                            {
                                                Value = new MatchQuery
                                                {
                                                    Query = search
                                                }
                                            }
                                        } 
                                    }
                                }
                            }
                        }
                    }
                },
                Source = new string[]
                {
                    Fields.LatestName,
                    Fields.RegistrationNumber,
                    Fields.Region,
                    Fields.Street,
                    Fields.HouseNumber,
                    Fields.ZipCode,
                    Fields.CountryCode,
                    Fields.City,
                    Fields.DoorNumber
                }
            };

            return JsonConvert.SerializeObject(query);
        }

        private static async Task<CrvServiceResponse?> GetResponse(HttpResponseMessage response)
        {
            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<CrvServiceResponse>(jsonResponse);
            }

            var responseContent = await response.Content.ReadAsStringAsync();
            throw new HttpException(responseContent, response.StatusCode, ErrorCodes.HttpRequest.DasnishCvrApiException);
        }
    }
}
