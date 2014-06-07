using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HiramsCraftLocalService.Models
{
    public class HiramsCraftProductEntity
    {
        public HiramsCraftProductEntity(HiramsCraftProduct hcp)
        {
            this.ProductId = hcp.ProductId;
            this.ProductImage = hcp.ProductImage;
            this.ImageFileName = hcp.ImageFileName;
            this.ImageMIMEType = hcp.ImageMIMEType;
            this.ProductName = hcp.ProductName;
            this.ProductCode = hcp.ProductCode;
            this.ProductCost = hcp.ProductCost;
            this.LastUpdate = hcp.LastUpdate;
            this.ProductDescription = hcp.ProductDescription;
            this.ProductCategory = hcp.ProductCategory;
            this.ProductSubCategory = hcp.ProductSubCategory;
            this.Units = hcp.Units;
            this.ProductType = hcp.ProductType; 
            this.WoodType = hcp.WoodType;
            this.WoodStainType = hcp.WoodStainType;
            this.MetalType = hcp.MetalType;
            this.MetalFinish = hcp.MetalFinish;
    
            //this.MaterialProviders  = new List<MaterialProvider>(hcp.MaterialProviders);
            //this.ServiceProviders = new List<ServiceProvider>(hcp.ServiceProviders);

            this.MaterialProviders = new List<MaterialProviderEntity>(SetMaterialProviderEntity(hcp.MaterialProviders));
            this.ServiceProviders = new List<ServiceProviderEntity>(ServiceProviderEntity(hcp.ServiceProviders));
        }

        public HiramsCraftProductEntity()
        {
            this.MaterialProviders = new List<MaterialProviderEntity>();
            this.ServiceProviders = new List<ServiceProviderEntity>();
        }

        public int ProductId { get; set; }
        public byte[] ProductImage { get; set; }
        public string ImageFileName { get; set; }
        public string ImageMIMEType { get; set; }
        public string ProductName { get; set; }
        public string ProductCode { get; set; }
        public double ProductCost { get; set; }
        public System.DateTime LastUpdate { get; set; }
        public string ProductDescription { get; set; }
        public string ProductCategory { get; set; }
        public string ProductSubCategory { get; set; }
        public string Units { get; set; }
        public int ProductType { get; set; }
        public int WoodType { get; set; }
        public int WoodStainType { get; set; }
        public int MetalType { get; set; }
        public int MetalFinish { get; set; }

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