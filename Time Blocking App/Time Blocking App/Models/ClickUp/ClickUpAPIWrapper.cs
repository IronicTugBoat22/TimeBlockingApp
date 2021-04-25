using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Time_Blocking_App.Controllers;
using Windows.Security.Authentication.Web;
using Xamarin.Essentials;

namespace Time_Blocking_App.Models.ClickUp
{
    public class ClickUpAPIWrapper : SingletonController<ClickUpAPIWrapper>
    {
        private static readonly string BaseUri = "https://app.clickup.com/api";
        private static string ClientId;
        private static string ClientSecret;
        private static string RedirectURL;

        private static HttpClient Client;
        private static string Token;

        public ClickUpAPIWrapper()
        {
            // Read in the client ID and redirect URI from the ClickUp.txt file in the project directory.
            //      File format should be 3 lines with no additional characters, as follows:
            //          Client ID on first line, Client Secret on second line, Redirect URL on third line.
            ClickUpAPIWrapper.ClientId = ClickUpCredentials.GetClientID();
            ClickUpAPIWrapper.ClientSecret = ClickUpCredentials.GetClientSecret();
            ClickUpAPIWrapper.RedirectURL = ClickUpCredentials.GetRedirectUri();

            ClickUpAPIWrapper.Client = new HttpClient();
            ClickUpAPIWrapper.Client.BaseAddress = new Uri(ClickUpAPIWrapper.BaseUri);
        }

        public Uri GetOauthEndpoint()
        {
            // Create the start URI
            string startUrl = ClickUpAPIWrapper.BaseUri +
                                "?client_id=" + ClickUpAPIWrapper.ClientId +
                                "&redirect_uri=" + ClickUpAPIWrapper.RedirectURL;
            return new Uri(startUrl);
        }

        public async Task<Uri> StartOAuthAsync()
        {
            Uri startUri = this.GetOauthEndpoint();

            await Browser.OpenAsync(startUri);
            return startUri;
        }

        public async Task GetOauthTokenAsync(string authorizationCode)
        {
            string tokenUrl = "api/v2/oauth/token?" +
                                "client_id=" + ClickUpAPIWrapper.ClientId +
                                "client_secret=" + ClickUpAPIWrapper.ClientSecret +
                                "code=" + authorizationCode;

            string responseBody = "";
            try
            {
                HttpResponseMessage response = await ClickUpAPIWrapper.Client.PostAsync(tokenUrl, null);
                response.EnsureSuccessStatusCode();
                responseBody = await response.Content.ReadAsStringAsync();
            }
            catch (Exception ex)
            {

            }

            // Parse out the token.
            System.Diagnostics.Debug.WriteLine(responseBody);
        }

        public async Task StartOAuthAsync2()
        {
            // Create the start and end URLs
            string startUrl = ClickUpAPIWrapper.BaseUri +
                                "?client_id=" + ClickUpAPIWrapper.ClientId +
                                "&redirect_uri=" + ClickUpAPIWrapper.RedirectURL;
            Uri startUri = new Uri(startUrl);
            Uri endUri = new Uri(ClickUpAPIWrapper.RedirectURL);

            string result = "";

            try
            {
                var webAuthenticationResult = await WebAuthenticationBroker.AuthenticateAsync(
                    WebAuthenticationOptions.None,
                    startUri,
                    endUri);

                switch (webAuthenticationResult.ResponseStatus)
                {
                    case WebAuthenticationStatus.Success:
                        // Successful authentication. 
                        result = webAuthenticationResult.ResponseData.ToString();
                        break;
                    case WebAuthenticationStatus.UserCancel:
                        // HTTP error. 
                        result = webAuthenticationResult.ResponseData.ToString();
                        break;
                    case WebAuthenticationStatus.ErrorHttp:
                        // HTTP error. 
                        result = webAuthenticationResult.ResponseErrorDetail.ToString();
                        break;
                    default:
                        // Other error.
                        result = webAuthenticationResult.ResponseData.ToString();
                        break;
                }
            }
            catch (Exception ex)
            {
                // Authentication failed. Handle parameter, SSL/TLS, and Network Unavailable errors here. 
                result = ex.Message;
            }

            System.Diagnostics.Debug.WriteLine("RESULT: " + result);
        }
    }
}
