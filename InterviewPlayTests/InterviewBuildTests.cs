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
        private BuildInterview _target;

        [SetUp]
        public void Setup()
        {
            _mockInterviewBuilder = new Mock<IBuildInterview>();
            _mockInterviewBuilder.Setup(c => c.Build("en")).Returns(It.IsAny<SurveyModel>());

            _target = new BuildInterview(File.ReadAllText("../../../questionnaire.json"));
        }

        [Test]
        public void DeserialiseAndBuildModelTest()
        {
            var model = _target.Build("en");

            Assert.IsNotNull(model);
            Assert.AreEqual(1000, model.QuestionnaireId);          
        }

        [Test]
        public void EnglishSelectedModelReturnsEnglishTest()
        {
            var model = _target.Build("en");
            var enText = model.QuestionnaireItems.Any(c => c.SubjectText == "My work");

            Assert.IsTrue(enText);
        }

        [Test]
        public void DutchSelectedModelReturnsDutchTest()
        {
            var model = _target.Build("nl");
            var enText = model.QuestionnaireItems.Any(c => c.SubjectText == "Mijn werk");

            Assert.IsTrue(enText);
        }
    }
}