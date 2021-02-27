using Microsoft.VisualStudio.TestTools.UnitTesting;
using T.Negocio;
namespace UnitTest
{
    [TestClass]
    public class testgenpasword
    {
        [TestMethod]
        public void test_Gen_Password()
        {
            //Arrange: inicializar las variables
            int longitud = 5;


            //Act : ejecutamos el metodo a testear
            string resultado = GenPassword.GenerarContraseña(longitud);

            //Assert : comprobacion de los valore
            Assert.AreEqual("solo quiero el resultado", resultado);
        }
    }
}
