// using TodoApi;
// using Microsoft.EntityFrameworkCore;
// using TodoApi;
// using Microsoft.EntityFrameworkCore;
// using System.Threading.Tasks;
// using Microsoft.AspNetCore.Mvc;
// using System;

// // var builder = WebApplication.CreateBuilder(args);

// // var app = builder.Build();
// var builder = WebApplication.CreateBuilder(args);

// // הוספת שירותי DbContext עם מחרוזת החיבור
// var connectionString = builder.Configuration.GetConnectionString("ToDoDB");
// //הוספת השירות שיהיה זמין בכל מקום באפליקציה
// builder.Services.AddDbContext<ToDoDbContext>(options =>
//     options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

// builder.Services.AddSingleton<Item>();
// //
// builder.Services.AddControllers();

// builder.Services.AddEndpointsApiExplorer();
// // builder.Services.AddSwaggerGen();
// // builder.Services.AddCors(x => x.AddPolicy("all", a => a.AllowAnyHeader()
// // .AllowAnyMethod().AllowAnyOrigin()));

// var app = builder.Build();

// // ...existing code...
// builder.Services.AddCors(options =>
// {
//     options.AddPolicy("all", builder =>
//     {
//         builder.AllowAnyOrigin()
//                .AllowAnyMethod()
//                .AllowAnyHeader();
//     });
// });
// // ...existing code...
// app.UseCors("all");
// // ...existing code...



// app.MapGet("/", () => "Hello World!");

// // app.MapGet("/items", async (ToDoDbContext db) =>
// // {
// //     var items = await db.Items.ToListAsync();
// //   if (items is null || items.Count == 0)
// //   return result.NoFound();
  
// //   return Ok(items);

    
// //     // return await db.Items.ToListAsync();
// // });
// //  app.MapGet("/items", async (ToDoDbContext db) =>
// //  {
// //      var items = await db.Items.ToListAsync();

// //      if (items.Count == 0)
// //          return Results.NotFound();
    
// //      return Results.Ok(items);
// //  });



// // app.MapPost("/todo", async ([FromBody] Item todoItem, ToDoDbContext db) =>
// // {
// //     if (todoItem == null || string.IsNullOrEmpty(todoItem.Name))
// //     {
// //         return Results.BadRequest("Name cannot be null or empty.");
// //     }

// //     todoItem.IsComplete = false; 

// //     db.Items.Add(todoItem);
// //     await db.SaveChangesAsync();

// //     return Results.Created($"/todo/{todoItem.Id}", todoItem);
// // });



// // app.MapPost("/todo", async ([FromBody] string NameT, ToDoDbContext db) =>
// // {
// //     Item todoItem = new Item()
// //     {
// //         IsComplete = false,
// //         Name = NameT
    
// // };


// //    db.Items.Add(todoItem);
// //     await db.SaveChangesAsync();
// //     // if(NameT==null)
// //     //  return Results.NotFound();
// // // return todoItem.Id;
// //   return Results.Ok(todoItem);

// //     //   return Results.Created($"/todo/{todoItem.Id}", todoItem);
// //  });

// ////////not//////
// // app.MapPost("/", async (ToDoDbContext db) => {
// //     if (ToDoDbContext.Request.HasJsonContentType()) {
// //         var todo = await db.Request.ReadFromJsonAsync<Items>(options);
// //         if (todo is not null) {
// //             todo.Name = todo.Name
// //             ;
// //         }
// //         return Results.Ok(todo);
// //     }
// //     else {
// //         return Results.BadRequest();
// //     }
// // });

// //יצירת תשתית להגדרת כל השירותים והתצורות ליצירת האפליקציה


// // if (app.Environment.IsDevelopment())
// // {
// //     app.UseSwagger();
// //     app.UseSwaggerUI();
// // }

// // app.UseCors("all");

// // app.UseHttpsRedirection();

// // app.UseAuthorization();

// // app.MapGet("/", async (ToDoDbContext db) =>
// // {
// //     return await db.Items.ToListAsync();
// // });




// // app.MapGet("/sayH", () => ItemF.sayHellow());

// // app.MapGet("/allTasks", async (ToDoDbContext db) =>
// // {
// //     return await db.Items.ToListAsync();
// // });

// // app.MapPost("/addTask", async  ([FromBody] string NameT ,  ToDoDbContext db) =>
// // {
// //     Item t = new Item(){Name = NameT , IsComplete = false };
// //     db.Items.Add(t);
// //     await db.SaveChangesAsync();
// //     return t;
// // });


// app.MapDelete("/deleteTask/{id}", async (int id, ToDoDbContext db) =>
// {
//     var item = await db.Items.FindAsync(id);
//     if (item != null)
//     {
//         db.Items.Remove(item);
//         await db.SaveChangesAsync();
//     }
    
//     return Results.NotFound();
// });

// // app.MapPut("/updateTask", async ([FromBody] Item t, ToDoDbContext db) =>
// // {
// //     db.Items.Update(t);
// //     await db.SaveChangesAsync();
// //     return t;
// // });

// // app.Run();


// app.Run();
///////////////////////////////////////////////////
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
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("all");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapGet("/", async (ToDoDbContext db) =>
{
    return await db.Items.ToListAsync();
});

// app.MapGet("/sayH", () => ItemF.sayHellow());

// app.MapGet("/allTasks", async (ToDoDbContext db) =>
// {
//     return await db.Items.ToListAsync();
// });

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
//   app.MapPut("/todoitems/{id}", async (int Id, Item Items, ToDoDbContext db) =>
// {
//     var todo = await db.Items.FindAsync(Id);

//     if (todo is null) return Results.NotFound();

//     todo.Name = Items.Name;
//     todo.IsComplete = Items.IsComplete;

//     await db.SaveChangesAsync();

//     return Results.NoContent();
// });
app.MapPut("/todoitems/{id}", async (int Id, [FromBody] Item updatedItem, ToDoDbContext db) =>
{
    var todo = await db.Items.FindAsync(Id);

    if (todo is null) return Results.NotFound();

    // todo.Name = updatedItem.Name;
    todo.IsComplete = updatedItem.IsComplete;

    await db.SaveChangesAsync();

    return Results.NoContent();
});

// app.MapDelete("/todoitems/{id}", async (int Id, ToDoDbContext db) =>
// {
//     if (await db.Items.FindAsync(Id) is Items Items)
//     {
//         db.Items.Remove(item);
//         await Db.SaveChangesAsync();
//         return Results.Ok(item);
//     }

//     return Results.NotFound();
// });
      
// app.MapDelete("/deleteTask/{id}", async (int id, ToDoDbContext db) =>
// {
//     var item = await db.Items.FindAsync(id);
//     if (item != null)
//     {
//         db.Items.Remove(item);
//         await db.SaveChangesAsync();
//     }
// });

// app.MapPut("/updateTask/{id}", async (int id, ToDoDbContext db) =>
// {
//     var item= await db.Items.FindAsync(id);
//     if(item.IsComplete==false)
//     item.IsComplete=true;
//     else
//     item.IsComplete=false;
//     await db.SaveChangesAsync();
//     return item;
// });

app.Run();
  






