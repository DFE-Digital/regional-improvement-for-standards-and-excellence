using Dfe.ManageSchoolImprovement.Domain.Entities.SupportProject;
using Dfe.ManageSchoolImprovement.Domain.ValueObjects;

namespace Dfe.ManageSchoolImprovement.Domain.Tests.Entities.SupportProject
{
    public class SupportProjectNoteTests
    { 
        [Fact]
        public void SetNote_ShouldUpdateNoteAndModificationProperties()
        {
            // Arrange
            var id = new SupportProjectNoteId(Guid.NewGuid());
            var note = "Initial note";
            var author = "Author";
            var date = DateTime.UtcNow;
            var supportProjectId = new SupportProjectId(1);
            var supportProjectNote = new SupportProjectNote(id, note, author, date, supportProjectId);

            var newNote = "Updated note";
            var newAuthor = "New Author";
            var dateUpdated = DateTime.UtcNow.AddHours(1);

            // Act
            supportProjectNote.SetNote(newNote, newAuthor, dateUpdated);

            // Assert
            Assert.Equal(newNote, supportProjectNote.Note);
            Assert.Equal(newAuthor, supportProjectNote.LastModifiedBy);
            Assert.Equal(dateUpdated, supportProjectNote.LastModifiedOn);
        }
    }

}
