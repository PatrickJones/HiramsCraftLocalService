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
    public class ProductsController : ApiController
    {
        private HiramGroupLocalDB db = new HiramGroupLocalDB();

        // GET api/Products
        public List<HiramsCraftProductEntity> GetHiramsCraftProducts()
        {
            List<HiramsCraftProductEntity> prods = new List<HiramsCraftProductEntity>();
            foreach (var p in db.HiramsCraftProducts)
            {
                HiramsCraftProductEntity prodEntity = new HiramsCraftProductEntity(p);
                prods.Add(prodEntity);
            }

            return prods;
        }

        // GET api/Products/5
        [ResponseType(typeof(HiramsCraftProductEntity))]
        public async Task<IHttpActionResult> GetHiramsCraftProduct([FromUri]int id)
        {
            HiramsCraftProduct hiramscraftproduct = await db.HiramsCraftProducts.FindAsync(id);
            if (hiramscraftproduct == null)
            {
                return NotFound();
            }
            HiramsCraftProductEntity prodEntity = new HiramsCraftProductEntity(hiramscraftproduct);

            return Ok(prodEntity);
        }

        // PUT api/Products/5
        public async Task<IHttpActionResult> PutHiramsCraftProduct([FromUri]int id, [FromBody]HiramsCraftProduct hiramscraftproduct)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != hiramscraftproduct.ProductId)
            {
                return BadRequest();
            }

            var prod = await db.HiramsCraftProducts.FindAsync(id);

            prod.ProductImage = hiramscraftproduct.ProductImage;
            prod.ImageFileName = hiramscraftproduct.ImageFileName;
            prod.ImageMIMEType = hiramscraftproduct.ImageMIMEType;
            prod.ProductName = hiramscraftproduct.ProductName;
            prod.ProductCode = hiramscraftproduct.ProductCode;
            prod.ProductCost = hiramscraftproduct.ProductCost;
            prod.LastUpdate = hiramscraftproduct.LastUpdate;
            prod.ProductDescription = hiramscraftproduct.ProductDescription;
            prod.ProductCategory = hiramscraftproduct.ProductCategory;
            prod.ProductSubCategory = hiramscraftproduct.ProductSubCategory;
            prod.Units = hiramscraftproduct.Units;
            prod.ProductType = hiramscraftproduct.ProductType;
            prod.WoodType = hiramscraftproduct.WoodType;
            prod.WoodStainType = hiramscraftproduct.WoodStainType;
            prod.MetalType = hiramscraftproduct.MetalType;
            prod.MetalFinish = hiramscraftproduct.MetalFinish;

            prod.MaterialProviders.Clear();
            prod.MaterialProviders = new List<MaterialProvider>(hiramscraftproduct.MaterialProviders);
            prod.ServiceProviders.Clear();
            prod.ServiceProviders = new List<ServiceProvider>(hiramscraftproduct.ServiceProviders);
            //db.Entry(hiramscraftproduct).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HiramsCraftProductExists(id))
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

        // POST api/Products
        [ResponseType(typeof(HiramsCraftProduct))]
        public async Task<IHttpActionResult> PostHiramsCraftProduct([FromBody]HiramsCraftProduct hiramscraftproduct)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.HiramsCraftProducts.Add(hiramscraftproduct);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = hiramscraftproduct.ProductId }, hiramscraftproduct);
        }

        // DELETE api/Products/5
        [ResponseType(typeof(HiramsCraftProduct))]
        public async Task<IHttpActionResult> DeleteHiramsCraftProduct([FromUri]int id)
        {
            HiramsCraftProduct hiramscraftproduct = await db.HiramsCraftProducts.FindAsync(id);
            if (hiramscraftproduct == null)
            {
                return NotFound();
            }

            db.HiramsCraftProducts.Remove(hiramscraftproduct);
            await db.SaveChangesAsync();

            return Ok(hiramscraftproduct);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool HiramsCraftProductExists(int id)
        {
            return db.HiramsCraftProducts.Count(e => e.ProductId == id) > 0;
        }
    }
}