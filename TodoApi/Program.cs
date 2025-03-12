

using TodoApi;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System;
//  ליצירת התצורות התשתית ליצירת תשתית
var builder = WebApplication.CreateBuilder(args);
var connectionString=builder.Configuration.GetConnectionString("ToDoDB");
builder.Services.AddDbContext<ToDoDbContext>
(options => options.UseMySql(connectionString,ServerVersion.AutoDetect(connectionString)));

builder.Services.AddSingleton<Item>();
//
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
 builder.Services.AddSwaggerGen();

builder.Services.AddCors(x => x.AddPolicy("all", a => a.AllowAnyHeader()
.AllowAnyMethod().AllowAnyOrigin()));

var app = builder.Build();
// if (app.Environment.IsDevelopment())
// {
    app.UseSwagger();
    app.UseSwaggerUI();
// }
app.UseCors("all");

app.UseHttpsRedirection();

app.UseAuthorization();


app.MapGet("/", async (ToDoDbContext db) =>
{
    return await db.Items.ToListAsync();
});



  app.MapGet("/items", async (ToDoDbContext db) =>
 {
     var i = await db.Items.ToListAsync();

     if (i == null )
          return Results.NotFound();
    
     return Results.Ok(i); });




app.MapPost("/addTask", async  (ToDoDbContext db,string name ) =>
{
    Item t=new Item(){Name=name,IsComplete=false};
     db.Items.Add(t);
    await db.SaveChangesAsync();
    return t;
   
});

app.MapDelete("/todoitems/{id}", async (int Id, ToDoDbContext db) =>
{
    var item = await db.Items.FindAsync(Id);
    if (item != null)
    {
        db.Items.Remove(item);
        await db.SaveChangesAsync();
        return Results.Ok(item);
    }

    return Results.NotFound();
});

app.MapPut("/todoitems/{id}", async (int Id, [FromBody] Item updatedItem, ToDoDbContext db) =>
{
    var todo = await db.Items.FindAsync(Id);

    if (todo is null) return Results.NotFound();

    // todo.Name = updatedItem.Name;
    todo.IsComplete = updatedItem.IsComplete;

    await db.SaveChangesAsync();

    return Results.NoContent();
});



app.Run();
  






