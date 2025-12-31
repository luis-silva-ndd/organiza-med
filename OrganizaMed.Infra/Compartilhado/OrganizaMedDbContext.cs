using Microsoft.EntityFrameworkCore;
using Organiza_Med.Compartilhado;
using Organiza_Med.ModuloAtividade;
using Organiza_Med.ModuloAutenticacao;
using Organiza_Med.ModuloMedico;
using OrganizaMed.Infra.ModuloAtividade;
using OrganizaMed.Infra.ModuloMedico;
namespace OrganizaMed.Infra.Compartilhado;
public class OrganizaMedDbContext(DbContextOptions options) : DbContext(options), IContextoPersistencia
{
    public async Task<int> GravarAsync()
    {
        return await SaveChangesAsync();
    }

    public async Task RollBackAsync()
    {
        foreach (var entry in ChangeTracker.Entries())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.State = EntityState.Detached;
                    break;
                case EntityState.Modified:
                    entry.State = EntityState.Unchanged;
                    break;
                case EntityState.Deleted:
                    entry.State = EntityState.Unchanged;
                    break;
            }
        }

        await Task.CompletedTask;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new MapeadorMedicoOrm());

        modelBuilder.ApplyConfiguration(new MapeadorAtividadeOrm());

        base.OnModelCreating(modelBuilder);
    }

}