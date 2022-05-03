using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace RCMendoza.Models.ModelDB
{
    public partial class DBContext : DbContext
    {
        public DBContext()
        {
        }

        public DBContext(DbContextOptions<DBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Categorium> Categoria { get; set; }
        public virtual DbSet<Departamento> Departamentos { get; set; }
        public virtual DbSet<Distrito> Distritos { get; set; }
        public virtual DbSet<Producto> Productos { get; set; }
        public virtual DbSet<Proveedor> Proveedors { get; set; }
        public virtual DbSet<Provincium> Provincia { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Tipodocumento> Tipodocumentos { get; set; }
        public virtual DbSet<Unidadmedidum> Unidadmedida { get; set; }
        public virtual DbSet<Usuario> Usuarios { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseNpgsql("Host=ec2-35-168-194-15.compute-1.amazonaws.com;Database=d7o3ptkejfetrc;Username=nrdumnppqsrluc;Password=db0105ca6f6af7daf4d523a652a1a7a75788323f305e03e07a8ce66538b7d71d;Port=5432;SSL Mode=Require;Trust Server Certificate=true");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "en_US.UTF-8");

            modelBuilder.Entity<Categorium>(entity =>
            {
                entity.HasKey(e => e.IdCategoria)
                    .HasName("categoria_pkey");

                entity.ToTable("categoria");

                entity.Property(e => e.IdCategoria).HasColumnName("id_categoria");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(100)
                    .HasColumnName("descripcion");
            });

            modelBuilder.Entity<Departamento>(entity =>
            {
                entity.HasKey(e => e.IdDepartamento)
                    .HasName("departamento_pkey");

                entity.ToTable("departamento");

                entity.Property(e => e.IdDepartamento).HasColumnName("id_departamento");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(100)
                    .HasColumnName("descripcion");
            });

            modelBuilder.Entity<Distrito>(entity =>
            {
                entity.HasKey(e => e.IdDistrito)
                    .HasName("distrito_pkey");

                entity.ToTable("distrito");

                entity.Property(e => e.IdDistrito).HasColumnName("id_distrito");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(100)
                    .HasColumnName("descripcion");

                entity.Property(e => e.FkProvincia).HasColumnName("fk_provincia");

                entity.HasOne(d => d.FkProvinciaNavigation)
                    .WithMany(p => p.Distritos)
                    .HasForeignKey(d => d.FkProvincia)
                    .HasConstraintName("fk_provincia");
            });

            modelBuilder.Entity<Producto>(entity =>
            {
                entity.HasKey(e => e.IdProducto)
                    .HasName("producto_pkey");

                entity.ToTable("producto");

                entity.Property(e => e.IdProducto).HasColumnName("id_producto");

                entity.Property(e => e.Cantidad).HasColumnName("cantidad");

                entity.Property(e => e.Codigo)
                    .HasMaxLength(100)
                    .HasColumnName("codigo");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(100)
                    .HasColumnName("descripcion");

                entity.Property(e => e.Fecharegistro)
                    .HasColumnType("date")
                    .HasColumnName("fecharegistro");

                entity.Property(e => e.Fechavencimiento)
                    .HasColumnType("date")
                    .HasColumnName("fechavencimiento");

                entity.Property(e => e.FkCategoria).HasColumnName("fk_categoria");

                entity.Property(e => e.FkProveedor).HasColumnName("fk_proveedor");

                entity.Property(e => e.FkUnidadmedida).HasColumnName("fk_unidadmedida");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(100)
                    .HasColumnName("nombre");

                entity.Property(e => e.Preciocompra).HasColumnName("preciocompra");

                entity.Property(e => e.Precioventa).HasColumnName("precioventa");

                entity.HasOne(d => d.FkCategoriaNavigation)
                    .WithMany(p => p.Productos)
                    .HasForeignKey(d => d.FkCategoria)
                    .HasConstraintName("fk_categoria");

                entity.HasOne(d => d.FkProveedorNavigation)
                    .WithMany(p => p.Productos)
                    .HasForeignKey(d => d.FkProveedor)
                    .HasConstraintName("fk_proveedor");

                entity.HasOne(d => d.FkUnidadmedidaNavigation)
                    .WithMany(p => p.Productos)
                    .HasForeignKey(d => d.FkUnidadmedida)
                    .HasConstraintName("fk_unidadmedida");
            });

            modelBuilder.Entity<Proveedor>(entity =>
            {
                entity.HasKey(e => e.IdProveedor)
                    .HasName("proveedor_pkey");

                entity.ToTable("proveedor");

                entity.Property(e => e.IdProveedor).HasColumnName("id_proveedor");

                entity.Property(e => e.Direccion)
                    .HasMaxLength(150)
                    .HasColumnName("direccion");

                entity.Property(e => e.Email)
                    .HasMaxLength(150)
                    .HasColumnName("email");

                entity.Property(e => e.FkDistrito).HasColumnName("fk_distrito");

                entity.Property(e => e.FkTipodocumento).HasColumnName("fk_tipodocumento");

                entity.Property(e => e.Numerodocumento)
                    .HasMaxLength(30)
                    .HasColumnName("numerodocumento");

                entity.Property(e => e.Razonsocial)
                    .HasMaxLength(150)
                    .HasColumnName("razonsocial");

                entity.Property(e => e.Telefono)
                    .HasMaxLength(15)
                    .HasColumnName("telefono");

                entity.HasOne(d => d.FkDistritoNavigation)
                    .WithMany(p => p.Proveedors)
                    .HasForeignKey(d => d.FkDistrito)
                    .HasConstraintName("fk_distrito");

                entity.HasOne(d => d.FkTipodocumentoNavigation)
                    .WithMany(p => p.Proveedors)
                    .HasForeignKey(d => d.FkTipodocumento)
                    .HasConstraintName("fk_tipodocumento");
            });

            modelBuilder.Entity<Provincium>(entity =>
            {
                entity.HasKey(e => e.IdProvincia)
                    .HasName("provincia_pkey");

                entity.ToTable("provincia");

                entity.Property(e => e.IdProvincia).HasColumnName("id_provincia");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(100)
                    .HasColumnName("descripcion");

                entity.Property(e => e.FkDepartamento).HasColumnName("fk_departamento");

                entity.HasOne(d => d.FkDepartamentoNavigation)
                    .WithMany(p => p.Provincia)
                    .HasForeignKey(d => d.FkDepartamento)
                    .HasConstraintName("fk_departamento");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.HasKey(e => e.IdRoles)
                    .HasName("roles_pkey");

                entity.ToTable("roles");

                entity.Property(e => e.IdRoles).HasColumnName("id_roles");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(100)
                    .HasColumnName("descripcion");
            });

            modelBuilder.Entity<Tipodocumento>(entity =>
            {
                entity.HasKey(e => e.IdTipodocumento)
                    .HasName("tipodocumento_pkey");

                entity.ToTable("tipodocumento");

                entity.Property(e => e.IdTipodocumento).HasColumnName("id_tipodocumento");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(100)
                    .HasColumnName("descripcion");
            });

            modelBuilder.Entity<Unidadmedidum>(entity =>
            {
                entity.HasKey(e => e.IdUnidadmedida)
                    .HasName("unidadmedida_pkey");

                entity.ToTable("unidadmedida");

                entity.Property(e => e.IdUnidadmedida).HasColumnName("id_unidadmedida");

                entity.Property(e => e.Abreviatura)
                    .HasMaxLength(10)
                    .HasColumnName("abreviatura");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(100)
                    .HasColumnName("descripcion");
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasKey(e => e.IdUsuario)
                    .HasName("usuario_pkey");

                entity.ToTable("usuario");

                entity.Property(e => e.IdUsuario).HasColumnName("id_usuario");

                entity.Property(e => e.Apellidomaterno)
                    .HasMaxLength(100)
                    .HasColumnName("apellidomaterno");

                entity.Property(e => e.Apellidopaterno)
                    .HasMaxLength(100)
                    .HasColumnName("apellidopaterno");

                entity.Property(e => e.Contrasenia)
                    .HasMaxLength(255)
                    .HasColumnName("contrasenia");

                entity.Property(e => e.Direccion)
                    .HasMaxLength(150)
                    .HasColumnName("direccion");

                entity.Property(e => e.Email)
                    .HasMaxLength(150)
                    .HasColumnName("email");

                entity.Property(e => e.Fecharegistro)
                    .HasColumnType("date")
                    .HasColumnName("fecharegistro");

                entity.Property(e => e.FkRoles).HasColumnName("fk_roles");

                entity.Property(e => e.FkTipodocumento).HasColumnName("fk_tipodocumento");

                entity.Property(e => e.Nombres)
                    .HasMaxLength(100)
                    .HasColumnName("nombres");

                entity.Property(e => e.Telefono)
                    .HasMaxLength(9)
                    .HasColumnName("telefono");

                entity.HasOne(d => d.FkRolesNavigation)
                    .WithMany(p => p.Usuarios)
                    .HasForeignKey(d => d.FkRoles)
                    .HasConstraintName("fk_roles");

                entity.HasOne(d => d.FkTipodocumentoNavigation)
                    .WithMany(p => p.Usuarios)
                    .HasForeignKey(d => d.FkTipodocumento)
                    .HasConstraintName("fk_tipodocumento");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
