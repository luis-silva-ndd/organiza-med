using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using Moq;
using Organiza_Med.Compartilhado;
using Organiza_Med.ModuloMedico;
using OrganizaMed.Aplicacao.ModuloMedico.Commands.Inserir;

namespace OrganizaMed.Testes.Unidade.ModuloMedico.Aplicacao;
[TestClass]
[TestCategory("Testes de Unidade")]
public class InserirMedicoRequestHandlerTests
{
    private Mock<IContextoPersistencia> _contextoMock;
    private Mock<IRepositorioMedico> _repositorioMedicoMock;
    private Mock<IValidator<Medico>> _validadorMock;

    private InserirMedicoRequestHandler _handler;
    [TestInitialize]
    public void Inicializar()
    {
        _contextoMock = new Mock<IContextoPersistencia>();
        _repositorioMedicoMock = new Mock<IRepositorioMedico>();
        _validadorMock = new Mock<IValidator<Medico>>();

        _handler = new InserirMedicoRequestHandler(
            _contextoMock.Object,
            _repositorioMedicoMock.Object,
            _validadorMock.Object
        );
    }
    [TestMethod]
    public async Task Deve_Inserir_Medico()
    {
        var request = new InserirMedicoRequest("Luis", "77777-SC");
        
        _validadorMock.Setup(v => v.ValidateAsync(It.IsAny<Medico>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new FluentValidation.Results.ValidationResult());

        _repositorioMedicoMock.Setup(r => r.SelecionarTodosAsync()).ReturnsAsync(new List<Medico>());

        _repositorioMedicoMock.Setup(c => c.InserirAsync(It.IsAny<Medico>()))
            .ReturnsAsync(Guid.NewGuid);

        _contextoMock.Setup(c => c.GravarAsync()).ReturnsAsync(1);

        var result = await _handler.Handle(request, It.IsAny<CancellationToken>());

        _repositorioMedicoMock.Verify(x => x.InserirAsync(It.IsAny<Medico>()), Times.Once);
        _contextoMock.Verify(x => x.GravarAsync(), Times.Once);

        Assert.IsTrue(result.IsSuccess);
    }
}