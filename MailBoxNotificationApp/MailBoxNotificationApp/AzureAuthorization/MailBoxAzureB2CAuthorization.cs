
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MailBoxNotificationApp
{
    class MailBoxAzureB2CAuthorization
    {
        #region Connection Data
        public static string ClientId = "ClientId";
        public static string TenantId = "TenantId";
        public static string PolicyId = "PolicyId";
        public static string RedirectUri = "RedirectUri";
        private static readonly string[] Scopes = { "Scopes" };
        public const string TokenKey = "token";
        public static string SignInAuthority = "https://MailBoxProject.b2clogin.com/tfp/" + $"{TenantId}/" + $"{PolicyId}";
        #endregion
        private static readonly Win32Registry Registry = new Win32Registry();

        private static IPublicClientApplication clientApp;
        public IPublicClientApplication PublicClientApp { get { return clientApp; } }

        public MailBoxAzureB2CAuthorization()
        {
            InitializeAuth();
        }

        public Task<IEnumerable<IAccount>> GetAccountsAsync()
        {
            return PublicClientApp.GetAccountsAsync();
        }

        public async Task Logout()
        {
            var accounts = await PublicClientApp.GetAccountsAsync();
            if (accounts.Any())
            {
                try
                {
                    await PublicClientApp.RemoveAsync(accounts.FirstOrDefault());
                }
                catch (MsalException ex)
                {
                    throw new Exception($"Error signing-out user: {ex.Message}");
                }
            }
        }

        public async Task<AuthenticationResult> Login()
        {
            AuthenticationResult authResult;
            var accounts = await PublicClientApp.GetAccountsAsync();
            var firstAccount = accounts.FirstOrDefault();

            try
            {
                authResult = await PublicClientApp.AcquireTokenSilent(Scopes, firstAccount)
                    .ExecuteAsync();
            }
            catch (MsalUiRequiredException ex)
            {
                System.Diagnostics.Debug.WriteLine($"MsalUiRequiredException: {ex.Message}");

                try
                {
                    authResult = await PublicClientApp.AcquireTokenInteractive(Scopes)
                        .WithAccount(accounts.FirstOrDefault())
                        .WithPrompt(Prompt.SelectAccount)
                        .ExecuteAsync();
                }
                catch (MsalException msalex)
                {
                    throw new Exception($"Error Acquiring Token:{Environment.NewLine}{msalex}");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error Acquiring Token Silently:{Environment.NewLine}{ex}");
            }
            return authResult;
        }

        private static void InitializeAuth()
        {
            clientApp = PublicClientApplicationBuilder.Create(ClientId)
                .WithRedirectUri(RedirectUri)
                .WithB2CAuthority(SignInAuthority)
                .WithTenantId(TenantId)
                .Build();
            clientApp.UserTokenCache.SetBeforeAccess(BeforeAccessNotification);
            clientApp.UserTokenCache.SetAfterAccess(AfterAccessNotification);
            TokenCacheHelper.EnableSerialization(clientApp.UserTokenCache);
        }

        private static void BeforeAccessNotification(TokenCacheNotificationArgs args)
        {
            args.TokenCache.DeserializeMsalV3(Registry.Get<byte[]>(TokenKey));
        }

        private static void AfterAccessNotification(TokenCacheNotificationArgs args)
        {
            if (args.HasStateChanged)
            {
                Registry.Set(TokenKey, args.TokenCache.SerializeMsalV3());
            }
        }
    }
}
