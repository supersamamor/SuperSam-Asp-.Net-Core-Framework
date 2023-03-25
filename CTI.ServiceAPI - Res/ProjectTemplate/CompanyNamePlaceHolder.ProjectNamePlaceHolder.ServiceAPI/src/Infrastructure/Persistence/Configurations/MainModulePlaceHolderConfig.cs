using ProjectNamePlaceHolder.Services.Domain.Entities;
using Cti.Core.Infrastructure.EntityFrameworkCore.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ProjectNamePlaceHolder.Services.Infrastructure.Persistence.Configurations
{
    public class MainModulePlaceHolderConfig : IEntityTypeConfiguration<MainModulePlaceHolder>
    {
        public void Configure(EntityTypeBuilder<MainModulePlaceHolder> builder)
        {
            builder.ToTable("Template:[InsertNewTableNameTextHere]");

            Template:[InsertNewPrimaryKeyConfigTextHere]

            Template:[InsertNewFieldAttributesTextHere]			

			builder.Property(t => t.DateCreated)
                .IsRequired();

            builder.Property(t => t.CreatedBy)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(t => t.ModifiedBy)
                .HasMaxLength(50);
			
            Template:[InsertNewTableNamePrefixTextHere]
			
			Template:[InsertNewSubCollectionFluentApiTextHere]
        }
    }
}
