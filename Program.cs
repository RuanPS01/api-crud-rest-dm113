
using Microsoft.EntityFrameworkCore;

namespace api_crud_rest_dm113;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddDbContext<AppDbContext>();
        builder.Services.AddAuthorization();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        app.UseHttpsRedirection();
        app.UseAuthorization();

        app.MapGet(
            "/",
            (HttpContext httpContext) =>
            {
                return "Hello World!";
            }
        );

        app.MapPost(
            "/message",
            (Message message, AppDbContext db) =>
            {
                Message newMessage = new Message(
                    id: Guid.NewGuid(),
                    title: message.title,
                    text: message.text,
                    createdAt: DateTime.UtcNow,
                    updatedAt: null
                );
                Console.WriteLine(newMessage);
                db.Messages.Add(newMessage);
                db.SaveChanges();
                return Results.Created();
            }
        );

        app.MapGet(
            "/message/list",
            async (AppDbContext db, int page = 1, int pageSize = 10) =>
            {
                if (page < 1 || pageSize < 1)
                {
                    return Results.BadRequest("Os valores de página e tamanho da página devem ser maiores que zero.");
                }

                var messages = await db.Messages
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();

                return Results.Ok(messages);
            }
        );

        app.MapPut(
            "/message/{id}",
            async (Guid id, Message updatedMessage, AppDbContext db) =>
            {
                var existingMessage = await db.Messages.FindAsync(id);
                if (existingMessage == null)
                {
                    return Results.NotFound();
                }

                var newMessage = existingMessage with
                {
                    title = updatedMessage.title ?? existingMessage.title,
                    text = updatedMessage.text ?? existingMessage.text,
                    createdAt = updatedMessage.createdAt ?? existingMessage.createdAt,
                    updatedAt = DateTime.UtcNow
                };

                db.Entry(existingMessage).CurrentValues.SetValues(newMessage);
                await db.SaveChangesAsync();

                return Results.Ok(newMessage);
            }
        );

        app.MapDelete(
            "/message/{id}",
            async (Guid id, AppDbContext db) =>
            {
                var message = await db.Messages.FindAsync(id);
                if (message == null)
                {
                    return Results.NotFound();
                }

                db.Messages.Remove(message);
                await db.SaveChangesAsync();

                return Results.Ok();
            }
        );

        app.Run();
    }
}
