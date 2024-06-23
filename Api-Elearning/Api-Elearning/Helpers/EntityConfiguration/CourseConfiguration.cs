using Api_Elearning.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Api_Elearning.Helpers.EntityConfiguration
{
    public class CourseConfiguration : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> builder)
        {
           
            builder.Property(e => e.Description).IsRequired().HasMaxLength(100);
            builder.Property(e => e.Price).IsRequired();
            
        }
    }
}
