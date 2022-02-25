﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MoneerProject.Models.Entity;

namespace MoneerProject.Models.EntityMap
{
    public class UsersMap : IEntityTypeConfiguration<Users>
    {

        public void Configure(EntityTypeBuilder<Users> builder)
        {
            builder.ToTable("Users", "dbo");
            builder.HasKey(x => x.UserId);
            builder.Property(x => x.Name);
            builder.Property(x => x.Password);

        }
    }
}
