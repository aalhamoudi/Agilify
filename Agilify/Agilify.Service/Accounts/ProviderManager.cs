using System.Web.Http;
using System.Web.Http.Tracing;
using Microsoft.Azure.Mobile.Server;
using Microsoft.Azure.Mobile.Server.Config;
using AgilifyService.Models;
using System.Threading.Tasks;
using System.Security.Claims;
using System.Linq;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;
using System;
using System.Text;
using System.Security.Cryptography;
using System.IO;
using System.Security.Principal;
using AgilifyService.Data;
using Microsoft.Azure.Mobile.Server.Authentication;

namespace AgilifyService.Accounts
{
    public class ProviderManager
    {
        const string TwitterConsumerKey = "P66l1LeOkTZXM7c0Bto64Xw4b";
        const string TwitterConsumerSecret = "4SMrE98O7UcIVLJ87lZ5o0n4aXUXPECYSpHBxTaQimzfh5jcCK";

        public AgilifyContext DbContext { get; set; } = new AgilifyContext();
        public HttpRequestMessage Request { get; set; }
        public IPrincipal User { get; set; }

        public ProviderManager(HttpRequestMessage request, IPrincipal user)
        {
            Request = request;
            User = user;
        }

        public async Task<UserBindingModel> Get()
        {
            string provider, secret, token;
            GetAccessToken(out provider, out secret, out token);

            var user = new UserBindingModel();

            switch (provider)
            {
                case "facebook":
                    using (HttpClient client = new HttpClient())
                    {
                        using (HttpResponseMessage response = await client.GetAsync("https://graph.facebook.com/me" + "?access_token=" + token))
                        {
                            var o = JObject.Parse(await response.Content.ReadAsStringAsync());
                            user.Name = o["name"].ToString();
                            user.Email = GetEmailAddress(await GetUserCredentials("Facebook"));
                        }
                        using (HttpResponseMessage response = await client.GetAsync("https://graph.facebook.com/me" + "/picture?redirect=false&access_token=" + token))
                        {
                            var x = JObject.Parse(await response.Content.ReadAsStringAsync());
                            user.Image = (x["data"]["url"].ToString());
                        }
                    }
                    break;
                case "google":
                    using (HttpClient client = new HttpClient())
                    {
                        client.DefaultRequestHeaders.Authorization = AuthenticationHeaderValue.Parse("Bearer " + token);
                        using ( HttpResponseMessage response = await client.GetAsync("https://www.googleapis.com/oauth2/v2/useruser"))
                        {
                            var o = JObject.Parse(await response.Content.ReadAsStringAsync());
                            user.Name = o["name"].ToString();
                            user.Email = GetEmailAddress(await GetUserCredentials("Google"));
                            user.Image = (o["picture"].ToString());
                        }
                    }
                    break;
                case "twitter":

                    //generating signature as of https://dev.twitter.com/oauth/overview/creating-signatures Jump
                    string nonce = GenerateNonce();
                    string s = "oauth_consumer_key=" + TwitterConsumerKey + "&oauth_nonce=" +
                               nonce +
                               "&oauth_signature_method=HMAC-SHA1&oauth_timestamp=" +
                               DateTimeToUnixTimestamp(DateTime.Now) + "&oauth_token=" + token +
                               "&oauth_version=1.0";
                    string sign = "GET" + "&" +
                                  Uri.EscapeDataString("https://api.twitter.com/1.1/account/verify_credentials.json") +
                                  "&" + Uri.EscapeDataString(s);
                    string sec = Uri.EscapeDataString(TwitterConsumerSecret) + "&" + Uri.EscapeDataString(secret);
                    byte[] key = Encoding.ASCII.GetBytes(sec);
                    string signature = Uri.EscapeDataString(Encode(sign, key));

                    using (HttpClient client = new HttpClient())
                    {

                        client.DefaultRequestHeaders.Authorization = AuthenticationHeaderValue.Parse("OAuth oauth_consumer_key =\"" + TwitterConsumerKey +
                                                            "\",oauth_signature_method=\"HMAC-SHA1\",oauth_timestamp=\"" +
                                                            DateTimeToUnixTimestamp(DateTime.Now) + "\",oauth_nonce=\"" +
                                                            nonce +
                                                            "\",oauth_version=\"1.0\",oauth_token=\"" + token +
                                                            "\",oauth_signature =\"" + signature + "\"");
                        using ( HttpResponseMessage response = await client.GetAsync("https://api.twitter.com/1.1/account/verify_credentials.json"))
                        {
                            var o = JObject.Parse(await response.Content.ReadAsStringAsync());
                            user.Name = o["name"].ToString();
                            user.Email = GetEmailAddress(await GetUserCredentials("Twitter"));
                            user.Image = (o["profile_image_url"].ToString());
                        }
                    }
                    break;
                case "microsoftaccount":
                    using (HttpClient client = new HttpClient())
                    {
                        using (HttpResponseMessage response = await client.GetAsync("https://apis.live.net/v5.0/me" + "?access_token=" + token))
                        {
                            var o = JObject.Parse(await response.Content.ReadAsStringAsync());
                            user.Name = o["name"].ToString();
                            user.Email = GetEmailAddress(await GetUserCredentials("MicrosoftAccount"));
                        }
                    }
                    using (HttpClient client = new HttpClient())
                    {
                        using (HttpResponseMessage response = await client.GetAsync("https://apis.live.net/v5.0/me" + "/picture?suppress_redirects=true&type=medium&access_token=" + token))
                        {
                            var o = JObject.Parse(await response.Content.ReadAsStringAsync());
                            user.Image = o["location"].ToString();
                        }
                    }
                    break;
            }

            

            return user;

        }

