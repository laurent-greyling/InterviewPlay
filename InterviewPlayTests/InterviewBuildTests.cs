using InterviewPlay.Models;
using InterviewPlay.Services;
using Moq;
using NUnit.Framework;
using System.IO;
using System.Linq;

namespace Tests
{
    public class InterviewBuildTests
    {
        private Mock<IBuildInterview> _mockInterviewBuilder;
        private Mock<ISqlClient> _mockSqlClient;
        private BuildInterview _target;

        [SetUp]
        public void Setup()
        {
            _mockInterviewBuilder = new Mock<IBuildInterview>();
            _mockSqlClient = new Mock<ISqlClient>();
            _mockInterviewBuilder.Setup(c => c.Build("en", "respondentId")).Returns(It.IsAny<SurveyModel>());
            _mockSqlClient.Setup(s => s.CreateRespondentTableIfNotExist(123));
            _mockSqlClient.Setup(s => s.InsertRespodentDetails(123, "respondentId"));

            _target = new BuildInterview(File.ReadAllText("../../../questionnaire.json"));
        }

        [Test]
        public void DeserialiseAndBuildModelTest()
        {
            var model = _target.Build("en", "respondentId");

            Assert.IsNotNull(model);
            Assert.AreEqual(1000, model.QuestionnaireId);          
        }

        [Test]
        public void EnglishSelectedModelReturnsEnglishTest()
        {
            var model = _target.Build("en", "respondentId");
            var enText = model.QuestionnaireItems.Any(c => c.SubjectText == "My work");

            Assert.IsTrue(enText);
        }

        [Test]
        public void DutchSelectedModelReturnsDutchTest()
        {
            var model = _target.Build("nl", "respondentId");
            var enText = model.QuestionnaireItems.Any(c => c.SubjectText == "Mijn werk");

            Assert.IsTrue(enText);
        }
    }
}