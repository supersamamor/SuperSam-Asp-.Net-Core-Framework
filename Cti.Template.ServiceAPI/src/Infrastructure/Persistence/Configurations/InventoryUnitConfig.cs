using CHANGE_TO_APP_NAME.Services.Domain.Entities;
using Cti.Core.Infrastructure.EntityFrameworkCore.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CHANGE_TO_APP_NAME.Services.Infrastructure.Persistence.Configurations
{
    public class InventoryUnitConfig : IEntityTypeConfiguration<InventoryUnit>
    {
        public void Configure(EntityTypeBuilder<InventoryUnit> builder)
        {
            builder.ToTable("immInventoryUnit");

            builder.HasKey(t => t.ReferenceObject);

            builder.Property(t => t.ReferenceObject)
                .IsRequired()
                .HasMaxLength(20);

            builder.Property(t => t.ProjectCode)
                .IsRequired()
                .HasMaxLength(10);

            builder.Property(t => t.PhaseBuildingCode)
                .HasMaxLength(10);

            builder.Property(t => t.BlockFloorClusterCode)
                .HasMaxLength(10);

            builder.Property(t => t.LotUnitShareNumber)
                .HasMaxLength(50);

            builder.Property(t => t.InventoryUnitNumber)
                .HasMaxLength(10);

            builder.Property(t => t.CompanyCode)
                .IsRequired()
                .HasMaxLength(4);

            builder.Property(t => t.UnitClassificationCode)
                .HasMaxLength(4);

            builder.Property(t => t.MarketProductTypeCode)
                .HasMaxLength(4);

            builder.Property(t => t.MarketProductID)
                .HasMaxLength(5);

            builder.Property(t => t.MarketProductUseCode)
                .HasMaxLength(4);

            builder.Property(t => t.MarketProductSubTypeCode)
                .HasMaxLength(4);

            builder.Property(t => t.MarketProductAttributeCode)
                .HasMaxLength(4);

            builder.Property(t => t.ParkingTypeCode)
                .HasMaxLength(4);

            builder.Property(t => t.DateCreated)
                .IsRequired();

            builder.Property(t => t.CreatedBy)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(t => t.ModifiedBy)
                .HasMaxLength(50);

            builder.AddColumnPrefix("inv");
        }
    }
}
