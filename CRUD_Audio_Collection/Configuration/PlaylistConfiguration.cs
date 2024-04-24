using CRUD_Audio_Collection.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRUD_Audio_Collection.Configuration;

public class PlaylistConfiguration : IEntityTypeConfiguration<Playlist>
{
    public void Configure(EntityTypeBuilder<Playlist> builder)
    {
        builder.HasKey(p => p.Id);
        
        builder.HasMany(p => p.Tracks)
            .WithMany(t => t.Playlists)
            .UsingEntity(j => j.ToTable("PlaylistTracks"));
        
        builder.HasOne(p => p.User)
            .WithMany(u => u.Playlists)
            .HasForeignKey(p => p.UserId);
    }
}