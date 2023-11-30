using CarBuilder.Models;
using CarBuilder.Models.DTOs;

List<Interior> interiors = new List<Interior>
{
    new Interior { Id = 1, Price = 899.99M, Material = "Beige Fabric" },
    new Interior { Id = 2, Price = 999.9M, Material = "Charcoal Fabric"},
    new Interior { Id = 3, Price = 699.99M, Material = "White Leather"},
    new Interior { Id = 4, Price = 499.99M, Material = "Black Leather"},
};

List<Order> orders = new List<Order>
{
    new Order { Id = 1, Timestamp = new DateTime(2023, 12, 4), WheelId = 1, TechnologyId = 2, PaintId = 3, InteriorId = 4},
    new Order { Id = 2, Timestamp = new DateTime(2023, 11, 19), WheelId = 2, TechnologyId = 3, PaintId = 4, InteriorId = 1},
    new Order { Id = 3, Timestamp = new DateTime(2023, 12, 15), WheelId = 3, TechnologyId = 4, PaintId = 1, InteriorId = 2},
    new Order { Id = 4, Timestamp = new DateTime(2023, 10, 4), WheelId = 4, TechnologyId = 3, PaintId = 2, InteriorId = 1},
};

List<PaintColor> paintColors = new List<PaintColor>
{
    new PaintColor { Id = 1, Price = 69.99M, Color = "Silver" },
    new PaintColor { Id = 2, Price = 79.99M, Color = "Midnight Blue" },
    new PaintColor { Id = 3, Price = 89.99M, Color = "Firebrick Red" },
    new PaintColor { Id = 4, Price = 59.99M, Color = "Spring Green" },
};

List<Technology> technologies = new List<Technology>
{
    new Technology { Id = 1, Price = 2999.99M, Package = "Basic Package"},
    new Technology { Id = 2, Price = 3500.99M, Package = "Navigation Package"},
    new Technology { Id = 3, Price = 4000.99M, Package = "Visibility Package"},
    new Technology { Id = 4, Price = 4500.99M, Package = "Ultra Package"}
};

List<Wheel> wheels = new List<Wheel>
{
    new Wheel { Id = 1, Price = 99.99M, Style = "Pair Radial" },
    new Wheel { Id = 2, Price = 149.99M, Style = "Pair Radial Black" },
    new Wheel { Id = 3, Price = 199.99M, Style = "Pair Spoke Silver" },
    new Wheel { Id = 4, Price = 249.99M, Style = "Pair Spoke Black" }
};



var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors();

var app = builder.Build();

// Configure the HTTP request pipeline.
// if (app.Environment.IsDevelopment())
// {
//     app.UseSwagger();
//     app.UseSwaggerUI();
// }

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseCors(options =>
                {
                    options.AllowAnyOrigin();
                    options.AllowAnyMethod();
                    options.AllowAnyHeader();
                });
}

app.UseHttpsRedirection();


app.MapGet("/wheels", () =>
{
    return wheels.Select(w => new WheelsDTO
    {
        Id = w.Id,
        Price = w.Price,
        Style = w.Style
    });
});

app.MapGet("/technologies", () =>
{
    return technologies.Select(t => new TechnologyDTO
    {
        Id = t.Id,
        Price = t.Price,
        Package = t.Package
    });
});

app.MapGet("/interiors", () =>
{
    return interiors.Select(i => new InteriorDTO
    {
        Id = i.Id,
        Price = i.Price,
        Material = i.Material
    });
});

app.MapGet("/paintcolors", () =>
{
    return paintColors.Select(p => new PaintColorDTO
    {
        Id = p.Id,
        Price = p.Price,
        Color = p.Color
    });
});

// app.MapGet("/orders", () =>
// {
//     //filters the list of orders to get only the unfulfilled orders & converts the result to a list
//     List<Order> unfulfilledOrders = orders.Where(o => o.Fulfilled == false).ToList();

