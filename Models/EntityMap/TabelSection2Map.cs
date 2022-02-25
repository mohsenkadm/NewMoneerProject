using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MoneerProject.Models.Entity;

namespace MoneerProject.Models.EntityMap
{
    public class TabelSection2Map : IEntityTypeConfiguration<TabelSection2>
    { 
        public void Configure(EntityTypeBuilder<TabelSection2> builder)
        {
            builder.ToTable("TabelSection2", "dbo");
            builder.HasKey(x => x.Section2Id);
            builder.Property(x => x.Image);
            builder.Property(x => x.Title);
            builder.Property(x => x.PathMp3); 

        }
    } 
}
