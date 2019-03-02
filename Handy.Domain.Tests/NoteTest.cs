using System;
using System.Linq.Expressions;
using System.Threading;
using Handy.Domain.AccountContext.Commands;
using Handy.Domain.AccountContext.Entities;
using Handy.Domain.AccountContext.Services;
using Handy.Domain.NoteContext.Commands;
using Handy.Domain.NoteContext.Entities;
using Handy.Domain.NoteContext.Services;
using Handy.Domain.SharedContext.Services;
using MediatR;
using Moq;
using Xunit;

namespace Handy.Domain.Tests
{
    public class NoteTest
    {
        [Fact]
        public async void NoteAdds()
        {
            var mockNoteRepo = new Mock<IRepository<Note>>();
            var mockBus = new Mock<IMediator>();
            var noteCmdHandler = new NoteCommandHandler(mockNoteRepo.Object, TestHelper.GetMockMapper(), mockBus.Object);

            var cmd = new AddNote
            {
                Title = "ABC",
                Content = "Lalala",
                AccountId = Guid.NewGuid()
            };
            
            var note = await noteCmdHandler.Handle(cmd, CancellationToken.None);
            
            Assert.Equal(cmd.Title, note.Title);
            Assert.Equal(cmd.Content, note.Content);
            Assert.NotNull(note.Created);
        }
        
        [Fact]
        public async void NoteModifies()
        {
            var accId = Guid.NewGuid();
            var mockNote = new Note(accId, "vodka", "pisya");
            var mockNoteRepo = new Mock<IRepository<Note>>();
            mockNoteRepo
                .Setup(x => x.GetByCriteria(It.IsAny<Expression<Func<Note, bool>>>()))
                .ReturnsAsync(mockNote);
            var mockBus = new Mock<IMediator>();
            
            var noteCmdHandler = new NoteCommandHandler(mockNoteRepo.Object, TestHelper.GetMockMapper(), mockBus.Object);
            var cmd = new ModifyNote
            {
                Title = "Qooqareqoo",
                Content = "Uasya",
                NoteId = mockNote.Id,
                AccountId = accId
            };
            
            var note = await noteCmdHandler.Handle(cmd, CancellationToken.None);
            
            Assert.Equal(cmd.Title, note.Title);
            Assert.Equal(cmd.Content, note.Content);
            Assert.NotNull(note.Modified);
        }
    }
}