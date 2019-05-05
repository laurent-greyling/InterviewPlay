using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using SurveyDeserialise.Entities;
using SurveyDeserialise.SurveyDetails;

namespace Tests
{
    public class SurveyDetailsTests
    {
        private Mock<DbSet<CategoryEntity>> _mockCategory;
        private Mock<DbSet<QuestionnaireEntity>> _mockQuestionnaire;
        private Mock<DbSet<QuestionnaireItemEntity>> _mockQuestionnaireItem;
        private Mock<SurveyDbContext> _mockContext;
        private SurveyDetailsRepository _target;

        [SetUp]
        public void Setup()
        {
            _mockCategory = new Mock<DbSet<CategoryEntity>>();
            _mockQuestionnaire = new Mock<DbSet<QuestionnaireEntity>>();
            _mockQuestionnaireItem = new Mock<DbSet<QuestionnaireItemEntity>>();
            _mockContext = new Mock<SurveyDbContext>();

            _mockContext.Setup(m => m.Categories.Add(It.IsAny<CategoryEntity>())).Returns(_mockCategory.Object.Add(It.IsAny<CategoryEntity>()));
            _mockContext.Setup(m => m.Questionnaire.Add(It.IsAny<QuestionnaireEntity>())).Returns(_mockQuestionnaire.Object.Add(It.IsAny<QuestionnaireEntity>()));
            _mockContext.Setup(m => m.QuestionnaireItem.Add(It.IsAny<QuestionnaireItemEntity>())).Returns(_mockQuestionnaireItem.Object.Add(It.IsAny<QuestionnaireItemEntity>()));

            _target = new SurveyDetailsRepository(_mockContext.Object);
        }

        [Test]
        public void InsertSurveyDetailsTest()
        {
            _target.UpdateSurveyDetails();

            _mockCategory.Verify(m => m.Add(It.IsAny<CategoryEntity>()), Times.Once);
            _mockQuestionnaire.Verify(m => m.Add(It.IsAny<QuestionnaireEntity>()), Times.Once);
            _mockQuestionnaireItem.Verify(m => m.Add(It.IsAny<QuestionnaireItemEntity>()), Times.Once);

            _mockContext.Verify(m => m.SaveChanges(), Times.Once);
        }
    }
}