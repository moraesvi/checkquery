using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CheckQuery.Domain;
using System.Collections.Generic;
using CheckQuery.Domain.Interfaces;
using CheckQuery.Domain.POCO;

namespace CheckQuery.Test.Business.Test
{
    [TestClass]
    public class FileReader
    {
        string fileTest = @"C:\Users\andre.santos\Desktop\VM - Resources\KT - V201401\Demandas\Integration\Atento\V20140812_01\Modelagem\COST_CENTER\V20141002\CREATION\CARGA - QA\ATO_HIERARQUIA_CECO.SQL";
        private CheckQuery.Business.Utils.FileReader objFileReader = null;
        [TestInitialize]
        public void Initialize()
        {
            objFileReader = new CheckQuery.Business.Utils.FileReader(new File<FileData>(new TxtFile()));
        }
        [TestMethod]
        public void VALIDAR_SE_ESTA_SENDO_RETORNADO_OS_DADOS_DE_UM_ARQUIVO_SERIALIZADO()
        {
            IFile file = objFileReader.ReadData(fileTest);
            Assert.IsInstanceOfType(file, typeof(IFile));
            Assert.IsNotNull(file);
        }
        [TestMethod]
        public void VALIDAR_SE_ESTA_SENDO_BUSCADO_UM_VALOR_DO_ARQUIVO_PELA_KEY()
        {
            IFile file = objFileReader.ReadData(fileTest);
            IList<string> lstResult = new List<string>();
            foreach (string linha in file.Data.DataCollection)
            {
                var result = objFileReader.GetFieldByKey(linha, "(", ")");
                lstResult.Add(result);
            }
            Assert.IsNotNull(lstResult);
            Assert.AreNotEqual(0, lstResult.Count);
        }
        [TestMethod]
        public void VALIDAR_SE_ESTA_SENDO_BUSCADO_O_TIPO_DO_ARQUIVO_PELA_KEY()
        {
            IFile file = objFileReader.ReadData(fileTest);
            IList<string> lstResult = new List<string>();
            foreach (string linha in file.Data.DataCollection)
            {
                var result = objFileReader.GetFieldByKey(linha, "(", ")");
                lstResult.Add(result);
            }
            Assert.IsNotNull(lstResult);
            Assert.AreNotEqual(0, lstResult.Count);
        }
        [TestMethod]
        public void VALIDAR_SE_ESTA_SENDO_BUSCADO_UM_VALOR_DO_ARQUIVO_PELA_KEY_2()
        {
            IFile file = objFileReader.ReadData(fileTest);
            IList<string> lstResult = new List<string>();
            lstResult = objFileReader.GetFieldByKey(file.Data.DataCollection, "(", ")");
            Assert.IsNotNull(lstResult);
            Assert.AreNotEqual(0, lstResult.Count);
        }
    }
}
