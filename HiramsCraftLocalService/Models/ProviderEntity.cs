using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HiramsCraftLocalService.Models
{
    public class ProviderEntity
    {
        public ProviderEntity(Provider provider)
        {
            this.ProviderId = provider.ProviderId;
            this.ProviderName = provider.ProviderName;
            this.Profile = provider.Profile;
            this.Website = provider.Website;
            this.Address1 = provider.Address1;
            this.Address2 = provider.Address2;
            this.City = provider.City;
            this.State = provider.State;
            this.ZipCode = provider.ZipCode;
            this.Country = provider.Country;
            this.Phone = provider.Phone;
            this.Fax = provider.Fax;
            this.ContactName = provider.ContactName;
            this.ContactEmail = provider.ContactEmail;

            this.MaterialProviders = new List<MaterialProviderEntity>(SetMaterialProviderEntity(provider.MaterialProviders));
            this.ServiceProviders = new List<ServiceProviderEntity>(ServiceProviderEntity(provider.ServiceProviders));
        }

        public ProviderEntity()
        {
            this.MaterialProviders = new List<MaterialProviderEntity>();
            this.ServiceProviders = new List<ServiceProviderEntity>();        
        }

        public int ProviderId { get; set; }
        public string ProviderName { get; set; }
        public string Profile { get; set; }
        public string Website { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string Country { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public string ContactName { get; set; }
        public string ContactEmail { get; set; }

        public virtual ICollection<MaterialProviderEntity> MaterialProviders { get; set; }
        public virtual ICollection<ServiceProviderEntity> ServiceProviders { get; set; }

        private IEnumerable<MaterialProviderEntity> SetMaterialProviderEntity(IEnumerable<MaterialProvider> materialProviders)
        {
            List<MaterialProviderEntity> results = new List<MaterialProviderEntity>();
            if (materialProviders.Count() != 0)
            {
                foreach (var matP in materialProviders)
                {
                    var matEntity = new MaterialProviderEntity(matP);
                    results.Add(matEntity);
                }
            }

            return results;
        }

        private IEnumerable<ServiceProviderEntity> ServiceProviderEntity(IEnumerable<ServiceProvider> serviceProviders)
        {
            List<ServiceProviderEntity> results = new List<ServiceProviderEntity>();
            if (serviceProviders.Count() != 0)
            {
                foreach (var servP in serviceProviders)
                {
                    var servEntity = new ServiceProviderEntity(servP);
                    results.Add(servEntity);
                }
            }

            return results;
        }
    }
}