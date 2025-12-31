using Microsoft.AspNetCore.Identity;

namespace Organiza_Med.ModuloAutenticacao;
public class Usuario : IdentityUser<Guid>
{
    public Usuario()
    {
        EmailConfirmed = true;
    }
}