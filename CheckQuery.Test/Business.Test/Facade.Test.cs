using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CheckQuery.Business;
using CheckQuery.Domain;
using CheckQuery.Domain.Interfaces;

namespace CheckQuery.Test.Business.Test
{
    [TestClass]
    public class Facade
    {
        string fileInsertTest = @"C:\Users\andre.santos\Desktop\VM - Resources\KT - V201401\Demandas\Integration\Atento\V20140812_01\Modelagem\COST_CENTER\V20141002\CREATION\CARGA - QA\ATO_HIERARQUIA_CECO.SQL";
        string fileDeleteTest = @"C:\Users\andre.santos\Desktop\CARGA_03\TESTE_DELETE.sql";
        [TestMethod]
        public void VALIDAR_SE_ESTA_SENDO_RETORNADO_OS_DADOS_DE_UM_ARQUIVO_INSERINDO_NO_TEMPLATE_INSERT() 
        {
            File<InsertTemplate> objFileInsert = new File<InsertTemplate>(fileInsertTest);
            string result = objFileInsert.InsertToTemplate();
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void VALIDAR_SE_ESTA_SENDO_RETORNADO_OS_DADOS_DE_UM_ARQUIVO_SERIALIZADO_E_INSERINDO_NO_TEMPLATE_INSERT()
        {
            File<InsertTemplate> objFileInsert = new File<InsertTemplate>(fileInsertTest);
            var result = objFileInsert.InsertToTemplate<FileData, TxtFile>();
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(IFile));
            Assert.AreNotEqual(result.Data.DataCollection, 0);
        }

        [TestMethod]
        public void VALIDAR_SE_ESTA_SENDO_RETORNADO_OS_DADOS_DE_UM_ARQUIVO_INSERINDO_NO_TEMPLATE_DELETE()
        {
            File<DeleteTemplate> objFileInsert = new File<DeleteTemplate>(fileDeleteTest);
            string result = objFileInsert.InsertToTemplate();
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void VALIDAR_SE_ESTA_SENDO_RETORNADO_OS_DADOS_DE_UM_ARQUIVO_SERIALIZADO_E_INSERINDO_NO_TEMPLATE_DELETE()
        {
            File<DeleteTemplate> objFileInsert = new File<DeleteTemplate>(fileDeleteTest);
            var result = objFileInsert.InsertToTemplate<FileData, TxtFile>();
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(IFile));
            Assert.AreNotEqual(result.Data.DataCollection, 0);
        }

        [TestMethod]
        public void VALIDAR_SE_ESTA_SENDO_RETORNADO_OS_DADOS_DE_UM_ARQUIVO_E_INSERINDO_NO_TEMPLATE_INSERT_EM_TEMPO_DE_EXECUCAO()
        {
            File<AnyTemplate> objFileInsert = new File<AnyTemplate>(fileInsertTest);
            string result = objFileInsert.InsertToAnyTemplate();
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void VALIDAR_SE_ESTA_SENDO_RETORNADO_OS_DADOS_DE_UM_ARQUIVO_SERIALIZADO_E_INSERINDO_NO_TEMPLATE_INSERT_EM_TEMPO_DE_EXECUCAO()
        {
            File<AnyTemplate> objFileInsert = new File<AnyTemplate>(fileInsertTest);
            var result = objFileInsert.InsertToAnyTemplate<FileData, TxtFile>();
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(IFile));
            Assert.AreNotEqual(result.Data.DataCollection, 0);
        }
    }
}
