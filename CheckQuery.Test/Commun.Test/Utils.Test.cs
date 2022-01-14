using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CheckQuery.Test.Commun.Test
{
    [TestClass]
    public class Utils
    {
        string fileTest = @"C:\Users\andre.santos\Desktop\VM - Resources\KT - V201401\Demandas\Integration\Atento\V20140812_01\Modelagem\COST_CENTER\V20141002\CREATION\CARGA - QA\ATO_HIERARQUIA_CECO.SQL";
        [TestInitialize]
        public void Iniatialize() 
        {
            
        }

        [TestMethod]
        public void VALIDAR_SE_ESTA_SENDO_RETORNADO_OS_VALORES_DE_UM_ARQUIVO_TXT()
        {
            CheckQuery.Utils.File = fileTest;
            string result = CheckQuery.Utils.FileData;
            Assert.AreNotEqual(0, result.Length);
        }
    }
}
