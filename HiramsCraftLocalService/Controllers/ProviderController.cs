using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using HiramsCraftLocalService.Models;
using System.Web.Http.Cors;

namespace HiramsCraftLocalService.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class ProviderController : ApiController
    {
        private HiramGroupLocalDB db = new HiramGroupLocalDB();

        // GET api/Provider
        public List<ProviderEntity> GetProviders()
        {
            List<ProviderEntity> provs = new List<ProviderEntity>();
            foreach (var p in db.Providers)
            {
                ProviderEntity provEntity = new ProviderEntity(p);
                provs.Add(provEntity);
            }

            return provs;
        }

        // GET api/Provider/5
        [ResponseType(typeof(ProviderEntity))]
        public async Task<IHttpActionResult> GetProvider([FromUri]int id)
        {
            Provider provider = await db.Providers.FindAsync(id);
            if (provider == null)
            {
                return NotFound();
            }
            ProviderEntity pEntity = new ProviderEntity(provider);

            return Ok(pEntity);
        }

        // PUT api/Provider/5
        public async Task<IHttpActionResult> PutProvider([FromUri]int id, [FromBody]Provider provider)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != provider.ProviderId)
            {
                return BadRequest();
            }

            var prov = await db.Providers.FindAsync(id);

            prov.ProviderName = provider.ProviderName;
            prov.Profile = provider.Profile;
            prov.Website = provider.Website;
            prov.Address1 = provider.Address1;
            prov.Address2 = provider.Address2;
            prov.City = provider.City;
            prov.State = provider.State;
            prov.ZipCode = provider.ZipCode;
            prov.Country = provider.Country;
            prov.Phone = provider.Phone;
            prov.Fax = provider.Fax;
            prov.ContactName = provider.ContactName;
            prov.ContactEmail = provider.ContactEmail;

            prov.MaterialProviders.Clear();
            prov.MaterialProviders = new List<MaterialProvider>(provider.MaterialProviders);
            prov.ServiceProviders.Clear();
            prov.ServiceProviders = new List<ServiceProvider>(provider.ServiceProviders);
            //db.Entry(provider).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProviderExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST api/Provider
        [ResponseType(typeof(Provider))]
        public async Task<IHttpActionResult> PostProvider([FromBody]Provider provider)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Providers.Add(provider);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = provider.ProviderId }, provider);
        }

        // DELETE api/Provider/5
        [ResponseType(typeof(Provider))]
        public async Task<IHttpActionResult> DeleteProvider([FromUri]int id)
        {
            Provider provider = await db.Providers.FindAsync(id);
            if (provider == null)
            {
                return NotFound();
            }

            db.Providers.Remove(provider);
            await db.SaveChangesAsync();

            return Ok(provider);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ProviderExists(int id)
        {
            return db.Providers.Count(e => e.ProviderId == id) > 0;
        }
    }
}