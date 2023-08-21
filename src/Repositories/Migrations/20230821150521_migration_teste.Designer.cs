﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Repositories;

#nullable disable

namespace Repositories.Migrations
{
    [DbContext(typeof(AplicacaoContext))]
    [Migration("20230821150521_migration_teste")]
    partial class migration_teste
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.10")
                .HasAnnotation("Proxies:ChangeTracking", false)
                .HasAnnotation("Proxies:CheckEquality", false)
                .HasAnnotation("Proxies:LazyLoading", true);

            modelBuilder.Entity("Domain.Cliente", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Teste")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Clientes", (string)null);
                });

            modelBuilder.Entity("Domain.Maquina", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Maquinas", (string)null);
                });

            modelBuilder.Entity("Domain.OrdemProducao", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("ClienteId")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("LiberadoParaProducao")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Ordem")
                        .HasColumnType("INTEGER");

                    b.Property<int>("ProdutoId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Quantidade")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("ClienteId");

                    b.HasIndex("ProdutoId");

                    b.ToTable("OrdensProducao", (string)null);
                });

            modelBuilder.Entity("Domain.Pedido", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("ClienteId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("CodigoDaVenda")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("Data")
                        .HasColumnType("TEXT");

                    b.Property<int>("OrdemDeProducaoId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("ProdutoId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Quantidade")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("VendaFinalizada")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("ClienteId");

                    b.HasIndex("OrdemDeProducaoId")
                        .IsUnique();

                    b.HasIndex("ProdutoId");

                    b.ToTable("Pedidos", (string)null);
                });

            modelBuilder.Entity("Domain.Producao", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("Data")
                        .HasColumnType("TEXT");

                    b.Property<int>("MaquinaId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("OrdemProducaoId")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("ProducaoFinalizada")
                        .HasColumnType("INTEGER");

                    b.Property<int>("ProdutoId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Quantidade")
                        .HasColumnType("INTEGER");

                    b.Property<int>("QuantidadeProduzida")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Sequencia")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("MaquinaId");

                    b.HasIndex("OrdemProducaoId")
                        .IsUnique();

                    b.HasIndex("ProdutoId");

                    b.ToTable("Producoes", (string)null);
                });

            modelBuilder.Entity("Domain.Produto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Produtos", (string)null);
                });

            modelBuilder.Entity("Domain.Usuario", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Senha")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Usuarios", (string)null);
                });

            modelBuilder.Entity("Domain.OrdemProducao", b =>
                {
                    b.HasOne("Domain.Cliente", "Cliente")
                        .WithMany("OrdensProducao")
                        .HasForeignKey("ClienteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Produto", "Produto")
                        .WithMany("OrdensProducao")
                        .HasForeignKey("ProdutoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cliente");

                    b.Navigation("Produto");
                });

            modelBuilder.Entity("Domain.Pedido", b =>
                {
                    b.HasOne("Domain.Cliente", "Cliente")
                        .WithMany("Pedidos")
                        .HasForeignKey("ClienteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.OrdemProducao", "OrdemDeProducao")
                        .WithOne("Pedido")
                        .HasForeignKey("Domain.Pedido", "OrdemDeProducaoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Produto", "Produto")
                        .WithMany("Pedidos")
                        .HasForeignKey("ProdutoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cliente");

                    b.Navigation("OrdemDeProducao");

                    b.Navigation("Produto");
                });

            modelBuilder.Entity("Domain.Producao", b =>
                {
                    b.HasOne("Domain.Maquina", "Maquina")
                        .WithMany("Producoes")
                        .HasForeignKey("MaquinaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.OrdemProducao", "OrdemProducao")
                        .WithOne("Producao")
                        .HasForeignKey("Domain.Producao", "OrdemProducaoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Produto", "Produto")
                        .WithMany("Producoes")
                        .HasForeignKey("ProdutoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Maquina");

                    b.Navigation("OrdemProducao");

                    b.Navigation("Produto");
                });

            modelBuilder.Entity("Domain.Cliente", b =>
                {
                    b.Navigation("OrdensProducao");

                    b.Navigation("Pedidos");
                });

            modelBuilder.Entity("Domain.Maquina", b =>
                {
                    b.Navigation("Producoes");
                });

            modelBuilder.Entity("Domain.OrdemProducao", b =>
                {
                    b.Navigation("Pedido")
                        .IsRequired();

                    b.Navigation("Producao")
                        .IsRequired();
                });

            modelBuilder.Entity("Domain.Produto", b =>
                {
                    b.Navigation("OrdensProducao");

                    b.Navigation("Pedidos");

                    b.Navigation("Producoes");
                });
#pragma warning restore 612, 618
        }
    }
}
