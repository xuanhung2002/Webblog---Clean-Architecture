﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Newtonsoft.Json;
using WebBlog.Domain.Entities;
using WebBlog.Infrastructure.Persistances;

namespace WebBlog.Infrastructure.Configurations
{
    public class CommentConfiguration : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.ToTable(TableNames.Comment);
            builder.HasKey(t => t.Id);

            builder.HasMany(s => s.Reactions)
                .WithOne()
                .HasForeignKey(t => t.CommentId)
                .IsRequired();
            builder.Property(s => s.Histories)
                .HasConversion(
                v => JsonConvert.SerializeObject(v),
                v => JsonConvert.DeserializeObject<List<CommentUpdateHistory>>(v));
        }
    }
}
