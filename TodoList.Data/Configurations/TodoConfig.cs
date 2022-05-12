using Microsoft.EntityFrameworkCore;
using TodoList.Core.Models;

namespace TodoList.Data.Configurations
{
    public class TodoConfig : IEntityTypeConfiguration<Todo>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Todo> builder)
        {
            builder.HasKey(t => t.Id);

            builder.Property(t => t.Id)
                .UseIdentityColumn();

            builder.Property(t => t.Title)
                .IsRequired()
                .HasMaxLength(200);

            builder.HasOne(t => t.User)
                .WithMany(u => u.Todos)
                .HasForeignKey(t => t.UserId);

            builder.ToTable("Todos");
        }
    }
}