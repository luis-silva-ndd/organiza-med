using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Organiza_Med.ModuloAtividade;
using Organiza_Med.ModuloMedico;

namespace OrganizaMed.Infra.ModuloAtividade;

public class MapeadorAtividadeOrm : IEntityTypeConfiguration<Atividade>
{
    public void Configure(EntityTypeBuilder<Atividade> builder)
    {
        builder.ToTable("TBAtividade");

        builder.Property(x => x.Id)
            .ValueGeneratedNever();

        builder.Property(x => x.Tipo)
            .IsRequired();

        builder.Property(x => x.HorarioInicio)
            .IsRequired();

        builder.Property(x => x.HorarioTermino)
            .IsRequired();

        builder.Property(x => x.Ativo)
            .IsRequired();

        builder.HasMany(x => x.Medicos)
            .WithMany(x => x.Atividades)
            .UsingEntity<Dictionary<string, object>>(
                "TBAtividade_TBMedico",
                j => j
                    .HasOne<Medico>()
                    .WithMany()
                    .HasForeignKey("MedicoId")
                    .HasConstraintName("FK_Atividade_Medico"),
                j => j
                    .HasOne<Atividade>()
                    .WithMany()
                    .HasForeignKey("AtividadeId")
                    .HasConstraintName("FK_Medico_Atividade")); ;
    }
}