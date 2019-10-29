using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace GUFOS_BackEnd.Domains
{
    public partial class GufosContext : DbContext
    {
        public GufosContext()
        {
        }

        public GufosContext(DbContextOptions<GufosContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Categoria> Categoria { get; set; }
        public virtual DbSet<Evento> Evento { get; set; }
        public virtual DbSet<Localizacao> Localizacao { get; set; }
        public virtual DbSet<Presenca> Presenca { get; set; }
        public virtual DbSet<TipoUsuario> TipoUsuario { get; set; }
        public virtual DbSet<Usuario> Usuario { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=DESKTOP-2P4HA47\\SQLEXPRESS;Database=Gufos;User Id=sa;Password=132;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Categoria>(entity =>
            {
                entity.HasKey(e => e.IdCategoria)
                    .HasName("PK__Categori__A3C02A1079E74B64");

                entity.HasIndex(e => e.Titulo)
                    .HasName("UQ__Categori__7B406B561DCC1CD0")
                    .IsUnique();

                entity.Property(e => e.Titulo).IsUnicode(false);
            });

            modelBuilder.Entity<Evento>(entity =>
            {
                entity.HasKey(e => e.IdEvento)
                    .HasName("PK__Eventos__034EFC04EF1B7169");

                entity.Property(e => e.AcessoLivre).HasDefaultValueSql("((1))");

                entity.Property(e => e.Titulo).IsUnicode(false);

                entity.HasOne(d => d.IdCategoriaNavigation)
                    .WithMany(p => p.Evento)
                    .HasForeignKey(d => d.IdCategoria)
                    .HasConstraintName("FK__Eventos__IdCateg__44FF419A");

                entity.HasOne(d => d.IdLocalizacaoNavigation)
                    .WithMany(p => p.Evento)
                    .HasForeignKey(d => d.IdLocalizacao)
                    .HasConstraintName("FK__Eventos__IdLocal__46E78A0C");
            });

            modelBuilder.Entity<Localizacao>(entity =>
            {
                entity.HasKey(e => e.IdLocalizacao)
                    .HasName("PK__Localiza__C96A5BF67C936AAE");

                entity.HasIndex(e => e.Cnpj)
                    .HasName("UQ__Localiza__AA57D6B4B375090F")
                    .IsUnique();

                entity.HasIndex(e => e.RazaoSocial)
                    .HasName("UQ__Localiza__448779F0F99EED33")
                    .IsUnique();

                entity.Property(e => e.Cnpj)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Endereco).IsUnicode(false);

                entity.Property(e => e.RazaoSocial).IsUnicode(false);
            });

            modelBuilder.Entity<Presenca>(entity =>
            {
                entity.HasKey(e => e.IdPresenca)
                    .HasName("PK__Presenca__50FB6F5D2FC36235");

                entity.Property(e => e.PresencaStatus).IsUnicode(false);

                entity.HasOne(d => d.IdEventoNavigation)
                    .WithMany(p => p.Presenca)
                    .HasForeignKey(d => d.IdEvento)
                    .HasConstraintName("FK__Presenca__IdEven__49C3F6B7");

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.Presenca)
                    .HasForeignKey(d => d.IdUsuario)
                    .HasConstraintName("FK__Presenca__IdUsua__4AB81AF0");
            });

            modelBuilder.Entity<TipoUsuario>(entity =>
            {
                entity.HasKey(e => e.IdTipoUsuario)
                    .HasName("PK__TipoUsua__CA04062BCDDC0C51");

                entity.HasIndex(e => e.Titulo)
                    .HasName("UQ__TipoUsua__7B406B56F574C799")
                    .IsUnique();

                entity.Property(e => e.Titulo).IsUnicode(false);
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasKey(e => e.IdUsuario)
                    .HasName("PK__Usuario__5B65BF973A99CC2B");

                entity.HasIndex(e => e.Email)
                    .HasName("UQ__Usuario__A9D10534CEFF7E9C")
                    .IsUnique();

                entity.Property(e => e.Email).IsUnicode(false);

                entity.Property(e => e.Nome).IsUnicode(false);

                entity.Property(e => e.Senha).IsUnicode(false);

                entity.HasOne(d => d.IdTipoUsuarioNavigation)
                    .WithMany(p => p.Usuario)
                    .HasForeignKey(d => d.IdTipoUsuario)
                    .HasConstraintName("FK__Usuario__IdTipoU__3B75D760");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