//     //transforms each unfulfilled order into an OrderDTO.
//     //for each order, it retrieves associated info and creates DTOs for each
//     return unfulfilledOrders.Select(o => new OrderDTO
//     {
//         Id = o.Id,
//         Timestamp = o.Timestamp,
//         WheelId = o.WheelId,
//         TechnologyId = o.TechnologyId,
//         PaintId = o.PaintId,
//         InteriorId = o.InteriorId,
//         Fulfilled = o.Fulfilled,
//         Wheels = wheels.FirstOrDefault(w => w.Id == o.WheelId) == null ? null : new WheelsDTO
//         {
//             Id = o.WheelId,
//             Price = wheels.First(w => w.Id == o.WheelId).Price,
//             Style = wheels.First(w => w.Id == o.WheelId).Style
//         },
//         Technology = technologies.FirstOrDefault(t => t.Id == o.TechnologyId) == null ? null : new TechnologyDTO
//         {
//             Id = o.TechnologyId,
//             Price = technologies.First(t => t.Id == o.TechnologyId).Price,
//             Package = technologies.First(t => t.Id == o.TechnologyId).Package
//         },
//         PaintColor = paintColors.FirstOrDefault(pc => pc.Id == o.PaintId) == null ? null : new PaintColorDTO
//         {
//             Id = o.PaintId,
//             Price = paintColors.First(pc => pc.Id == o.PaintId).Price,
//             Color = paintColors.First(pc => pc.Id == o.PaintId).Color
//         },
//         Interior = interiors.FirstOrDefault(i => i.Id == o.InteriorId) == null ? null : new InteriorDTO
//         {
//             Id = o.InteriorId,
//             Price = interiors.First(i => i.Id == o.InteriorId).Price,
//             Material = interiors.First(i => i.Id == o.InteriorId).Material
//         },
//         TotalCost = o.TotalCost
//         //each property of the OrderDTO is set based on the associated info or set to null
//         //if the associated info is not found
//     });
//     //this code creates a DTO representation of unfulfilled orders w additional details about
//     //the associated wheels, tech, paint, interior
//     //the ternary operator is used to handle cases where the associated info is not found (null)
// });



app.MapGet("/orders", () =>
{
    // filters the list of orders to get only the unfulfilled orders & converts the result to a list
    List<Order> unfulfilledOrders = orders.Where(o => o.Fulfilled == false).ToList();

    // transforms each unfulfilled order into an OrderDTO.
    // for each order, it retrieves associated info and creates DTOs for each
    return unfulfilledOrders.Select(o =>
    {
        WheelsDTO wheelsDto = wheels.FirstOrDefault(w => w.Id == o.WheelId) == null
            ? null
            : new WheelsDTO
            {
                Id = o.WheelId,
                Price = wheels.First(w => w.Id == o.WheelId).Price,
                Style = wheels.First(w => w.Id == o.WheelId).Style
            };

        TechnologyDTO technologyDto = technologies.FirstOrDefault(t => t.Id == o.TechnologyId) == null
            ? null
            : new TechnologyDTO
            {
                Id = o.TechnologyId,
                Price = technologies.First(t => t.Id == o.TechnologyId).Price,
                Package = technologies.First(t => t.Id == o.TechnologyId).Package
            };

        PaintColorDTO paintColorDto = paintColors.FirstOrDefault(pc => pc.Id == o.PaintId) == null
            ? null
            : new PaintColorDTO
            {
                Id = o.PaintId,
                Price = paintColors.First(pc => pc.Id == o.PaintId).Price,
                Color = paintColors.First(pc => pc.Id == o.PaintId).Color
            };

        InteriorDTO interiorDto = interiors.FirstOrDefault(i => i.Id == o.InteriorId) == null
            ? null
            : new InteriorDTO
            {
                Id = o.InteriorId,
                Price = interiors.First(i => i.Id == o.InteriorId).Price,
                Material = interiors.First(i => i.Id == o.InteriorId).Material
            };

        return new OrderDTO
        {
            Id = o.Id,
            Timestamp = o.Timestamp,
            WheelId = o.WheelId,
            TechnologyId = o.TechnologyId,
            PaintId = o.PaintId,
            InteriorId = o.InteriorId,
            Fulfilled = o.Fulfilled,
            Wheels = wheelsDto,
            Technology = technologyDto,
            PaintColor = paintColorDto,
            Interior = interiorDto,
            TotalCost = (wheelsDto?.Price ?? 0) + (technologyDto?.Price ?? 0) + (paintColorDto?.Price ?? 0) + (interiorDto?.Price ?? 0)
        };
        // each property of the OrderDTO is set based on the associated info or set to null
        // if the associated info is not found
    });
    // this code creates a DTO representation of unfulfilled orders w additional details about
    // the associated wheels, tech, paint, interior
    // the ternary operator is used to handle cases where the associated info is not found (null)
});






