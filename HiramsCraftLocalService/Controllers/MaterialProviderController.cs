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
using System.Collections;
using System.Web.Http.Cors;

namespace HiramsCraftLocalService.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class MaterialProviderController : ApiController
    {
        private HiramGroupLocalDB db = new HiramGroupLocalDB();

        // GET api/MaterialProvider
        public List<MaterialProviderEntity> GetMaterialProviders()
        {
            List<MaterialProviderEntity> mps = new List<MaterialProviderEntity>();
            foreach (var mp in db.MaterialProviders)
            {
                MaterialProviderEntity matEntity = new MaterialProviderEntity(mp);
                mps.Add(matEntity);
            }

            return mps;
        }

        // GET api/MaterialProvider/5
        [ResponseType(typeof(MaterialProviderEntity))]
        public async Task<IHttpActionResult> GetMaterialProvider([FromUri]int id)
        {
            MaterialProvider materialprovider = await db.MaterialProviders.FindAsync(id);
            if (materialprovider == null)
            {
                return NotFound();
            }
            MaterialProviderEntity matEntity = new MaterialProviderEntity(materialprovider);

            return Ok(matEntity);
        }

        // PUT api/MaterialProvider/5
        public async Task<IHttpActionResult> PutMaterialProvider([FromUri]int id, [FromBody]MaterialProvider materialprovider)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != materialprovider.MaterialProviderId)
            {
                return BadRequest();
            }

            db.Entry(materialprovider).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MaterialProviderExists(id))
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

        // POST api/MaterialProvider
        [ResponseType(typeof(MaterialProvider))]
        public async Task<IHttpActionResult> PostMaterialProvider([FromBody]MaterialProvider materialprovider)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.MaterialProviders.Add(materialprovider);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = materialprovider.MaterialProviderId }, materialprovider);
        }

        // DELETE api/MaterialProvider/5
        [ResponseType(typeof(MaterialProvider))]
        public async Task<IHttpActionResult> DeleteMaterialProvider([FromUri]int id)
        {
            MaterialProvider materialprovider = await db.MaterialProviders.FindAsync(id);
            if (materialprovider == null)
            {
                return NotFound();
            }

            db.MaterialProviders.Remove(materialprovider);
            await db.SaveChangesAsync();

            return Ok(materialprovider);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool MaterialProviderExists(int id)
        {
            return db.MaterialProviders.Count(e => e.MaterialProviderId == id) > 0;
        }
    }
}