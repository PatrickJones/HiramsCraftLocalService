using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HiramsCraftLocalService.Models
{
    public class MaterialProviderEntity
    {
        public MaterialProviderEntity(MaterialProvider matProvider)
        {
            this.MaterialProviderId = matProvider.MaterialProviderId;
            this.Material = matProvider.Material;
            this.Description = matProvider.Description;
            this.EstimatedCost = matProvider.EstimatedCost;
            this.ActualCost = matProvider.ActualCost;
            this.LeadTime = matProvider.LeadTime;
            this.HasMinimumOrder = matProvider.HasMinimumOrder;
            this.MinimumOrder = matProvider.MinimumOrder;
            this.HasBulkDiscount = matProvider.HasBulkDiscount;
            this.BulkDiscount = matProvider.BulkDiscount;
            this.ProviderId = matProvider.ProviderId;
            this.ProductId = matProvider.ProductId;

            //set these to null, and just ids
            this.HiramsCraftProductEntity = null;
            this.ProviderEntity = null;
        }

        public MaterialProviderEntity()
        {
            //set these to null, and just ids
            this.HiramsCraftProductEntity = null;
            this.ProviderEntity = null;
        }

        public int MaterialProviderId { get; set; }
        public string Material { get; set; }
        public string Description { get; set; }
        public decimal EstimatedCost { get; set; }
        public decimal ActualCost { get; set; }
        public System.DateTime LeadTime { get; set; }
        public bool HasMinimumOrder { get; set; }
        public string MinimumOrder { get; set; }
        public bool HasBulkDiscount { get; set; }
        public string BulkDiscount { get; set; }
        public int ProviderId { get; set; }
        public int ProductId { get; set; }

        public virtual HiramsCraftProductEntity HiramsCraftProductEntity { get; set; }
        public virtual ProviderEntity ProviderEntity { get; set; }
    }
}