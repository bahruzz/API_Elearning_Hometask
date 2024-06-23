using Api_Elearning.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Api_Elearning.Helpers.EntityConfiguration
{
    public class SliderConfiguration : IEntityTypeConfiguration<Slider>
    {
        public void Configure(EntityTypeBuilder<Slider> builder)
        {
            builder.Property(e => e.Title).IsRequired().HasMaxLength(30);
            builder.Property(e => e.Description).IsRequired().HasMaxLength(30);
            builder.Property(e => e.Image).IsRequired();
        }
    }
}
