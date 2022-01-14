using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CheckQuery.Domain;
using System.Collections.Generic;
using CheckQuery.Domain.Interfaces;
using System.Threading.Tasks;
using CheckQuery.Business;

namespace CheckQuery.Test.Business.Test
{
    [TestClass]
    public class QueryUtils
    {
        string fileTest = @"C:\Users\andre.santos\Desktop\VM - Resources\KT - V201401\Demandas\Integration\Atento\V20140812_01\Modelagem\COST_CENTER\V20141002\CREATION\CARGA - QA\ATO_HIERARQUIA_CECO.SQL";
        private CheckQuery.Business.Utils.QueryUtils objQueryUtils = null;
        private CheckQuery.Business.Utils.FileReader objFileReader = null;
        [TestInitialize]
        public void Initialize()
        {
            objFileReader = new CheckQuery.Business.Utils.FileReader(new CheckQuery.Domain.POCO.File<FileData>(new TxtFile()));
            objQueryUtils = new CheckQuery.Business.Utils.QueryUtils(objFileReader);
        }
        [TestMethod]
        public void VALIDAR_SE_ESTA_SENDO_REALIZADO_A_CONCATENACAO_DE_UM_ARQUIVO_A_PARTIR_DE_UM_TEMPLATE()
        {
            string queryResult = string.Empty;
            IFile file = objFileReader.ReadData(fileTest);
            ICheck objTemplate = new CheckQuery.Business.POCO.Template<InsertTemplate>(file);
            objQueryUtils.Template = objTemplate.TemplateData;
            queryResult = objQueryUtils.ConcatToTemplate(objTemplate, new List<ILine>(), new Line());
            Assert.IsNotNull(queryResult);
            Assert.AreNotEqual(0, queryResult.Length);
        }
        [TestMethod]
        public void VALIDAR_SE_ESTA_SENDO_REALIZADO_A_CONCATENACAO_DE_UM_ARQUIVO_A_PARTIR_DE_UM_TEMPLATE_SERIALIZANDO_SEU_RETORNO()
        {
            string queryResult = string.Empty;
            IFile result = null;
            IFile file = objFileReader.ReadData(fileTest);
            ICheck objTemplate = new CheckQuery.Business.POCO.Template<InsertTemplate>(file);
            objQueryUtils.Template = objTemplate.TemplateData;
            result = objQueryUtils.ConcatToTemplate<FileDataConcat, TxtFile>(objTemplate, new List<ILine>(), new Line());
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Data.Data);
            Assert.AreNotEqual(0, result.Data.DataCollection.Count);
        }
    }
}