app.MapGet("orders/{id}", (int id) =>
{
    //retrieves order w the specified id from the orders list
    Order order = orders.FirstOrDefault(o => o.Id == id);
    //if order is not found, error 404 is returned
    if (order == null)
    {
        return Results.NotFound();
    }

    //returns associated info for the order
    Wheel wheel = wheels.FirstOrDefault(w => w.Id == order.WheelId);
    Technology technology = technologies.FirstOrDefault(t => t.Id == order.TechnologyId);
    Interior interior = interiors.FirstOrDefault(i => i.Id == order.InteriorId);
    PaintColor paintColor = paintColors.FirstOrDefault(pc => pc.Id == order.PaintId);

    //creates an orderDTO and returns it, if no  info found returns 'null'
    return Results.Ok(new OrderDTO
    {
        Id = order.Id,
        Timestamp = order.Timestamp,
        Fulfilled = order.Fulfilled,
        WheelId = order.WheelId,
        Wheels = wheels == null ? null : new WheelsDTO
        {
            Id = wheel.Id,
            Price = wheel.Price,
            Style = wheel.Style
        },
        TechnologyId = order.TechnologyId,
        Technology = technologies == null ? null : new TechnologyDTO
        {
            Id = technology.Id,
            Price = technology.Price,
            Package = technology.Package
        },
        PaintId = order.PaintId,
        PaintColor = paintColors == null ? null : new PaintColorDTO
        {
            Id = paintColor.Id,
            Price = paintColor.Price,
            Color = paintColor.Color
        },
        InteriorId = order.InteriorId,
        Interior = interiors == null ? null : new InteriorDTO
        {
            Id = interior.Id,
            Price = interior.Price,
            Material = interior.Material
        }
    });
    //this endpoint is designed to return detailed info about a specific order
    //including info about the associated wheel, tech, paint, intertiors.
    //if not found, returns a 404
});


app.MapPost("/orders", (Order order) =>
{
    //this is generating a new Id & timestamp for the order by finding the maximum ID 
    // in the existing orders & incrementing it by 1
    order.Id = orders.Max(o => o.Id) + 1;
    //sets the timestamp for the order to the current date & time
    order.Timestamp = DateTime.Now;
    //retrieving info associated w these, and checking for missing associated info
    Wheel wheel = wheels.FirstOrDefault(w => w.Id == order.WheelId);
    Technology technology = technologies.FirstOrDefault(t => t.Id == order.TechnologyId);
    Interior interior = interiors.FirstOrDefault(i => i.Id == order.InteriorId);
    PaintColor paintColor = paintColors.FirstOrDefault(pc => pc.Id == order.PaintId);

    if (wheel == null || technology == null || interior == null || paintColor == null)
    {
        //if any info is mising it returns a 400 error
        return Results.BadRequest();
    }

    orders.Add(order);

    return Results.Created($"/orders/{order.Id}", new OrderDTO
    {
        Id = order.Id,
        Timestamp = order.Timestamp,
        WheelId = order.WheelId,
        Wheels = new WheelsDTO
        {
            Id = wheel.Id,
            Price = wheel.Price,
            Style = wheel.Style
        },
        TechnologyId = order.TechnologyId,
        Technology = new TechnologyDTO
        {
            Id = technology.Id,
            Price = technology.Price,
            Package = technology.Package
        },
        PaintId = order.PaintId,
        PaintColor = new PaintColorDTO
        {
            Id = paintColor.Id,
            Price = paintColor.Price,
            Color = paintColor.Color
        },
        InteriorId = order.InteriorId,
        Interior = new InteriorDTO
        {
            Id = interior.Id,
            Price = interior.Price,
            Material = interior.Material
        }
    });
});

app.MapPost("/orders/{id}/fulfill", (int id) =>
{
    Order orderToFulfill = orders.FirstOrDefault(o => o.Id == id);
    //sets the 'fulfilled' property of the retrieved order to 'true' indicating that the order has been fulfilled
    orderToFulfill.Fulfilled = true;
    //this endpoint marks a specific order as fullfilled when a POST request is made
    //to the /orders/{id}/fullfill endpoint. it assumes that the order ID is provided in the route
    //and it sets the fulfilled property of the corresponding order to 'true'
});

app.Run();

