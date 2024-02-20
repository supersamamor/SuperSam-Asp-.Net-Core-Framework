using CHANGE_TO_APP_NAME.Services.Application.Common.Exceptions;
using CHANGE_TO_APP_NAME.Services.Application.TodoItems.Commands.CreateTodoItem;
using CHANGE_TO_APP_NAME.Services.Application.TodoItems.Commands.DeleteTodoItem;
using CHANGE_TO_APP_NAME.Services.Application.TodoLists.Commands.CreateTodoList;
using CHANGE_TO_APP_NAME.Services.Domain.Entities;
using FluentAssertions;
using System.Threading.Tasks;
using NUnit.Framework;

namespace CHANGE_TO_APP_NAME.Services.Application.IntegrationTests.TodoItems.Commands
{
    using static Testing;

    public class DeleteTodoItemTests : TestBase
    {
        [Test]
        public void ShouldRequireValidTodoItemId()
        {
            var command = new DeleteTodoItemCommand { Id = 99 };

            FluentActions.Invoking(() =>
                SendAsync(command)).Should().Throw<NotFoundException>();
        }

        [Test]
        public async Task ShouldDeleteTodoItem()
        {
            var listId = await SendAsync(new CreateTodoListCommand
            {
                Title = "New List"
            });

            var itemId = await SendAsync(new CreateTodoItemCommand
            {
                ListId = listId,
                Title = "New Item"
            });

            await SendAsync(new DeleteTodoItemCommand
            {
                Id = itemId
            });

            var list = await FindAsync<TodoItem>(listId);

            list.Should().BeNull();
        }
    }
}
