
using Microsoft.EntityFrameworkCore;

namespace TeacherAPIProject
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<DataContext>();


            var app = builder.Build();

            if(app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseHttpsRedirection();


            var teachers = new List<Teacher>
            {
                new Teacher{Id=1, Name="Marius", Description="Destytojas", Type="Lektrius"},
                new Teacher{Id=2, Name="Tomas", Description="Destytojas", Type="Lektrius"},

            };


            app.MapGet("/teachers", async (DataContext context) =>
            await context.Teachers.ToListAsync());


            app.MapPost("teachers", async (DataContext context, Teacher teacher) =>
            {
                context.Teachers.Add(teacher);
                await context.SaveChangesAsync();
                return Results.Ok(await context.Teachers.ToListAsync());
            });

            app.MapGet("teachers/{id}", async (DataContext context, int id) =>
            
                await context.Teachers.FindAsync(id) is Teacher teacher 
                ? Results.Ok(teacher) 
                : Results.NotFound("Sorry teacher not found!"));

            app.MapPut("teachers/{id}", async (DataContext context, Teacher updatedteacher, int id) =>
            {

                var teacher = await context.Teachers.FindAsync(id);

                if(teacher is null)
                {
                    return Results.NotFound("Teacher id is wrong!");
                }

                teacher.Name = updatedteacher.Name;
                teacher.Description = updatedteacher.Description;
                teacher.Type = updatedteacher.Type;
                await context.SaveChangesAsync();

                return Results.Ok(teacher);

            });


            app.MapDelete("teachers/{id}", async (DataContext context, int id) =>
            {
                var teacher = await context.Teachers.FindAsync(id);

                if (teacher is null)
                {
                    return Results.NotFound("Not found id cant delete record!");
                }

                context.Teachers.Remove(teacher);
                await context.SaveChangesAsync();
                return Results.Ok(await context.Teachers.ToListAsync());

            });




            app.Run();



           

        }
    }
}