using Dominio;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repositorio.Config
{
    public class LogConfig : IEntityTypeConfiguration<Log>
    {
        public void Configure(EntityTypeBuilder<Log> builder)
        {
            builder.ToTable("log");

            builder.HasKey(HashCode => HashCode.Id);
            builder.Property(p => p.Id).HasColumnName("id");
            builder.Property(p => p.Chave).HasColumnName("chave");
            builder.Property(p => p.CodigoRetorno).HasColumnName("codigoretorno");
            builder.Property(p => p.DataLog).HasColumnName("datalog");
            builder.Property(p => p.Retorno).HasColumnName("retorno");
            builder.Property(p => p.Texto).HasColumnName("texto");
        }
    }
}