        public async Task<string> Get(string property)
        {
            switch (property)
            {
                case "email":
                {
                    return GetEmailAddress(await GetUserCredentials("MicrosoftAccount"));
                }
            }
            return "Error";
        }


        private async Task<ProviderCredentials> GetUserCredentials(string provider)
        {
            ProviderCredentials credentials = null;

            switch (provider)
            {
                case "MicrosoftAccount":
                    credentials = await this.User.GetAppServiceIdentityAsync<MicrosoftAccountCredentials>(this.Request);
                    break;
                case "Facebook":
                    credentials = await this.User.GetAppServiceIdentityAsync<FacebookCredentials>(this.Request);
                    break;
                case "Twitter":
                    credentials = await this.User.GetAppServiceIdentityAsync<TwitterCredentials>(this.Request);
                    break;
                case "Google":
                    credentials = await this.User.GetAppServiceIdentityAsync<GoogleCredentials>(this.Request);
                    break;

            }

            return credentials;
        }

        private string GetEmailAddress(ProviderCredentials credentials)
        {
            return credentials.UserClaims
                .Where(claim => claim.Type.Contains("email"))
                .First<Claim>()
                .Value;
        }

        private void GetAccessToken(out string provider, out string secret, out string token)
        {
            
            var serviceUser = this.User as ClaimsPrincipal;
            var idP = serviceUser.FindFirst("http://schemas.microsoft.com/identity/claims/identityprovider").Value;
            provider = idP;
            token = secret = "";

            switch (idP)
            {
                case "facebook":
                    token = Request.Headers.GetValues("X-MS-TOKEN-FACEBOOK-ACCESS-TOKEN").FirstOrDefault();
                    break;
                case "google":
                    token = Request.Headers.GetValues("X-MS-TOKEN-GOOGLE-ACCESS-TOKEN").FirstOrDefault();
                    break;
                case "microsoftaccount":
                    token = Request.Headers.GetValues("X-MS-TOKEN-MICROSOFTACCOUNT-ACCESS-TOKEN").FirstOrDefault();
                    break;
                case "twitter":
                    token = Request.Headers.GetValues("X-MS-TOKEN-TWITTER-ACCESS-TOKEN").FirstOrDefault();
                    secret = Request.Headers.GetValues("X-MS-TOKEN-TWITTER-ACCESS-TOKEN-SECRET").FirstOrDefault();
                    break;
            }
        }

        public static string Encode(string input, byte[] key)
        {
            HMACSHA1 myhmacsha1 = new HMACSHA1(key);
            byte[] byteArray = Encoding.ASCII.GetBytes(input);
            MemoryStream stream = new MemoryStream(byteArray);
            return Convert.ToBase64String(myhmacsha1.ComputeHash(stream));
        }

        public static long DateTimeToUnixTimestamp(DateTime dateTime)
        {
            return (long)(TimeZoneInfo.ConvertTimeToUtc(dateTime) -
                           new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc)).TotalSeconds;
        }

        public static string GenerateNonce()
        {
            return new Random()
                .Next(123400, int.MaxValue)
                .ToString("X");
        }
    }
}
