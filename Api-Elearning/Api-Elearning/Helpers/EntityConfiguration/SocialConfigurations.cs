using Api_Elearning.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Api_Elearning.Helpers.EntityConfiguration
{
    public class SocialConfigurations : IEntityTypeConfiguration<Social>
    {
        public void Configure(EntityTypeBuilder<Social> builder)
        {

            builder.Property(e => e.Name).IsRequired().HasMaxLength(50);
           
            builder.Property(e => e.Icon).IsRequired();
        }
    }
}
