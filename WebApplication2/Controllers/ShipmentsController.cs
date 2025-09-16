using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using static System.Net.WebRequestMethods;
using Newtonsoft.Json;
using WebApplication2.Model;
using Microsoft.AspNetCore.WebUtilities;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApplication2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShipmentsController : ControllerBase
    {
        public string proxyAddress = "https://login.hillebrand.com/oauth2/aus95hq7r8iIqp14M0i7/v1/token";
        public string proxyUserName = "dashti81@gmail.com";
        public string proxyPassword = "(op*1*9B}8M2";
        public HttpClientHandler httpHandler;
        private readonly ILogger<ShipmentsController> _logger;

        public  ShipmentsController(ILogger<ShipmentsController> logger)
        {
            _logger = logger;
            var proxy = new WebProxy
            {
                Address = new Uri(proxyAddress),
                Credentials = new NetworkCredential(proxyUserName, proxyPassword),
                UseDefaultCredentials = false // Explicitly set to false when providing custom credentials
            };

            httpHandler = new HttpClientHandler
            {
                Proxy = proxy,
                UseProxy = true
            };
        }
        // GET: api/<ShipmentsController>
        [HttpGet]
        public async Task<ActionResult<ShipmentResponse>> Get([FromQuery] string mainModality, [FromQuery]int pagination )
        {
            try
            {
                ShipmentResponse shipmentResponse;
                if (Request.Headers.ContainsKey("Authorization"))
                {

                    string authorizationHeader = Request.Headers["Authorization"];


                    if (AuthenticationHeaderValue.TryParse(authorizationHeader, out var headerValue))
                    {
                        if (headerValue.Scheme == "Bearer")
                        {
                            string token = headerValue.Parameter;
                            var client = new HttpClient();
                            client.DefaultRequestHeaders.Add("Accept", "application/json");
                            client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", token);

                            var query = new Dictionary<string, string>
                            {
                                ["mainModality"] = mainModality,
                                ["pageNumber"] = pagination.ToString(),
                            };

                            


                            var response = await client.GetAsync(QueryHelpers.AddQueryString("https://api.hillebrandgori.com/v6/shipments",query));

                            string responseBody = await response.Content.ReadAsStringAsync();

                            shipmentResponse = JsonConvert.DeserializeObject<ShipmentResponse>(responseBody);


                            _logger.LogInformation(response.StatusCode.ToString());

                            return Ok(shipmentResponse);

                        }
                        else
                        {
                            return NotFound();
                        }
                    }
                    else
                    {
                        return NotFound();
                    }
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // GET api/<ShipmentsController>/5
        [HttpGet("{id}")]
        public  async Task<ActionResult<ShipmentResponse>> GetAsync(int id)
        {
            try
            {
                ShipmentResponse shipmentResponse;
                if (Request.Headers.ContainsKey("Authorization"))
                {

                    string authorizationHeader = Request.Headers["Authorization"];


                    if (AuthenticationHeaderValue.TryParse(authorizationHeader, out var headerValue))
                    {
                        if (headerValue.Scheme == "Bearer")
                        {
                            string token = headerValue.Parameter;

                            var client = new HttpClient();
                            client.DefaultRequestHeaders.Add("Accept", "application/json");
                            client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", token);



                            var response = await client.GetAsync($"https://api.hillebrandgori.com/v6/shipments/{id}");
                            string responseBody = await response.Content.ReadAsStringAsync();

                            shipmentResponse = JsonConvert.DeserializeObject<ShipmentResponse>(responseBody);


                            _logger.LogInformation(response.StatusCode.ToString());

                            return Ok(shipmentResponse);
                        }
                        else
                        {
                            return NotFound();
                        }
                    }
                    else
                    {
                        return NotFound();
                    }
                }
                else
                {
                    return NotFound();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
             
        }

        // POST api/<ShipmentsController>
        [HttpPost]
        public async Task<TokenResponse> Post([FromBody] string value)
        {
            try
            {
                var client = new HttpClient();
                client.DefaultRequestHeaders.Add("Accept", "application/json");
                client.DefaultRequestHeaders.Authorization =
        new AuthenticationHeaderValue("Basic", "MG9hYjJkdmpudmJiQ1VqTGswaTc6TkJpbENtNC1IQkp3VVZ3TFhmejloSklPTjdSZ2dQZkc3MnFIQkdiMGFCWXRFQ2ZhZGJmZUItanlSTXlDaGY3Vg==");
                var content = new FormUrlEncodedContent(new Dictionary<string, string>
        {
            { "grant_type", "password" },
            //    {"client_id","0oab2dvjnvbbCUjLk0i7" },
            //    {"client_secret","NBilCm4-HBJwUVwLXfz9hJION7RggPfG72qHBGb0aBYtECfadbfeB-jyRMyChf7V" },
            { "username", "dashti81@gmail.com" },
            { "password", "(op*1*9B}8M2" },
            { "scope", "offline_access" }
        });



                var response = await client.PostAsync(proxyAddress, content);
                string responseBody = await response.Content.ReadAsStringAsync();
                var token = JsonConvert.DeserializeObject<TokenResponse>(responseBody);

                _logger.LogInformation(response.StatusCode.ToString());


                return token;
            }
            catch(Exception ex)
            {
                throw ex;
            }
            
        }

        // PUT api/<ShipmentsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ShipmentsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
