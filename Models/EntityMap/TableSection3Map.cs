using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MoneerProject.Models.Entity;

namespace MoneerProject.Models.EntityMap
{
    public class TableSection3Map : IEntityTypeConfiguration<TableSection3>
    {

        public void Configure(EntityTypeBuilder<TableSection3> builder)
        {
            builder.ToTable("TableSection3", "dbo");
            builder.HasKey(x => x.Section3Id);
            builder.Property(x => x.Image);
            builder.Property(x => x.Title);
            builder.Property(x => x.PathMp3);
            builder.Property(x => x.UserId);

        }
    }
}
