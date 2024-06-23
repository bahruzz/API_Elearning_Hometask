using Api_Elearning.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Api_Elearning.Helpers.EntityConfiguration
{
    public class InstructorConfiguration : IEntityTypeConfiguration<Instructor>
    {
        public void Configure(EntityTypeBuilder<Instructor> builder)
        {
            builder.Property(e => e.FullName).IsRequired(). HasMaxLength(100);
            builder.Property(e => e.Address).IsRequired().HasMaxLength(100);
            builder.Property(e => e.Email).IsRequired();
            builder.Property(e => e.Image).IsRequired();
            builder.Property(e => e.Field).IsRequired();
            builder.Property(e => e.Phone).IsRequired();
           
        }
    }
}
