using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Windows.Security.Credentials;
using Xamarin.Auth;

namespace InfusionGames.CityScramble.UWP.Services
{
    /// <summary>
    /// UWP support for Xamarin.Auth AccountStore
    /// </summary>
    public class UwpAccountStore : AccountStore
    {
        PasswordVault _vault;

        public UwpAccountStore()
        {
            _vault = new PasswordVault();
        }

        public override void Delete(Account account, string serviceId)
        {
            var credential =
                _vault
                    .FindAllByResource(serviceId)
                    .Where(i => i.UserName == account.Username)
                    .FirstOrDefault();

            if (credential != null)
            {
                _vault.Remove(credential);
            }
        }

        public override Task DeleteAsync(Account account, string serviceId)
        {
            return Task.Run(() => Delete(account, serviceId));
        }

        public override IEnumerable<Account> FindAccountsForService(string serviceId)
        {
            try
            {
                return
                        _vault
                            .FindAllByResource(serviceId)
                            .Select(ToAccount);
            }
            catch (Exception)
            {
                return Enumerable.Empty<Account>();
            }                
        }

        public override Task<List<Account>> FindAccountsForServiceAsync(string serviceId)
        {
            var accounts = FindAccountsForService(serviceId).ToList();
            return Task.FromResult(accounts);

            //return Task.Run(() => 
            //{
            //    return FindAccountsForService(serviceId).ToList();
            //});
        }

        public override void Save(Account account, string serviceId)
        {
            var password = account.Properties["Password"];
            var credential = new PasswordCredential(serviceId, account.Username, password);

            _vault.Add(credential);
        }

        public override Task SaveAsync(Account account, string serviceId)
        {
            return Task.Run(() => Save(account, serviceId));
        }

        private Account ToAccount(PasswordCredential credential)
        {
            credential.RetrievePassword();

            var account = new Account(credential.UserName);
            account.Properties["Password"] = credential.Password;

            return account;
        }
    }
}
