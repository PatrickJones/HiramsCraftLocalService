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
    public class PurchaseOrderController : ApiController
    {
        private HiramGroupLocalDB db = new HiramGroupLocalDB();

        // GET api/PurchaseOrder
        public IQueryable<HiramsCraftPO> GetHiramsCraftPOes()
        {
            return db.HiramsCraftPOes;
        }

        // GET api/PurchaseOrder/5
        [ResponseType(typeof(HiramsCraftPO))]
        public async Task<IHttpActionResult> GetHiramsCraftPO([FromUri]int id)
        {
            HiramsCraftPO hiramscraftpo = await db.HiramsCraftPOes.FindAsync(id);
            if (hiramscraftpo == null)
            {
                return NotFound();
            }

            return Ok(hiramscraftpo);
        }

        // PUT api/PurchaseOrder/5
        public async Task<IHttpActionResult> PutHiramsCraftPO([FromUri]int id, [FromBody]HiramsCraftPO hiramscraftpo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != hiramscraftpo.PurchaseOrderId)
            {
                return BadRequest();
            }

            db.Entry(hiramscraftpo).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HiramsCraftPOExists(id))
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

        // POST api/PurchaseOrder
        [ResponseType(typeof(HiramsCraftPO))]
        public async Task<IHttpActionResult> PostHiramsCraftPO([FromBody]HiramsCraftPO hiramscraftpo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.HiramsCraftPOes.Add(hiramscraftpo);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = hiramscraftpo.PurchaseOrderId }, hiramscraftpo);
        }

        // DELETE api/PurchaseOrder/5
        [ResponseType(typeof(HiramsCraftPO))]
        public async Task<IHttpActionResult> DeleteHiramsCraftPO([FromUri]int id)
        {
            HiramsCraftPO hiramscraftpo = await db.HiramsCraftPOes.FindAsync(id);
            if (hiramscraftpo == null)
            {
                return NotFound();
            }

            db.HiramsCraftPOes.Remove(hiramscraftpo);
            await db.SaveChangesAsync();

            return Ok(hiramscraftpo);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool HiramsCraftPOExists(int id)
        {
            return db.HiramsCraftPOes.Count(e => e.PurchaseOrderId == id) > 0;
        }
    }
}