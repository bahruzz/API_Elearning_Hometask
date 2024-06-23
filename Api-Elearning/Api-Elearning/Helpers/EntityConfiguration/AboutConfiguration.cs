using Api_Elearning.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Api_Elearning.Helpers.EntityConfiguration
{
    public class AboutConfiguration : IEntityTypeConfiguration<About>
    {
       

        public void Configure(EntityTypeBuilder<About> builder)
        {
            builder.Property(e => e.Title).IsRequired().HasMaxLength(100);
            builder.Property(e => e.Description).IsRequired().HasMaxLength(100);
            builder.Property(e => e.Image).IsRequired();
        }
    }
}
