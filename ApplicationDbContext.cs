using APELC.Helper;
using APELC.Model;
using Microsoft.EntityFrameworkCore;
using System;



public class ApplicationDbContext : DbContext
{
    private DbSet<PenggunaApelCMain> apelCUsers;


    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<PenggunaApelCMain>().ToTable("APELC_PENGGUNA_UPNM");
        modelBuilder.Entity<PenggunaApelCMain>().Property(nameof(PenggunaApelCMain.ID_PENGGUNA)).HasColumnName("ID_PENGGUNA");
        modelBuilder.Entity<PenggunaApelCMain>().Property(nameof(PenggunaApelCMain.KATA_LALUAN_PENGGUNA)).HasColumnName("JENIS_MODUL_PENGGUNA_UPNM_FK");
        modelBuilder.Entity<PenggunaApelCMain>().Property(nameof(PenggunaApelCMain.JENIS_MODUL_PENGGUNA_UPNM_FK)).HasColumnName("JENIS_MODUL_PENGGUNA_UPNM_FK");
        modelBuilder.Entity<PenggunaApelCMain>().Property(nameof(PenggunaApelCMain.SESSION_TIMEOUT)).HasColumnName("SESSION_TIMEOUT");
        modelBuilder.Entity<PenggunaApelCMain>().Property(nameof(PenggunaApelCMain.STATUS_AKTIF_PENGGUNA_UPNM_FK)).HasColumnName("STATUS_AKTIF_PENGGUNA_UPNM_FK");
    }
}
