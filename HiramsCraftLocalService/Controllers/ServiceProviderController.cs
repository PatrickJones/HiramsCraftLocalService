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
    public class ServiceProviderController : ApiController
    {
        private HiramGroupLocalDB db = new HiramGroupLocalDB();

        // GET api/ServiceProvider
        public List<ServiceProviderEntity> GetServiceProviders()
        {
            List<ServiceProviderEntity> sps = new List<ServiceProviderEntity>();
            foreach (var sp in db.ServiceProviders)
            {
                ServiceProviderEntity matEntity = new ServiceProviderEntity(sp);
                sps.Add(matEntity);
            }

            return sps;
        }

        // GET api/ServiceProvider/5
        [ResponseType(typeof(ServiceProviderEntity))]
        public async Task<IHttpActionResult> GetServiceProvider([FromUri]int id)
        {
            ServiceProvider serviceprovider = await db.ServiceProviders.FindAsync(id);
            if (serviceprovider == null)
            {
                return NotFound();
            }
            ServiceProviderEntity servEntity = new ServiceProviderEntity(serviceprovider);

            return Ok(servEntity);
        }

        // PUT api/ServiceProvider/5
        public async Task<IHttpActionResult> PutServiceProvider([FromUri]int id, [FromBody]ServiceProvider serviceprovider)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != serviceprovider.ServiceProviderId)
            {
                return BadRequest();
            }

            db.Entry(serviceprovider).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ServiceProviderExists(id))
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

        // POST api/ServiceProvider
        [ResponseType(typeof(ServiceProvider))]
        public async Task<IHttpActionResult> PostServiceProvider([FromBody]ServiceProvider serviceprovider)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ServiceProviders.Add(serviceprovider);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = serviceprovider.ServiceProviderId }, serviceprovider);
        }

        // DELETE api/ServiceProvider/5
        [ResponseType(typeof(ServiceProvider))]
        public async Task<IHttpActionResult> DeleteServiceProvider([FromUri]int id)
        {
            ServiceProvider serviceprovider = await db.ServiceProviders.FindAsync(id);
            if (serviceprovider == null)
            {
                return NotFound();
            }

            db.ServiceProviders.Remove(serviceprovider);
            await db.SaveChangesAsync();

            return Ok(serviceprovider);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ServiceProviderExists(int id)
        {
            return db.ServiceProviders.Count(e => e.ServiceProviderId == id) > 0;
        }
    }
}