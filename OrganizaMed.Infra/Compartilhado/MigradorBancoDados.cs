using Microsoft.EntityFrameworkCore;

namespace OrganizaMed.Infra.Compartilhado;

public static class MigradorBancoDados
{
    public static bool AtualizarBancoDados(DbContext db)
    {
        var qtdMigracoesPendentes = db.Database.GetPendingMigrations().Count();

        if (qtdMigracoesPendentes == 0)
        {
            Console.WriteLine("Nenhuma migração pendente, continuando...");

            return false;
        }

        Console.WriteLine("Aplicando migrações pendentes, isso pode demorar alguns segundos...");

        db.Database.Migrate();

        return true;
    }
}