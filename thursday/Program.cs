using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();

List<Microphone> repo = [];

app.MapGet("/get", () => repo);
app.MapPost("/add", (Microphone dto) => repo.Add(dto));
app.MapPut("/update", ([FromQuery]int id, UpdateMicrophoneDTO dto) =>
    {
        Microphone buffer = repo.Find(m => m.Price == id);
        if (buffer == null)
        {
            return Results.NotFound();
        }
        else
        {
            if (buffer.Id != dto.id)
                buffer.Id = dto.id;
            if (buffer.Name != dto.name)
                buffer.Name = dto.name;
            if (buffer.Type != dto.type)
                buffer.Type = dto.type;
            if (buffer.FreqRange != dto.freqrange)
                buffer.FreqRange = dto.freqrange;
            if (buffer.Sensitivity != dto.sensitivity)
                buffer.Sensitivity = dto.sensitivity;
            if (buffer.SupplyType != dto.supplytype)
                buffer.SupplyType = dto.supplytype;
            if (buffer.Price != dto.price)
                buffer.Price = dto.price;
            return Results.Json(buffer);
        }
});
app.MapDelete("/delete", ([FromQuery] int id) =>
{
    Microphone buffer = repo.Find(m => m.Price == id);
    repo.Remove(buffer);
});

app.Run();

public class Microphone
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Type { get; set; }
    public string FreqRange { get; set; }
    public string Sensitivity { get; set; }
    public string SupplyType { get; set; }
    public int Price { get; set; }
    public DateTime DateCreated { get; set; }
};

record class UpdateMicrophoneDTO (int id, string name, string type, string freqrange, string sensitivity, string supplytype, int price);
