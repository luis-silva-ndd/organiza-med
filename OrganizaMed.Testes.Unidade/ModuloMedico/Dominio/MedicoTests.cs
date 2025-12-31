using FluentValidation.TestHelper;
using Organiza_Med.ModuloMedico;

namespace OrganizaMed.Testes.Unidade.ModuloMedico.Dominio
{
    [TestClass]
    [TestCategory("Testes de unidade")]
    public class MedicoTests
    {
        private ValidadorMedico _validador;

        [TestInitialize]
        public void Inicializar()
        {
            _validador = new ValidadorMedico();
        }

        [TestMethod]
        public void Deve_Passar_Quando_Crm_E_Valido()
        {
            var medico = new Medico("Vanessa Nunes", "96786-SC");
            var result = _validador.TestValidate(medico);
            Assert.IsTrue(result.IsValid);
        }

        [TestMethod]
        public void Deve_Falhar_Com_Crm_Vazio()
        {
            var medico = new Medico("Luis Carlos", "");
            var result = _validador.TestValidate(medico);
            result.ShouldHaveValidationErrorFor(m => m.Crm)
                .WithErrorMessage("O campo Crm é obrigatório.");
        }
        [TestMethod]
        public void Deve_Falhar_Com_Crm_Incorreto()
        {
            var medico = new Medico("Luis Carlos", "1234-SC");
            var result = _validador.TestValidate(medico);
            result.ShouldHaveValidationErrorFor(m => m.Crm)
                .WithErrorMessage("O campo Crm deve estar no formato 00000-UF.");
        }
    }
}
