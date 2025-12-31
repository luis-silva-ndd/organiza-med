using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Organiza_Med.ModuloMedico;

namespace OrganizaMed.Infra.ModuloMedico;

public class MapeadorMedicoOrm : IEntityTypeConfiguration<Medico>
{
    public void Configure(EntityTypeBuilder<Medico> builder)
    {
        builder.ToTable("TBMedico");

        builder.Property(x => x.Id)
            .ValueGeneratedNever();

        builder.Property(x => x.Ativo)
            .IsRequired();

        builder.Property(x => x.Nome)
            .IsRequired();

        builder.Property(x => x.Crm)
            .HasColumnType("char(8)")
            .IsRequired();
    }
}